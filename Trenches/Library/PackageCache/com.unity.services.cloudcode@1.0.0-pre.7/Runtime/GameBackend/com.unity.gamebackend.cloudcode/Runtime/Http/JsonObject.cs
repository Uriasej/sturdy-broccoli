using System;
using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.GameBackend.CloudCode.Http
{
    [Preserve]
    [Obsolete("This was made public unintentionally and should not be used.")]
    public class JsonObject
    {
        [Preserve]
        internal JsonObject(object obj)
        {
            this.obj = obj;
        }

        [Preserve]
        internal object obj;

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

    [Preserve]
    internal class JsonObjectInternal
    {
        [Preserve]
        internal JsonObjectInternal(object obj)
        {
            this.obj = obj;
        }

        [Preserve]
        internal object obj;

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

        public T GetAs<T>(DeserializationSettingsInternal deserializationSettings = null)
        {
            // Check if derializationSettings is null so we can use the default value.
            deserializationSettings = deserializationSettings ?? new DeserializationSettingsInternal();
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings
            {
                MissingMemberHandling = deserializationSettings.MissingMemberHandling == MissingMemberHandlingInternal.Error
                    ? Newtonsoft.Json.MissingMemberHandling.Error
                    : Newtonsoft.Json.MissingMemberHandling.Ignore
            };
            try
            {
                return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj), jsonSettings);
            }
            catch (Newtonsoft.Json.JsonSerializationException e)
            {
                throw new DeserializationExceptionInternal(e.Message);
            }
            catch (System.Exception)
            {
                throw new DeserializationExceptionInternal("Unable to deserialize object.");
            }
        }
    }
}
