using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolProjectServer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm mainWindow = new MainForm() { StartPosition = FormStartPosition.Manual, Location = new System.Drawing.Point(200, 200) };
            Console.WriteLine("L: " + mainWindow.Left + "| W: " + mainWindow.Width);
            CustomLog.LogWindows.CreateLogWindow(new System.Drawing.Point(mainWindow.Left + mainWindow.Width, mainWindow.Top), new System.Drawing.Size(400, 400), "Event log");

            Application.Run(mainWindow);
        }
    }
}
