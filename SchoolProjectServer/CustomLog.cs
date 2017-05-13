using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CustomLog
{
    /// <summary>
    /// Makes logging possible to any control within a module that uses this one
    /// </summary>
    public static class LogExtension
    {
        public enum LogLevels { Trace, Debug, Info, Warning, Error }

        //public static void RichLog(this Control source, LogLevels level, object[] richMessage)
        //{
        //    MatchCollection parsedMessage = Regex.Matches(message, @"(\S+)", RegexOptions.IgnoreCase);
        //    List<object> messageParts = new List<object>();

        //    foreach (var msgPart in parsedMessage)
        //    {
        //        if (Regex.IsMatch(msgPart.ToString(), "#")) messageParts.Add(Color.Cyan);
        //        else if (Regex.IsMatch(msgPart.ToString(), "@")) messageParts.Add(Color.Blue);
        //        else if (Regex.IsMatch(msgPart.ToString(), "%")) messageParts.Add(Color.IndianRed);
        //        else messageParts.Add(Color.Black);
        //        messageParts.Add(msgPart.ToString());
        //    }
        //}

        [System.Diagnostics.Conditional("DEBUG")]
        public static void Log(this object source, LogLevels level, string message)
        {
            // TODO: Add log logic here!
            LogWindows.SendToWindow(0, System.DateTime.Now.ToString() + " [ " + level.ToString() + " ] : " + message);
        }
    }

    /// <summary>
    /// Creates and manages log windows 
    /// </summary>
    public static class LogWindows
    {
        private static List<Form> logWindowList = new List<Form>();

        [System.Diagnostics.Conditional("DEBUG")]
        public static void CreateLogWindow(Point pStartPosition, Size pWindowSize, string pCaption)
        {
            Form logForm = new Form()
            {
                StartPosition = FormStartPosition.Manual,
                AutoScaleDimensions = new SizeF(6F, 13F),
                AutoScaleMode = AutoScaleMode.Font,
                ClientSize = pWindowSize,
                Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238))),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false,
                Text = pCaption,
                Tag = pStartPosition,
            };

            logWindowList.Add(logForm);

            logForm.Controls.Add(new RichTextBoxExt()
            {
                BackColor = SystemColors.ControlDark,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238))),
                Location = new Point(3, 93),
                TabIndex = 1,
                ReadOnly = true,
                Text = "",
                Name = "RTB",
                Padding = new Padding(4),
            });

            logForm.Load += LogWindow_Load;
            logForm.FormClosing += LogWindow_FormClosing;
            logForm.Show();
        }

        [System.Diagnostics.Conditional("DEBUG")]
        public static void SendToWindow(int windowID, string message)
        {
            if (windowID < logWindowList.Count)
            { 
                Form window = logWindowList[windowID];
                if (window != null )
                    ((RichTextBoxExt)window.Controls[0]).AppendText(message + "\n");
            }
        }

        private static void LogWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = e.CloseReason == CloseReason.UserClosing;

            //e.Cancel =
            //    e.CloseReason != CloseReason.FormOwnerClosing
            //    &&
            //    MessageBox.Show
            //        ("Do you really want to close the debug window?",
            //        "Closing debugform",
            //        MessageBoxButtons.YesNo,
            //        MessageBoxIcon.Question,
            //        MessageBoxDefaultButton.Button2) == DialogResult.No;
        }

        private static void LogWindow_Load(object sender, System.EventArgs e)
        {
            ((Form)sender).Location = (Point)((Form)sender).Tag;
            ((Form)sender).Tag = null;
        }
    }

    public class RichTextBoxExt : RichTextBox
    {
        public int m_MaxLines { get; }

        public RichTextBoxExt()
        {
            m_MaxLines = 50;
        }

        public void AppendText(string p_text, Color p_color)
        {
            SelectionStart = TextLength;
            SelectionLength = 0;

            SelectionColor = p_color;
            AppendText(p_text);
            SelectionColor = ForeColor;
           
            if (Lines.Length > m_MaxLines)
            {
                SelectionStart = GetFirstCharIndexFromLine(0);
                SelectionLength = Lines[0].Length + 1;
                SelectedText = System.String.Empty;
            }
        }
    }
}

