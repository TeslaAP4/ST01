using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Acura3._0.FunctionForms
{
    public partial class LoadingMarqueeForm : Form
    {
        private string _Caption = string.Empty;
       // public EventDatabase EV = new EventDatabase();
        public bool StopRefresh;

        public LoadingMarqueeForm()
        {
            InitializeComponent();

            picLogo.Image = Properties.Resources.JabilLogo;
        }

        public void SetCaption(string Caption)
        {
            _Caption = Caption;
        }

        public void RefreshUI()
        {
            Graphics g1 = picLogo.CreateGraphics();
            Graphics g2 = picText1.CreateGraphics();
            Graphics g3 = picText2.CreateGraphics();
            Font FontText1 = new System.Drawing.Font("Arial Black", 24, FontStyle.Bold);
            Font FontText2 = new System.Drawing.Font("Arial Black", 12, FontStyle.Regular);
            g1.DrawImage(picLogo.Image, 0, 0, picLogo.Width, picLogo.Height);
            
            int Count = 0;
            while (!StopRefresh)
            {
                string LoadMessage = _Caption + " is loading";
                for (int i = 0; i <= (Count % 5); i++)
                    LoadMessage += " .";
                g3.Clear(Color.White);
                g3.DrawString(LoadMessage, FontText2, System.Drawing.Brushes.Black, 0, 0);
                g2.DrawString("A C U R A 3.0", FontText1, System.Drawing.Brushes.Black, 160, 10);
                Count++;
                Thread.Sleep(100);
            }
        }
    }
}
