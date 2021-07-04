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

namespace ADBGUIToolbyEvrenater.FastbootProcess
{
    class Fastboot
    {
        Form form1;
        TableLayoutPanel tableLayoutPanel1, tableLayoutPanel2;
        FlowLayoutPanel flowLayoutPanel1;
        RichTextBox richTextBox;
        Button flashBootButton, flashRecoveryButton, bootBootButton, bootRecoveryButton, flashSystemButton,
                eraseBootButton, eraseRecoveryButton, oemUnlockButton, oemUnlockCriticalButton,
                flashingUnlockButton, flashingUnlockCriticalButton, getvarAllButton;
        BackgroundWorker backgroundWorker1, backgroundWorker2, backgroundWorker3, backgroundWorker4,
                            backgroundWorker5, backgroundWorker6, backgroundWorker7, backgroundWorker8,
                            backgroundWorker9, backgroundWorker10, backgroundWorker11, backgroundWorker12,
                            backgroundWorker13, backgroundWorker14, backgroundWorker15, backgroundWorker16,
                            backgroundWorker17, backgroundWorker18, backgroundWorker19, backgroundWorker20,
                            backgroundWorker21, backgroundWorker22, backgroundWorker23, backgroundWorker24;

        string bootFile, recoveryFile, systemFile;
        public Fastboot()
        {
            form1 = new();
            tableLayoutPanel1 = new();
            flowLayoutPanel1 = new();
            tableLayoutPanel2 = new();
            richTextBox = new();
            flashBootButton = new();
            flashRecoveryButton = new();
            bootBootButton = new();
            bootRecoveryButton = new();
            flashSystemButton = new();
            eraseBootButton = new();
            eraseRecoveryButton = new();
            oemUnlockButton = new();
            oemUnlockCriticalButton = new();
            flashingUnlockButton = new(); ;
            flashingUnlockCriticalButton = new();
            getvarAllButton = new();
            backgroundWorker1 = new();
            backgroundWorker2 = new();
            backgroundWorker3 = new();
            backgroundWorker4 = new();
            backgroundWorker5 = new();
            backgroundWorker6 = new();
            backgroundWorker7 = new();
            backgroundWorker8 = new();
            backgroundWorker9 = new();
            backgroundWorker10 = new();
            backgroundWorker11 = new();
            backgroundWorker12 = new();
            backgroundWorker13 = new();
            backgroundWorker14 = new();
            backgroundWorker15 = new();
            backgroundWorker16 = new();
            backgroundWorker17 = new();
            backgroundWorker18 = new();
            backgroundWorker19 = new();
            backgroundWorker20 = new();
            backgroundWorker21 = new();
            backgroundWorker22 = new();
            backgroundWorker23 = new();
            backgroundWorker24 = new();


            form1.SuspendLayout();

            form1.Text = "Fastboot";
            richTextBox.Text = "Fastboot";
            flashBootButton.Text = "Flash Boot";
            flashRecoveryButton.Text = "Flash Recovery";
            bootBootButton.Text = "Boot Boot";
            bootRecoveryButton.Text = "Boot Recovery";
            flashSystemButton.Text = "Flash System";
            eraseBootButton.Text = "Erase Boot";
            eraseRecoveryButton.Text = "Erase Recovery";
            oemUnlockButton.Text = "OEM Unlock";
            oemUnlockCriticalButton.Text = "OEM Unlock Critical";
            flashingUnlockButton.Text = "Flashing Unlock";
            flashingUnlockCriticalButton.Text = "Flashing Unlock Critical";
            getvarAllButton.Text = "GetVar All";

            form1.MaximizeBox = false;
            form1.MinimizeBox = false;
            flashBootButton.AutoSize = true;
            flashRecoveryButton.AutoSize = true;
            bootBootButton.AutoSize = true;
            bootRecoveryButton.AutoSize = true;
            flashSystemButton.AutoSize = true;
            eraseBootButton.AutoSize = true;
            eraseRecoveryButton.AutoSize = true;
            oemUnlockButton.AutoSize = true;
            oemUnlockCriticalButton.AutoSize = true;
            flashingUnlockButton.AutoSize = true;
            flashingUnlockCriticalButton.AutoSize = true;
            getvarAllButton.AutoSize = true;
            richTextBox.ReadOnly = true;

            flashBootButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flashRecoveryButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bootBootButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bootRecoveryButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flashSystemButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            eraseBootButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            eraseRecoveryButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            oemUnlockButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            oemUnlockCriticalButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flashingUnlockButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flashingUnlockCriticalButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            getvarAllButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            richTextBox.Dock = DockStyle.Fill;
            tableLayoutPanel1.Dock = DockStyle.Top;
            flowLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.BackColor = Color.AliceBlue;
            flowLayoutPanel1.BackColor = Color.Aquamarine;
            form1.Width = Screen.PrimaryScreen.Bounds.Width / 2;
            form1.Height = Screen.PrimaryScreen.Bounds.Height / 2;
            tableLayoutPanel2.Size = form1.Size;

            form1.SizeChanged += Form1_SizeChanged;
            flashBootButton.Click += FlashBootButton_Click;
            flashRecoveryButton.Click += FlashRecoveryButton_Click;
            bootBootButton.Click += BootBootButton_Click;
            bootRecoveryButton.Click += BootRecoveryButton_Click;
            flashSystemButton.Click += FlashSystemButton_Click;
            eraseBootButton.Click += EraseBootButton_Click;
            eraseRecoveryButton.Click += EraseRecoveryButton_Click;
            oemUnlockButton.Click += OemUnlockButton_Click;
            oemUnlockCriticalButton.Click += OemUnlockCriticalButton_Click;
            flashingUnlockButton.Click += FlashingUnlockButton_Click;
            flashingUnlockCriticalButton.Click += FlashingUnlockCriticalButton_Click;
            getvarAllButton.Click += GetvarAllButton_Click;
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
            backgroundWorker6.DoWork += BackgroundWorker6_DoWork;
            backgroundWorker6.RunWorkerCompleted += BackgroundWorker6_RunWorkerCompleted;
            backgroundWorker7.DoWork += BackgroundWorker7_DoWork;
            backgroundWorker7.RunWorkerCompleted += BackgroundWorker7_RunWorkerCompleted;
            backgroundWorker8.DoWork += BackgroundWorker8_DoWork;
            backgroundWorker8.RunWorkerCompleted += BackgroundWorker8_RunWorkerCompleted;
            backgroundWorker9.DoWork += BackgroundWorker9_DoWork;
            backgroundWorker9.RunWorkerCompleted += BackgroundWorker9_RunWorkerCompleted;
            backgroundWorker10.DoWork += BackgroundWorker10_DoWork;
            backgroundWorker10.RunWorkerCompleted += BackgroundWorker10_RunWorkerCompleted;
            backgroundWorker11.DoWork += BackgroundWorker11_DoWork;
            backgroundWorker11.RunWorkerCompleted += BackgroundWorker11_RunWorkerCompleted;
            backgroundWorker12.DoWork += BackgroundWorker12_DoWork;
            backgroundWorker12.RunWorkerCompleted += BackgroundWorker12_RunWorkerCompleted;
            backgroundWorker13.DoWork += BackgroundWorker13_DoWork;
            backgroundWorker13.RunWorkerCompleted += BackgroundWorker13_RunWorkerCompleted;
            backgroundWorker14.DoWork += BackgroundWorker14_DoWork;
            backgroundWorker14.RunWorkerCompleted += BackgroundWorker14_RunWorkerCompleted;
            backgroundWorker15.DoWork += BackgroundWorker15_DoWork;
            backgroundWorker15.RunWorkerCompleted += BackgroundWorker15_RunWorkerCompleted;
            backgroundWorker16.DoWork += BackgroundWorker16_DoWork;
            backgroundWorker16.RunWorkerCompleted += BackgroundWorker16_RunWorkerCompleted;
            backgroundWorker17.DoWork += BackgroundWorker17_DoWork;
            backgroundWorker17.RunWorkerCompleted += BackgroundWorker17_RunWorkerCompleted;
            backgroundWorker18.DoWork += BackgroundWorker18_DoWork;
            backgroundWorker18.RunWorkerCompleted += BackgroundWorker18_RunWorkerCompleted;
            backgroundWorker19.DoWork += BackgroundWorker19_DoWork;
            backgroundWorker19.RunWorkerCompleted += BackgroundWorker19_RunWorkerCompleted;
            backgroundWorker20.DoWork += BackgroundWorker20_DoWork;
            backgroundWorker20.RunWorkerCompleted += BackgroundWorker20_RunWorkerCompleted;
            backgroundWorker21.DoWork += BackgroundWorker21_DoWork;
            backgroundWorker21.RunWorkerCompleted += BackgroundWorker21_RunWorkerCompleted;
            backgroundWorker22.DoWork += BackgroundWorker22_DoWork;
            backgroundWorker22.RunWorkerCompleted += BackgroundWorker22_RunWorkerCompleted;
            backgroundWorker23.DoWork += BackgroundWorker23_DoWork;
            backgroundWorker23.RunWorkerCompleted += BackgroundWorker23_RunWorkerCompleted;
            backgroundWorker24.DoWork += BackgroundWorker24_DoWork;
            backgroundWorker24.RunWorkerCompleted += BackgroundWorker24_RunWorkerCompleted;

            tableLayoutPanel1.Controls.AddRange(new Control[] { richTextBox });
            flowLayoutPanel1.Controls.AddRange(new Control[] { flashBootButton, flashRecoveryButton, bootBootButton,
                                                bootRecoveryButton, eraseBootButton, eraseRecoveryButton,
                                                flashSystemButton, oemUnlockButton, oemUnlockCriticalButton,
                                                flashingUnlockButton, flashingUnlockCriticalButton, getvarAllButton});
            tableLayoutPanel2.Controls.AddRange(new Control[] { tableLayoutPanel1, flowLayoutPanel1 });
            form1.Controls.AddRange(new Control[] { tableLayoutPanel2 });
            form1.ResumeLayout();
            form1.ShowDialog();
        }

        private void FlashBoot()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult dialogResult = openFileDialog.ShowDialog(form1);
            if (dialogResult.Equals(DialogResult.OK))
            {

                bootFile = openFileDialog.FileName;

                richTextBox.Text = "Fastboot";
                flashBootButton.Text = "Waiting...";
                flashBootButton.Enabled = false;

                if (backgroundWorker1.IsBusy != true)
                {
                    backgroundWorker1.RunWorkerAsync();
                }
            }
        }

        private void FlashRecovery()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult dialogResult = openFileDialog.ShowDialog(form1);
            if (dialogResult.Equals(DialogResult.OK))
            {

                recoveryFile = openFileDialog.FileName;

                richTextBox.Text = "Fastboot";
                flashRecoveryButton.Text = "Waiting...";
                flashRecoveryButton.Enabled = false;

                if (backgroundWorker3.IsBusy != true)
                {
                    backgroundWorker3.RunWorkerAsync();
                }
            }
        }

        private void BootBoot()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult dialogResult = openFileDialog.ShowDialog(form1);
            if (dialogResult.Equals(DialogResult.OK))
            {
                bootFile = openFileDialog.FileName;

                richTextBox.Text = "Fastboot";
                bootBootButton.Text = "Waiting...";
                bootBootButton.Enabled = false;

                if (backgroundWorker5.IsBusy != true)
                {
                    backgroundWorker5.RunWorkerAsync();
                }
            }
        }

        private void BootRecovery()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult dialogResult = openFileDialog.ShowDialog(form1);
            if (dialogResult.Equals(DialogResult.OK))
            {
                recoveryFile = openFileDialog.FileName;

                richTextBox.Text = "Fastboot";
                bootRecoveryButton.Text = "Waiting...";
                bootRecoveryButton.Enabled = false;

                if (backgroundWorker7.IsBusy != true)
                {
                    backgroundWorker7.RunWorkerAsync();
                }
            }
        }

        private void FlashSystem()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult dialogResult = openFileDialog.ShowDialog(form1);
            if (dialogResult.Equals(DialogResult.OK))
            {
                systemFile = openFileDialog.FileName;

                richTextBox.Text = "Fastboot";
                flashSystemButton.Text = "Waiting...";
                flashSystemButton.Enabled = false;

                if (backgroundWorker9.IsBusy != true)
                {
                    backgroundWorker9.RunWorkerAsync();
                }
            }
        }

        private void EraseBoot()
        {
            richTextBox.Text = "Fastboot";
            eraseBootButton.Text = "Waiting...";
            eraseBootButton.Enabled = false;

            if (backgroundWorker11.IsBusy != true)
            {
                backgroundWorker11.RunWorkerAsync();
            }
        }

        private void EraseRecovery()
        {
            richTextBox.Text = "Fastboot";
            eraseRecoveryButton.Text = "Waiting...";
            eraseRecoveryButton.Enabled = false;

            if (backgroundWorker13.IsBusy != true)
            {
                backgroundWorker13.RunWorkerAsync();
            }
        }

        private void OEMUnlock()
        {
            richTextBox.Text = "Fastboot";
            oemUnlockButton.Text = "Waiting...";
            oemUnlockButton.Enabled = false;

            if (backgroundWorker15.IsBusy != true)
            {
                backgroundWorker15.RunWorkerAsync();
            }
        }

        private void OEMUnlockCritical()
        {
            richTextBox.Text = "Fastboot";
            oemUnlockCriticalButton.Text = "Waiting...";
            oemUnlockCriticalButton.Enabled = false;

            if (backgroundWorker17.IsBusy != true)
            {
                backgroundWorker17.RunWorkerAsync();
            }
        }

        private void FlashingUnlock()
        {
            richTextBox.Text = "Fastboot";
            flashingUnlockButton.Text = "Waiting...";
            flashingUnlockButton.Enabled = false;

            if (backgroundWorker19.IsBusy != true)
            {
                backgroundWorker19.RunWorkerAsync();
            }
        }

        private void FlashingUnlockCritical()
        {
            richTextBox.Text = "Fastboot";
            flashingUnlockCriticalButton.Text = "Waiting...";
            flashingUnlockCriticalButton.Enabled = false;

            if (backgroundWorker21.IsBusy != true)
            {
                backgroundWorker21.RunWorkerAsync();
            }
        }

        private void GetVarAll()
        {
            richTextBox.Text = "Fastboot";
            getvarAllButton.Text = "Waiting...";
            getvarAllButton.Enabled = false;

            if (backgroundWorker23.IsBusy != true)
            {
                backgroundWorker23.RunWorkerAsync();
            }
        }

        private void BackgroundWorker24_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                richTextBox.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                richTextBox.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!
                richTextBox.Text = ProcessCreate.cmdOutput;
                getvarAllButton.Text = "GetVar All";
                getvarAllButton.Enabled = true;
            }
        }
        private void BackgroundWorker24_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.
                ProcessCreate.Command("fastboot getvar all");
            }
        }
        private void BackgroundWorker23_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                richTextBox.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                richTextBox.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!

                string[] lines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                                                                StringSplitOptions.RemoveEmptyEntries);

                if (lines.Count() == 0)
                {
                    // Not Connected
                    richTextBox.Text = "Fastboot Device Not Found";
                    getvarAllButton.Enabled = true;
                    getvarAllButton.Text = "GetVar All";
                }
                else
                {
                    foreach (string line in lines)
                    {
                        if ((line.IndexOf("*") != 0) && (line.IndexOf(" ") != 4))
                        {
                            if (line.Contains("fastboot"))
                            {
                                // Connected

                                richTextBox.Text = "Fastboot Device Connected: " + line;
                                if (backgroundWorker24.IsBusy != true)
                                {
                                    // Start the asynchronous operation.

                                    backgroundWorker24.RunWorkerAsync();
                                }

                            }
                            else
                            {
                                // Not Connected
                                richTextBox.Text = "Fastboot Device Not Found";
                                getvarAllButton.Enabled = true;
                                getvarAllButton.Text = "GetVar All";
                            }
                        }
                    }
                }
            }
        }
        private void BackgroundWorker23_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.

                ProcessCreate.Command("fastboot devices");
            }
        }
        private void BackgroundWorker22_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                richTextBox.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                richTextBox.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!
                richTextBox.Text = ProcessCreate.cmdOutput;
                flashingUnlockCriticalButton.Text = "Flashing Unlock Critical";
                flashingUnlockCriticalButton.Enabled = true;
            }
        }
        private void BackgroundWorker22_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.
                ProcessCreate.Command("fastboot flashing unlock_critical");
            }
        }
        private void BackgroundWorker21_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                richTextBox.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                richTextBox.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!

                string[] lines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                                                                StringSplitOptions.RemoveEmptyEntries);

                if (lines.Count() == 0)
                {
                    // Not Connected
                    richTextBox.Text = "Fastboot Device Not Found";
                    flashingUnlockCriticalButton.Enabled = true;
                    flashingUnlockCriticalButton.Text = "Flasing Unlock Critical";
                }
                else
                {
                    foreach (string line in lines)
                    {
                        if ((line.IndexOf("*") != 0) && (line.IndexOf(" ") != 4))
                        {
                            if (line.Contains("fastboot"))
                            {
                                // Connected

                                richTextBox.Text = "Fastboot Device Connected: " + line;
                                if (backgroundWorker22.IsBusy != true)
                                {
                                    // Start the asynchronous operation.

                                    backgroundWorker22.RunWorkerAsync();
                                }

                            }
                            else
                            {
                                // Not Connected
                                richTextBox.Text = "Fastboot Device Not Found";
                                flashingUnlockCriticalButton.Enabled = true;
                                flashingUnlockCriticalButton.Text = "Flashing Unlock Critical";
                            }
                        }
                    }
                }
            }
        }
        private void BackgroundWorker21_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.

                ProcessCreate.Command("fastboot devices");
            }
        }
        private void BackgroundWorker20_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                richTextBox.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                richTextBox.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!
                richTextBox.Text = ProcessCreate.cmdOutput;
                flashingUnlockButton.Text = "Flasing Unlock";
                flashingUnlockButton.Enabled = true;
            }
        }
        private void BackgroundWorker20_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.
                ProcessCreate.Command("fastboot flashing unlock");
            }
        }
        private void BackgroundWorker19_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                richTextBox.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                richTextBox.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!

                string[] lines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                                                                StringSplitOptions.RemoveEmptyEntries);

                if (lines.Count() == 0)
                {
                    // Not Connected
                    richTextBox.Text = "Fastboot Device Not Found";
                    flashingUnlockButton.Enabled = true;
                    flashingUnlockButton.Text = "Flashing Unlock Critical";
                }
                else
                {
                    foreach (string line in lines)
                    {
                        if ((line.IndexOf("*") != 0) && (line.IndexOf(" ") != 4))
                        {
                            if (line.Contains("fastboot"))
                            {
                                // Connected

                                richTextBox.Text = "Fastboot Device Connected: " + line;
                                if (backgroundWorker20.IsBusy != true)
                                {
                                    // Start the asynchronous operation.

                                    backgroundWorker20.RunWorkerAsync();
                                }

                            }
                            else
                            {
                                // Not Connected
                                richTextBox.Text = "Fastboot Device Not Found";
                                flashingUnlockButton.Enabled = true;
                                flashingUnlockButton.Text = "Flashing Unlock Critical";
                            }
                        }
                    }
                }
            }
        }
        private void BackgroundWorker19_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.

                ProcessCreate.Command("fastboot devices");
            }
        }
        private void BackgroundWorker18_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                richTextBox.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                richTextBox.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!
                richTextBox.Text = ProcessCreate.cmdOutput;
                oemUnlockCriticalButton.Text = "OEM Unlock Critical";
                oemUnlockCriticalButton.Enabled = true;
            }
        }
        private void BackgroundWorker18_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.
                ProcessCreate.Command("fastboot oem unlock_critical");
            }
        }
        private void BackgroundWorker17_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                richTextBox.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                richTextBox.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!

                string[] lines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                                                                StringSplitOptions.RemoveEmptyEntries);

                if (lines.Count() == 0)
                {
                    // Not Connected
                    richTextBox.Text = "Fastboot Device Not Found";
                    oemUnlockCriticalButton.Enabled = true;
                    oemUnlockCriticalButton.Text = "OEM Unlock Critical";
                }
                else
                {
                    foreach (string line in lines)
                    {
                        if ((line.IndexOf("*") != 0) && (line.IndexOf(" ") != 4))
                        {
                            if (line.Contains("fastboot"))
                            {
                                // Connected

                                richTextBox.Text = "Fastboot Device Connected: " + line;
                                if (backgroundWorker18.IsBusy != true)
                                {
                                    // Start the asynchronous operation.

                                    backgroundWorker18.RunWorkerAsync();
                                }

                            }
                            else
                            {
                                // Not Connected
                                richTextBox.Text = "Fastboot Device Not Found";
                                oemUnlockCriticalButton.Enabled = true;
                                oemUnlockCriticalButton.Text = "OEM Unlock Critical";
                            }
                        }
                    }
                }
            }
        }
        private void BackgroundWorker17_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.

                ProcessCreate.Command("fastboot devices");
            }
        }
        private void BackgroundWorker16_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                richTextBox.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                richTextBox.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!
                richTextBox.Text = ProcessCreate.cmdOutput;
                oemUnlockButton.Text = "OEM Unlock";
                oemUnlockButton.Enabled = true;
            }
        }
        private void BackgroundWorker16_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.
                ProcessCreate.Command("fastboot oem unlock");
            }
        }
        private void BackgroundWorker15_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                richTextBox.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                richTextBox.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!

                string[] lines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                                                                StringSplitOptions.RemoveEmptyEntries);

                if (lines.Count() == 0)
                {
                    // Not Connected
                    richTextBox.Text = "Fastboot Device Not Found";
                    oemUnlockButton.Enabled = true;
                    oemUnlockButton.Text = "OEM Unlock";
                }
                else
                {
                    foreach (string line in lines)
                    {
                        if ((line.IndexOf("*") != 0) && (line.IndexOf(" ") != 4))
                        {
                            if (line.Contains("fastboot"))
                            {
                                // Connected

                                richTextBox.Text = "Fastboot Device Connected: " + line;
                                if (backgroundWorker16.IsBusy != true)
                                {
                                    // Start the asynchronous operation.

                                    backgroundWorker16.RunWorkerAsync();
                                }

                            }
                            else
                            {
                                // Not Connected
                                richTextBox.Text = "Fastboot Device Not Found";
                                oemUnlockButton.Enabled = true;
                                oemUnlockButton.Text = "OEM Unlock";
                            }
                        }
                    }
                }
            }
        }
        private void BackgroundWorker15_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.

                ProcessCreate.Command("fastboot devices");
            }
        }
        private void BackgroundWorker14_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                richTextBox.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                richTextBox.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!
                richTextBox.Text = ProcessCreate.cmdOutput;
                eraseRecoveryButton.Text = "Erase Recovery";
                eraseRecoveryButton.Enabled = true;
            }
        }
        private void BackgroundWorker14_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.
                ProcessCreate.Command("fastboot erase recovery");
            }
        }
        private void BackgroundWorker13_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                richTextBox.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                richTextBox.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!

                string[] lines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                                                                StringSplitOptions.RemoveEmptyEntries);

                if (lines.Count() == 0)
                {
                    // Not Connected
                    richTextBox.Text = "Fastboot Device Not Found";
                    eraseRecoveryButton.Enabled = true;
                    eraseRecoveryButton.Text = "Erase Recovery";
                }
                else
                {
                    foreach (string line in lines)
                    {
                        if ((line.IndexOf("*") != 0) && (line.IndexOf(" ") != 4))
                        {
                            if (line.Contains("fastboot"))
                            {
                                // Connected

                                richTextBox.Text = "Fastboot Device Connected: " + line;
                                if (backgroundWorker14.IsBusy != true)
                                {
                                    // Start the asynchronous operation.

                                    backgroundWorker14.RunWorkerAsync();
                                }

                            }
                            else
                            {
                                // Not Connected
                                richTextBox.Text = "Fastboot Device Not Found";
                                eraseRecoveryButton.Enabled = true;
                                eraseRecoveryButton.Text = "Erase Recovery";
                            }
                        }
                    }
                }
            }
        }
        private void BackgroundWorker13_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.

                ProcessCreate.Command("fastboot devices");
            }
        }
        private void BackgroundWorker12_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                richTextBox.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                richTextBox.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!
                richTextBox.Text = ProcessCreate.cmdOutput;
                eraseBootButton.Text = "Erase Boot";
                eraseBootButton.Enabled = true;
            }
        }
        private void BackgroundWorker12_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.
                ProcessCreate.Command("fastboot erase boot");
            }
        }
        private void BackgroundWorker11_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                richTextBox.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                richTextBox.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!

                string[] lines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                                                                StringSplitOptions.RemoveEmptyEntries);

                if (lines.Count() == 0)
                {
                    // Not Connected
                    richTextBox.Text = "Fastboot Device Not Found";
                    eraseBootButton.Enabled = true;
                    eraseBootButton.Text = "Erase Boot";
                }
                else
                {
                    foreach (string line in lines)
                    {
                        if ((line.IndexOf("*") != 0) && (line.IndexOf(" ") != 4))
                        {
                            if (line.Contains("fastboot"))
                            {
                                // Connected

                                richTextBox.Text = "Fastboot Device Connected: " + line;
                                if (backgroundWorker12.IsBusy != true)
                                {
                                    // Start the asynchronous operation.

                                    backgroundWorker12.RunWorkerAsync();
                                }

                            }
                            else
                            {
                                // Not Connected
                                richTextBox.Text = "Fastboot Device Not Found";
                                eraseBootButton.Enabled = true;
                                eraseBootButton.Text = "Erase Boot";
                            }
                        }
                    }
                }
            }
        }
        private void BackgroundWorker11_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.

                ProcessCreate.Command("fastboot devices");
            }
        }
        private void BackgroundWorker10_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                richTextBox.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                richTextBox.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!
                richTextBox.Text = ProcessCreate.cmdOutput;
                flashSystemButton.Text = "Flash System";
                flashSystemButton.Enabled = true;
            }
        }
        private void BackgroundWorker10_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.
                ProcessCreate.Command("fastboot flash system " + systemFile);
            }
        }
        private void BackgroundWorker9_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                richTextBox.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                richTextBox.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!

                string[] lines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                                                                StringSplitOptions.RemoveEmptyEntries);

                if (lines.Count() == 0)
                {
                    // Not Connected
                    richTextBox.Text = "Fastboot Device Not Found";
                    flashSystemButton.Enabled = true;
                    flashSystemButton.Text = "Flash System";
                }
                else
                {
                    foreach (string line in lines)
                    {
                        if ((line.IndexOf("*") != 0) && (line.IndexOf(" ") != 4))
                        {
                            if (line.Contains("fastboot"))
                            {
                                // Connected

                                richTextBox.Text = "Fastboot Device Connected: " + line;
                                if (backgroundWorker10.IsBusy != true)
                                {
                                    // Start the asynchronous operation.

                                    backgroundWorker10.RunWorkerAsync();
                                }

                            }
                            else
                            {
                                // Not Connected
                                richTextBox.Text = "Fastboot Device Not Found";
                                flashSystemButton.Enabled = true;
                                flashSystemButton.Text = "Flash System";
                            }
                        }
                    }
                }
            }
        }
        private void BackgroundWorker9_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.

                ProcessCreate.Command("fastboot devices");
            }
        }
        private void BackgroundWorker8_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                richTextBox.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                richTextBox.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!
                richTextBox.Text = ProcessCreate.cmdOutput;
                bootRecoveryButton.Text = "Boot Recovery";
                bootRecoveryButton.Enabled = true;
            }
        }
        private void BackgroundWorker8_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.
                ProcessCreate.Command("fastboot boot " + bootFile);
            }
        }
        private void BackgroundWorker7_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                richTextBox.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                richTextBox.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!

                string[] lines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                                                                StringSplitOptions.RemoveEmptyEntries);

                if (lines.Count() == 0)
                {
                    // Not Connected
                    richTextBox.Text = "Fastboot Device Not Found";
                    bootRecoveryButton.Enabled = true;
                    bootRecoveryButton.Text = "Boot Recovery";
                }
                else
                {
                    foreach (string line in lines)
                    {
                        if ((line.IndexOf("*") != 0) && (line.IndexOf(" ") != 4))
                        {
                            if (line.Contains("fastboot"))
                            {
                                // Connected

                                richTextBox.Text = "Fastboot Device Connected: " + line;
                                if (backgroundWorker8.IsBusy != true)
                                {
                                    // Start the asynchronous operation.

                                    backgroundWorker8.RunWorkerAsync();
                                }

                            }
                            else
                            {
                                // Not Connected
                                richTextBox.Text = "Fastboot Device Not Found";
                                bootRecoveryButton.Enabled = true;
                                bootRecoveryButton.Text = "Boot Recovery";
                            }
                        }
                    }
                }
            }
        }
        private void BackgroundWorker7_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.

                ProcessCreate.Command("fastboot devices");
            }
        }
        private void BackgroundWorker6_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                richTextBox.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                richTextBox.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!
                richTextBox.Text = ProcessCreate.cmdOutput;
                bootBootButton.Text = "Boot Boot";
                bootBootButton.Enabled = true;
            }
        }
        private void BackgroundWorker6_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.
                ProcessCreate.Command("fastboot boot " + bootFile);
            }
        }
        private void BackgroundWorker5_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                richTextBox.Text = "Canceled!";
                bootBootButton.Enabled = true;
                bootBootButton.Text = "Boot Boot";
            }
            else if (e.Error != null)
            {
                richTextBox.Text = "Error: " + e.Error.Message;
                bootBootButton.Enabled = true;
                bootBootButton.Text = "Boot Boot";
            }
            else
            {
                // Done!

                string[] lines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                                                                StringSplitOptions.RemoveEmptyEntries);

                if (lines.Count() == 0)
                {
                    // Not Connected
                    richTextBox.Text = "Fastboot Device Not Found";
                    bootBootButton.Enabled = true;
                    bootBootButton.Text = "Boot Boot";
                }
                else
                {
                    foreach (string line in lines)
                    {
                        if ((line.IndexOf("*") != 0) && (line.IndexOf(" ") != 4))
                        {
                            if (line.Contains("fastboot"))
                            {
                                // Connected

                                richTextBox.Text = "Fastboot Device Connected: " + line;
                                if (backgroundWorker6.IsBusy != true)
                                {
                                    // Start the asynchronous operation.

                                    backgroundWorker6.RunWorkerAsync();
                                }

                            }
                            else
                            {
                                // Not Connected
                                richTextBox.Text = "Fastboot Device Not Found";
                                bootBootButton.Enabled = true;
                                bootBootButton.Text = "Boot Boot";
                            }
                        }
                    }
                }
            }
        }
        private void BackgroundWorker5_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.

                ProcessCreate.Command("fastboot devices");
            }
        }
        private void BackgroundWorker4_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                richTextBox.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                richTextBox.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!
                richTextBox.Text = ProcessCreate.cmdOutput;
                flashRecoveryButton.Text = "Flash Recovery";
                flashRecoveryButton.Enabled = true;
            }
        }
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
                ProcessCreate.Command("fastboot flash recovery " + recoveryFile);
            }
        }
        private void BackgroundWorker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                richTextBox.Text = "Canceled!";
                flashRecoveryButton.Enabled = true;
                flashRecoveryButton.Text = "Flash Recovery";
            }
            else if (e.Error != null)
            {
                richTextBox.Text = "Error: " + e.Error.Message;
                flashRecoveryButton.Enabled = true;
                flashRecoveryButton.Text = "Flash Recovery";
            }
            else
            {
                // Done!

                string[] lines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                                                                StringSplitOptions.RemoveEmptyEntries);

                if (lines.Count() == 0)
                {
                    // Not Connected
                    richTextBox.Text = "Fastboot Device Not Found";
                    flashRecoveryButton.Enabled = true;
                    flashRecoveryButton.Text = "Flash Recovery";
                }
                else
                {
                    foreach (string line in lines)
                    {
                        if ((line.IndexOf("*") != 0) && (line.IndexOf(" ") != 4))
                        {
                            if (line.Contains("fastboot"))
                            {
                                // Connected

                                richTextBox.Text = "Fastboot Device Connected: " + line;
                                if (backgroundWorker4.IsBusy != true)
                                {
                                    // Start the asynchronous operation.

                                    backgroundWorker4.RunWorkerAsync();
                                }

                            }
                            else
                            {
                                // Not Connected
                                richTextBox.Text = "Fastboot Device Not Found";
                                flashRecoveryButton.Enabled = true;
                                flashRecoveryButton.Text = "Flash Recovery";
                            }
                        }
                    }
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

                ProcessCreate.Command("fastboot devices");
            }
        }
        private void BackgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                richTextBox.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                richTextBox.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!
                richTextBox.Text = ProcessCreate.cmdOutput;
                flashBootButton.Text = "Flash Boot";
                flashBootButton.Enabled = true;
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
                ProcessCreate.Command("fastboot flash boot " + bootFile);
            }
        }
        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                richTextBox.Text = "Canceled!";
                flashBootButton.Enabled = true;
                flashBootButton.Text = "Flash Boot";
            }
            else if (e.Error != null)
            {
                richTextBox.Text = "Error: " + e.Error.Message;
                flashBootButton.Enabled = true;
                flashBootButton.Text = "Flash Boot";
            }
            else
            {
                // Done!

                string[] lines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                                                                StringSplitOptions.RemoveEmptyEntries);

                if (lines.Count() == 0)
                {
                    // Not Connected
                    richTextBox.Text = "Fastboot Device Not Found";
                    flashBootButton.Enabled = true;
                    flashBootButton.Text = "Flash Boot";
                }
                else
                {
                    foreach (string line in lines)
                    {
                        if ((line.IndexOf("*") != 0) && (line.IndexOf(" ") != 4))
                        {
                            if (line.Contains("fastboot"))
                            {
                                // Connected

                                richTextBox.Text = "Fastboot Device Connected: " + line;
                                if (backgroundWorker2.IsBusy != true)
                                {
                                    // Start the asynchronous operation.

                                    backgroundWorker2.RunWorkerAsync();
                                }

                            }
                            else
                            {
                                // Not Connected
                                richTextBox.Text = "Fastboot Device Not Found";
                                flashBootButton.Enabled = true;
                                flashBootButton.Text = "Flash Boot";
                            }
                        }
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

                ProcessCreate.Command("fastboot devices");
            }
        }
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            tableLayoutPanel2.Size = form1.Size;
        }
        private void FlashBootButton_Click(object sender, EventArgs e)
        {
            FlashBoot();
        }
        private void FlashRecoveryButton_Click(object sender, EventArgs e)
        {
            FlashRecovery();
        }
        private void BootBootButton_Click(object sender, EventArgs e)
        {
            BootBoot();
        }
        private void BootRecoveryButton_Click(object sender, EventArgs e)
        {
            BootRecovery();
        }
        private void FlashSystemButton_Click(object sender, EventArgs e)
        {
            FlashSystem();
        }
        private void EraseBootButton_Click(object sender, EventArgs e)
        {
            EraseBoot();
        }
        private void EraseRecoveryButton_Click(object sender, EventArgs e)
        {
            EraseRecovery();
        }
        private void OemUnlockButton_Click(object sender, EventArgs e)
        {
            OEMUnlock();
        }
        private void OemUnlockCriticalButton_Click(object sender, EventArgs e)
        {
            OEMUnlockCritical();
        }
        private void FlashingUnlockCriticalButton_Click(object sender, EventArgs e)
        {
            FlashingUnlockCritical();
        }
        private void FlashingUnlockButton_Click(object sender, EventArgs e)
        {
            FlashingUnlock();
        }
        private void GetvarAllButton_Click(object sender, EventArgs e)
        {
            GetVarAll();
        }
    }
}
