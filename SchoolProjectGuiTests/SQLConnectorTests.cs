using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace SchoolProjectGui.Tests
{
    [TestClass]
    public partial class SQLConnectorTests
    {
        private static SQLConnector sc = null;

        [ClassInitialize]
        public static void InitializeTests(TestContext tc)
        {
            sc = new SQLConnector();
        }

        [TestMethod]
        public void SQLConnector_CanPullFiveTweetsFromDatabase()
        {
            System.Collections.Generic.List<Tweet> twl = sc.GetTweetsFromDatabase(5);
            Assert.IsTrue(twl.Count == 5);
        }

        [TestMethod]
        public void SQLConnector_CanPushATweetToDatabase()
        {
            System.Collections.Generic.List<Tweet> twl_new = new System.Collections.Generic.List<Tweet>();
            twl_new.Add(new Tweet(99999999, "tweetText", new DateTime(1994, 4, 4, 4, 4, 4), 4, 4));
            sc.AddTweetsToDatabase(twl_new);
            System.Collections.Generic.List<Tweet> twl = sc.GetTweetsFromDatabase(0);
            Tweet tweetQuery = twl
                .Where(tw => tw.TweetID == 99999999).FirstOrDefault();
            Assert.IsTrue(tweetQuery != null);
        }

        [TestMethod]
        public void SQLConnector_CanRemoveATweetFromDatabase()
        {
            System.Collections.Generic.List<long> idList = new System.Collections.Generic.List<long>();
            idList.Add(99999999);
            sc.RemoveTweetsFromDatabase(idList);
            System.Collections.Generic.List<Tweet> twl = sc.GetTweetsFromDatabase(0);
            var tweetQuery = twl
                .Where(tw => tw.TweetID == 99999999).Select(tw => tw).FirstOrDefault();
            Assert.IsTrue(tweetQuery == null);
        }

        [TestMethod]
        public void SQLConnector_CanPullTweetStyles()
        {
            System.Collections.Generic.List<TweetStyle> twst = sc.GetTweetStyles();
            Assert.IsTrue(twst.Count > 0);
        }

        [TestMethod]
        public void SQLConnector_CanPullTweetStyleProperties()
        {
            System.Collections.Generic.List<TweetStyle> twst = sc.GetTweetStyles();
            System.Collections.Generic.List<StyleProperty> sp = sc.GetTweetStyleProperties(twst[0].styleName);
            Assert.IsTrue(sp.Count > 0);
        }

        [TestMethod]
        public void SQLConnector_CanAddNewStyle()
        {
            System.Collections.Generic.List<TweetStyle> twst_old = sc.GetTweetStyles();
            sc.AddNewStyle("NewStyleTest");
            System.Collections.Generic.List<TweetStyle> twst_new = sc.GetTweetStyles();
            Assert.IsTrue(twst_old.Count == twst_new.Count - 1);
        }

        [TestMethod]
        public void SQLConnector_CanUpdateStyle()
        {
            bool found = false;
            System.Collections.Generic.List<StyleProperty> spl_one = sc.GetTweetStyleProperties("NewStyleTest");
            StyleProperty sp_one = new StyleProperty("PropOrigTest", "PropReplTest");
            spl_one.Add(sp_one);
            sc.UpdateStyle("NewStyleTest", spl_one);
            System.Collections.Generic.List<StyleProperty> spl_two = sc.GetTweetStyleProperties("NewStyleTest");
            foreach (StyleProperty sp_two in spl_two)
                if (sp_two.Original == sp_one.Original && sp_two.Replacement == sp_one.Replacement)
                    found = true;
            Assert.IsTrue(found);
        }

        [TestMethod]
        public void SQLConnector_CanDeleteStyle()
        {
            System.Collections.Generic.List<TweetStyle> twst_old = sc.GetTweetStyles();
            sc.RemoveStyle("NewStyleTest");
            System.Collections.Generic.List<TweetStyle> twst_new = sc.GetTweetStyles();
            Assert.IsTrue(twst_old.Count == twst_new.Count + 1);
        }
    }
}
