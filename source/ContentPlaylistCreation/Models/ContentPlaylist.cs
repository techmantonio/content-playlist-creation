using System.Collections.Generic;

namespace ContentPlaylistCreation.Models
{
    /// <summary>
    /// A class for representing a content playlist.
    /// </summary>
    public class ContentPlaylist
    {
        /// <summary>
        /// A playlist identifier.
        /// </summary>
        public string PlaylistId { get; set; }

        /// <summary>
        /// A list of pre-roll videos.
        /// </summary>
        public IList<VideoReference> PreRollVideos { get; private set; }

        /// <summary>
        /// A list of content videos.
        /// </summary>
        public IList<VideoReference> ContentVideos { get; private set; }

        /// <summary>
        /// Creates an instance of <see cref="ContentPlaylist"/>.
        /// </summary>
        public ContentPlaylist()
        {
            PreRollVideos = new List<VideoReference>();
            ContentVideos = new List<VideoReference>();
        }

        /// <summary>
        /// Generates a string representation of a <see cref="ContentPlaylist"/>.
        /// </summary>
        /// <returns>A string representation of a <see cref="ContentPlaylist"/>.</returns>
        public override string ToString()
        {
            string contentPlaylist = $"{PlaylistId}\n{{";

            if (PreRollVideos.Count > 0)
            {
                contentPlaylist += $"{string.Join(", ", PreRollVideos)}, ";
            }

            return contentPlaylist += $"{string.Join(", ", ContentVideos)}}}";
        }
    }
}
