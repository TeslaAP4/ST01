using JabilSDK.UserControlLib;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace AcuraLibrary.Forms
{
    [ToolboxItem(true)]
    public class FlowChartMessage : FlowChart
    {
        private MessageForm msgForm = new MessageForm();
        //public bool isPauseInitialize = false;
        public bool isPausePress = false;
        public bool isPauseRetry = false;
        public bool isPauseSkip = false;

        //[Browsable(false)]
        //[Category("#JabilSDK")]
        //[Description("")]
        //public new FlowChart NEXT
        //{
        //    get;
        //    set;
        //}

        [Browsable(false)]
        [Category("#JabilSDK")]
        [Description("")]
        public new FlowChart CASE2
        {
            get;
            set;
        }

        [Browsable(false)]
        [Category("#JabilSDK")]
        [Description("")]
        public new FlowChart CASE3
        {
            get;
            set;
        }



        [Browsable(true)]
        [Category("#JabilSDKMessage")]
        [Description("Title **NO Init--> NO CASE2 Retry-->CASE1 NO Skip-->NO NEXT")]
        public string Title
        {
            get; set;
        }

        [Browsable(true)]
        [Category("#JabilSDKMessage")]
        [Description("Content **NO Init-->NO CASE2 Retry-->CASE1 NO Skip-->NO NEXT")]
        public string Content
        {
            get; set;
        }

        [Browsable(true)]
        [Category("#JabilSDKMessage")]
        [Description("Hide Mute Button")]
        public bool HideButtonMute
        {
            get; set;
        }
        [Browsable(true)]
        [Category("#JabilSDKMessage")]
        [Description("Hide Pause Button")]
        public bool HideButtonPause
        {
            get; set;
        }

        [Browsable(true)]
        [Category("#JabilSDKMessage")]
        [Description("Hide Retry Button")]
        public bool HideButtonRetry
        {
            get; set;
        }

        [Browsable(true)]
        [Category("#JabilSDKMessage")]
        [Description("Hide Skip Button")]
        public bool HideButtonSkip
        {
            get; set;
        }

        //[Browsable(true)]
        //[Category("#JabilSDKMessage")]
        //[Description("Hide Initialize Button")]
        //public bool HideButtonInitialize
        //{
        //    get; set;
        //}

        [Browsable(true)]
        [Category("#JabilSDKMessage")]
        [Description("Text For Retry Button")]
        public string ButtonRetryText
        {
            get; set;
        } = "Retry";

        [Browsable(true)]
        [Category("#JabilSDKMessage")]
        [Description("Text For Skip Button")]
        public string ButtonSkipText
        {
            get; set;
        } = "Skip";

        //[Browsable(true)]
        //[Category("#JabilSDKMessage")]
        //[Description("Text For Initialize Button")]
        //public string ButtonInitializeText
        //{
        //    get; set;
        //} = "Initialize";

        public FlowChartMessage()
        {
            FlowRun += FlowChartMessage_FlowRun;
        }

        public void MessageReset()
        {
            if (msgForm.isShow == true)
                msgForm.Close();
            isPausePress = false;
            isPauseRetry = false;
            isPauseSkip = false;
            //isPauseInitialize = false;
            msgForm.isShow = null;
            msgForm = new MessageForm();
        }

        private JabilSDK.Enums.FCResultType FlowChartMessage_FlowRun(object sender, EventArgs e)
        {
            if (msgForm.isShow == true)
                return JabilSDK.Enums.FCResultType.IDLE;
            else if (msgForm.isShow == false)
            {
                if (msgForm.isRetry)
                {
                    MessageReset();
                    isPausePress = false;
                    isPauseRetry = true;
                    isPauseSkip = false;
                    //isPauseInitialize = false;
                    //msgForm = new MessageForm();
                    NotifyResetTimerRaise(sender);
                    return JabilSDK.Enums.FCResultType.CASE1;
                }
                else if (msgForm.isSkip)
                {
                    MessageReset();
                    isPausePress = false;
                    isPauseRetry = false;
                    isPauseSkip = true;
                    //isPauseInitialize = false;
                    //msgForm = new MessageForm();
                    NotifyResetTimerRaise(sender);
                    return JabilSDK.Enums.FCResultType.NEXT;
                }
                //else if (msgForm.isInitialize)
                //{
                //    MessageReset();
                //    isPausePress = false;
                //    isPauseInitialize = true;
                //    isPauseRetry = false;
                //    isPauseSkip = false;
                //    //msgForm = new MessageForm();
                //    return JabilSDK.Enums.FCResultType.CASE2;
                //}
                else if (msgForm.isPause)
                {
                    MessageReset();
                    isPausePress = true;
                    isPauseRetry = false;
                    isPauseSkip = false;
                    //isPauseInitialize = false;
                    NotifyPauseRaise(this);
                    //msgForm = new MessageForm();
                    return JabilSDK.Enums.FCResultType.IDLE;
                }
            }
            //msgForm = new MessageForm();
            if (NEXT==null)
                HideButtonSkip = true;
            MessageReset();
            msgForm.lbltitle.Text = Title;
            msgForm.lblMessage.Text = Content;
            msgForm.btnMute.Visible = !HideButtonMute;
            msgForm.btnPause.Visible = !HideButtonPause;
            msgForm.btnRetry.Visible = !HideButtonRetry;
            msgForm.btnRetry.Text = ButtonRetryText;
            msgForm.btnSkip.Visible = !HideButtonSkip;
            msgForm.btnSkip.Text = ButtonSkipText;
            //msgForm.btnInitialize.Visible = !HideButtonInitialize;
            //msgForm.btnInitialize.Text = ButtonInitializeText;
            //msgForm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            msgForm.isShow = true;
            msgForm.TopMost = true;
            
            NotifyMessageFormRaise(sender);
            new Thread(delegate ()
            {
                Application.Run(msgForm);
                //msgForm.ShowDialog();
            }).Start();
            return JabilSDK.Enums.FCResultType.IDLE;

        }

        public static event EventHandler ResetTimerRaise;

        public static void NotifyResetTimerRaise(object sender)
        {
            ResetTimerRaise?.Invoke(sender, null);
        }

        public static event EventHandler PauseRaise;

        public static void NotifyPauseRaise(object sender)
        {
            PauseRaise?.Invoke(sender, null);
        }

        public static event EventHandler MessageFormRaise;

        public static void NotifyMessageFormRaise(object sender)
        {
            MessageFormRaise?.Invoke(sender, null);
        }
    }
}
