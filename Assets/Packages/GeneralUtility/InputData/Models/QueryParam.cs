using System;

namespace Models
{
    /// <summary>
    /// A model to represent query params.
    /// </summary>
    [Serializable]
    public struct QueryParam
    {
        /// <summary>
        /// The key of the query param.
        /// </summary>
        public string Key;
        
        /// <summary>
        /// The value of the query param.
        /// </summary>
        public string Value;
    }
}
