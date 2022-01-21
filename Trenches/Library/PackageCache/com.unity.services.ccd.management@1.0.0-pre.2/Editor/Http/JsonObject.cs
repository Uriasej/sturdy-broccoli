using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Utilities;
using UnityEngine;

namespace Unity.Services.CCD.Management.Http
{
    /// <summary>
    /// JsonObject
    /// </summary>
    public class JsonObject
    {
        internal JsonObject(object obj)
        {
            this.obj = obj;
        }

        internal object obj;

        /// <summary>
        /// Get string
        /// </summary>
        /// <returns></returns>
        public string GetAsString()
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch (System.Exception)
            {
                throw new System.Exception("Failed to convert JsonObject to string.");
            }
        }

        /// <summary>
        /// Get object as class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="deserializationSettings"></param>
        /// <returns></returns>
        public T GetAs<T>(DeserializationSettings deserializationSettings = null)
        {
            // Check if derializationSettings is null so we can use the default value.
            deserializationSettings = deserializationSettings ?? new DeserializationSettings();
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = deserializationSettings.MissingMemberHandling == MissingMemberHandling.Error
                    ? Newtonsoft.Json.MissingMemberHandling.Error
                    : Newtonsoft.Json.MissingMemberHandling.Ignore
            };
            try
            {
                return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj), jsonSettings);
            }
            catch (Newtonsoft.Json.JsonSerializationException e)
            {
                throw new DeserializationException(e.Message);
            }
            catch (System.Exception)
            {
                throw new DeserializationException("Unable to deserialize object.");
            }
        }
    }
}
