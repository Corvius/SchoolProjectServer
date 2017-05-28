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

        internal readonly static string defaultServerURL = "localhost";
        internal readonly static string defaultServerPort = ""; // 1433

        internal bool IsConnectionAvailable
        {
            get { return CheckConnection() != ConnectionState.Broken; }
        }

        public SQLConnector()
        {
        }

        public bool BuildConnection(string serverURL, string serverPort)
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
                this.Log(LogExtension.LogLevels.Info, "Connection established successfully!");
            }
            catch (SqlException ex)
            {
                this.Log(LogExtension.LogLevels.Error, "Connection failed! Reason:\n" + ex.Message);
                return false;
            }

            return true;
        }

        public ConnectionState CheckConnection()
        {
            try
            {
                if (sqlConnection == null)
                    sqlConnection = new SqlConnection(connectionString);

                sqlConnection.Open();
            }
            catch (Exception)
            {
                this.Log(LogExtension.LogLevels.Error, "Unable to open a connection to tweet database!");
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }
            return sqlConnection.State;
        }

        /// <summary>
        /// Test that the server is connected
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <returns>true if the connection is opened</returns>
        public bool IsServerConnected()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool UpdateTweets(List<Tweet> tweets)
        {
            bool result = true;
            foreach (Tweet tweet in tweets)
            {
                string insertRowCommand = string.Format(
                    "INSERT INTO dbo.tweets (tweetID, tweetText, tweetTimeStamp, tweetUpVote, tweetDownVote) " +
                        "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}');",
                    tweet.TweetID, Tweet.Base64Encode(tweet.TweetText), tweet.TweetTimeStamp.ToString("yyyy.MM.dd HH:mm:ss"), tweet.TweetUpvotes, tweet.TweetDownvotes);

                try
                {
                    sqlConnection.Open();
                    using (SqlCommand command = new SqlCommand(insertRowCommand, sqlConnection))
                        command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    result = false;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }

            return result;
        }

        private DataTable RetrieveTweets(int count)
        {
            List<Tweet> results = new List<Tweet>();

            string selectTweetsCommand = string.Format(
                "SELECT TOP {0} * FROM dbo.tweets ORDER BY tweetTimeStamp DESC;", count.ToString());

            return SelectData(selectTweetsCommand, "Tweets");
        }

        public List<Tweet> GetTweets(int count)
        {
            DataTable tweets = RetrieveTweets(count);

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

        /// <summary>
        /// Connects to the database and executes the given SELECT command.
        /// </summary>
        /// <param name="pSelectCommandString">The SQL SELECT command to be executed on the database</param>
        /// <returns>If the query is successful, returns a DataSet object</returns>
        private DataTable SelectData(string pSelectCommandString, string tableName)
        {
            // Create return variable
            DataTable results = new DataTable(tableName);

            sqlDataAdapter.SelectCommand = new SqlCommand(pSelectCommandString, sqlConnection);

            // Populate the dataset from the database
            sqlDataAdapter.Fill(results);

            return results;
        }

        public DataSet GetTweetStyles()
        {
            DataSet results = new DataSet("TweetStyles");
            DataTable nameQuery = null;
            DataTable contentQuery = null;
            string selectNamesCommand;
            string selectContentsCommand;
            string styleName;
            string styleImage;

            selectNamesCommand =
                "SELECT StyleName, StyleImage " +
                "FROM TTS_DB.dbo.styles " +
                "WHERE OBJECT_ID(LOWER(N'dbo.' + StyleName), N'U') IS NOT NULL;";

            nameQuery = SelectData(selectNamesCommand, "StyleNames");

            results.Tables.Add(nameQuery);

            for (int nameRowIndex = 0; nameRowIndex < nameQuery.Rows.Count; nameRowIndex++)
            { 
                styleName = nameQuery.Rows[nameRowIndex][0].ToString();
                styleImage = nameQuery.Rows[nameRowIndex][1].ToString();

                selectContentsCommand = string.Format(
                    "SELECT Original, Replacement " +
                    "FROM TTS_DB.dbo.{0};", styleName.ToLower());

                contentQuery = SelectData(selectContentsCommand, styleName);

                results.Tables.Add(contentQuery);

            }

            return results;
        }

        public bool AddNewStyle(string styleName)
        {
            string createTableCommand = string.Format(
                "CREATE TABLE {0} (" +
                "Original nvarchar(50) NOT NULL PRIMARY KEY, " +
                "Replacement nvarchar(50) NOT NULL);", styleName.ToLower());

            string updateTableCommand = string.Format(
                "INSERT INTO dbo.styles (StyleName, StyleImage) VALUES ('{0}', '');", styleName);

            try
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(createTableCommand, sqlConnection))
                    command.ExecuteNonQuery();

                using (SqlCommand command = new SqlCommand(updateTableCommand, sqlConnection))
                    command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                this.Log(LogExtension.LogLevels.Error, "Create/Update table command cannot be executed!");
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }

            this.Log(LogExtension.LogLevels.Info, "Create/Update table command was successfully executed");

            return true;
        }

        public bool RemoveStyle(string styleName)
        {
            string dropTableCommand = string.Format(
                "DROP TABLE dbo.{0};", styleName.ToLower());

            string updateTableCommand = string.Format(
                "DELETE FROM dbo.styles WHERE StyleName = '{0}';", styleName);

            try
            {
                sqlConnection.Open();
                using (SqlCommand command = new SqlCommand(dropTableCommand, sqlConnection))
                    command.ExecuteNonQuery();

                using (SqlCommand command = new SqlCommand(updateTableCommand, sqlConnection))
                    command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                this.Log(LogExtension.LogLevels.Error, "Drop/Update table command cannot be executed!");
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }

            this.Log(LogExtension.LogLevels.Info, "Drop/Update table command was successfully executed");

            return true;
        }

        public bool UpdateStyle(DataTable styleData)
        {
            try
            {
                sqlDataAdapter.Update(styleData);
            }
            catch
            {
                this.Log(LogExtension.LogLevels.Error, "Unable to update server!");
                return false;
            }

            this.Log(LogExtension.LogLevels.Info, "Server was successfully updated");
            return true;
        }
    }
}

// Welcome to the Graveyard!
// You may find both trash and treasure in this section for discarded methods. Enjoy!
/*

/// <summary>
/// Gets all (attirbutes, stats) data related to a single character in a specific order
/// </summary>
/// <param name="pCharName">Name of the character</param>
/// <returns>Returns a DataHolder list filled with the requested data</returns>
public List<DataHolder> GetElementData(string pCharName)
{
    this.Log(LogExtension.LogLevels.Info, "Retrieving element data");

    string selectCommandString =
        "SELECT * " +
        "FROM dbo.pirate;";

    List<DataHolder> results = new List<DataHolder>();

    foreach (var entry in SelectData(selectCommandString, true))
    {
        results.Add(new DataHolder(entry[0], entry[1]));
    }

    return results;
}

            for (int rowIndex = 0; rowIndex < dataSet.Tables[tableIndex].Rows.Count; rowIndex++)
            {
                results.Add(new List<string>());

                for (int columnIndex = 0; columnIndex < dataSet.Tables[tableIndex].Columns.Count; columnIndex++)
                {
                    string cellValue = dataSet.Tables[tableIndex].Rows[rowIndex][columnIndex].ToString();


        public List<TweetStyle> GetTweetStyles()
        {
            List<TweetStyle> results = new List<TweetStyle>();
            TweetStyle currentStyle = null;
            DataTable nameQuery = null;
            DataTable contentQuery = null;
            string selectNamesCommand;
            string selectContentsCommand;
            string styleName;
            string styleImage;
            string original;
            string replacement;

            selectNamesCommand =
                "SELECT StyleName, StyleImage " +
                "FROM TTS_DB.dbo.styles " +
                "WHERE OBJECT_ID(LOWER(N'dbo.' + StyleName), N'U') IS NOT NULL;";

            nameQuery = SelectData(selectNamesCommand);

            for (int nameRowIndex = 0; nameRowIndex < nameQuery.Rows.Count; nameRowIndex++)
            { 
                styleName = nameQuery.Rows[nameRowIndex][0].ToString();
                styleImage = nameQuery.Rows[nameRowIndex][1].ToString();

                currentStyle = new TweetStyle(styleName, styleImage);

                selectContentsCommand = string.Format(
                    "SELECT Original, Replacement " +
                    "FROM TTS_DB.dbo.{0};", styleName.ToLower());

                contentQuery = SelectData(selectContentsCommand);

                for (int contentRowIndex = 0; contentRowIndex < contentQuery.Rows.Count; contentRowIndex++)
                {
                    original = nameQuery.Rows[nameRowIndex][0].ToString();
                    replacement = nameQuery.Rows[nameRowIndex][1].ToString();

                    currentStyle.AddProperty(original, replacement);
                }

                results.Add(currentStyle);
            }

            return results;
           
        }
    }


*/
