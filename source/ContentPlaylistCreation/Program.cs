using ContentPlaylistCreation.Interfaces;
using ContentPlaylistCreation.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContentPlaylistCreation
{
    /// <summary>
    /// Application host.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        /// <returns>A zero exit code if executions is successful, one if otherwise.</returns>
        /// <remarks>
        /// Parameter input order.
        /// 1 - InputFilePath (required)
        /// 2 - ContentId (required)
        /// 3 - CountryCode (required)
        /// </remarks>
        static int Main(string[] args)
        {
            string inputFilePath = args.ElementAtOrDefault(0);
            string contentId = args.ElementAtOrDefault(1);
            string countryCode = args.ElementAtOrDefault(2);

            try
            {
                IContentDataProvider contentDataProvider = new FileBasedContentDataProvider(inputFilePath);
                ContentPlaylistGenerator contentPlaylistGenerator = new ContentPlaylistGenerator(contentDataProvider);

                IEnumerable<ContentPlaylist> contentPlaylists = contentPlaylistGenerator.GetContentPlaylists(contentId, countryCode);
                if (contentPlaylists != null)
                {
                    foreach (ContentPlaylist contentPlaylist in contentPlaylists)
                    {
                        Console.WriteLine(contentPlaylist);
                    }
                }

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }
        }
    }
}
