using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectServer
{
    class TweetUpdateThread
    {
        BackgroundWorker incrementerBW;

        public void InitializeWorker()
        {
            // TODO: Move this to program.cs
            incrementerBW = new BackgroundWorker();
            //incrementerBW.ProgressChanged += Incrementer_ProgressChanged;
            //incrementerBW.DoWork += Incrementer_DoWork;
            //incrementerBW.RunWorkerCompleted += Incrementer_RunWorkerCompleted;
            incrementerBW.WorkerReportsProgress = true;
            incrementerBW.WorkerSupportsCancellation = true;
        }
    }
}
