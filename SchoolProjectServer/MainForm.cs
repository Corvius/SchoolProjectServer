using CustomLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace SchoolProjectServer
{
    public partial class MainForm : Form
    {
        private const string defaultServerURL = "localhost";
        private const string defaultServerPort = "";
        private SQLConnector sqlDBConnection = new SQLConnector(defaultServerURL, defaultServerPort);
        private Twitter twitter = new Twitter();
        private OrderedDictionary tweetStyles = new OrderedDictionary();

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

            //Start checking on a separate thread
            //Task.Factory.StartNew(() => { TweetCheck(); }).Wait();
        }

        private void TweetCheck()
        {
            var tweets = twitter.GetTweets("RealDonaldTrump", 10).Result; // TODO: Fix Erors!!
            foreach (var t in tweets)
            {
                Console.WriteLine(t.ToString() + "\n");
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

        private void btOpenImage_Click(object sender, EventArgs e)
        {
            const int MaxImageSize = 200 * 1024; // 200 Kb
            OpenFileDialog ofd = new OpenFileDialog();
                        
            ofd.Filter = "Image Files (*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png)|*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png";
            ofd.FilterIndex = 1;
            ofd.Multiselect = false;

            DialogResult dr = DialogResult.None;
            long fileLength = MaxImageSize + 1;
            while (fileLength > MaxImageSize)
            {
                dr = ofd.ShowDialog();
                if (dr == DialogResult.Cancel)
                    break;

                System.IO.FileInfo fi = new System.IO.FileInfo(ofd.FileName);
                fileLength = fi.Length;

                if (fileLength > MaxImageSize)
                    MessageBox.Show("File size must be less than " + (MaxImageSize / 1024).ToString() + " kbytes", "Oversized file!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (dr == DialogResult.OK)
            {
                pbStyleImage.Load(ofd.FileName);
                txtImagePath.Text = ofd.FileName;
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
            if (inputResult == DialogResult.OK)
                if (tweetStyles.Contains(styleName))
                {
                    string message = "There is a style, called " + styleName + ".\nDo you want to overwrite it?";
                    DialogResult overwriteResult = MessageBox.Show(message, "Overwrite style?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (overwriteResult == DialogResult.Yes)
                        tweetStyles[styleName] = new TweetStyle(styleName);
                }
                else
                {
                    tweetStyles.Add(styleName, new TweetStyle(styleName));
                    cbStyles.Items.Add(styleName);
                    cbStyles.SelectedIndex = 0;
                }
        }

        private void btRemoveStyle_Click(object sender, EventArgs e)
        {
            int index = cbStyles.SelectedIndex;
            string styleName = cbStyles.Items[index].ToString();
            if (styleName != string.Empty)
            {
                string message = "Are you sure, you want to remove the style, called " + styleName + "?";
                DialogResult removeResult = MessageBox.Show(message, "Remove style?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (removeResult == DialogResult.Yes && tweetStyles.Contains(styleName))
                    tweetStyles.Remove(styleName);
                cbStyles.Items.Remove(styleName);
                cbStyles.SelectedIndex = -1;
                cbStyles.Text = "";
                // TODO: Make a reset method to clear all components

            }
        }
    }
}
