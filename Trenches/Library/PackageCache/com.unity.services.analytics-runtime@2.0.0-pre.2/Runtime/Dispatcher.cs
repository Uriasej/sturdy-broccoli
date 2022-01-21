using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

namespace Unity.Services.Analytics.Internal
{
    public interface IDispatcher
    {
        string CollectUrl { get; set; }
        void Flush();
    }
    
    public class Dispatcher: IDispatcher
    {
        readonly IBuffer m_DataBuffer;
        Dictionary<UnityWebRequestAsyncOperation, List<Buffer.Token>> m_Requests = new Dictionary<UnityWebRequestAsyncOperation, List<Buffer.Token>>();

        public string CollectUrl { get; set; }

        IConsentTracker ConsentTracker { get; }

        public Dispatcher(IBuffer buffer, IConsentTracker consentTracker = null)
        {
            m_DataBuffer = buffer;
            ConsentTracker = consentTracker;
        }

        public void Flush()
        {
            // Some sanity check that we aren't spinning out of control.
            // This should be very unlikely.
            if (m_Requests.Count > 128)
            {
                Debug.LogWarning("Analytics Dispatcher has reached limit of pending requests.");
                return;
            }

            // Also, check if the consent was definitely checked and given at this point.
            if (!ConsentTracker.IsGeoIpChecked() || !ConsentTracker.IsConsentGiven())
            {
                Debug.LogWarning("Required consent wasn't checked and given when trying to dispatch events, the events cannot be sent.");
                return;
            }

            FlushBufferToService();
        }

        void FlushBufferToService()
        {
            // Serialize it into a JSON Blob, then POST it to the Collect bulk URL.
            // 'Bulk Events' -> https://docs.deltadna.com/advanced-integration/rest-api/

            List<Buffer.Token> tokens = m_DataBuffer.CloneTokens();
            
            string collectData = m_DataBuffer.Serialize(tokens);

            if (string.IsNullOrEmpty(collectData))
            {
                return;
            }

            byte[] postBytes = Encoding.UTF8.GetBytes(collectData);
            
            UnityWebRequest request = new UnityWebRequest(CollectUrl, UnityWebRequest.kHttpVerbPOST);
            UploadHandlerRaw upload = new UploadHandlerRaw(postBytes);
            upload.contentType = "application/json";
            request.uploadHandler = upload;

            if (ConsentTracker.IsGeoIpChecked() && ConsentTracker.IsConsentGiven())
            {
                foreach (var header in ConsentTracker.requiredHeaders)
                {
                    request.SetRequestHeader(header.Key, header.Value);
                }
            }

            UnityWebRequestAsyncOperation requestOp = request.SendWebRequest();

            #if UNITY_ANALYTICS_DEVELOPMENT
            Debug.Log("AnalyticsRuntime: Flush");
            #endif

            // Callback
            // If the result is successful we will remove the request.
            // else if there was a failure, we insert the tokens back into the buffer.
            requestOp.completed += _ =>
            {
                #if UNITY_ANALYTICS_DEVELOPMENT
                Debug.LogFormat("AnalyticsRuntime: Web Callback - Request.Count = {0}", m_Requests.Count);
                #endif
                
                long code = requestOp.webRequest.responseCode;

                if (!request.isNetworkError && code == 204)
                {
                    #if UNITY_ANALYTICS_DEVELOPMENT
                    Debug.Assert(code == 204, "AnalyticsRuntime: Incorrect response, check your JSON for errors.");
                    #endif
                    
                    #if UNITY_ANALYTICS_EVENT_LOGS
                    Debug.LogFormat("Events uploaded successfully!", code);
                    #endif
                }
                else
                {
                    // Reinsert the tokens back into the buffer.
                    m_DataBuffer.InsertTokens(m_Requests[requestOp]);
                    
                    #if UNITY_ANALYTICS_EVENT_LOGS
                    if (request.isNetworkError)
                    {
                        Debug.Log("Events failed to upload (network error) -- will retry at next heartbeat.");
                    }
                    else
                    {
                        Debug.LogFormat("Events failed to upload (code {0}) -- will retry at next heartbeat.", code);
                    }
                    #endif
                }
                
                // Clear the request now that we are done.
                m_Requests.Remove(requestOp);
                request.Dispose();
            };
            
            m_Requests.Add(requestOp, tokens);
            
            #if UNITY_ANALYTICS_DEVELOPMENT
            Debug.LogFormat("AnalyticsRuntime: Request In Queue - Request.Count = {0}", m_Requests.Count);
            #endif

            #if UNITY_ANALYTICS_EVENT_LOGS
            Debug.Log("Uploading events...");
            #endif
        }
    }
}
