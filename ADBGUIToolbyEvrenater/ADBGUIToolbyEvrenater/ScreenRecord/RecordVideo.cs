using ADBGUIToolbyEvrenater.ProcessCreating;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADBGUIToolbyEvrenater.ScreenRecord
{
    class RecordVideo
    {
        Form form1;
        TableLayoutPanel tableLayoutPanel;
        Label resultLabel;
        Button screenRecordButton, stopButton;
        BackgroundWorker backgroundWorker1, backgroundWorker2;
        PictureBox pictureBox1;

        string date;

        public RecordVideo()
        {
            form1 = new();
            tableLayoutPanel = new();
            resultLabel = new();
            screenRecordButton = new();
            stopButton = new();
            backgroundWorker1 = new();
            backgroundWorker2 = new();
            pictureBox1 = new();
            form1.SuspendLayout();

            form1.Text = "Screen Record";
            resultLabel.Text = "Screen Record Location:\r\nInternal Storage - screenrecord<time>.mp4";
            screenRecordButton.Text = "Start Record";
            stopButton.Text = "Stop";

            resultLabel.AutoSize = true;
            screenRecordButton.AutoSize = true;
            stopButton.AutoSize = true;
            stopButton.Enabled = false;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker2.WorkerSupportsCancellation = true;

            screenRecordButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            stopButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel.Width = form1.Width - 20;
            tableLayoutPanel.Height = form1.Height - 20;
            pictureBox1.Width = 100;
            pictureBox1.Height = 200;

            resultLabel.AutoSizeChanged += ResultLabel_AutoSizeChanged;
            resultLabel.SizeChanged += ResultLabel_SizeChanged;
            screenRecordButton.Click += screenshotButton_Click;
            stopButton.Click += CancelButton_Click;
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
            backgroundWorker2.DoWork += BackgroundWorker2_DoWork;
            backgroundWorker2.RunWorkerCompleted += BackgroundWorker2_RunWorkerCompleted;
            form1.Load += Form1_Load;
            form1.SizeChanged += Form1_SizeChanged;

            tableLayoutPanel.Controls.AddRange(new Control[] { resultLabel, screenRecordButton, stopButton,
                                                    pictureBox1});
            form1.Controls.Add(tableLayoutPanel);
            form1.ResumeLayout();
            form1.ShowDialog();

        }

        private void Record()
        {
            screenRecordButton.Text = "Recording...";
            screenRecordButton.Enabled = false;
            stopButton.Enabled = true;

            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.

                backgroundWorker1.RunWorkerAsync();
            }
        }

        #region
        private void BackgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (e.Cancelled == true)
            {
                resultLabel.Text = "Stopped!";
            }
            else if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!

                if (backgroundWorker2.IsBusy != true)
                {
                    // Start the asynchronous operation.

                    backgroundWorker2.RunWorkerAsync();
                }

            }
        }
        private void BackgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.

                ProcessCreate.Command2("adb kill-server");

            }
        }        
        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                resultLabel.Text = "Aborted!";
            }
            else if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!


                string[] lines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                                                                StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                  /*  if (((line.IndexOf('a') == 0) && (line.IndexOf('d') == 1)
                        && (line.IndexOf('b') == 2) && (line.IndexOf(':') == 3))
                        || (((line.IndexOf('E') == 0) || (line.IndexOf('e') == 0)) && (line.IndexOf('r') == 1)
                        && (line.IndexOf('o') == 3) && (line.IndexOf(':') == 5))
                        || line.Contains("no devices")) ;
                    {
                        resultLabel.Text = line;
                    }
                    else
                    {
                    }*/

                    if(lines.Length == 0)
                    {
                        // Success
                    }
                    else if(line.IndexOf('*') != 0)
                    {
                        // Failed
                        resultLabel.Text = line;
                        stopButton.Enabled = false;
                        screenRecordButton.Enabled = true;
                        screenRecordButton.Text = "Start Record";

                    }

                }
            }
        }
        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.


                DateTime dateTime = new DateTime();
                dateTime = DateTime.Now;
                date = dateTime.Hour + "-" + dateTime.Minute + "-" + dateTime.Second + "-" + dateTime.Millisecond;

                ProcessCreate.Command("adb shell screenrecord /sdcard/screenrecord" + date + ".mp4");

            }
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
            stopButton.Enabled = false;


            DialogResult dialogResult = DialogResult.Yes;
            dialogResult = MessageBox.Show(form1, "You may need to reconnect your device over Wi-Fi " +
                                "or physically reconnect over USB after stopping.",
                                "Stop Recording", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (dialogResult.Equals(DialogResult.No))
            {
            }
            else
            {
                form1.Dispose();
                form1.Close();
                if (backgroundWorker2.IsBusy != true)
                {
                    // Start the asynchronous operation.

                    backgroundWorker2.RunWorkerAsync();
                }

            }
        }
        private void screenshotButton_Click(object sender, EventArgs e)
        {
            Record();
        }
        private void ResultLabel_AutoSizeChanged(object sender, EventArgs e)
        {
            tableLayoutPanel.Width = resultLabel.Width + 20;
        }
        private void ResultLabel_SizeChanged(object sender, EventArgs e)
        {
            tableLayoutPanel.Width = resultLabel.Width + 20;
        }
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            tableLayoutPanel.Size = form1.Size;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            // Debug.WriteLine();
        }
        #endregion
    }
}
