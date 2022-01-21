using System;
using System.Collections.Generic;
using UnityEngine.Scripting;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Unity.Services.CCD.Management.Http;



namespace Unity.Services.CCD.Management.Models
{
    /// <summary>
    /// CcdChangecount model
    /// <param name="add">add param</param>
    /// <param name="delete">delete param</param>
    /// <param name="last_modified">last_modified param</param>
    /// <param name="last_modified_by">last_modified_by param</param>
    /// <param name="last_modified_by_name">last_modified_by_name param</param>
    /// <param name="loading">loading param</param>
    /// <param name="unchanged">unchanged param</param>
    /// <param name="update">update param</param>
    /// </summary>

    [Preserve]
    [DataContract(Name = "ccd.changecount")]
    public class CcdChangecount
    {
        /// <summary>
        /// Creates an instance of CcdChangecount.
        /// </summary>
        /// <param name="add">add param</param>
        /// <param name="delete">delete param</param>
        /// <param name="lastModified">last_modified param</param>
        /// <param name="lastModifiedBy">last_modified_by param</param>
        /// <param name="lastModifiedByName">last_modified_by_name param</param>
        /// <param name="loading">loading param</param>
        /// <param name="unchanged">unchanged param</param>
        /// <param name="update">update param</param>
        [Preserve]
        public CcdChangecount(int add = default, int delete = default, DateTime lastModified = default, string lastModifiedBy = default, string lastModifiedByName = default, bool loading = default, int unchanged = default, int update = default)
        {
            Add = add;
            Delete = delete;
            LastModified = lastModified;
            LastModifiedBy = lastModifiedBy;
            LastModifiedByName = lastModifiedByName;
            Loading = loading;
            Unchanged = unchanged;
            Update = update;
        }

        /// <summary>
        /// Number of added entries
        /// </summary>
        [Preserve]
        [DataMember(Name = "add", EmitDefaultValue = false)]
        public int Add { get; }

        /// <summary>
        /// Number of deleted entries
        /// </summary>
        [Preserve]
        [DataMember(Name = "delete", EmitDefaultValue = false)]
        public int Delete { get; }

        /// <summary>
        /// Last Modified Date
        /// </summary>
        [Preserve]
        [DataMember(Name = "last_modified", EmitDefaultValue = false)]
        public DateTime LastModified { get; }

        /// <summary>
        /// Last Modified By
        /// </summary>
        [Preserve]
        [DataMember(Name = "last_modified_by", EmitDefaultValue = false)]
        public string LastModifiedBy { get; }

        /// <summary>
        /// Last Modified By Name
        /// </summary>
        [Preserve]
        [DataMember(Name = "last_modified_by_name", EmitDefaultValue = false)]
        public string LastModifiedByName { get; }

        /// <summary>
        /// Is loading 
        /// </summary>
        [Preserve]
        [DataMember(Name = "loading", EmitDefaultValue = true)]
        public bool Loading { get; }

        /// <summary>
        /// Number of unchanged entries
        /// </summary>
        [Preserve]
        [DataMember(Name = "unchanged", EmitDefaultValue = false)]
        public int Unchanged { get; }

        /// <summary>
        /// Number of updated entries
        /// </summary>
        [Preserve]
        [DataMember(Name = "update", EmitDefaultValue = false)]
        public int Update { get; }

    }
}

