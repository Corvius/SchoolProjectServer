using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SchoolProjectGui
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Form Setup
            MainForm mainWindow = new MainForm() { StartPosition = FormStartPosition.Manual, Location = new System.Drawing.Point(200, 200) };
            CustomLog.LogWindows.CreateLogWindow("Events", "Event log", new System.Drawing.Point(mainWindow.Left + mainWindow.Width, mainWindow.Top), new System.Drawing.Size(400, 400));

            Application.Run(mainWindow);
        }
    }
}
