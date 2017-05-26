using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CustomLog;

namespace SchoolProjectServer
{
    /// <summary>
    /// Handles communication and dataconversion between the application and the database
    /// </summary>
    class SQLConnector
    {
        /// <summary>
        /// Holds the necessary data to connect to the database
        /// </summary>
        private readonly string connectionString;
        private SqlConnection sqlConnection;
        private SqlDataAdapter dataAdapter;

        /// <summary>
        /// Initializes the object and builds the connection string
        /// </summary>
        public SQLConnector(string serverURL, string serverPort)
        {
            try
            {
                if (serverPort != "")
                    serverURL += ":" + serverPort;

                SqlConnectionStringBuilder builder =
                    new SqlConnectionStringBuilder(
                        String.Format("Data Source={0};Initial Catalog=TTS_DB;Persist Security Info=True;User ID=TTS_APP", serverURL)
                    );

                // TODO: Encrypt this
                builder.Password = "GetTrumpTweets";

                connectionString = builder.ConnectionString;

                sqlConnection = new SqlConnection(connectionString);
                dataAdapter = new SqlDataAdapter();
            }
            catch (Exception ex)
            {
                // TODO: Log error
                Console.WriteLine(ex.Message);
            }
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
            //TODO: Write update method
            foreach (var t in tweets)
                Console.WriteLine(t.ToString());

            return false;
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

            dataAdapter.SelectCommand = new SqlCommand(pSelectCommandString, sqlConnection);

            // Populate the dataset from the database
            dataAdapter.Fill(results);

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

        public bool UpdateStyle(DataSet styleData)
        {
            foreach (DataTable table in styleData.Tables)
                foreach (DataRow row in table.Rows)
                    Console.WriteLine(row[0].ToString());
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
