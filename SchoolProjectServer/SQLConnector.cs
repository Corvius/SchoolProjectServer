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

        private SqlConnection sqlConnection;
        private SqlDataAdapter sqlDataAdapter;
        private string connectionString;

        private readonly string dbName = "TTS_DB";
        private readonly string dbUser = "TTS_APP";
        private readonly string dbPass = "GetTrumpTweets";

        internal readonly static string defaultServerURL = "89.132.188.93";
        internal readonly static string defaultServerPort = ""; // 1433

        internal SQLConnector()
        {
        }

        internal bool BuildConnection(string serverURL, string serverPort)
        {
            string URL = (serverURL == string.Empty) ? SQLConnector.defaultServerURL : serverURL;
            string Port = (serverPort == string.Empty) ? SQLConnector.defaultServerPort : serverPort;

            string dbURL = (Port != "") ? URL + ":" + Port : URL;

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = dbURL;
            builder.InitialCatalog = dbName;
            builder.PersistSecurityInfo = true;
            builder.UserID = dbUser;
            // TODO: Encrypt this
            builder.Password = dbPass;
            
            connectionString = builder.ConnectionString;

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlDataAdapter = new SqlDataAdapter();
                sqlConnection.Open();
                this.Log(LogExtension.LogLevels.Info, "Connection established successfully!");
            }
            catch (SqlException ex)
            {
                this.Log(LogExtension.LogLevels.Error, "Connection failed! Reason:\n" + ex.Message);
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }

            return true;
        }

        internal bool isDbConnectionAvailable()
        {
            try
            {
                this.Log(LogExtension.LogLevels.Debug, "Checking if DB connection is available...");
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    this.Log(LogExtension.LogLevels.Debug, "DB connection is alive");
                    return true;
                }
            }
            catch (Exception)
            {
                this.Log(LogExtension.LogLevels.Debug, "DB connection is down");
                return false;
            }
        }

        internal DataTable RetrieveTweetsFromSql(int tweetCount = 0)
        {
            string count = (tweetCount > 0) ? "TOP " + tweetCount.ToString() + " *" : "*"; 

            string selectTweetsCommand = string.Format(
                "SELECT {0} FROM dbo.tweets ORDER BY tweetTimeStamp DESC;", count);

            return SelectData(selectTweetsCommand, "Tweets");
        }

        internal List<Tweet> GetTweets(int count)
        {
            DataTable tweets = RetrieveTweetsFromSql(count);

            List<Tweet> tweetQuery = tweets
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

        private DataTable SelectData(string pSelectCommandString, string tableName)
        {
            DataTable results = new DataTable(tableName);
            sqlDataAdapter.SelectCommand = new SqlCommand(pSelectCommandString, sqlConnection);
            sqlDataAdapter.Fill(results);
            return results;
        }

        internal DataSet GetTweetStyles()
        {
            DataSet results = new DataSet("TweetStyles");

            string selectNamesCommand =
                "SELECT StyleName, StyleImage " +
                "FROM TTS_DB.dbo.styles " +
                "WHERE OBJECT_ID(LOWER(N'dbo.' + StyleName), N'U') IS NOT NULL;";

            DataTable nameQuery = SelectData(selectNamesCommand, "StyleNames");

            results.Tables.Add(nameQuery);

            for (int nameRowIndex = 0; nameRowIndex < nameQuery.Rows.Count; nameRowIndex++)
            { 
                string styleName = nameQuery.Rows[nameRowIndex][0].ToString();

                string selectContentsCommand = string.Format(
                    "SELECT Original, Replacement " +
                    "FROM TTS_DB.dbo.{0};", styleName.ToLower());

                DataTable contentQuery = SelectData(selectContentsCommand, styleName);

                results.Tables.Add(contentQuery);
            }

            return results;
        }

        internal void UpdateTweets(List<Tweet> tweets)
        {
            try
            {
                sqlConnection.Open();
                DataTable rawTweetData = RetrieveTweetsFromSql(0);

                List<long> existingIDs = rawTweetData
                    .Rows
                    .Cast<DataRow>()
                    .Select(row => (long)row["tweetID"]).ToList();

                foreach (Tweet tweet in tweets)
                {
                    if (existingIDs.Contains(tweet.TweetID))
                        continue;

                    string insertRowCommand = string.Format(
                        "INSERT INTO dbo.tweets (tweetID, tweetText, tweetTimeStamp, tweetUpVote, tweetDownVote) " +
                            "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');",
                        tweet.TweetID, Tweet.Base64Encode(tweet.TweetText), tweet.TweetTimeStamp.ToString("yyyy.MM.dd HH:mm:ss"), tweet.TweetUpvotes, tweet.TweetDownvotes);

                    using (SqlCommand command = new SqlCommand(insertRowCommand, sqlConnection))
                        command.ExecuteNonQuery();
                }
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        internal void AddNewStyle(string styleName)
        {
            try
            {
                sqlConnection.Open();

                string createTableCommand = string.Format(
                    "CREATE TABLE {0} (" +
                    "Original nvarchar(50) NOT NULL PRIMARY KEY, " +
                    "Replacement nvarchar(50) NOT NULL);", styleName.ToLower());

                string updateTableCommand = string.Format(
                    "INSERT INTO dbo.styles (StyleName, StyleImage) VALUES ('{0}', '');", styleName);

                using (SqlCommand command = new SqlCommand(createTableCommand, sqlConnection))
                    command.ExecuteNonQuery();

                using (SqlCommand command = new SqlCommand(updateTableCommand, sqlConnection))
                    command.ExecuteNonQuery();

                this.Log(LogExtension.LogLevels.Info, "Create/Update table command was successfully executed");
            }
            catch (Exception)
            {
                this.Log(LogExtension.LogLevels.Error, "Create/Update table command cannot be executed!");
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        internal void RemoveStyle(string styleName)
        {
            try
            {
                sqlConnection.Open();

                string dropTableCommand = string.Format(
                    "DROP TABLE dbo.{0};", styleName.ToLower());

                string updateTableCommand = string.Format(
                    "DELETE FROM dbo.styles WHERE StyleName = '{0}';", styleName);

                using (SqlCommand command = new SqlCommand(dropTableCommand, sqlConnection))
                    command.ExecuteNonQuery();

                using (SqlCommand command = new SqlCommand(updateTableCommand, sqlConnection))
                    command.ExecuteNonQuery();

                this.Log(LogExtension.LogLevels.Info, "Drop/Update table command was successfully executed");
            }
            catch (Exception)
            {
                this.Log(LogExtension.LogLevels.Error, "Drop/Update table command cannot be executed!");
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        internal void UpdateStyle(DataTable styleData)
        {
            try
            {
                sqlConnection.Open();

                string tableName = styleData.TableName.ToLower();
                string clearCommand = string.Format("TRUNCATE TABLE dbo.{0};", tableName);

                using (SqlCommand command = new SqlCommand(clearCommand, sqlConnection))
                    command.ExecuteNonQuery();

                List<StyleProperty> properties = styleData
                    .Rows
                    .Cast<DataRow>()
                    .Select(row => new StyleProperty(row["Original"].ToString(), row["Replacement"].ToString())).ToList();

                foreach (StyleProperty property in properties)
                {
                    string updateCommand = string.Format("INSERT INTO dbo.{0} (Original, Replacement) VALUES ('{1}', '{2}');", tableName, property.Original, property.Replacement);
                    using (SqlCommand command = new SqlCommand(updateCommand, sqlConnection))
                        command.ExecuteNonQuery();
                }

                this.Log(LogExtension.LogLevels.Info, "Server was successfully updated");
            }
            catch (Exception ex)
            {
                this.Log(LogExtension.LogLevels.Error, "Unable to update server! Reason:\n" + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
