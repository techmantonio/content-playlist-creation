using ContentPlaylistCreation.Interfaces;
using ContentPlaylistCreation.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ContentPlaylistCreation
{
    /// <summary>
    /// A class for generating playlist content.
    /// </summary>
    public class ContentPlaylistGenerator
    {
        /// <summary>
        /// An implementation of <see cref="IContentDataProvider"/>.
        /// </summary>
        private readonly IContentDataProvider contentDataProvider;

        /// <summary>
        /// Creates an instance of <see cref="ContentPlaylistGenerator"/>.
        /// </summary>
        /// <param name="contentDataProvider">An implementation of <see cref="IContentDataProvider"/>.</param>
        public ContentPlaylistGenerator(IContentDataProvider contentDataProvider)
        {
            if (contentDataProvider == null)
            {
                throw new ArgumentNullException(nameof(contentDataProvider));
            }

            this.contentDataProvider = contentDataProvider;
        }

        /// <summary>
        /// Generates a collection of playlists based on the given <paramref name="contentId"/> and <paramref name="countryCode"/>.
        /// </summary>
        /// <param name="contentId">A content Id.</param>
        /// <param name="countryCode">A alphabet-based country code.</param>
        /// <returns>A collection of <see cref="ContentPlaylist"/>.</returns>
        public IEnumerable<ContentPlaylist> GetContentPlaylists(string contentId, string countryCode)
        {
            if (string.IsNullOrWhiteSpace(contentId))
            {
                throw new ArgumentException("Content Id must be provided.", nameof(contentId));
            }

            if (string.IsNullOrWhiteSpace(countryCode))
            {
                throw new ArgumentException("Country code must be provided.", nameof(countryCode));
            }

            IList<ContentPlaylist> contentPlaylists = new List<ContentPlaylist>();
            IList<string> errors = new List<string>();

            Content content = contentDataProvider.GetContentById(contentId);
            if (content == null)
            {
                // If the content Id does not exist in the dataset no playlists
                // are returned, this could also be an exception if desired.
                return contentPlaylists;
            }

            foreach (Video contentVideo in content.Videos)
            {
                if (contentVideo.Attributes == null ||
                    contentVideo.Attributes.Countries == null ||
                    !contentVideo.Attributes.Countries.Contains(countryCode, StringComparer.OrdinalIgnoreCase))
                {
                    continue;
                }

                ContentPlaylist contentPlaylist = new ContentPlaylist();
                contentPlaylist.ContentVideos.Add(contentVideo);

                bool preRollCompatibilityIssueDetected = false;
                if (content.PreRollReferences != null)
                {
                    foreach (PreRollReference preRollReference in content.PreRollReferences)
                    {
                        PreRoll preRoll = contentDataProvider.GetPreRollById(preRollReference.PreRollId);
                        if (preRoll == null)
                        {
                            throw new InvalidDataException("Referenced pre-roll is not available in the provided dataset.");
                        }

                        foreach (Video preRollVideo in preRoll.Videos)
                        {
                            if (preRollVideo.Attributes == null ||
                                preRollVideo.Attributes.Countries == null ||
                                !preRollVideo.Attributes.Countries.Contains(countryCode, StringComparer.OrdinalIgnoreCase) ||
                                !string.Equals(contentVideo.Attributes.Language, preRollVideo.Attributes.Language, StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }

                            if (contentVideo.Attributes.AspectRatio == preRollVideo.Attributes.AspectRatio)
                            {
                                contentPlaylist.PreRollVideos.Add(preRollVideo);
                            }
                            else
                            {
                                preRollCompatibilityIssueDetected = true;
                                errors.Add($"(No legal playlist possible because the Pre-Roll Video isn't compatible with the aspect ratio of the Content Video for the {countryCode})");
                            }
                        }
                    }
                }

                // If no pre-rolls were successfully matched and there is an aspect ratio
                // compatibility issue, report that issue to the user as an error. 
                if (!preRollCompatibilityIssueDetected || (preRollCompatibilityIssueDetected && contentPlaylist.PreRollVideos.Any()))
                {
                    contentPlaylist.PlaylistId = $"Playlist{contentPlaylists.Count() + 1}";
                    contentPlaylists.Add(contentPlaylist);
                }
                else
                {
                    throw new InvalidOperationException(string.Join("\n", errors));
                }
            }

            return contentPlaylists;
        }
    }
}
