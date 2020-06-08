using Newtonsoft.Json;
using System.Collections.Generic;

namespace ContentPlaylistCreation.Models
{
    /// <summary>
    /// A class for importing content data.
    /// </summary>
    public class ContentData
    {
        /// <summary>
        /// A collection of content.
        /// </summary>
        [JsonProperty("content")]
        public IEnumerable<Content> Content { get; set; }

        /// <summary>
        /// A collection of pre-rolls.
        /// </summary>
        [JsonProperty("preroll")]
        public IEnumerable<PreRoll> PreRolls { get; set; }
    }
}
