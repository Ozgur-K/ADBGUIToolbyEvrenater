using ADBGUIToolbyEvrenater.ProcessCreating;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADBGUIToolbyEvrenater.Storage
{
    class SdToInternal
    {
        Form form1;
        TableLayoutPanel tableLayoutPanel;
        Label resultLabel;
        TextBox ratioTextBox;
        NumericUpDown numericUpDown;
        Button formatButton, cancelButton;
        BackgroundWorker backgroundWorker1, backgroundWorker2, backgroundWorker3, backgroundWorker4, backgroundWorker5;

        string diskNumber;
        public SdToInternal()
        {
            form1 = new();
            tableLayoutPanel = new();
            resultLabel = new();
            ratioTextBox = new();
            numericUpDown = new();
            formatButton = new();
            cancelButton = new();
            backgroundWorker1 = new();
            backgroundWorker2 = new();
            backgroundWorker3 = new();
            backgroundWorker4 = new();
            backgroundWorker5 = new();

            form1.SuspendLayout();

            form1.Text = "Format SD Card";
            resultLabel.Text = "Turn Portable SD Card Into Internal Storage\r\n" +
                                "What percentage of the sd card do you want to\r\nmake external storage?";
            ratioTextBox.PlaceholderText = "ratio";
            formatButton.Text = "Format";
            cancelButton.Text = "Cancel";

            cancelButton.Enabled = false;
            cancelButton.AutoSize = true;
            resultLabel.AutoSize = true;
            formatButton.AutoSize = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker2.WorkerSupportsCancellation = true;
            backgroundWorker3.WorkerSupportsCancellation = true;
            backgroundWorker4.WorkerSupportsCancellation = true;
            backgroundWorker5.WorkerSupportsCancellation = true;

            cancelButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            formatButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel.Width = form1.Width - 20;
            tableLayoutPanel.Height = form1.Height - 50;
            numericUpDown.Maximum = 80;
            numericUpDown.Minimum = 20;


            form1.Shown += Form1_Shown;
            resultLabel.SizeChanged += ResultLabel_TextChanged;
            form1.SizeChanged += Form1_SizeChanged;
            formatButton.Click += FormatButton_Click;
            cancelButton.Click += CancelButton_Click;
            numericUpDown.KeyDown += NumericUpDown_KeyDown;
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
            backgroundWorker2.DoWork += BackgroundWorker2_DoWork;
            backgroundWorker2.RunWorkerCompleted += BackgroundWorker2_RunWorkerCompleted;
            backgroundWorker3.DoWork += BackgroundWorker3_DoWork;
            backgroundWorker3.RunWorkerCompleted += BackgroundWorker3_RunWorkerCompleted;
            backgroundWorker4.DoWork += BackgroundWorker4_DoWork;
            backgroundWorker4.RunWorkerCompleted += BackgroundWorker4_RunWorkerCompleted;
            backgroundWorker5.DoWork += BackgroundWorker5_DoWork;
            backgroundWorker5.RunWorkerCompleted += BackgroundWorker5_RunWorkerCompleted;

            tableLayoutPanel.Controls.AddRange(new Control[] { resultLabel, numericUpDown, formatButton,
                                                cancelButton});
            form1.Controls.Add(tableLayoutPanel);
            form1.ResumeLayout();
            form1.ShowDialog();

        }

        public void Format()
        {
            formatButton.Text = "Formatting...";
            cancelButton.Enabled = true;
            formatButton.Enabled = false;

            if (backgroundWorker1.IsBusy != true)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        #region Events
        private void BackgroundWorker5_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine("comp5");
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
            }
        }

        private void BackgroundWorker5_DoWork(object sender, DoWorkEventArgs e)
        {
            Debug.WriteLine("do5");
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.
            }
        }

        private void BackgroundWorker4_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine("comp4");
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
                resultLabel.Text = "Done!";
                formatButton.Text = "Format";
                cancelButton.Enabled = false;
                formatButton.Enabled = true;
            }
        }

        private void BackgroundWorker4_DoWork(object sender, DoWorkEventArgs e)
        {
            Debug.WriteLine("do4");
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.

                ProcessCreate.Command("adb shell sm set-force-adoptable false");
            }
        }

        private void BackgroundWorker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine("comp3");
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
                    backgroundWorker4.RunWorkerAsync();
                }
            }
        }

        private void BackgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            Debug.WriteLine("do");
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.
                ProcessCreate.Command("adb shell sm partition disk:" + diskNumber + " mixed " + numericUpDown.Value);
            }
        }

        private void BackgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine("comp2");
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

                if (backgroundWorker3.IsBusy != true)
                {
                    backgroundWorker3.RunWorkerAsync();
                }
            }
        }

        private void BackgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            Debug.WriteLine("do2");
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.
                ProcessCreate.Command("adb shell sm set-force-adoptable true");
            }
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine("comp1");
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
                if (lines.Length == 0)
                {
                    resultLabel.Text = "Error";
                    formatButton.Text = "Format";
                    formatButton.Enabled = true;
                    cancelButton.Enabled = false;
                }
                else
                {
                    foreach (string line in lines)
                    {
                        if (((line.IndexOf('a') == 0) && (line.IndexOf('d') == 1)
                            && (line.IndexOf('b') == 2) && (line.IndexOf(':') == 3))
                            || (((line.IndexOf('e') == 0) && (line.IndexOf('r') == 1)
                             && (line.IndexOf('o') == 3))))
                        {
                            resultLabel.Text = line;

                            formatButton.Text = "Format";
                            formatButton.Enabled = true;
                            cancelButton.Enabled = false;
                        }
                        else if (((line.IndexOf('d') == 0) && (line.IndexOf('i') == 1)
                           && (line.IndexOf('s') == 2) && (line.IndexOf('k') == 3)))
                        {
                            diskNumber = line.Substring(line.IndexOf(":") + 1);

                            if (backgroundWorker2.IsBusy != true)
                            {
                                backgroundWorker2.RunWorkerAsync();
                            }
                        }
                        else
                        {
                            resultLabel.Text = line;

                            formatButton.Text = "Format";
                            formatButton.Enabled = true;
                            cancelButton.Enabled = false;
                        }
                    }
                }
            }
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Debug.WriteLine("do1");
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.
                ProcessCreate.Command("adb shell sm list-disks");
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

        private void FormatButton_Click(object sender, EventArgs e)
        {
            Format();
        }

        private void NumericUpDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.Equals(Keys.Enter))
                formatButton.PerformClick();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {

        }

        private void ResultLabel_TextChanged(object sender, EventArgs e)
        {
            if (resultLabel.Width > 500)
                tableLayoutPanel.Width = resultLabel.Width;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            ratioTextBox.Focus();
        }
        #endregion
    }
}
