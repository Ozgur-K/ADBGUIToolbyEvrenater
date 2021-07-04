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

namespace ADBGUIToolbyEvrenater.Screenshot
{
    class ScreenCapture
    {
        Form form1;
        TableLayoutPanel tableLayoutPanel;
        Label resultLabel;
        Button screenshotButton, cancelButton;
        BackgroundWorker backgroundWorker1;
        PictureBox pictureBox1;

        string imageFile;

        public ScreenCapture()
        {
            form1 = new();
            tableLayoutPanel = new();
            resultLabel = new();
            screenshotButton = new();
            cancelButton = new();
            backgroundWorker1 = new();
            pictureBox1 = new();
            form1.SuspendLayout();

            form1.Text = "Screenshot";
            resultLabel.Text = "Screenshot Capture";
            screenshotButton.Text = "Screenshot";
            cancelButton.Text = "Cancel";

            resultLabel.AutoSize = true;
            screenshotButton.AutoSize = true;
            cancelButton.AutoSize = true;
            cancelButton.Enabled = false;
            backgroundWorker1.WorkerSupportsCancellation = true;

            screenshotButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cancelButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel.Width = form1.Width - 20;
            tableLayoutPanel.Height = form1.Height - 20;
            pictureBox1.Width = 100;
            pictureBox1.Height = 200;

            resultLabel.AutoSizeChanged += ResultLabel_AutoSizeChanged;
            resultLabel.SizeChanged += ResultLabel_SizeChanged;
            screenshotButton.Click += screenshotButton_Click;
            cancelButton.Click += CancelButton_Click;
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
            form1.Load += Form1_Load;
            form1.SizeChanged += Form1_SizeChanged;

            tableLayoutPanel.Controls.AddRange(new Control[] { resultLabel, screenshotButton, cancelButton,
                                                    pictureBox1});
            form1.Controls.Add(tableLayoutPanel);
            form1.ResumeLayout();
            form1.ShowDialog();

        }

        private void Screenshot()
        {
            screenshotButton.Text = "Capturing...";
            screenshotButton.Enabled = false;
            cancelButton.Enabled = true;

            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.

                backgroundWorker1.RunWorkerAsync();
            }
        }

        #region

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                resultLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!
                screenshotButton.Text = "Screenshot";
                resultLabel.Text = "Screenshot Location:\r\n"
                                    + Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                                    + "\\screenshot<time>.png";
                screenshotButton.Enabled = true;
                cancelButton.Enabled = false;


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

                    if (lines.Length == 0)
                    {
                        // Success

                        screenshotButton.Text = "Screenshot";
                        screenshotButton.Enabled = true;
                        cancelButton.Enabled = false;
                        // tableLayoutPanel.BackgroundImage = Image.FromFile(imageFile);
                        Image image = Image.FromFile(imageFile);
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        pictureBox1.Image = image;
                    }
                    else if (line.IndexOf('*') != 0)
                    {
                        // Failed
                        resultLabel.Text = line;
                        screenshotButton.Text = "Screenshot";
                        screenshotButton.Enabled = true;
                        cancelButton.Enabled = false;

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
                string date = dateTime.Hour + "-" + dateTime.Minute + "-" + dateTime.Second + "-" + dateTime.Millisecond;

                imageFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                                        + "\\screenshot" + date + ".png";

                ProcessCreate.Command("adb exec-out screencap -p > " + imageFile);

            }
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
            cancelButton.Enabled = false;


            DialogResult dialogResult = DialogResult.Yes;
            dialogResult = MessageBox.Show(form1, "You may need to reconnect your device over Wi-Fi " +
                                "or physically reconnect over USB after cancellation. I suggest clicking no and" +
                                "disconnecting the phone. Do you want to cancel?",
                                "Terminate", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult.Equals(DialogResult.No))
            {
            }
            else
            {
                ProcessCreate.Command2("adb kill-server");

                form1.Dispose();
                form1.Close();
                if (backgroundWorker1.WorkerSupportsCancellation == true)
                {
                    // Cancel the asynchronous operation.
                    // 
                    backgroundWorker1.CancelAsync();
                }
            }
        }
        private void screenshotButton_Click(object sender, EventArgs e)
        {
            Screenshot();
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

            //Debug.WriteLine();
        }
        #endregion
    }
}
