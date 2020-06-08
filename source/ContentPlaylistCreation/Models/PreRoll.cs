using Newtonsoft.Json;
using System.Collections.Generic;

namespace ContentPlaylistCreation.Models
{
    /// <summary>
    /// A class for representing a pre-list.
    /// </summary>
    public class PreRoll : PreRollReference
    {
        /// <summary>
        ///  A collection of videos.
        /// </summary>
        [JsonProperty("videos")]
        public IEnumerable<Video> Videos { get; set; }
    }
}
