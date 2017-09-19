using CustomLog;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Xml;
using System.Text.RegularExpressions;

namespace SchoolProjectServer
{
    public partial class MainForm : Form
    {
        private const int maxTweetsToFetch = 100;
        private int timerIntervalSeconds = 20;

        private bool IsConnectionEstablished = false;
        private Thread listenerThread;

        private DataSet tweetStyleDataX;
        private Twitter twitter = new Twitter();
        private BindingSource bsGridBinder = new BindingSource();
        private TTSConnectionServer connectionServer;

        internal SQLConnector sqlDBConnection = null;

        public MainForm()
        {
            InitializeComponent();

            txtServerURL.Text = SQLConnector.defaultServerURL;
            txtServerPort.Text = SQLConnector.defaultServerPort;

            SetupDataGrid();

            DisableStyleComponents();

            UpdateServerAddress();
        }

        private void SetupDataGrid()
        {
            int scrollBarOffset = SystemInformation.VerticalScrollBarWidth - 5;
            dgwStyleElements.AutoGenerateColumns = false;
            dgwStyleElements.ColumnCount = 2;
            dgwStyleElements.ScrollBars = ScrollBars.Vertical;

            dgwStyleElements.Columns[0].Name = "original";
            dgwStyleElements.Columns[0].HeaderText = "Original";
            dgwStyleElements.Columns[0].DataPropertyName = "Original";
            dgwStyleElements.Columns[0].Width = (dgwStyleElements.Width / 2) - scrollBarOffset;

            dgwStyleElements.Columns[1].Name = "replacement";
            dgwStyleElements.Columns[1].HeaderText = "Replacement";
            dgwStyleElements.Columns[1].DataPropertyName = "Replacement";
            dgwStyleElements.Columns[1].Width = (dgwStyleElements.Width / 2) + scrollBarOffset;

        }

        private void UpdateServerAddress()
        {
            string selfIP = new WebClient().DownloadString("http://distantworlds.org/tts/whatsmyip.php");
            string remoteIP = new WebClient().DownloadString("http://distantworlds.org/tts/getaddress.php");

            if (selfIP != remoteIP)
            {
                string setIpUrl = string.Format("http://distantworlds.org/tts/setaddress.php?address={0}", selfIP);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(setIpUrl);
                WebResponse res = request.GetResponse();
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            // Timer setup
            tmrRecheckTweets.Interval = timerIntervalSeconds * 1000;
            tmrRecheckTweets.Log(LogExtension.LogLevels.Info, "Timer interval is set to " + timerIntervalSeconds.ToString() + " seconds");

            tmrRecheckTweets.Start();
            tmrRecheckTweets.Log(LogExtension.LogLevels.Info, "Timer has started!");

            connectionServer = new TTSConnectionServer(this);

            listenerThread = new Thread(connectionServer.StartListening);
            listenerThread.Start();

        }

        private void tmrRecheckTweets_Tick(object sender, EventArgs e)
        {
            if (IsConnectionEstablished)
            {
                this.Log(LogExtension.LogLevels.Info, "Trying to fetch and update tweets!");

                //Start checking on a separate thread
                Task.Factory.StartNew(() => { TweetCheck(); }).Wait();
                this.Log(LogExtension.LogLevels.Info, "Tweet updates completed!");
            }
            else
                this.Log(LogExtension.LogLevels.Warning, "DB connection is not available, skipping automatic tweet update!");
        }

        private void TweetCheck()
        {
            List<Tweet> tweets = twitter.GetTweets("RealDonaldTrump", maxTweetsToFetch).Result;
            //sqlDBConnection.UpdateTweets(tweets);

        }

        private void LoadStyleComponents(string styleName)
        {
            tweetStyleDataX = null;
            List<TweetStyle> tweetStyles = sqlDBConnection.GetTweetStyles();
            connectionServer.UpdateTweetStyles(tweetStyles);

            foreach (TweetStyle tweetStyle in tweetStyles)
            {
                if (tweetStyle.styleName == styleName)
                {
                    if (tweetStyle.styleImageURL != "")
                    {
                        pbStyleImage.Load(tweetStyle.styleImageURL);
                        txtImagePath.Text = tweetStyle.styleImageURL;
                    }
                    else
                    {
                        pbStyleImage.Image = null;
                        txtImagePath.Text = "";
                    }
                    break;
                }
            }

            bsGridBinder.DataSource = sqlDBConnection.GetTweetStyleProperties(styleName);
            dgwStyleElements.DataSource = bsGridBinder;
        }

        private void EnableStyleComponents()
        {
            SetStyleEditabiliy(true);
            tmrRecheckTweets.Log(LogExtension.LogLevels.Info, "Style editing enabled");
        }

        private void DisableStyleComponents()
        {
            SetStyleEditabiliy(false);
            tmrRecheckTweets.Log(LogExtension.LogLevels.Info, "Style editing disabled");
        }

        private void SetStyleEditabiliy(bool Editable)
        {
            dgwStyleElements.Enabled = Editable;
            if (dgwStyleElements.Enabled)
            {
                //TODO: Make this more visual
                dgwStyleElements.DefaultCellStyle.BackColor = SystemColors.Window;
                dgwStyleElements.DefaultCellStyle.ForeColor = SystemColors.ControlText;
                dgwStyleElements.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Window;
                dgwStyleElements.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
            }
            else
            {
                dgwStyleElements.DefaultCellStyle.BackColor = SystemColors.Control;
                dgwStyleElements.DefaultCellStyle.ForeColor = SystemColors.GrayText;
                dgwStyleElements.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
                dgwStyleElements.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.GrayText;

            }

            cbStyles.Enabled = Editable;
            pbStyleImage.Enabled = Editable;
            txtImagePath.Enabled = Editable;
            btLoadImageURL.Enabled = Editable;
            btClearImage.Enabled = Editable;
            btAddNewStyle.Enabled = Editable;
            btRemoveStyle.Enabled = Editable;
            btReloadStyle.Enabled = Editable;
            btUpdateServer.Enabled = Editable;
        }

        private void RefreshStyleList()
        {
            List<TweetStyle> tweetStyles = sqlDBConnection.GetTweetStyles();

            cbStyles.DataSource = null;
            List<string> styleNames = tweetStyles
                .Select(style => style.styleName).ToList();
            cbStyles.DataSource = styleNames;
        }

        internal List<StyleProperty> GetStyleProperties(string styleName)
        {
            return sqlDBConnection.GetTweetStyleProperties(styleName);
        }

        #region Event Handlers
        private void btReloadStyle_Click(object sender, EventArgs e)
        {
            string styleName = cbStyles.GetItemText(cbStyles.SelectedItem);
            if (styleName != string.Empty)
                LoadStyleComponents(styleName);
        }

        private void btOpenImage_Click(object sender, EventArgs e)
        {
            try
            {
                pbStyleImage.Load(txtImagePath.Text);
            }
            catch (InvalidOperationException)
            {
                string message = (txtImagePath.Text == string.Empty) ? "<No URL specified>" : txtImagePath.Text;
                this.Log(LogExtension.LogLevels.Error, "Unable to open image from " + message + "!");
            }
        }

        private void btRemoveImage_Click(object sender, EventArgs e)
        {
            pbStyleImage.Image = null;
            txtImagePath.Text = "";
        }

        private void btAddNewStyle_Click(object sender, EventArgs e)
        {
            string styleName = "";
            while (true)
            {
                StylePopupForm inputBox = new StylePopupForm();
                DialogResult inputResult = inputBox.ShowDialog();
                styleName = inputBox.StyleName;

                string nameQuery = tweetStyleDataX.Tables["StyleNames"]
                    .Rows
                    .Cast<DataRow>()
                    .Where(row => row["StyleName"].ToString() == styleName)
                    .Select(row => row["StyleName"].ToString()).FirstOrDefault();

                if (nameQuery != null)
                {
                    MessageBox.Show("There is already a style called " + styleName);
                    continue;
                }

                if (inputResult == DialogResult.Cancel)
                    return;

                if (inputResult == DialogResult.OK)
                    break;
            }

            sqlDBConnection.AddNewStyle(styleName);
            RefreshStyleList();
            cbStyles.SelectedIndex = 0;
        }

        private void btRemoveStyle_Click(object sender, EventArgs e)
        {
            string styleName = cbStyles.Items[cbStyles.SelectedIndex].ToString();
            if (styleName != string.Empty)
            {
                string message = "Are you sure you want to remove the style '" + styleName + "'?";
                DialogResult removeResult = MessageBox.Show(message, "Remove style?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (removeResult == DialogResult.Yes)
                {
                    sqlDBConnection.RemoveStyle(styleName);
                    RefreshStyleList();
                    if (cbStyles.Items.Count > 0)
                        cbStyles.SelectedIndex = 0;
                }
            }
        }

        private void btConnectToDatabase_Click(object sender, EventArgs e)
        {
            string urlMessage = (txtServerPort.Text != string.Empty) ? txtServerURL.Text + ":" + txtServerPort.Text : txtServerURL.Text;
            this.Log(LogExtension.LogLevels.Info, "Trying to build connection to " + urlMessage);

            sqlDBConnection = new SQLConnector();

            RefreshStyleList();
            cbStyles.SelectedIndex = 0;

            btConnectToDatabase.Text = "Connected!";
            IsConnectionEstablished = true;
            EnableStyleComponents();
        }

        private void btResetConnectionFields_Click(object sender, EventArgs e)
        {
            IsConnectionEstablished = false;
            sqlDBConnection = null;
            btConnectToDatabase.Text = "Create DB connection";
            DisableStyleComponents();

            this.Log(LogExtension.LogLevels.Info, "Connection closed");
        }

        private void btClearImage_Click(object sender, EventArgs e)
        {
            pbStyleImage.Image = null;
        }

        private void cbStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            string styleName = cbStyles.GetItemText(cbStyles.SelectedItem);
            if (styleName != string.Empty)
                LoadStyleComponents(styleName);
        }

        private void btUpdateServer_Click(object sender, EventArgs e)
        {
            sqlDBConnection.UpdateStyle((DataTable)bsGridBinder.DataSource);
            string styleName = cbStyles.GetItemText(cbStyles.SelectedItem);
            LoadStyleComponents(styleName);
        }

        private void dgwStyleElements_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            bsGridBinder.EndEdit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            listenerThread.Interrupt();
        }
    }
}
#endregion