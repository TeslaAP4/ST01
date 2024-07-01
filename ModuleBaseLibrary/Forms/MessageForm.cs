using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace AcuraLibrary.Forms
{
    public partial class MessageForm : Form
    {
        public bool isInitialize = false;
        public bool isRetry = false;
        public bool isSkip = false;
        public bool isPause = false;
        public bool isMute = false;
        public bool? isShow = null;
        public MessageForm()
        {
            InitializeComponent();
            pBottom.BackColor = AcuraColors.Background;
            
            this.panelMain.Location = new Point(
                this.ClientSize.Width / 2 - this.panelMain.Size.Width / 2,
                this.ClientSize.Height / 2 - this.panelMain.Size.Height / 2); //Zax - rearrange to center
            this.panelMain.Anchor = AnchorStyles.None;
        }

        //private void btnInitialize_Click(object sender, EventArgs e)
        //{
        //    isInitialize = true;
        //    isRetry = false;
        //    isSkip = false;
        //    isPause = false;
        //    isShow = false;
        //    Close();
        //}

        private void btnRetry_Click(object sender, EventArgs e)
        {
            isInitialize = false;
            isRetry = true;
            isSkip = false;
            isPause = false;
            isShow = false;
            Close();
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            isInitialize = false;
            isSkip = true;
            isRetry = false;
            isPause = false;
            isShow = false;
            Close();
        }

        private void uiRefresh_Tick(object sender, EventArgs e)
        {
            if (lbltitle.BackColor == Color.WhiteSmoke)
            {
                lbltitle.BackColor = Color.LightYellow;
            }
            else
            {
                lbltitle.BackColor = Color.WhiteSmoke;
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            isInitialize = false;
            isSkip = false;
            isRetry = false;
            isPause = true;
            isShow = false;
            Close();
        }
        public static event EventHandler MuteRaise;

        public static void NotifyMuteRaise(object sender)
        {
            MuteRaise?.Invoke(sender, null);
        }
        private void btnMute_Click(object sender, EventArgs e)
        {
            isMute = true;
            NotifyMuteRaise(sender);
        }

        private void MessageForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isSkip && !isRetry && !isPause && !isInitialize)
                isShow = null;
        }
    }
}
