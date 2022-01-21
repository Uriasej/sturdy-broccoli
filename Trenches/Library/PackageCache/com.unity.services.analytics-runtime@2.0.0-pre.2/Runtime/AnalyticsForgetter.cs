using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Unity.Services.Analytics.Internal
{
    public interface IAnalyticsForgetter
    {
        void AttemptToForget();
    }
    
    public class AnalyticsForgetter: IAnalyticsForgetter
    {
        readonly string s_CollectUrl;
        readonly byte[] s_Event;
        readonly Action s_Callback;

        bool m_SuccessfullyUploaded;
        UnityWebRequestAsyncOperation m_Request;
        IConsentTracker ConsentTracker { get; }

        public AnalyticsForgetter(string collectUrl, string userId, string timestamp, string callingMethod, 
            Action successfulUploadCallback, IConsentTracker consentTracker = null)
        {
            // NOTE: we cannot use String.Format on JSON because it gets confused by all the {}s
            string eventJson =
            "{\"eventList\":[{" +
                "\"eventName\":\"ddnaForgetMe\"," +
                "\"userID\":\"" + userId + "\"," +
                "\"eventUUID\":\"" + Guid.NewGuid().ToString() + "\"," +
                "\"eventTimestamp\":\"" + timestamp + "\"," +
                "\"eventVersion\":1," +
                "\"eventParams\":{" +
                    "\"clientVersion\":\"" + Application.version + "\"," +
                    "\"sdkMethod\":\"" + callingMethod + "\"" +
            "}}]}";
            
            s_Event = Encoding.UTF8.GetBytes(eventJson);
            s_CollectUrl = collectUrl;
            s_Callback = successfulUploadCallback;
            ConsentTracker = consentTracker;
        }

        public void AttemptToForget()
        {
            if (m_Request != null || m_SuccessfullyUploaded)
            {
                return;
            }

            UnityWebRequest request = new UnityWebRequest(s_CollectUrl, UnityWebRequest.kHttpVerbPOST);
            UploadHandlerRaw upload = new UploadHandlerRaw(s_Event);
            upload.contentType = "application/json";
            request.uploadHandler = upload;

            if (ConsentTracker.IsGeoIpChecked() && ConsentTracker.IsOptingOutInProgress())
            {
                foreach (var header in ConsentTracker.requiredHeaders)
                {
                    request.SetRequestHeader(header.Key, header.Value);
                }
            }
            
            m_Request = request.SendWebRequest();
            m_Request.completed += UploadComplete;
        }

        void UploadComplete(AsyncOperation _)
        {
            long code = m_Request.webRequest.responseCode;

            if (!m_Request.webRequest.isNetworkError && code == 204)
            {
                m_SuccessfullyUploaded = true;
                s_Callback();
            }

            // Clear the request to allow another request to be sent.
            m_Request.webRequest.Dispose();
            m_Request = null;
        }
    }
}
