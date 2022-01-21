using System;
using System.Collections.Generic;
using UnityEngine.Scripting;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Unity.GameBackend.CloudCode.Http;


using System.ComponentModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Unity.GameBackend.CloudCode.Models
{
    /// <summary>
    /// RunScript400OneOf model
    /// </summary>
    [Preserve]
    [JsonConverter(typeof(RunScript400OneOfJsonConverter))]
    [DataContract(Name = "RunScript400OneOf")]
    [Obsolete("This was made public unintentionally and should not be used.")]
    public class RunScript400OneOf : IOneOf
    {
        public object Value { get; }
        public Type Type { get; }
        private const string DiscriminatorKey = "type";

        public RunScript400OneOf(object value, Type type)
        {
            this.Value = value;
            this.Type = type;
        }

        private static Dictionary<string, Type> TypeLookup = new Dictionary<string, Type>(){ { "problems/basic", typeof(BasicErrorResponse) },{ "problems/validation", typeof(ValidationErrorResponse) },{ "BasicErrorResponse", typeof(BasicErrorResponse) }, { "ValidationErrorResponse", typeof(ValidationErrorResponse) } };
        private static List<Type> PossibleTypes = new List<Type>(){ typeof(BasicErrorResponse) , typeof(ValidationErrorResponse)  };

        private static Type GetConcreteType(string type)
        {
            if (!TypeLookup.ContainsKey(type))
            {
                string possibleValues = String.Join(", ", TypeLookup.Keys.ToList());
                throw new ArgumentException("Failed to lookup discriminator value for " + type + ". Possible values: " + possibleValues);
            }
            else
            {
                return TypeLookup[type];
            }
        }

        /// <summary>
        /// Converts the JSON string into an instance of RunScript400OneOf
        /// </summary>
        /// <param name="jsonString">JSON string</param>
        /// <returns>An instance of RunScript400OneOf</returns>
        public static RunScript400OneOf FromJson(string jsonString)
        {
            if (jsonString == null)
            {
                return null;
            }

            if (String.IsNullOrEmpty(DiscriminatorKey))
            {
                return DeserializeIntoActualObject(jsonString);
            }
            else
            {
                var parsedJson = JObject.Parse(jsonString);
                if (!parsedJson.ContainsKey(DiscriminatorKey))
                {
                    throw new MissingFieldException("RunScript400OneOf", DiscriminatorKey);
                }
                string discriminatorValue = parsedJson[DiscriminatorKey].ToString();

                return DeserializeIntoActualObject(discriminatorValue, jsonString);
            }
        }

        private static RunScript400OneOf DeserializeIntoActualObject(string discriminatorValue, string jsonString)
        {
            object actualObject = null;
            Type concreteType = GetConcreteType(discriminatorValue);

            if (concreteType == null)
            {
                string possibleValues = String.Join(", ", TypeLookup.Keys.ToList());
                throw new InvalidDataException("Failed to lookup discriminator value for " + discriminatorValue + ". Possible values: " + possibleValues);
            }

            actualObject = JsonConvert.DeserializeObject(jsonString, concreteType);

            return new RunScript400OneOf(actualObject, concreteType);
        }

        private static RunScript400OneOf DeserializeIntoActualObject(string jsonString)
        {
            var results = new List<(object ActualObject, Type ActualType)>();
            foreach (Type t in PossibleTypes)
            {
                try
                {
                    var deserializedClass = JsonConvert.DeserializeObject(jsonString, t);
                    results.Add((deserializedClass, t));
                }
                catch (Exception)
                {
                    // Do nothing
                }
            }

            if (results.Count() == 0)
            {
                string message = $"Could not deserialize into any of possible types. Possible types are: {String.Join(", ", PossibleTypes)}";
                throw new ResponseDeserializationException(message);
            }

            if (results.Count() > 1)
            {
                string message = $"Could not deserialize; type is ambiguous. Possible types are: {String.Join(", ", results.Select(p => p.ActualType))}";
                throw new ResponseDeserializationException(message);
            }

            return new RunScript400OneOf(results.First().ActualObject, results.First().ActualType);
        }
    }

    /// <summary>
    /// RunScript400OneOf model
    /// </summary>
    [Preserve]
    [JsonConverter(typeof(RunScript400OneOfJsonConverter))]
    [DataContract(Name = "RunScript400OneOf")]
    internal class RunScript400OneOfInternal : IOneOfInternal
    {
        public object Value { get; }
        public Type Type { get; }
        private const string DiscriminatorKey = "type";

        public RunScript400OneOfInternal(object value, Type type)
        {
            this.Value = value;
            this.Type = type;
        }

        private static Dictionary<string, Type> TypeLookup = new Dictionary<string, Type>(){ { "problems/basic", typeof(BasicErrorResponseInternal) },{ "problems/validation", typeof(ValidationErrorResponseInternal) },{ "BasicErrorResponse", typeof(BasicErrorResponseInternal) }, { "ValidationErrorResponse", typeof(ValidationErrorResponseInternal) } };
        private static List<Type> PossibleTypes = new List<Type>(){ typeof(BasicErrorResponseInternal) , typeof(ValidationErrorResponseInternal)  };

        private static Type GetConcreteType(string type)
        {
            if (!TypeLookup.ContainsKey(type))
            {
                string possibleValues = String.Join(", ", TypeLookup.Keys.ToList());
                throw new ArgumentException("Failed to lookup discriminator value for " + type + ". Possible values: " + possibleValues);
            }
            else
            {
                return TypeLookup[type];
            }
        }

        /// <summary>
        /// Converts the JSON string into an instance of RunScript400OneOf
        /// </summary>
        /// <param name="jsonString">JSON string</param>
        /// <returns>An instance of RunScript400OneOf</returns>
        public static RunScript400OneOfInternal FromJson(string jsonString)
        {
            if (jsonString == null)
            {
                return null;
            }

            if (String.IsNullOrEmpty(DiscriminatorKey))
            {
                return DeserializeIntoActualObject(jsonString);
            }
            else
            {
                var parsedJson = JObject.Parse(jsonString);
                if (!parsedJson.ContainsKey(DiscriminatorKey))
                {
                    throw new MissingFieldException("RunScript400OneOf", DiscriminatorKey);
                }
                string discriminatorValue = parsedJson[DiscriminatorKey].ToString();

                return DeserializeIntoActualObject(discriminatorValue, jsonString);
            }
        }

        private static RunScript400OneOfInternal DeserializeIntoActualObject(string discriminatorValue, string jsonString)
        {
            object actualObject = null;
            Type concreteType = GetConcreteType(discriminatorValue);

            if (concreteType == null)
            {
                string possibleValues = String.Join(", ", TypeLookup.Keys.ToList());
                throw new InvalidDataException("Failed to lookup discriminator value for " + discriminatorValue + ". Possible values: " + possibleValues);
            }

            actualObject = JsonConvert.DeserializeObject(jsonString, concreteType);

            return new RunScript400OneOfInternal(actualObject, concreteType);
        }

        private static RunScript400OneOfInternal DeserializeIntoActualObject(string jsonString)
        {
            var results = new List<(object ActualObject, Type ActualType)>();
            foreach (Type t in PossibleTypes)
            {
                try
                {
                    var deserializedClass = JsonConvert.DeserializeObject(jsonString, t);
                    results.Add((deserializedClass, t));
                }
                catch (Exception)
                {
                    // Do nothing
                }
            }

            if (results.Count() == 0)
            {
                string message = $"Could not deserialize into any of possible types. Possible types are: {String.Join(", ", PossibleTypes)}";
                throw new ResponseDeserializationException(message);
            }

            if (results.Count() > 1)
            {
                string message = $"Could not deserialize; type is ambiguous. Possible types are: {String.Join(", ", results.Select(p => p.ActualType))}";
                throw new ResponseDeserializationException(message);
            }

            return new RunScript400OneOfInternal(results.First().ActualObject, results.First().ActualType);
        }
    }

    /// <summary>
    /// Custom JSON converter for RunScript400OneOf to allow for deserialization into OneOf type
    /// </summary>
    [Preserve]
    internal class RunScript400OneOfJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if(reader.TokenType != JsonToken.Null)
            {
                return RunScript400OneOfInternal.FromJson(JObject.Load(reader).ToString(Formatting.None));
            }
            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}


