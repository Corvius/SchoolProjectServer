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
            }
            catch (Exception ex)
            {
                // TODO: Log error
                Console.WriteLine(ex.Message);
            }
        }

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

        /// <summary>
        /// Connects to the database and executes the given SELECT command.
        /// </summary>
        /// <param name="pSelectCommandString">The SQL SELECT command to be executed on the database</param>
        /// <param name="idFilter">If true, all columns that have the substring 'ID' in their name will be ignored</param>
        /// <returns>Returns a DataHolder list filled with the requested data</returns>
        private List<List<string>> SelectData(string pSelectCommandString, bool idFilter)
        {
            try
            {
                // Create return variable
                List<List<string>> results = new List<List<string>>();

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
                    {
                        dataAdapter.SelectCommand = new SqlCommand(pSelectCommandString, sqlConnection);

                        using (DataSet dataSet = new DataSet())
                        {
                            // Populate the dataset from the database
                            dataAdapter.Fill(dataSet);

                            int tableIndex = 0;
                            for (int rowIndex = 0; rowIndex < dataSet.Tables[tableIndex].Rows.Count; rowIndex++)
                            {
                                results.Add(new List<string>());

                                for (int columnIndex = 0; columnIndex < dataSet.Tables[tableIndex].Columns.Count; columnIndex++)
                                {
                                    string cellValue = dataSet.Tables[tableIndex].Rows[rowIndex][columnIndex].ToString();

                                    results[rowIndex].Add(cellValue);
                                }
                            }
                        }
                    }
                }
                // Final error check
                if (results.Count == 0)
                {
                    throw new Exception("No data returned");
                }

                return results;
            }
            catch (Exception ex)
            {
                // TODO: Log error
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
