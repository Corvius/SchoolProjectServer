using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace SchoolProjectServer.Tests
{
    [TestClass]
    [PexClass(typeof(SQLConnector))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    public partial class SQLConnectorTest
    {
        private static SQLConnector sc = null;

        [ClassInitialize]
        public static void InitializeTests(TestContext tc) {
            sc = new SQLConnector();
        }

        [TestMethod]
        public void SQLConnector_CanPullFiveTweetsFromDatabase()
        {
            System.Collections.Generic.List<Tweet> twl = sc.GetTweetsfromDatabase(5);
            Assert.IsTrue(twl.Count == 5);
        }

        [TestMethod]
        public void SQLConnector_CanPushATweetToDatabase()
        {
            System.Collections.Generic.List<Tweet> twl = sc.GetTweetsfromDatabase(1);
            Assert.IsTrue(false);
            //Tweet oldTweetData = GetLastTweetFromRetrievedData();
            //AddNewTestRowToDataSet();
            //sc.PushDataToDatabase();
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
        public void SQLConnector_CanDeleteStyle()
        {
            System.Collections.Generic.List<TweetStyle> twst_old = sc.GetTweetStyles();
            sc.RemoveStyle("NewStyleTest");
            System.Collections.Generic.List<TweetStyle> twst_new = sc.GetTweetStyles();
            Assert.IsTrue(twst_old.Count == twst_new.Count + 1);
        }


        //private Tweet GetLastTweetFromRetrievedData()
        //{
        //    System.Data.DataRow row = sc.tweetDbData.Tables[0].Rows[0];
        //    long a = (long)row["tweetID"];
        //    string b = row["tweetText"].ToString();
        //    DateTime c = DateTime.Parse(row["tweetTimeStamp"].ToString());
        //    int d = (int)row["tweetUpVote"];
        //    int e = (int)row["tweetDownVote"];

        //    return new Tweet(a, b, c, d, e);
        //    //return new Tweet(
        //    //    (long)row["tweetID"],
        //    //    row["tweetText"].ToString(),
        //    //    DateTime.Parse(row["tweetTimeStamp"].ToString()),
        //    //    (int)row["tweetUpVote"],
        //    //    (int)row["tweetDownVote"]
        //    //);
        //}

        //private void AddNewTestRowToDataSet()
        //{
        //    System.Data.DataRow newRow = sc.tweetDbData.Tables[0].NewRow();
        //    newRow["tweetID"] = 100000000000000000;
        //    newRow["tweetText"] = "Update Test";
        //    newRow["tweetTimeStamp"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //    newRow["tweetUpVote"] = 99;
        //    newRow["tweetDownVote"] = 99;
        //    sc.tweetDbData.Tables[0].Rows.Add(newRow);
        //}
    }
}
