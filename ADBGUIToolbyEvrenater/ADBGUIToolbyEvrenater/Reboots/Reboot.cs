using ADBGUIToolbyEvrenater.ProcessCreating;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADBGUIToolbyEvrenater.Reboots
{
    class Reboot
    {
        BackgroundWorker backgroundWorker1, backgroundWorker2;
        Form form1;
        TableLayoutPanel tableLayoutPanel;
        Button rebootButton, rebootRButton, cancelButton;
        Label resultLabel;

        public object Daebug { get; private set; }

        public Reboot()
        {
            backgroundWorker1 = new();
            backgroundWorker2 = new();
            form1 = new();
            tableLayoutPanel = new();
            rebootButton = new();
            rebootRButton = new();
            cancelButton = new();
            resultLabel = new();


            form1.SuspendLayout();

            form1.Text = "Reboot";
            rebootButton.Text = "Reboot System";
            rebootRButton.Text = "Reboot Recovery";
            resultLabel.Text = "Reboot System or Reboot To Recovery";
            cancelButton.Text = "Cancel";

            rebootButton.AutoSize = true;
            rebootRButton.AutoSize = true;
            cancelButton.AutoSize = true;
            resultLabel.AutoSize = true;
            cancelButton.Enabled = false;

            rebootButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            rebootRButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cancelButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel.Width = form1.Width - 20;
            tableLayoutPanel.Height = form1.Height - 20;

            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker2.WorkerSupportsCancellation = true;

            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
            backgroundWorker2.DoWork += BackgroundWorker2_DoWork;
            backgroundWorker2.RunWorkerCompleted += BackgroundWorker2_RunWorkerCompleted;
            form1.SizeChanged += Form1_SizeChanged;
            rebootButton.Click += RebootButton_Click;
            rebootRButton.Click += RebootRButton_Click;
            cancelButton.Click += CancelButton_Click;
            resultLabel.TextChanged += ResultLabel_TextChanged;

            tableLayoutPanel.Controls.AddRange(new Control[] { resultLabel, rebootButton, rebootRButton, cancelButton });
            form1.Controls.Add(tableLayoutPanel);
            form1.ResumeLayout();
            form1.ShowDialog();
        }

        private void RebootSystem()
        {
            rebootButton.Text = "Rebooting";
            rebootButton.Enabled = false;
            cancelButton.Enabled = true;

            if(backgroundWorker1.IsBusy != true)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void RebootToRecovery()
        {
            rebootRButton.Text = "Rebooting";
            rebootRButton.Enabled = false;
            cancelButton.Enabled = true;

            if (backgroundWorker2.IsBusy != true)
            {
                backgroundWorker2.RunWorkerAsync();
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
                rebootButton.Text = "Reboot";
                rebootButton.Enabled = true;
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
                ProcessCreate.Command("adb reboot");
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
                    if (((line.IndexOf('a') == 0) && (line.IndexOf('d') == 1)
                        && (line.IndexOf('b') == 2) && (line.IndexOf(':') == 3))
                        || (((line.IndexOf('e') == 0) && (line.IndexOf('r') == 1)
                         && (line.IndexOf('o') == 3))))
                    {
                        resultLabel.Text = line;
                    }
                }
                rebootRButton.Text = "Reboot To Recovery";
                rebootRButton.Enabled = true;
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
                ProcessCreate.Command("adb reboot recovery");
            }
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
                backgroundWorker1.CancelAsync();
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
                }
            
        }
        private void ResultLabel_TextChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("form1width" + form1.Width + "resultLabelwith" +  resultLabel.Width
                + "tablelayoutpanlewith" + tableLayoutPanel.Width);
            if(resultLabel.Width !< form1.Width)
            {
                tableLayoutPanel.Width = resultLabel.Width + 50;
                Debug.WriteLine("inif condit");
            }
        }
        private void RebootRButton_Click(object sender, EventArgs e)
        {
            RebootToRecovery();
        }
        private void RebootButton_Click(object sender, EventArgs e)
        {
            RebootSystem();
        }
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            tableLayoutPanel.Width = form1.Width - 20;
            tableLayoutPanel.Height = form1.Height - 20;
        }
    }
}
