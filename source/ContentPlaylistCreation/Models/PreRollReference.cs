using Newtonsoft.Json;

namespace ContentPlaylistCreation.Models
{
    /// <summary>
    /// A pre-roll reference.
    /// </summary>
    public class PreRollReference
    {
        /// <summary>
        /// A pre-roll identifier.
        /// </summary>
        [JsonProperty("name")]
        public string PreRollId { get; set; }
    }
}
