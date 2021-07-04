using ADBGUIToolbyEvrenater.ProcessCreating;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADBGUIToolbyEvrenater.ADBSideload
{
    class Sideload
    {
        Form form1;
        TableLayoutPanel tableLayoutPanel;
        Label resultLabel;
        Button sideloadButton, rebootButton, cancelButton;
        BackgroundWorker backgroundWorker1, backgroundWorker2, backgroundWorker3, backgroundWorker4;

        string zipFile;
        string zipDir;
        public Sideload()
        {
            form1 = new();
            tableLayoutPanel = new();
            resultLabel = new();
            sideloadButton = new();
            rebootButton = new();
            cancelButton = new();
            backgroundWorker1 = new();
            backgroundWorker2 = new();
            backgroundWorker3 = new();
            backgroundWorker4 = new();

            form1.SuspendLayout();

            form1.Text = "ADB Sideload";
            resultLabel.Text = "Drag Here The Sideload Zip File...";
            sideloadButton.Text = "Sideload";
            rebootButton.Text = "Reboot to Sideload";
            cancelButton.Text = "Cancel";

            resultLabel.AutoSize = true;
            sideloadButton.AutoSize = true;
            rebootButton.AutoSize = true;
            cancelButton.AutoSize = true;
            cancelButton.Enabled = false;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker2.WorkerSupportsCancellation = true;
            backgroundWorker3.WorkerSupportsCancellation = true;
            backgroundWorker4.WorkerSupportsCancellation = true;
            form1.AllowDrop = true;
            sideloadButton.Enabled = false;

            sideloadButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            rebootButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cancelButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel.Width = form1.Width - 20;
            tableLayoutPanel.Height = form1.Height;

            form1.SizeChanged += Form1_SizeChanged;
            resultLabel.SizeChanged += ResultLabel_SizeChanged;
            rebootButton.Click += RebootButton_Click;
            sideloadButton.Click += SideloadButton_Click;
            cancelButton.Click += CancelButton_Click;
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker2.RunWorkerCompleted += BackgroundWorker2_RunWorkerCompleted;
            backgroundWorker2.DoWork += BackgroundWorker2_DoWork;
            backgroundWorker3.RunWorkerCompleted += BackgroundWorker3_RunWorkerCompleted;
            backgroundWorker3.DoWork += BackgroundWorker3_DoWork;
            backgroundWorker4.RunWorkerCompleted += BackgroundWorker4_RunWorkerCompleted;
            backgroundWorker4.DoWork += BackgroundWorker4_DoWork;
            form1.DragEnter += Form1_DragEnter;
            form1.DragDrop += Form1_DragDrop;

            tableLayoutPanel.Controls.AddRange(new Control[] {resultLabel, sideloadButton, rebootButton,
                                                cancelButton});
            form1.Controls.Add(tableLayoutPanel);
            form1.ResumeLayout();
            form1.ShowDialog();
        }

        private void SideloadProcess()
        {
            sideloadButton.Enabled = false;
            cancelButton.Enabled = true;

            if (backgroundWorker3.IsBusy != true)
            {
                // Start the asynchronous operation.

                backgroundWorker3.RunWorkerAsync();
            }
        }
        private void GetFile(string [] files)
        {
            zipFile = files[0];
            resultLabel.Text = files[0];
            sideloadButton.Enabled = true;
            GetDirectory();
        }
        private void GetDirectory()
        {
            string str = zipFile.Substring(0, zipFile.LastIndexOf("\\"));
            str += "\\";
            zipDir = str;
        }
        private void RebootToRecovery()
        {
            rebootButton.Text = "Rebooting To Sideload";
            rebootButton.Enabled = false;
            cancelButton.Enabled = true;

            if (backgroundWorker1.IsBusy != true)
            {
                backgroundWorker1.RunWorkerAsync();
            }

        }
        private void Cancel()
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
                if (backgroundWorker1.IsBusy != true)
                {
                    // Start the asynchronous operation.

                    backgroundWorker1.RunWorkerAsync();
                }
            }
        }

        #region Events

        private void BackgroundWorker4_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.
                ProcessCreate.Command("adb sideload " + zipFile);
            }
        }

        private void BackgroundWorker4_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
                string[] lines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                                 StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    if (((line.IndexOf('a') == 0) && (line.IndexOf('d') == 1)
                        && (line.IndexOf('b') == 2) && (line.IndexOf(':') == 3))
                        || (((line.IndexOf('e') == 0) && (line.IndexOf('r') == 1)
                         && (line.IndexOf('o') == 3))))
                    {
                        resultLabel.Text = line;
                    }
                }

                sideloadButton.Enabled = true;
                cancelButton.Enabled = false;
            }
        }
        private void BackgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.

                ProcessCreate.Command("set ANDROID_PRODUCT_OUT=" + zipDir);
            }
        }

        private void BackgroundWorker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
                if (backgroundWorker4.IsBusy != true)
                {
                    // Start the asynchronous operation.

                    backgroundWorker4.RunWorkerAsync();
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

        private void BackgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cancelButton.Enabled = false;
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
                ProcessCreate.Command("adb reboot sideload");
            }
        }

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

                string[] lines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                                                 StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    if (((line.IndexOf('a') == 0) && (line.IndexOf('d') == 1)
                        && (line.IndexOf('b') == 2) && (line.IndexOf(':') == 3))
                        || (((line.IndexOf('e') == 0) && (line.IndexOf('r') == 1)
                         && (line.IndexOf('o') == 3))))
                    {
                        resultLabel.Text = line;
                    }
                }
                rebootButton.Text = "Reboot To Sideload";
                rebootButton.Enabled = true;
                cancelButton.Enabled = false;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        private void SideloadButton_Click(object sender, EventArgs e)
        {
            SideloadProcess();
        }

        private void RebootButton_Click(object sender, EventArgs e)
        {
            RebootToRecovery();
        }

        private void ResultLabel_SizeChanged(object sender, EventArgs e)
        {
            tableLayoutPanel.Width = resultLabel.Width + 20;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            GetFile((string[])e.Data.GetData(DataFormats.FileDrop));
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
        #endregion
    }
}
