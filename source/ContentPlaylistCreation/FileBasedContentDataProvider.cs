using ContentPlaylistCreation.Interfaces;
using ContentPlaylistCreation.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ContentPlaylistCreation
{
    /// <summary>
    /// A file-based implementation of <see cref="IContentDataProvider"/>.
    /// </summary>
    public class FileBasedContentDataProvider : IContentDataProvider
    {
        /// <summary>
        /// Internal look-up for content.
        /// </summary>
        private readonly IDictionary<string, Content> contentLookup = new Dictionary<string, Content>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Internal look-up for pre-rolls.
        /// </summary>
        private readonly IDictionary<string, PreRoll> preRollLookup = new Dictionary<string, PreRoll>(StringComparer.OrdinalIgnoreCase);

        // Videos

        /// <summary>
        /// Creates an instance of <see cref="FileBasedContentDataProvider"/>.
        /// </summary>
        /// <param name="inputFilePath">Path to a content data JSON file.</param>
        public FileBasedContentDataProvider(string inputFilePath)
        {
            if (string.IsNullOrWhiteSpace(inputFilePath))
            {
                throw new ArgumentException("Input file path must be provided.");
            }

            LoadContentData(inputFilePath);
        }

        /// <summary>
        /// Looks up a content asset using content Id.
        /// </summary>
        /// <param name="contentId">A content Id.</param>
        /// <returns>An instance of <see cref="Content"/> or <see cref="null"/> if the content cannot be located.</returns>
        public Content GetContentById(string contentId)
        {
            if (contentLookup.ContainsKey(contentId))
            {
                return contentLookup[contentId];
            }

            return null;
        }

        /// <summary>
        /// Looks up a pre-roll asset using a pre-roll Id.
        /// </summary>
        /// <param name="preRollId">A pre-roll Id.</param>
        /// <returns>An instance of <see cref="PreRoll"/> or <see cref="null"/> if the pre-roll cannot be located.</returns>
        public PreRoll GetPreRollById(string preRollId)
        {
            if (preRollLookup.ContainsKey(preRollId))
            {
                return preRollLookup[preRollId];
            }

            return null;
        }

        /// <summary>
        /// Loads and validates the content data from the given input file.
        /// </summary>
        /// <param name="inputFilePath">Path to a content data JSON file.</param>
        private void LoadContentData(string inputFilePath)
        {
            try
            {
                ContentData contentData = JsonConvert.DeserializeObject<ContentData>(File.ReadAllText(inputFilePath));
                if (contentData == null)
                {
                    throw new InvalidDataException("Content data file is not populated.");
                }

                if (contentData.Content == null)
                {
                    throw new InvalidDataException("Content is a required property.");
                }

                foreach (Content content in contentData.Content)
                {
                    if (string.IsNullOrWhiteSpace(content.ContentId))
                    {
                        throw new InvalidDataException("Content name is a required property.");
                    }

                    if (contentLookup.ContainsKey(content.ContentId))
                    {
                        throw new InvalidDataException($"Content name '{content.ContentId}' is included more than once in the dataset.");
                    }

                    if (content.Videos == null)
                    {
                        throw new InvalidDataException("Content video is a required property.");
                    }

                    contentLookup.Add(content.ContentId, content);
                }

                // Pre-rolls are not required so this property may be empty.
                if (contentData.PreRolls != null)
                {
                    foreach (PreRoll preRoll in contentData.PreRolls)
                    {
                        if (string.IsNullOrWhiteSpace(preRoll.PreRollId))
                        {
                            throw new InvalidDataException("Pre-roll name is a required property.");
                        }

                        if (preRollLookup.ContainsKey(preRoll.PreRollId))
                        {
                            throw new InvalidDataException($"Pre-roll name '{preRoll.PreRollId}' is included more than once in the dataset.");
                        }

                        preRollLookup.Add(preRoll.PreRollId, preRoll);
                    }
                }

            }
            catch (Exception e)
            {
                throw new InvalidDataException($"Failed to load input data from path '{inputFilePath}'.", e);
            }
        }
    }
}
