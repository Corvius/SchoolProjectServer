using CustomLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolProjectServer
{
    public partial class MainForm : Form
    {
        private const string defaultServerURL = "localhost";
        private const string defaultServerPort = "";
        private SQLConnector sqlDBConnection = new SQLConnector(defaultServerURL, defaultServerPort);
        private Twitter twitter = new Twitter();

        public MainForm()
        {
            InitializeComponent();
            txtServerURL.Text = defaultServerURL;
            txtServerPort.Text = defaultServerPort;

        }
        private void MainForm_Shown(object sender, EventArgs e)
        {
            // Timer setup
            int timerInterval = 10;
            tmrRecheckTweets.Interval = timerInterval * 1000; // 5 minutes
            tmrRecheckTweets.Log(LogExtension.LogLevels.Info, "Timer interval is set to " + timerInterval.ToString() + " seconds");

            tmrRecheckTweets.Start();
            tmrRecheckTweets.Log(LogExtension.LogLevels.Info, "Timer has started!");
        }

        private void tmrRecheckTweets_Tick(object sender, EventArgs e)
        {
            tmrRecheckTweets.Log(LogExtension.LogLevels.Info, "Timer has expired! Fetching tweets!");

            // Start checking on a separate thread
            Task.Factory.StartNew(() => { TweetCheck(); }).Wait();
        }

        private void TweetCheck()
        {
            IEnumerable<string> tweets = twitter.GetTweets("RealDonaldTrump", 10).Result;
            foreach (var t in tweets)
            {
                Console.WriteLine(t + "\n");
            }
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
