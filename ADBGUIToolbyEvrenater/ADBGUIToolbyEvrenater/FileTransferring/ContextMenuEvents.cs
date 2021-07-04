using ADBGUIToolbyEvrenater.ProcessCreating;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace ADBGUIToolbyEvrenater.FileTransferring
{
    public static class ContextMenuEvents
    {
        private static Form transferForm;
        private static TableLayoutPanel tableLayoutPanel;
        private static Label statusLabel;
        private static BackgroundWorker backgroundWorker1, backgroundWorker2, backgroundWorker3;
        private static TreeNode treeNode1, treeNode2;
        private static Button cancelButton;

        private static string pasteFullPath;

        private static string[] files, files3;
        private static string workingDirectory;
        public static void Delete(TreeNode treeNode)
        {
            treeNode1 = treeNode;
            if (FileTransfer.treeView.SelectedNode != null)
            {

                DialogResult dialogResult = MessageBox.Show(FileTransfer.fileTransferForm,
                    "Do you want to delete " + treeNode.Text.Trim() + "?", "ADB GUI Tool - File Transfer", MessageBoxButtons.YesNo);
                if (dialogResult.Equals(DialogResult.Yes))
                {
                    Debug.WriteLine("Yes");

                    CreateStatusForm("Deleting...");


                    // Start Background Process
                    if (backgroundWorker1.IsBusy != true)
                    {
                        backgroundWorker1.RunWorkerAsync(); // DoWork() else Block
                    }


                }
                else if (dialogResult.Equals(DialogResult.No))
                {
                    Debug.WriteLine("No");
                }
            }
            else
            {
                MessageBox.Show(FileTransfer.fileTransferForm, "Select a Item", "ADB GUI Tool - File Transfer");
            }
        }
        public static void Paste(TreeNode treeNode)
        {
            if (FileTransfer.treeView.SelectedNode != null)
            {
                if (treeNode.Nodes.Count > 0)
                {
                    files = (string[])Clipboard.GetDataObject().GetData(DataFormats.FileDrop);
                    if (files != null)
                    {
                        DialogResult dialogResult = MessageBox.Show(FileTransfer.fileTransferForm,
                                            "Do you want to paste " + files.Length + " file(s)to " + treeNode.Text.Trim() + "?",
                                            "ADB GUI Tool - File Transfer", MessageBoxButtons.YesNo);
                        if (dialogResult.Equals(DialogResult.Yes))
                        {
                            Debug.WriteLine("Yes");

                            CreateStatusForm("Copying...\n" +
                                "Your files are being copied, but it doesn't see any progress." +
                                "\nIt may take a long time.");

                            pasteFullPath = GetFullPath(treeNode);

                            // Start Background Process
                            if (backgroundWorker2.IsBusy != true)
                            {
                                backgroundWorker2.RunWorkerAsync(); // DoWork() else Block
                            }


                            // even if copy 4 files, output says 1 file pushed because not +=

                        }
                        else if (dialogResult.Equals(DialogResult.No))
                        {
                            Debug.WriteLine("no");
                        }
                    }
                    else
                    {
                        MessageBox.Show(FileTransfer.fileTransferForm, "Clipboard Is Empty", "ADB GUI Tool - File Transfer");
                    }
                }
                else
                {
                    MessageBox.Show(FileTransfer.fileTransferForm, "Select a Folder", "ADB GUI Tool - File Transfer");
                }
            }
            else
            {
                MessageBox.Show(FileTransfer.fileTransferForm, "Select a Folder", "ADB GUI Tool - File Transfer");
            }


        }
        public static void Copy(TreeNode treeNode)
        {
            if (FileTransfer.treeView.SelectedNode != null)
            {
                #region Copy To Temp Folder
                if (!FileTransfer.treeView.SelectedNode.Text.Contains("Inaccessible")
                    && !FileTransfer.treeView.SelectedNode.Text.Contains("Empty")
                    && !FileTransfer.treeView.SelectedNode.Text.Contains("No Devices Found"))
                {
                    workingDirectory = /*Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + */"platform-tools\\ADBGUITool\\";

                    CreateStatusForm("Copying To Clipboard...\n" +
                                     "Your files are being copied, but it doesn't see any progress." +
                                     "\nIt may take a long time.");

                    treeNode2 = treeNode;

                    // Start Background Process
                    if (backgroundWorker3.IsBusy != true)
                    {
                        backgroundWorker3.RunWorkerAsync(); // DoWork() else Block
                    }

                }
                #endregion
                string copiedItem = treeNode.Nodes.Count > 0 ? treeNode.Text.Trim() + "\\" : treeNode.Text.Trim();
                Debug.WriteLine("coppied item on temp:" + copiedItem + ".");
                string[] files = { workingDirectory + treeNode.Text.Trim() };
                files3 = files;
                Clipboard.SetData(DataFormats.FileDrop, files3);
            }
            else
            {
                MessageBox.Show(FileTransfer.fileTransferForm, "Select a Item", "ADB GUI Tool - File Transfer");
            }
        }
        private static string GetFullPath(TreeNode treeNode) // Checks folder or file
        {
            if (treeNode.Nodes.Count > 0)
                return AddNodes.GetFullPath(treeNode);
            else
                return AddNodes.GetFullPath(treeNode).Substring(0, (AddNodes.GetFullPath(treeNode).Length - 1));
        }
        private static void CreateStatusForm(string text) // Make reusable
        {
            transferForm = new Form();
            tableLayoutPanel = new TableLayoutPanel();
            statusLabel = new Label();
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker2 = new BackgroundWorker();
            backgroundWorker3 = new BackgroundWorker();
            cancelButton = new Button();

            statusLabel.AutoSize = true;
            transferForm.TopMost = true;
            cancelButton.AutoSize = true;
            cancelButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker2.WorkerSupportsCancellation = true;
            backgroundWorker3.WorkerSupportsCancellation = true;

            transferForm.Text = "ADB GUI Tool - File Transfer";
            statusLabel.Text = text;
            cancelButton.Text = "Cancel";

            transferForm.Width = 370;
            transferForm.Height = 130;
            tableLayoutPanel.Size = transferForm.Size;

            cancelButton.Click += CancelButton_Click;
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
            backgroundWorker2.DoWork += BackgroundWorker2_DoWork;
            backgroundWorker2.RunWorkerCompleted += BackgroundWorker2_RunWorkerCompleted;
            backgroundWorker3.DoWork += BackgroundWorker3_DoWork;
            backgroundWorker3.RunWorkerCompleted += BackgroundWorker3_RunWorkerCompleted;


            tableLayoutPanel.Controls.Add(statusLabel);
            tableLayoutPanel.Controls.Add(cancelButton);
            transferForm.Controls.Add(tableLayoutPanel);

            transferForm.Layout += TransferForm_Layout;

            transferForm.Show();
            transferForm.Refresh();
        }
        private static void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = DialogResult.Yes;
            // Cancel the asynchronous operation.
            dialogResult = MessageBox.Show(transferForm, "You have to reconnect your device over Wi-Fi " +
                                "or physically reconnect over USB after cancellation. Do you want to cancel?",
                                "Terminate", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);




            if (dialogResult.Equals(DialogResult.No))
            {
            }
            else
            {
                backgroundWorker1.CancelAsync();
                backgroundWorker2.CancelAsync();
                backgroundWorker3.CancelAsync();
                ProcessCreate.Command2("adb kill-server");
            }
        }
        private static void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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

                statusLabel.Text = ProcessCreate.cmdOutput;
                transferForm.Refresh();
                System.Threading.Thread.Sleep(1500);
                transferForm.Close();


            }
        }
        private static void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and...


                ProcessCreate.Command("adb shell rm -r " + GetFullPath(treeNode1));
            }
        }
        private static void BackgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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

                statusLabel.Text = ProcessCreate.cmdOutput;//.Substring(ProcessCreate.cmdOutput.LastIndexOf(":") + 2);
                transferForm.Refresh();
                System.Threading.Thread.Sleep(1500);
                transferForm.Close();
                System.Threading.Thread.Sleep(100);
                FileTransfer.fileTransferForm.Refresh();
            }
        }
        private static void BackgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and...
                foreach (string file in files)
                {
                    ProcessCreate.Command("adb push " + file + " " + pasteFullPath);
                }

            }
        }
        private static void BackgroundWorker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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

                statusLabel.Text = ProcessCreate.cmdOutput.Substring(ProcessCreate.cmdOutput.LastIndexOf(":") + 2); ;
                transferForm.Refresh();
                System.Threading.Thread.Sleep(1500);
                transferForm.Close();
                System.Threading.Thread.Sleep(100);
                FileTransfer.fileTransferForm.Refresh();
            }
        }

        private static void BackgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and...
                foreach (string file in files3)
                {
                    ProcessCreate.Command("adb pull " + GetFullPath(treeNode2) + " " + workingDirectory);
                }

            }

        }

        private static void TransferForm_Layout(object sender, LayoutEventArgs e)
        {
            tableLayoutPanel.Size = transferForm.Size;
        }
    }
}
