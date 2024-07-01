using Acura3._0.FunctionForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Acura3._0
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Boolean bCreatedNew;
            Mutex m = new Mutex(false, Application.ProductName, out bCreatedNew);
            if (!bCreatedNew)
            {
                MessageBox.Show("Program has been run", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MiddleLayer.LoadingMarqueeF = new LoadingMarqueeForm();
            MiddleLayer.LoadingMarqueeF.Show();
            Thread LoadingMarqueeT = new Thread(MiddleLayer.LoadingMarqueeF.RefreshUI);
            LoadingMarqueeT.Start();
            MiddleLayer.InitialProject(); //Initial Project
            MiddleLayer.LoadingMarqueeF.StopRefresh = true;
            LoadingMarqueeT.Join();
            MiddleLayer.LoadingMarqueeF.Close();

            Application.Run(MiddleLayer.MainF); //Start Project
            MiddleLayer.DisposeProject(); //Dispose Projec
            Environment.Exit(0); //Teong
        } 
    }
}
