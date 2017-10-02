using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace SchoolProjectServer
{
    class ServerSQLConnector
    {
        private string connectionString;

        private readonly string dbName = "TTS";
        private readonly string dbUser = "";
        private readonly string dbPass = "";

        internal readonly static string defaultServerURL = "trumptweetstyle.database.windows.net";

        internal ServerSQLConnector()
        {
            BuildConnectionString();
        }

        internal void BuildConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = defaultServerURL;
            builder.InitialCatalog = dbName;
            builder.PersistSecurityInfo = true;
            builder.UserID = dbUser;
            // TODO: Encrypt this
            builder.Password = dbPass;

            connectionString = builder.ConnectionString;
        }

        public List<Tweet> GetTweetsFromDatabase(int tweetCount)
        {
            string count = (tweetCount > 0) ? "TOP " + tweetCount.ToString() + " *" : "*";

            string selectTweetsCommand = string.Format(
                "SELECT {0} FROM dbo.tweets ORDER BY tweetTimeStamp DESC;", count);

            DataSet results = new DataSet();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
            using (SqlCommand command = new SqlCommand(selectTweetsCommand, sqlConnection))
            {
                sqlDataAdapter.SelectCommand = command;
                sqlDataAdapter.Fill(results);
            }

            List<Tweet> tweetQuery = results.Tables[0]
                .Rows
                .Cast<DataRow>()
                .Select(row => new Tweet(
                    (long)row["tweetID"],
                    (string)row["tweetText"],
                    DateTime.Parse(row["tweetTimeStamp"].ToString()),
                    (int)row["tweetUpVote"],
                    (int)row["tweetDownVote"])).ToList();

            return tweetQuery;
        }

        public void AddTweetsToDatabase(List<Tweet> tweets)
        {
            List<Tweet> rawTweetData = GetTweetsFromDatabase(0);

            List<long> existingIDs = rawTweetData
                .Select(row => row.TweetID).ToList();

            string insertRowCommand = "INSERT INTO dbo.tweets (tweetID, tweetText, tweetTimeStamp, tweetUpVote, tweetDownVote) VALUES ";
            int matchCounter = 0;

            foreach (Tweet tweet in tweets)
            {
                if (existingIDs.Contains(tweet.TweetID))
                {
                    matchCounter++;
                    continue;
                }

                insertRowCommand += string.Format(
                    "('{0}', '{1}', '{2}', '{3}', '{4}'), ",
                    tweet.TweetID, Tweet.Base64Encode(tweet.TweetText), tweet.TweetTimeStamp.ToString("yyyy-MM-dd HH:mm:ss"), tweet.TweetUpvotes, tweet.TweetDownvotes);
            }

            insertRowCommand = insertRowCommand.Remove(insertRowCommand.Length - 2);

            if (matchCounter == tweets.Count)
                return;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(insertRowCommand, sqlConnection))
            {
                sqlConnection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void RemoveTweetsFromDatabase(List<long> tweetIDs)
        {
            if (tweetIDs.Count == 0)
                return;

            string deleteRowCommand = "DELETE FROM dbo.tweets WHERE tweetID in (";

            foreach (long tweetId in tweetIDs)
                deleteRowCommand += tweetId.ToString() + ", ";

            deleteRowCommand = deleteRowCommand.Remove(deleteRowCommand.Length - 2);
            deleteRowCommand += ")";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(deleteRowCommand, sqlConnection))
            {
                sqlConnection.Open();
                command.ExecuteNonQuery();
            }
        }

        internal List<TweetStyle> GetTweetStyles()
        {
            string selectNamesCommand =
                "SELECT StyleName, StyleImage " +
                "FROM TTS.dbo.styles " +
                "WHERE OBJECT_ID(LOWER(N'dbo.' + StyleName), N'U') IS NOT NULL;";

            DataSet results = new DataSet();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
            using (SqlCommand command = new SqlCommand(selectNamesCommand, sqlConnection))
            {
                sqlDataAdapter.SelectCommand = command;
                sqlDataAdapter.Fill(results);
            }

            List<TweetStyle> tweetQuery = results.Tables[0]
                .Rows
                .Cast<DataRow>()
                .Select(row => new TweetStyle(
                    (string)row["StyleName"],
                    (string)row["StyleImage"])).ToList();

            return tweetQuery;
        }

        internal List<StyleProperty> GetTweetStyleProperties(string styleName)
        {
            string selectContentsCommand = string.Format(
                "SELECT Original, Replacement " +
                "FROM TTS.dbo.{0};", styleName.ToLower());

            DataSet results = new DataSet();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
            using (SqlCommand command = new SqlCommand(selectContentsCommand, sqlConnection))
            {
                sqlDataAdapter.SelectCommand = command;
                sqlDataAdapter.Fill(results);
            }

            List<StyleProperty> tweetPropertyQuery = results.Tables[0]
                .Rows
                .Cast<DataRow>()
                .Select(row => new StyleProperty(
                    (string)row["Original"],
                    (string)row["Replacement"])).ToList();

            return tweetPropertyQuery;
        }
    }
}
