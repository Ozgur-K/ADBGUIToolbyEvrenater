using ADBGUIToolbyEvrenater.ProcessCreating;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADBGUIToolbyEvrenater.ResolutionChanger
{
    class ResolutionChange
    {
        Form form1;
        TableLayoutPanel tableLayoutPanel;
        Label resultLabel;
        TextBox textBox;
        Button changeButton, resetButton, cancelButton;
        BackgroundWorker backgroundWorker1, backgroundWorker2, backgroundWorker3;

        public ResolutionChange()
        {
            form1 = new();
            tableLayoutPanel = new();
            resultLabel = new();
            textBox = new();
            changeButton = new();
            resetButton = new();
            cancelButton = new();
            backgroundWorker1 = new();
            backgroundWorker2 = new();
            backgroundWorker3 = new();

            form1.SuspendLayout();

            form1.Text = "Resolution Changer";
            resultLabel.Text = "Change Resolution\r\n[<width> lowercase letter x <height> (1080x1920)]";
            changeButton.Text = "Change";
            resetButton.Text = "Reset Resolution";
            cancelButton.Text = "Cancel";

            resultLabel.AutoSize = true;
            changeButton.AutoSize = true;
            resetButton.AutoSize = true;
            cancelButton.AutoSize = true;
            cancelButton.Enabled = false;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker2.WorkerSupportsCancellation = true;
            backgroundWorker3.WorkerSupportsCancellation = true;

            changeButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            resetButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cancelButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel.Width = form1.Width - 20;
            tableLayoutPanel.Height = form1.Height - 20;

            resultLabel.AutoSizeChanged += ResultLabel_AutoSizeChanged;
            changeButton.Click += ChangeButton_Click;
            resetButton.Click += ResetButton_Click;
            cancelButton.Click += CancelButton_Click;
            textBox.KeyDown += NumericUpDown_KeyDown;
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
            backgroundWorker2.DoWork += BackgroundWorker2_DoWork;
            backgroundWorker2.RunWorkerCompleted += BackgroundWorker2_RunWorkerCompleted;
            backgroundWorker3.DoWork += BackgroundWorker3_DoWork;
            backgroundWorker3.RunWorkerCompleted += BackgroundWorker3_RunWorkerCompleted;
            form1.Load += Form1_Load;

            tableLayoutPanel.Controls.AddRange(new Control[] { resultLabel, textBox, changeButton, resetButton,
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
                try
                {
                    string[] lines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                     StringSplitOptions.RemoveEmptyEntries);
                    foreach (string line in lines)
                    {
                        if (((line.IndexOf('P') == 0) && (line.IndexOf('h') == 1)
                            && (line.IndexOf('y') == 2) && (line.IndexOf('s') == 3)))
                        {
                            string density = line.Substring(line.IndexOf(":") + 2);
                            density = density.Trim();
                            Debug.WriteLine("density" + density);
                            // numericUpDown.Value = Convert.ToDecimal(density);
                        }
                    }
                }
                catch (Exception)
                {
                    Debug.WriteLine("BackgroundWorker2.RunWorkerCompletedError");
                }
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
                ProcessCreate.Command("adb shell wm density");

            }
        }
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

                    if (line.IndexOf('*') != 0)
                    {
                        // Failed
                        resultLabel.Text = line;
                        resetButton.Text = "Reset Resolution";
                        resetButton.Enabled = true;
                        cancelButton.Enabled = false;

                    }
                }
                if (lines.Length == 0)
                {
                    // Success

                    resetButton.Text = "Reset Resolution";
                    resetButton.Enabled = true;
                    cancelButton.Enabled = false;
                }
                else
                    Debug.WriteLine("resoolutionchangeelse");
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
                ProcessCreate.Command("adb shell wm size reset");
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

                        changeButton.Text = "Change";
                        changeButton.Enabled = true;
                        cancelButton.Enabled = false;
                    }
                    else if (line.IndexOf('*') != 0)
                    {
                        // Failed
                        resultLabel.Text = line;
                        changeButton.Text = "Change";
                        changeButton.Enabled = true;
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
                ProcessCreate.Command("adb shell wm size " + textBox.Text);
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
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        #endregion
    }
}
