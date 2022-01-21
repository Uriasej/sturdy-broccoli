using Newtonsoft.Json;
using UnityEngine.Scripting;

namespace Unity.Services.CCD.Management.Http
{
    /// <summary>
    /// Basic Error
    /// </summary>
    [Preserve]
    public class BasicError : IError
    {
        /// <summary>
        /// Type
        /// </summary>
        [Preserve]
        public string Type { get; }

        /// <summary>
        /// Title
        /// </summary>
        [Preserve]
        public string Title { get; }

        /// <summary>
        /// Status
        /// </summary>
        [Preserve]
        public int? Status { get; }
        /// <summary>
        /// Code
        /// </summary>
        [Preserve]
        public int Code { get; }
        /// <summary>
        /// Detail
        /// </summary>
        [Preserve]
        public string Detail { get; }

        /// <summary>
        /// Create basic error
        /// </summary>
        /// <param name="type"></param>
        /// <param name="title"></param>
        /// <param name="status"></param>
        /// <param name="code"></param>
        /// <param name="detail"></param>
        [Preserve]
        public BasicError(string type, string title, int? status, int code, string detail)
        {
            Type = type;
            Title = title;
            Status = status;
            Code = code;
            Detail = detail;
        }

        /// <summary>
        /// Format to string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}