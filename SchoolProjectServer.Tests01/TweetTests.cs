using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolProjectServer.Tests
{
    [TestClass]
    [PexClass(typeof(SQLConnector))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    public class TweetTests
    {
        private static Tweet tw = null;

        [ClassInitialize]
        public static void InitializeTests(TestContext tc)
        {
            tw = new Tweet(9999, "tweetText", new DateTime(4, 4, 4, 4, 4, 4), 4, 4);
        }
        
        [TestMethod]
        public void Tweet_AfterCreationDataIsCorrect()
        {
            Assert.IsTrue(tw.TweetID == 9999);
            Assert.IsTrue(tw.TweetText == "tweetText");
            Assert.IsTrue(tw.TweetTimeStamp.ToString() == "0004. 04. 04. 4:04:04");
            Assert.IsTrue(tw.TweetUpvotes == 4);
            Assert.IsTrue(tw.TweetDownvotes == 4);
        }

        [TestMethod]
        public void Tweet_CanUpdateText()
        {
            Assert.IsTrue(tw.TweetText == "tweetText");
            tw.Updatetext("updatedTweet");
            Assert.IsTrue(tw.TweetText == "updatedTweet");
        }

        [TestMethod]
        public void Tweet_CanUpVote()
        {
            int votes = tw.TweetUpvotes;
            tw.Upvote();
            Assert.IsTrue(tw.TweetUpvotes != votes);
        }

        [TestMethod]
        public void Tweet_CanDownVote()
        {
            int votes = tw.TweetDownvotes;
            tw.Downvote();
            Assert.IsTrue(tw.TweetDownvotes != votes);
        }

        [TestMethod]
        public void Tweet_CanBase64Encode()
        {
            Assert.IsTrue(Tweet.Base64Encode("tweetText") == "dHdlZXRUZXh0");
        }

        [TestMethod]
        public void Tweet_CanBase64Decode()
        {
            Assert.IsTrue(Tweet.Base64Decode("dHdlZXRUZXh0") == "tweetText");
        }
    }
}