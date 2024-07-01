using DPFP;
using DPFP.Processing;
using DPFP.Verification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Acura3._0.FunctionForms
{
    delegate void Function();   // a simple delegate for marshalling calls from event handlers to the GUI thread

    public partial class FingerprintCaptureForm : Form
    {
        Template tmp;

        public delegate void OnTemplateEventHandler(int UserIndex);
        public event OnTemplateEventHandler OnTemplate;

        private bool IsVerifcation = false;
        private Verification Verificator;
        private List<clsTemplate> lstTemplate = new List<clsTemplate>();


        private bool IsEnroll = false;
        public Enrollment Enroller;
        public string RegisterName = string.Empty;

        protected class clsTemplate
        {
            public int UserIndex = -1;
            public Template template = new Template();
        }

        public FingerprintCaptureForm()
        {
            InitializeComponent();
            Init();
            Verificator = new Verification();
            Enroller = new Enrollment();
        }

        protected virtual void Init()
        {
            try
            {
                Capturer = new DPFP.Capture.Capture();              // Create a capture operation.  
            }
            catch
            {
                MessageBox.Show("Can't initiate fingerprint capture operation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        public void ClearTemplate()
        {
            lstTemplate.Clear();
        }
        public void StartVerification()
        {
            IsEnroll = false;
            IsVerifcation = true;
            Start();
        }

        public void StopVerification()
        {
            IsVerifcation = false;
            Stop();
        }

        protected void Start()
        {
            if (null != Capturer)
            {
                try
                {
                    Capturer.StartCapture();
                }
                catch { }
            }
        }

        protected void Stop()
        {
            if (null != Capturer)
            {
                try
                {
                    Capturer.StopCapture();
                }
                catch { }
            }
        }

        #region Form Event Handlers:

        private void FingerprintCaptureForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                Start();
                SetStatus("Please press your finger!");
                Enroller.Clear();
                IsEnroll = true;
            }
            else
            {
                Stop();
                IsEnroll = false;
            }
        }

        private void FingerprintCaptureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = false;
            e.Cancel = true;
        }

        private void CaptureForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Stop();
        }
        #endregion

        #region EventHandler Members:
        #endregion

        protected Bitmap ConvertSampleToBitmap(DPFP.Sample Sample)
        {
            DPFP.Capture.SampleConversion Convertor = new DPFP.Capture.SampleConversion();  // Create a sample convertor.
            Bitmap bitmap = null;                                                           // TODO: the size doesn't matter
            Convertor.ConvertToPicture(Sample, ref bitmap);                                 // TODO: return bitmap as a result
            return bitmap;
        }

        protected DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
        {
            DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction();  // Create a feature extractor
            DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
            DPFP.FeatureSet features = new DPFP.FeatureSet();
            Extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref features);            // TODO: return features as a result?
            if (feedback == DPFP.Capture.CaptureFeedback.Good)
                return features;
            else
                return null;
        }

        protected void SetStatus(string status)
        {
            this.Invoke(new Function(delegate ()
            {
                StatusLine.Text = status;
            }));
        }

        private void DrawPicture(Bitmap bitmap)
        {
            this.Invoke(new Function(delegate ()
            {
                Picture.Image = new Bitmap(bitmap, Picture.Size);   // fit the image into the picture box
            }));
        }

        public static void RefreshDifferentThreadUI(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                Action refreshUI = new Action(action);
                control.Invoke(refreshUI);
            }
            else
            {
                action.Invoke();
            }
        }
        private DPFP.Capture.Capture Capturer;
    }
}