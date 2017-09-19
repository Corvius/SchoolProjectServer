using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CustomLog;

namespace SchoolProjectServer
{
    class SQLConnector
    {

        private string connectionString;

        private readonly string dbName = "TTS";
        private readonly string dbUser = "TTS_admin";
        private readonly string dbPass = "ReadyToUse1";

        internal readonly static string defaultServerURL = "trumptweetstyle.database.windows.net";
        internal readonly static string defaultServerPort = ""; // 1433

        internal SQLConnector()
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

        public List<Tweet> GetTweetsfromDatabase(int tweetCount)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                {
                    string count = (tweetCount > 0) ? "TOP " + tweetCount.ToString() + " *" : "*";

                    string selectTweetsCommand = string.Format(
                        "SELECT {0} FROM dbo.tweets ORDER BY tweetTimeStamp DESC;", count);

                    DataSet results = new DataSet();
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
            }
        }
        public void UpdateTweetsInDatabase() { }

        internal List<TweetStyle> GetTweetStyles()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                {
                    string selectNamesCommand =
                        "SELECT StyleName, StyleImage " +
                        "FROM TTS.dbo.styles " +
                        "WHERE OBJECT_ID(LOWER(N'dbo.' + StyleName), N'U') IS NOT NULL;";

                    DataSet results = new DataSet();
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
            }
        }

        internal List<StyleProperty> GetTweetStyleProperties(string styleName)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                {
                    string selectContentsCommand = string.Format(
                        "SELECT Original, Replacement " +
                        "FROM TTS.dbo.{0};", styleName.ToLower());

                    DataSet results = new DataSet();
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

        internal void UpdateTweets(List<Tweet> tweets)
        {
            try
            {
                //sqlConnectionX.Open();
                List<Tweet> rawTweetData = GetTweetsfromDatabase(0);

                List<long> existingIDs = rawTweetData
                    .Select(row => row.TweetID).ToList();

                //List<long> existingIDs = rawTweetData
                //    .Rows
                //    .Cast<DataRow>()
                //    .Select(row => (long)row["tweetID"]).ToList();

                foreach (Tweet tweet in tweets)
                {
                    if (existingIDs.Contains(tweet.TweetID))
                        continue;

                    string insertRowCommand = string.Format(
                        "INSERT INTO dbo.tweets (tweetID, tweetText, tweetTimeStamp, tweetUpVote, tweetDownVote) " +
                            "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');",
                        tweet.TweetID, Tweet.Base64Encode(tweet.TweetText), tweet.TweetTimeStamp.ToString("yyyy.MM.dd HH:mm:ss"), tweet.TweetUpvotes, tweet.TweetDownvotes);

                    //using (SqlCommand command = new SqlCommand(insertRowCommand, sqlConnectionX))
                    //    command.ExecuteNonQuery();
                }
            }
            finally
            {
                //sqlConnectionX.Close();
            }
        }

        internal void AddNewStyle(string styleName)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                {
                    string createTableCommand = string.Format(
                        "CREATE TABLE {0} (" +
                        "Original nvarchar(50) NOT NULL PRIMARY KEY, " +
                        "Replacement nvarchar(50) NOT NULL);", styleName.ToLower());

                    string updateTableCommand = string.Format(
                        "INSERT INTO dbo.styles (StyleName, StyleImage) VALUES ('{0}', '');", styleName);

                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand(createTableCommand, sqlConnection))
                        command.ExecuteNonQuery();

                    using (SqlCommand command = new SqlCommand(updateTableCommand, sqlConnection))
                        command.ExecuteNonQuery();

                    this.Log(LogExtension.LogLevels.Info, "Create/Update table command was successfully executed");
                }
            }
        }

        internal void RemoveStyle(string styleName)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter())
                {
                    string dropTableCommand = string.Format(
                        "DROP TABLE dbo.{0};", styleName.ToLower());

                    string updateTableCommand = string.Format(
                        "DELETE FROM dbo.styles WHERE StyleName = '{0}';", styleName);

                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand(dropTableCommand, sqlConnection))
                        command.ExecuteNonQuery();

                    using (SqlCommand command = new SqlCommand(updateTableCommand, sqlConnection))
                        command.ExecuteNonQuery();

                    this.Log(LogExtension.LogLevels.Info, "Drop/Update table command was successfully executed");
                }
            }
        }

        internal void UpdateStyle(DataTable styleData)
        {
            try
            {
                //sqlConnectionX.Open();

                string tableName = styleData.TableName.ToLower();
                string clearCommand = string.Format("TRUNCATE TABLE dbo.{0};", tableName);

                //using (SqlCommand command = new SqlCommand(clearCommand, sqlConnectionX))
                //    command.ExecuteNonQuery();

                List<StyleProperty> properties = styleData
                    .Rows
                    .Cast<DataRow>()
                    .Select(row => new StyleProperty(row["Original"].ToString(), row["Replacement"].ToString())).ToList();

                foreach (StyleProperty property in properties)
                {
                    string updateCommand = string.Format("INSERT INTO dbo.{0} (Original, Replacement) VALUES ('{1}', '{2}');", tableName, property.Original, property.Replacement);
                    //using (SqlCommand command = new SqlCommand(updateCommand, sqlConnectionX))
                    //    command.ExecuteNonQuery();
                }

                this.Log(LogExtension.LogLevels.Info, "Server was successfully updated");
            }
            catch (Exception ex)
            {
                this.Log(LogExtension.LogLevels.Error, "Unable to update server! Reason:\n" + ex.Message);
            }
            finally
            {
                //sqlConnectionX.Close();
            }
        }
    }
}
