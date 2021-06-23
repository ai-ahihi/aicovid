using AudioAnalytic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Application.SetHighDpiMode(HighDpiMode.SystemAware);
            // Application.EnableVisualStyles();
            // Application.SetCompatibleTextRenderingDefault(false);
            //// Application.Run(new Form1());


            AnalyticData pt = new AnalyticData("public_train/metadata_train_challenge.csv");
            var tmp = pt.AudioDetails;
        }
    }
}
