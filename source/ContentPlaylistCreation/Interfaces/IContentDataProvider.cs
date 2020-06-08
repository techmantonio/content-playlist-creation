using ContentPlaylistCreation.Models;

namespace ContentPlaylistCreation.Interfaces
{
    /// <summary>
    /// An interface for providing content data.
    /// </summary>
    public interface IContentDataProvider
    {
        /// <summary>
        /// Looks up a content asset using content Id.
        /// </summary>
        /// <param name="contentId">A content Id.</param>
        /// <returns>An instance of <see cref="Content"/> or <see cref="null"/> if the content cannot be located.</returns>
        Content GetContentById(string contentId);

        /// <summary>
        /// Looks up a pre-roll asset using a pre-roll Id.
        /// </summary>
        /// <param name="preRollId">A pre-roll Id.</param>
        /// <returns>An instance of <see cref="PreRoll"/> or <see cref="null"/> if the pre-roll cannot be located.</returns>
        PreRoll GetPreRollById(string preRollId);
    }
}
