using ContentPlaylistCreation.Interfaces;
using ContentPlaylistCreation.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ContentPlaylistCreation.UnitTests
{
    /// <summary>
    /// Tests the <see cref="ContentPlaylistGenerator"/> class.
    /// </summary>
    [TestClass]
    [DeploymentItem("TestInputFiles\\test-content-data-1.json")]
    [DeploymentItem("TestInputFiles\\test-content-data-2.json")]
    [DeploymentItem("TestInputFiles\\test-content-data-3.json")]
    public class TestContentPlaylistGenerator
    {
        /// <summary>
        /// Tests the <see cref="ContentPlaylistGenerator"/> class when a matched pre-roll is not compatible
        /// with the target aspect ratio (problem statement input 1).
        /// </summary>
        [TestMethod]
        public void TestContentPlaylistGenerator_GetContentPlaylists_SampleInput1()
        {
            IContentDataProvider contentDataProvider = new FileBasedContentDataProvider("test-content-data-1.json");
            ContentPlaylistGenerator contentPlaylistGenerator = new ContentPlaylistGenerator(contentDataProvider);

            try
            {
                IEnumerable<ContentPlaylist> contentPlaylists = contentPlaylistGenerator.GetContentPlaylists("MI3", "US");
                Assert.Fail("Exception is expected due to incompatible aspect ratio for US country code.");
            }
            catch (InvalidOperationException e)
            {
                string expectedErrorText = $"(No legal playlist possible because the Pre-Roll Video isn't compatible with " +
                                           $"the aspect ratio of the Content Video for the US)";
                Assert.AreEqual(expected: expectedErrorText, actual: e.Message);
            }
        }

        /// <summary>
        /// Tests the <see cref="ContentPlaylistGenerator"/> class when there is one playlist expected (problem statement input 2).
        /// </summary>
        [TestMethod]
        public void TestContentPlaylistGenerator_GetContentPlaylists_SampleInput2()
        {
            IContentDataProvider contentDataProvider = new FileBasedContentDataProvider("test-content-data-1.json");
            ContentPlaylistGenerator contentPlaylistGenerator = new ContentPlaylistGenerator(contentDataProvider);
            IEnumerable<ContentPlaylist> contentPlaylists = contentPlaylistGenerator.GetContentPlaylists("MI3", "CA");

            // Validate
            Assert.IsNotNull(contentPlaylists);
            Assert.AreEqual(expected: 1, actual: contentPlaylists.Count());
            Assert.AreEqual(expected: "Playlist1\n{V5, V1}", actual: contentPlaylists.ElementAt(0).ToString());
        }

        /// <summary>
        /// Tests the <see cref="ContentPlaylistGenerator"/> class when there are multiple playlist expected (problem statement input 3).
        /// </summary>
        [TestMethod]
        public void TestContentPlaylistGenerator_GetContentPlaylists_SampleInput3()
        {
            IContentDataProvider contentDataProvider = new FileBasedContentDataProvider("test-content-data-1.json");
            ContentPlaylistGenerator contentPlaylistGenerator = new ContentPlaylistGenerator(contentDataProvider);
            IEnumerable<ContentPlaylist> contentPlaylists = contentPlaylistGenerator.GetContentPlaylists("MI3", "UK");

            // Validate
            Assert.IsNotNull(contentPlaylists);
            Assert.AreEqual(expected: 2, actual: contentPlaylists.Count());
            Assert.AreEqual(expected: "Playlist1\n{V6, V2}", actual: contentPlaylists.ElementAt(0).ToString());
            Assert.AreEqual(expected: "Playlist2\n{V7, V3}", actual: contentPlaylists.ElementAt(1).ToString());
        }

        /// <summary>
        /// Tests the <see cref="ContentPlaylistGenerator"/> class when there are multiple pre-rolls associated with content.
        /// </summary>
        [TestMethod]
        public void TestContentPlaylistGenerator_GetContentPlaylists_MultiplePrellRolls()
        {
            IContentDataProvider contentDataProvider = new FileBasedContentDataProvider("test-content-data-2.json");
            ContentPlaylistGenerator contentPlaylistGenerator = new ContentPlaylistGenerator(contentDataProvider);
            IEnumerable<ContentPlaylist> contentPlaylists = contentPlaylistGenerator.GetContentPlaylists("MI3", "UK");

            // Validate
            Assert.IsNotNull(contentPlaylists);
            Assert.AreEqual(expected: 2, actual: contentPlaylists.Count());
            Assert.AreEqual(expected: "Playlist1\n{V6, V12, V2}", actual: contentPlaylists.ElementAt(0).ToString());
            Assert.AreEqual(expected: "Playlist2\n{V7, V10, V3}", actual: contentPlaylists.ElementAt(1).ToString());
        }

        /// <summary>
        /// Tests the <see cref="ContentPlaylistGenerator"/> class when there are pre-rolls associated with multiple content.
        /// </summary>
        [TestMethod]
        public void TestContentPlaylistGenerator_GetContentPlaylists_SharedPreRollsAccrossContent()
        {
            IContentDataProvider contentDataProvider = new FileBasedContentDataProvider("test-content-data-2.json");
            ContentPlaylistGenerator contentPlaylistGenerator = new ContentPlaylistGenerator(contentDataProvider);
            IEnumerable<ContentPlaylist> contentPlaylists = contentPlaylistGenerator.GetContentPlaylists("MI3", "CA");

            // Validate
            Assert.IsNotNull(contentPlaylists);
            Assert.AreEqual(expected: 3, actual: contentPlaylists.Count());
            Assert.AreEqual(expected: "Playlist1\n{V5, V11, V1}", actual: contentPlaylists.ElementAt(0).ToString());
            Assert.AreEqual(expected: "Playlist2\n{V5, V11, V3}", actual: contentPlaylists.ElementAt(1).ToString());
            Assert.AreEqual(expected: "Playlist3\n{V15, V13}", actual: contentPlaylists.ElementAt(2).ToString());
        }

        /// <summary>
        /// Tests the <see cref="ContentPlaylistGenerator"/> class when there are no pre-rolls associated with content.
        /// </summary>
        [TestMethod]
        public void TestContentPlaylistGenerator_GetContentPlaylists_NoPrellRolls()
        {
            IContentDataProvider contentDataProvider = new FileBasedContentDataProvider("test-content-data-2.json");
            ContentPlaylistGenerator contentPlaylistGenerator = new ContentPlaylistGenerator(contentDataProvider);
            IEnumerable<ContentPlaylist> contentPlaylists = contentPlaylistGenerator.GetContentPlaylists("MI3", "FR");

            // Validate
            Assert.IsNotNull(contentPlaylists);
            Assert.AreEqual(expected: 1, actual: contentPlaylists.Count());
            Assert.AreEqual(expected: "Playlist1\n{V14}", actual: contentPlaylists.ElementAt(0).ToString());
        }

        /// <summary>
        /// Tests the <see cref="ContentPlaylistGenerator"/> class when there is no content available with the given Id.
        /// </summary>
        [TestMethod]
        public void TestContentPlaylistGenerator_GetContentPlaylists_NoContent()
        {
            IContentDataProvider contentDataProvider = new FileBasedContentDataProvider("test-content-data-2.json");
            ContentPlaylistGenerator contentPlaylistGenerator = new ContentPlaylistGenerator(contentDataProvider);
            IEnumerable<ContentPlaylist> contentPlaylists = contentPlaylistGenerator.GetContentPlaylists("MI4", "FR");

            // Validate
            Assert.IsNotNull(contentPlaylists);
            Assert.AreEqual(expected: 0, actual: contentPlaylists.Count());
        }

        /// <summary>
        /// Tests the <see cref="ContentPlaylistGenerator"/> class when a pre-roll referenced by a content asset does not exist.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void TestContentPlaylistGenerator_GetContentPlaylists_ReferencePreRollDoesntExist()
        {
            IContentDataProvider contentDataProvider = new FileBasedContentDataProvider("test-content-data-3.json");
            ContentPlaylistGenerator contentPlaylistGenerator = new ContentPlaylistGenerator(contentDataProvider);
            contentPlaylistGenerator.GetContentPlaylists("MI3", "US");
        }
    }
}
