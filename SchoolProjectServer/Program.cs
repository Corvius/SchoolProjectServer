﻿using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SchoolProjectServer
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm mainWindow = new MainForm() { StartPosition = FormStartPosition.Manual, Location = new System.Drawing.Point(200, 200) };
            Application.Run(mainWindow);
        }
    }
}
