using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SchoolProjectGui.Tests
{
    [TestClass]
    public partial class TweetStyleTest
    {
        private static TweetStyle ts = null;

        [ClassInitialize]
        public static void InitializeTests(TestContext tc)
        {
            ts = new TweetStyle("tweetStyleName", "tweetStyleURL");
        }

        [TestMethod]
        public void TweetStyle_NameAndUrlAreCorrect()
        {
            Assert.IsTrue(ts.styleName == "tweetStyleName");
            Assert.IsTrue(ts.styleImageURL == "tweetStyleURL");
        }

        [TestMethod]
        public void TweetStyle_CanAddProperty()
        {
            int count = ts.styleProperties.Count;
            ts.AddProperty("TestPropOrig", "TestPropRepl");
            Assert.IsTrue(count < ts.styleProperties.Count);
            Assert.IsTrue(IsPropertyPresent("TestPropOrig", "TestPropRepl"));
        }

        [TestMethod]
        public void TweetStyle_CanDeleteProperty()
        {
            int count = ts.styleProperties.Count;
            ts.DeleteProperty("TestPropOrig");
            Assert.IsTrue(count > ts.styleProperties.Count);
            Assert.IsFalse(IsPropertyPresent("TestPropOrig", "TestPropRepl"));
        }

        private bool IsPropertyPresent(string original, string replacement)
        {
            foreach (var styleProperty in ts.styleProperties)
                if (styleProperty.Original == original && styleProperty.Replacement == replacement)
                    return true;
            return false;
        }
    }
}
