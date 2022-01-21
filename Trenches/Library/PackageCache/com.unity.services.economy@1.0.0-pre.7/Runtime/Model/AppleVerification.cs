using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine.Scripting;

namespace Unity.Services.Economy.Model
{
    /// <summary>
    /// The receipt verification details from the validation service.
    /// </summary>
    public class AppleVerification
    {
        [Preserve]
        public AppleVerification(StatusOptions status, AppleStore store)
        {
            Status = status;
            Store = store;
        }

        /// <summary>
        /// Status of the receipt verification.
        /// </summary>
        [Preserve]         
        [JsonConverter(typeof(StringEnumConverter))] 
        public StatusOptions Status;

        /// <summary>
        /// Further details from the receipt validation service.
        /// </summary>
        [Preserve]
        public AppleStore Store;

        /// <summary>
        /// Status of the receipt verification.
        /// </summary>
        /// <value>Status of the receipt verification. VALID: The purchase was valid. VALID_NOT_REDEEMED: The purchase was valid but seen before, but had not yet been redeemed. INVALID_ALREADY_REDEEMED: The purchase has already been redeemed. INVALID_VERIFICATION_FAILED: The receipt verification Service returned that the receipt data was not valid. INVALID_ANOTHER_PLAYER: The receipt has previously been used by a different player and validated. INVALID_CONFIGURATION: The service configuration is invalid, further information in the details section of the response. INVALID_PRODUCT_ID_MISMATCH: The purchase configuration store product identifier does not match the one in the receipt.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusOptions
        {
            /// <summary>
            /// Enum VALID for value: VALID
            /// </summary>
            [EnumMember(Value = "VALID")]
            VALID = 1,

            /// <summary>
            /// Enum VALIDNOTREDEEMED for value: VALID_NOT_REDEEMED
            /// </summary>
            [EnumMember(Value = "VALID_NOT_REDEEMED")]
            VALIDNOTREDEEMED = 2,

            /// <summary>
            /// Enum INVALIDALREADYREDEEMED for value: INVALID_ALREADY_REDEEMED
            /// </summary>
            [EnumMember(Value = "INVALID_ALREADY_REDEEMED")]
            INVALIDALREADYREDEEMED = 3,

            /// <summary>
            /// Enum INVALIDVERIFICATIONFAILED for value: INVALID_VERIFICATION_FAILED
            /// </summary>
            [EnumMember(Value = "INVALID_VERIFICATION_FAILED")]
            INVALIDVERIFICATIONFAILED = 4,

            /// <summary>
            /// Enum INVALIDANOTHERPLAYER for value: INVALID_ANOTHER_PLAYER
            /// </summary>
            [EnumMember(Value = "INVALID_ANOTHER_PLAYER")]
            INVALIDANOTHERPLAYER = 5,

            /// <summary>
            /// Enum INVALIDCONFIGURATION for value: INVALID_CONFIGURATION
            /// </summary>
            [EnumMember(Value = "INVALID_CONFIGURATION")]
            INVALIDCONFIGURATION = 6,

            /// <summary>
            /// Enum INVALIDPRODUCTIDMISMATCH for value: INVALID_PRODUCT_ID_MISMATCH
            /// </summary>
            [EnumMember(Value = "INVALID_PRODUCT_ID_MISMATCH")]
            INVALIDPRODUCTIDMISMATCH = 7

        }
    }
    
    /// <summary>
    /// Details from the receipt validation service.
    /// </summary>
    public class AppleStore
    {
        [Preserve]
        [JsonConstructor]
        public AppleStore(string code, string message, string receipt)
        {
            Code = code;
            Message = message;
            Receipt = receipt;
        }

        /// <summary>
        /// The status code sent back from the verification service.
        /// </summary>
        [Preserve] 
        public string Code;
        
        /// <summary>
        /// A textual description of the returned status code.
        /// </summary>
        [Preserve] 
        public string Message;
        
        /// <summary>
        /// The purchase receipt data.
        /// </summary>
        [Preserve] 
        public string Receipt;
    }
}
