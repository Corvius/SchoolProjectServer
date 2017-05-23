using CustomLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Linq;
using System.Data;
using System.Drawing;

namespace SchoolProjectServer
{
    public partial class MainForm : Form
    {
        private const string defaultServerURL = "localhost";
        private const string defaultServerPort = "";
        private const int maxTweetsToFetch = 10;

        private string currentServerURL = defaultServerURL;
        private string currentServerPort = defaultServerPort;
        private bool IsConnectionEstablished = false;

        private DataSet tweetStyleData;
        private SQLConnector sqlDBConnection = null;
        private Twitter twitter = new Twitter();

        public MainForm()
        {
            InitializeComponent();

            txtServerURL.Text = currentServerURL;
            txtServerPort.Text = currentServerPort;

            SetupDataGrid();

            DisableStyleComponents();
        }

        private void SetupDataGrid()
        {
            dgwStyleElements.AutoGenerateColumns = false;
            dgwStyleElements.ColumnCount = 2;

            dgwStyleElements.Columns[0].Name = "original";
            dgwStyleElements.Columns[0].HeaderText = "Original";
            dgwStyleElements.Columns[0].DataPropertyName = "Original";
            dgwStyleElements.Columns[0].Width = dgwStyleElements.Width / 2;

            dgwStyleElements.Columns[1].Name = "replacement";
            dgwStyleElements.Columns[1].HeaderText = "Replacement";
            dgwStyleElements.Columns[1].DataPropertyName = "Replacement";
            dgwStyleElements.Columns[1].Width = dgwStyleElements.Width / 2;
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
            tmrRecheckTweets.Log(LogExtension.LogLevels.Info, "Timer has expired! Trying to fetch tweets!");

            if (IsConnectionEstablished)
                //Start checking on a separate thread
                //Task.Factory.StartNew(() => { TweetCheck(); }).Wait();
                this.Log(LogExtension.LogLevels.Info, "Tweet updates completed!");
            else
                this.Log(LogExtension.LogLevels.Warning, "DB connection is not available, skipping tweet update!");
        }

        private void TweetCheck()
        {
            List<Tweet> tweets = twitter.GetTweets("RealDonaldTrump", maxTweetsToFetch).Result;
            sqlDBConnection.UpdateTweets(tweets);
        }

        private void ResetStylecomponents()
        {
            //TODO: Write a reset function!
            //TweetStyle style = tweetStyles[selectedtStyleID];

            //dgwStyleElements.Enabled = Editable;
            //cbStyles.Enabled = Editable;
            //pbStyleImage.Enabled = Editable;
            //txtImagePath.Enabled = Editable;
            //btLoadImageURL.Enabled = Editable;
            //btClearImage.Enabled = Editable;
            //btAddNewStyle.Enabled = Editable;
            //btRemoveStyle.Enabled = Editable;
            //btReloadStyle.Enabled = Editable;
            //btUpdateServer.Enabled = Editable;
        }

        private void LoadStyleComponents(string styleName)
        {
            DataTable selectedStyle = tweetStyleData.Tables[styleName];

            dgwStyleElements.DataSource = selectedStyle;
            //for (int contentRowIndex = 0; contentRowIndex < selectedStyle.Rows.Count; contentRowIndex++)
            //{
            //    cbStyles.Items.Add(selectedStyle.Rows[contentRowIndex][0].ToString());
            //}
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

        private bool CreateConnection(string serverURL, string serverPort)
        {
            sqlDBConnection = new SQLConnector(serverURL, serverPort);
            string message = serverURL + ((serverPort == string.Empty)? "" : ":" + serverPort);

            bool connected = sqlDBConnection.IsServerConnected();
            if (connected)
                this.Log(LogExtension.LogLevels.Info, "SQL connection is confirmed to " + message);
            else
                this.Log(LogExtension.LogLevels.Error, "Cannot establish SQL connection to " + message + "!");

            return connected;
        }

        #region Event Handlers
        private void btReloadStyle_Click(object sender, EventArgs e)
        {
            //TODO: WHAT THE HELL IS THIS!?
            //List<DataHolder> cellDataQuery;
            //cellDataQuery = sqlDBConnection.GetElementData("What");
            //foreach (DataHolder cellData in cellDataQuery)
            //{
            //    this.Log(LogExtension.LogLevels.Info, cellData.original + " - " + cellData.replacement);
            //}
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
            StylePopupForm inputBox = new StylePopupForm();
            DialogResult inputResult = inputBox.ShowDialog();
            string styleName = inputBox.StyleName;
            //if (inputResult == DialogResult.OK)
            //    if (tweetStyles.Contains(styleName))
            //    {
            //        string message = "There is a style, called " + styleName + ".\nDo you want to overwrite it?";
            //        DialogResult overwriteResult = MessageBox.Show(message, "Overwrite style?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //        if (overwriteResult == DialogResult.Yes)
            //            tweetStyles[styleName] = new TweetStyle(styleName);
            //    }
            //    else
            //    {
            //        tweetStyles.Add(styleName, new TweetStyle(styleName));
            //        cbStyles.Items.Add(styleName);
            //        cbStyles.SelectedIndex = 0;
            //    }
        }

        private void btRemoveStyle_Click(object sender, EventArgs e)
        {
            string styleName = cbStyles.Items[cbStyles.SelectedIndex].ToString();
            //if (styleName != string.Empty)
            //{
            //    string message = "Are you sure, you want to remove the style, called " + styleName + "?";
            //    DialogResult removeResult = MessageBox.Show(message, "Remove style?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (removeResult == DialogResult.Yes && tweetStyles.Contains(styleName))
            //        tweetStyles.Remove(styleName);
            //    cbStyles.Items.Remove(styleName);
            //    cbStyles.SelectedIndex = -1;
            //    cbStyles.Text = "";
            //    // TODO: Make a reset method to clear all components

            //}
        }

        private void btConnectToDatabase_Click(object sender, EventArgs e)
        {
            string serverURL = (txtServerURL.Text == string.Empty)? defaultServerURL : txtServerURL.Text;
            string serverPort = (txtServerPort.Text == string.Empty) ? defaultServerPort : txtServerPort.Text;

            if (!IsConnectionEstablished)
                if (CreateConnection(serverURL, serverPort))
                {
                    tweetStyleData = sqlDBConnection.GetTweetStyles();

                    DataTable styleNames = tweetStyleData.Tables["StyleNames"];

                    cbStyles.Items.Clear();

                    for (int contentRowIndex = 0; contentRowIndex < styleNames.Rows.Count; contentRowIndex++)
                    {
                        cbStyles.Items.Add(styleNames.Rows[contentRowIndex][0].ToString());
                    }

                    cbStyles.SelectedIndex = 0;

                    btConnectToDatabase.Text = "Connected!";
                    IsConnectionEstablished = true;
                    EnableStyleComponents();
                }
        }

        private void btResetConnectionFields_Click(object sender, EventArgs e)
        {
            IsConnectionEstablished = false;
            sqlDBConnection = null;
            btConnectToDatabase.Text = "Create DB connection!";
            DisableStyleComponents();

            this.Log(LogExtension.LogLevels.Info, "Reseting connection");
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
            Console.WriteLine("CB changed!");
        }
    }
}
#endregion

// Welcome to the Graveyard!
// You may find both trash and treasure in this section for discarded methods. Enjoy!
/*

        private void btOpenImage_Click(object sender, EventArgs e)
        {
            const int MaxImageSize = 200 * 1024; // 200 Kb
            OpenFileDialog ofd = new OpenFileDialog();
                        
            ofd.Filter = "Image Files (*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png)|*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png";
            ofd.FilterIndex = 1;
            ofd.Multiselect = false;

            DialogResult dr = DialogResult.None;
            bool fileExceedsLimit = false;
            do
            {
                dr = ofd.ShowDialog();
                if (dr == DialogResult.Cancel)
                    break;

                System.IO.FileInfo fi = new System.IO.FileInfo(ofd.FileName);
                fileExceedsLimit = fi.Length > MaxImageSize;

                if (fileExceedsLimit)
                    MessageBox.Show("File size must be less than " + (MaxImageSize / 1024).ToString() + " kbytes", "Oversized file!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            
            } while (fileExceedsLimit);

            if (dr == DialogResult.OK)
            {
                pbStyleImage.Load(ofd.FileName);
                txtImagePath.Text = ofd.FileName;
            }
        }


*/
