using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.ComponentModel;

namespace SchoolProjectServer
{
    public partial class MainForm : Form
    {
        private const int maxTweetsToFetch = 100;
        private int timerIntervalSeconds = 3600;
        private bool isServerRunning = false;

        internal BackgroundWorker tweetUpdaterThread;
        internal BackgroundWorker listenerThread;

        private Twitter twitter = new Twitter();
        private BindingSource bsGridBinder = new BindingSource();
        private TTSConnectionServer connectionServer;

        internal ServerSQLConnector sqlDBConnection = null;
        internal RichTextBoxExt txtConsoleOutput;


        public MainForm()
        {
            InitializeComponent();
            InitializeTweetUpdaterThread();
            InitializeListenerThread();
            txtServerURL.Text = ServerSQLConnector.defaultServerURL;
        }

        # region TweetUpdaterThread
        private void InitializeTweetUpdaterThread()
        {
            tweetUpdaterThread = new BackgroundWorker();
            tweetUpdaterThread.ProgressChanged += tweetUpdaterThread_ProgressChanged;
            tweetUpdaterThread.DoWork += tweetUpdaterThread_DoWork;
            tweetUpdaterThread.RunWorkerCompleted += tweetUpdaterThread_RunWorkerCompleted;
            tweetUpdaterThread.WorkerReportsProgress = true;
            tweetUpdaterThread.WorkerSupportsCancellation = true;
        }

        private void tweetUpdaterThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            txtConsoleOutput.Append(Color.Blue, "TweetUpdater", " thread has ");
            if (e.Cancelled)
                txtConsoleOutput.Append("been ", Color.Red, "interrupted");
            else
            {
                txtConsoleOutput.AppendLine(Color.ForestGreen, "finished");
                txtConsoleOutput.AppendLine("Tweet updates completed!");
            }
        }

        private void tweetUpdaterThread_DoWork(object sender, DoWorkEventArgs e)
        {
            List<Tweet> tweets = twitter.GetTweets("RealDonaldTrump", maxTweetsToFetch).Result;
            sqlDBConnection.AddTweetsToDatabase(tweets);
        }

        private void tweetUpdaterThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            txtConsoleOutput.AppendLine(Color.ForestGreen, "TweetUpdater", " - ", e.UserState.ToString());
        }
        #endregion

        # region ListenerThread
        private void InitializeListenerThread()
        {
            listenerThread = new BackgroundWorker();
            listenerThread.ProgressChanged += listenerThread_ProgressChanged;
            listenerThread.DoWork += listenerThread_DoWork;
            listenerThread.RunWorkerCompleted += listenerThread_RunWorkerCompleted;
            listenerThread.WorkerReportsProgress = true;
            listenerThread.WorkerSupportsCancellation = true;
        }

        private void listenerThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            txtConsoleOutput.Append(Color.Blue, "Listener", " thread has ");
            if (e.Cancelled)
                txtConsoleOutput.Append("been ", Color.Red, "interrupted");
            else
                txtConsoleOutput.AppendLine(Color.ForestGreen, "finished");
        }

        private void listenerThread_DoWork(object sender, DoWorkEventArgs e)
        {
            connectionServer.StartListening();
            while (true)
                if (listenerThread.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
        }

        private void listenerThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            txtConsoleOutput.AppendLine(Color.ForestGreen, "Listener", " - ", e.UserState.ToString());
        }
        #endregion

        private void UpdateServerAddress()
        {
            string selfIP = new WebClient().DownloadString("http://distantworlds.org/tts/whatsmyip.php");
            string remoteIP = new WebClient().DownloadString("http://distantworlds.org/tts/getaddress.php");

            if (selfIP != remoteIP)
                PushNewAddress(selfIP);
        }

        private void PushNewAddress(string newIP)
        {
            string setIpUrl = string.Format("http://distantworlds.org/tts/setaddress.php?address={0}", newIP);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(setIpUrl);
            WebResponse res = request.GetResponse();

            txtConsoleOutput.AppendLine("Server address updated in remote directory to: " + newIP);
        }

        private void tmrRecheckTweets_Tick(object sender, EventArgs e)
        {
            txtConsoleOutput.AppendLine(Color.Blue, "TweetUpdater", " thread has ", Color.ForestGreen, "started");
            tweetUpdaterThread.RunWorkerAsync();
        }

        private void TweetCheck()
        {
            List<Tweet> tweets = twitter.GetTweets("RealDonaldTrump", maxTweetsToFetch).Result;
            sqlDBConnection.AddTweetsToDatabase(tweets);
        }

        public List<TweetStyle> GetTweetStyles()
        {
            return sqlDBConnection.GetTweetStyles();
        }

        #region Event Handlers
        private void btStartServer_Click(object sender, EventArgs e)
        {
            if (isServerRunning)
            {
                txtConsoleOutput.AppendLine("Shutting down server...");
                this.Close();
                return;
            }

            txtConsoleOutput.Enabled = true;

            UpdateServerAddress();
            sqlDBConnection = new ServerSQLConnector();

            txtConsoleOutput.AppendLine("Trying to build connection to ", Color.Blue, txtServerURL.Text);

            // Timer setup
            tmrRecheckTweets.Interval = timerIntervalSeconds * 1000;
            txtConsoleOutput.AppendLine("Timer interval is set to ", Color.Blue, timerIntervalSeconds.ToString(), " seconds");

            tmrRecheckTweets.Start();
            txtConsoleOutput.AppendLine("Timer has started!");

            connectionServer = new TTSConnectionServer(this);

            listenerThread.RunWorkerAsync();
            txtConsoleOutput.AppendLine("Server is now ", Color.LawnGreen, "LISTENING", " on port ", Color.Blue, "7756"); //TODO: Add reference to port here

            btStartServer.Text = "Stop server and exit";
            isServerRunning = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isServerRunning)
                listenerThread.CancelAsync();
        }
    }
    #endregion

    public class RichTextBoxExt : RichTextBox
    {
        public int m_MaxLines { get; }

        public RichTextBoxExt()
        {
            m_MaxLines = 50;
        }

        public void AppendLine(params object[] textParams)
        {
            Append("[" + DateTime.Now.ToString() + "]:  ");
            object[] newParams = new object[textParams.Length + 1];
            Array.Copy(textParams, newParams, textParams.Length);
            newParams[newParams.Length - 1] = "\n";
            Append(newParams);
        }

        public void Append(params object[] textParams)
        {
            SelectionStart = TextLength;
            SelectionLength = 0;

            foreach (object textParam in textParams)
            {
                if (textParam is Color)
                    SelectionColor = (Color)textParam;
                else
                {
                    AppendText(textParam.ToString());
                    SelectionColor = ForeColor;
                }
            }

            if (Lines.Length > m_MaxLines)
            {
                SelectionStart = GetFirstCharIndexFromLine(0);
                SelectionLength = Lines[0].Length + 1;
                SelectedText = System.String.Empty;
            }
        }
    }
}
