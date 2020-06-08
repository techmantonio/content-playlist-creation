using Newtonsoft.Json;
using System.Collections.Generic;

namespace ContentPlaylistCreation.Models
{
    /// <summary>
    /// A class for representing content.
    /// </summary>
    public class Content
    {
        /// <summary>
        /// Content identifier
        /// </summary>
        [JsonProperty("name")]
        public string ContentId { get; set; }

        /// <summary>
        /// A collection of pre-roll references.
        /// </summary>
        [JsonProperty("preroll")]
        public IEnumerable<PreRollReference> PreRollReferences { get; set; }

        /// <summary>
        /// Video assets associated with this content.
        /// </summary>
        [JsonProperty("videos")]
        public IEnumerable<Video> Videos { get; set; }
    }
}
