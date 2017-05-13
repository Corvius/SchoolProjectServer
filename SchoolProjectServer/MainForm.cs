using CustomLog;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SchoolProjectServer
{
    public partial class MainForm : Form
    {
        private const string defaultServerURL = "localhost";
        private const string defaultServerPort = "";
        private SQLConnector sqlDBConnection = new SQLConnector(defaultServerURL, defaultServerPort);

        public MainForm()
        {
            InitializeComponent();
            txtServerURL.Text = defaultServerURL;
            txtServerPort.Text = defaultServerPort;
        }

        private void tlpMainLayout_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btReloadStyle_Click(object sender, EventArgs e)
        {

            List<DataHolder> cellDataQuery;
            cellDataQuery = sqlDBConnection.GetElementData("What");
            foreach (DataHolder cellData in cellDataQuery)
            {
                this.Log(LogExtension.LogLevels.Info, cellData.original + " - " + cellData.replacement);
            }
        }
    }
}
