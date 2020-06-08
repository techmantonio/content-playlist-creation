using Newtonsoft.Json;

namespace ContentPlaylistCreation.Models
{
    /// <summary>
    /// A class for representing a video.
    /// </summary>
    public class Video : VideoReference
    {
        /// <summary>
        /// Video attributes.
        /// </summary>
        [JsonProperty("attributes")]
        public VideoAttributes Attributes { get; set; }

        // VideoType [ Content, PreRoll ]

        // AssoicatedAssetId
    }
}
