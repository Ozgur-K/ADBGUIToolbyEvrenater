using ADBGUIToolbyEvrenater.ProcessCreating;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace ADBGUIToolbyEvrenater.FileTransferring
{
    public static class PushFilesWithDragDrop
    {
        static Form transferForm;
        static TableLayoutPanel tableLayoutPanel;
        static Label statusLabel;
        static BackgroundWorker backgroundWorker1;
        static Button cancelButton;
        static TreeNode to1;

        static string[] from1;
        public static void ToPhone(DragEventArgs e)
        {
            if (FileTransfer.treeView.SelectedNode != null &&
                FileTransfer.treeView.SelectedNode.Nodes.Count > 0)
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    Debug.WriteLine(FileTransfer.treeView.SelectedNode.Text.Trim() + "için" + file + ".");
                }

                DialogResult dialogResult =
                    MessageBox.Show(FileTransfer.fileTransferForm,
                    "Do you wanna copy " + files.Length + " item(s) to '" + FileTransfer.treeView.SelectedNode.Text.Trim() + "'?",
                    "ADB GUI Tool - File Transfer", MessageBoxButtons.OKCancel);


                // MessageBox.Show()

                if (dialogResult.Equals(DialogResult.OK))
                {

                    Send(files, FileTransfer.treeView.SelectedNode);

                    Debug.WriteLine("dialog result ok");
                }
                else
                {
                    Debug.WriteLine("dialog result cancel");
                }
            }
            else
            {

                MessageBox.Show(FileTransfer.fileTransferForm, "Select a Folder", "ADB GUI Tool - File Transfer");

            }
        }

        private static void Send(string[] from, TreeNode to)
        {
            from1 = from;
            to1 = to;

            CreateStatusForm();

            // Start Background Process
            if (backgroundWorker1.IsBusy != true)
            {
                backgroundWorker1.RunWorkerAsync(); // DoWork() else Block
            }




        }

        private static void CreateStatusForm() // Make reusable
        {
            transferForm = new Form();
            tableLayoutPanel = new TableLayoutPanel();
            statusLabel = new Label();
            backgroundWorker1 = new BackgroundWorker();
            cancelButton = new Button();

            backgroundWorker1.WorkerSupportsCancellation = true;
            statusLabel.AutoSize = true;
            transferForm.TopMost = true;
            cancelButton.AutoSize = true;
            cancelButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            transferForm.Text = "ADB GUI Tool - File Transfer";
            statusLabel.Text = "Copying...\nYour files are being copied, but it doesn't see any progress." +
                "\nIt may take a long time.";
            cancelButton.Text = "Cancel";

            transferForm.Width = 370;
            transferForm.Height = 130;
            tableLayoutPanel.Size = transferForm.Size;


            tableLayoutPanel.Controls.Add(statusLabel);
            tableLayoutPanel.Controls.Add(cancelButton);
            transferForm.Controls.Add(tableLayoutPanel);

            transferForm.Layout += TransferForm_Layout;
            cancelButton.Click += CancelButton_Click;
            backgroundWorker1.DoWork += BackgroundWorker_DoWork;
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;

            transferForm.Show();
            transferForm.Refresh();
        }

        private static void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                statusLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                statusLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!
                statusLabel.Text = ProcessCreate.cmdOutput;//.Substring(ProcessCreate.cmdOutput.LastIndexOf(":"));// + 2);
                transferForm.Refresh();
                System.Threading.Thread.Sleep(700);
                transferForm.Close();
                System.Threading.Thread.Sleep(100);
                FileTransfer.fileTransferForm.Refresh();
            }
        }

        private static void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and...

                Debug.WriteLine("Perform a time consuming operation");

                foreach (string file in from1)
                {
                    ProcessCreate.Command("adb push \"" + file + "\" " + AddNodes.GetFullPath(to1));

                }
            }
        }

        private static void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = DialogResult.Yes;
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                dialogResult = MessageBox.Show(transferForm, "You have to reconnect your device over Wi-Fi " +
                                    "or physically reconnect over USB after cancellation. Do you want to cancel?",
                                    "Terminate", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);


                backgroundWorker1.CancelAsync();
            }
            if (dialogResult.Equals(DialogResult.No))
            {
            }
            else
            {
                ProcessCreate.Command2("adb kill-server");
            }
        }

        private static void TransferForm_Layout(object sender, LayoutEventArgs e)
        {
            tableLayoutPanel.Size = transferForm.Size;
        }
    }
}

