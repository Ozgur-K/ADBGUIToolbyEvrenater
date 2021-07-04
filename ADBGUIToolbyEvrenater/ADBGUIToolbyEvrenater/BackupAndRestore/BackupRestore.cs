using ADBGUIToolbyEvrenater.ProcessCreating;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ADBGUIToolbyEvrenater.BackupAndRestore
{
    class BackupRestore
    {
        Form form1;
        Button backupButton, restoreButton, cancelBackupButton, cancelRestoreButton;
        Label resultLabel;
        TableLayoutPanel tableLayoutPanel1;
        BackgroundWorker backgroundWorker1, backgroundWorker2;

        string backupFile;
        public BackupRestore()
        {
            form1 = new();
            backupButton = new();
            restoreButton = new();
            cancelBackupButton = new();
            cancelRestoreButton = new();
            resultLabel = new();
            tableLayoutPanel1 = new();
            backgroundWorker1 = new(); // For Backup
            backgroundWorker2 = new(); // For Restore

            form1.SuspendLayout();

            form1.Text = "Backup And Restore";
            backupButton.Text = "Backup";
            restoreButton.Text = "Restore";
            cancelBackupButton.Text = "Cancel Backup";
            cancelRestoreButton.Text = "Cancel Restore";
            resultLabel.Text = "Backup Location Documents\\backup.ab\r\nBackup Contains: -all -apk";

            backupButton.AutoSize = true;
            restoreButton.AutoSize = true;
            cancelBackupButton.AutoSize = true;
            cancelRestoreButton.AutoSize = true;
            resultLabel.AutoSize = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker2.WorkerSupportsCancellation = true;
            cancelBackupButton.Enabled = false;
            cancelRestoreButton.Enabled = false;

            backupButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            restoreButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cancelBackupButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cancelRestoreButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.Size = form1.Size;
            backupButton.Click += BackupButton_Click;
            restoreButton.Click += RestoreButton_Click;
            cancelBackupButton.Click += CancelBackupButton_Click;
            cancelRestoreButton.Click += CancelRestoreButton_Click;
            resultLabel.SizeChanged += ResultLabel_SizeChanged;
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
            backgroundWorker2.DoWork += BackgroundWorker2_DoWork;
            backgroundWorker2.RunWorkerCompleted += BackgroundWorker2_RunWorkerCompleted;

            tableLayoutPanel1.Controls.AddRange(new Control[] { resultLabel, backupButton, cancelBackupButton,
                                                    restoreButton, cancelRestoreButton});
            form1.Controls.Add(tableLayoutPanel1);
            form1.ResumeLayout();
            form1.ShowDialog();

        }

        private void Backup()
        {
            backupButton.Text = "Check Your Phone...";
            backupButton.Enabled = false;
            cancelBackupButton.Enabled = true;

            if (backgroundWorker1.IsBusy != true)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }
        private void Restore()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Android Backup Files (*.ab)|*.ab";
            DialogResult dialogResult = openFileDialog.ShowDialog(form1);
            if (dialogResult.Equals(DialogResult.OK))
            {

                backupFile = openFileDialog.FileName;
                restoreButton.Text = "Check Your Phone...";
                restoreButton.Enabled = false;
                cancelRestoreButton.Enabled = true;

                if (backgroundWorker2.IsBusy != true)
                {
                    backgroundWorker2.RunWorkerAsync();
                }
            }
        }

        #region Events
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
                ProcessCreate.Command("adb backup -all -apk -f "
                    + Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\backup.ab");
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
                backupButton.Text = "Backup";
                backupButton.Enabled = true;
                cancelBackupButton.Enabled = false;
                string[] lines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                                                                 StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    if ((line.IndexOf('a') == 0) && (line.IndexOf('d') == 1)
                        && (line.IndexOf('b') == 2) && (line.IndexOf(':') == 3))
                    {
                        resultLabel.Text = line;
                    }
                }
            }
        }
        private void CancelBackupButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                backgroundWorker1.CancelAsync();
                DialogResult dialogResult = DialogResult.Yes;
                dialogResult = MessageBox.Show(form1, "You have to reconnect your device over Wi-Fi " +
                                    "or physically reconnect over USB after cancellation. Do you want to cancel?",
                                    "Terminate", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult.Equals(DialogResult.No))
                {
                }
                else
                {
                    ProcessCreate.Command2("adb kill-server");
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
                ProcessCreate.Command("adb restore " + backupFile);
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
                restoreButton.Text = "Restore";
                restoreButton.Enabled = true;
                cancelRestoreButton.Enabled = false;
                string[] lines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                                                                 StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    if ((line.IndexOf('a') == 0) && (line.IndexOf('d') == 1)
                        && (line.IndexOf('b') == 2) && (line.IndexOf(':') == 3))
                    {
                        resultLabel.Text = line;
                    }
                }
            }
        }
        private void CancelRestoreButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker2.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                backgroundWorker2.CancelAsync();
                DialogResult dialogResult = DialogResult.Yes;
                dialogResult = MessageBox.Show(form1, "You have to reconnect your device over Wi-Fi " +
                                    "or physically reconnect over USB after cancellation. Do you want to cancel?",
                                    "Terminate", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult.Equals(DialogResult.No))
                {
                }
                else
                {
                    ProcessCreate.Command2("adb kill-server");
                }
            }
        }
        private void BackupButton_Click(object sender, EventArgs e)
        {
            Backup();
        }
        private void RestoreButton_Click(object sender, EventArgs e)
        {
            Restore();
        }
        private void ResultLabel_SizeChanged(object sender, EventArgs e)
        {
            tableLayoutPanel1.Width = resultLabel.Width;
        }
        #endregion
    }
}
