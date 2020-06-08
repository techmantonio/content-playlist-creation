using ContentPlaylistCreation.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace ContentPlaylistCreation.UnitTests
{
    /// <summary>
    /// Tests the file base implementation of <see cref="IContentDataProvider"/>.
    /// </summary>
    [TestClass]
    [DeploymentItem("TestInputFiles\\invalid-content-data.json")]
    public class TestFileBasedContentDataProvider
    {
        /// <summary>
        /// Tests <see cref="FileBasedContentDataProvider"/> when no file path is provided.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestFileBasedContentDataProvider_PathNotProvided()
        {
            new FileBasedContentDataProvider(string.Empty);
        }

        /// <summary>
        /// Tests <see cref="FileBasedContentDataProvider"/> when a bad file path is provided.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void TestFileBasedContentDataProvider_InvaidPathProvided()
        {
            new FileBasedContentDataProvider("bad-path");
        }

        /// <summary>
        /// Tests <see cref="FileBasedContentDataProvider"/> when invalid JSON is provided.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void TestFileBasedContentDataProvider_InvaidJsonProvided()
        {
            new FileBasedContentDataProvider("invalid-content-data.json");
        }
    }
}
