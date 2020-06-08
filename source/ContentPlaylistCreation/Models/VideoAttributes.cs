using Newtonsoft.Json;
using System.Collections.Generic;

namespace ContentPlaylistCreation.Models
{
    /// <summary>
    /// A class for representing video attributes.
    /// </summary>
    public class VideoAttributes
    {
        /// <summary>
        /// A collection of supported country codes.
        /// </summary>
        [JsonProperty("countries")]
        public ISet<string> Countries { get; set; }

        /// <summary>
        /// Supported language.
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }

        /// <summary>
        /// Supported aspect ratio.
        /// </summary>
        [JsonProperty("aspect")]
        public string AspectRatio { get; set; }
    }
}
