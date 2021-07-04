using ADBGUIToolbyEvrenater.ProcessCreating;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADBGUIToolbyEvrenater.Battery
{
    class BatteryLevel
    {
        Form form1;
        TableLayoutPanel tableLayoutPanel;
        Label resultLabel;
        NumericUpDown numericUpDown;
        Button changeButton, resetButton, cancelButton;
        BackgroundWorker backgroundWorker1, backgroundWorker2;

        public BatteryLevel()
        {
            form1 = new();
            tableLayoutPanel = new();
            resultLabel = new();
            numericUpDown = new();
            changeButton = new();
            resetButton = new();
            cancelButton = new();
            backgroundWorker1 = new();
            backgroundWorker2 = new();

            form1.SuspendLayout();

            form1.Text = "Battery Level Changer";
            resultLabel.Text = "Change Battery Level";
            changeButton.Text = "Change";
            resetButton.Text = "Reset Battery Level";
            cancelButton.Text = "Cancel";

            resultLabel.AutoSize = true;
            changeButton.AutoSize = true;
            resetButton.AutoSize = true;
            cancelButton.AutoSize = true;
            cancelButton.Enabled = false;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker2.WorkerSupportsCancellation = true;

            changeButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            resetButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cancelButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel.Width = form1.Width - 20;
            tableLayoutPanel.Height = form1.Height - 20;
            numericUpDown.Maximum = 500;
            numericUpDown.Minimum = 0;
            numericUpDown.Value = 50;

            resultLabel.AutoSizeChanged += ResultLabel_AutoSizeChanged;
            changeButton.Click += ChangeButton_Click;
            resetButton.Click += ResetButton_Click;
            cancelButton.Click += CancelButton_Click;
            numericUpDown.KeyDown += NumericUpDown_KeyDown;
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
            backgroundWorker2.DoWork += BackgroundWorker2_DoWork;
            backgroundWorker2.RunWorkerCompleted += BackgroundWorker2_RunWorkerCompleted;

            tableLayoutPanel.Controls.AddRange(new Control[] { resultLabel, numericUpDown, changeButton, resetButton,
                                                cancelButton});
            form1.Controls.Add(tableLayoutPanel);
            form1.ResumeLayout();
            form1.ShowDialog();

        }

        private void Change()
        {
            changeButton.Text = "Changing...";
            changeButton.Enabled = false;
            cancelButton.Enabled = true;

            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.

                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void Reset()
        {
            resetButton.Text = "Resetting...";
            resetButton.Enabled = false;
            cancelButton.Enabled = true;
            if (backgroundWorker2.IsBusy != true)
            {
                // Start the asynchronous operation.

                backgroundWorker2.RunWorkerAsync();
            }
        }

        #region
        private void BackgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
                resetButton.Text = "Reset";
                resetButton.Enabled = true;
                cancelButton.Enabled = false;
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
                ProcessCreate.Command("adb shell dumpsys battery reset");
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
                        Debug.WriteLine("success");
                        changeButton.Text = "Change";
                        changeButton.Enabled = true;
                        cancelButton.Enabled = false;
                    }
                    else if (line.IndexOf('*') != 0)
                    {
                        // Failed
                        Debug.WriteLine("failed");
                        resultLabel.Text = line;
                        changeButton.Enabled = true;
                        cancelButton.Enabled = false;
                        changeButton.Text = "Change";

                    }
                }
                changeButton.Text = "Change";
                changeButton.Enabled = true;
                cancelButton.Enabled = false;
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
                ProcessCreate.Command("adb shell dumpsys battery set level " + numericUpDown.Value);
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

        private void ResetButton_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            Change();
        }

        private void NumericUpDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.Equals(Keys.Enter))
                changeButton.PerformClick();
        }

        private void ResultLabel_AutoSizeChanged(object sender, EventArgs e)
        {
            tableLayoutPanel.Width = resultLabel.Width + 20;
        }
        #endregion
    }
}
