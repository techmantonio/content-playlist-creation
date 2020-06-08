using Newtonsoft.Json;

namespace ContentPlaylistCreation.Models
{
    /// <summary>
    /// A video reference.
    /// </summary>
    public class VideoReference
    {
        /// <summary>
        /// A video identifier.
        /// </summary>
        [JsonProperty("name")]
        public string VideoId { get; set; }

        /// <summary>
        /// Generates a string representation of a <see cref="VideoReference"/>.
        /// </summary>
        /// <returns>A string representation of a <see cref="VideoReference"/>.</returns>
        public override string ToString()
        {
            return VideoId;
        }
    }
}
