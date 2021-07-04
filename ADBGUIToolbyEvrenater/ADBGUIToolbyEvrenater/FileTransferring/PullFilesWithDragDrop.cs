using ADBGUIToolbyEvrenater.ProcessCreating;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ADBGUIToolbyEvrenater.FileTransferring
{
    public static class PullFilesWithDragDrop
    {

        static Form transferForm;
        static TableLayoutPanel tableLayoutPanel;
        static Label statusLabel;
        static Button cancelButton;
        static BackgroundWorker backgroundWorker1;
        static ItemDragEventArgs itemDragEventArgs;

        public static string workingDirectory =
           /* Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + */"platform-tools\\ADBGUITool\\";

        static string fullPath = "Empty";

        public static void From(TreeNode thisNode, ItemDragEventArgs e)
        {
            CreateStatusForm();
            CopyFileToTempFolder(workingDirectory, thisNode);
            itemDragEventArgs = e;
        }

        public static void CopyFileToTempFolder(string workingDirectory, TreeNode node)
        {
            fullPath = GetTheFullPathOfTheNode(node);

            Debug.WriteLine("hasChildren" + node.GetNodeCount(true));

            #region Is The Node File 

            if (node.GetNodeCount(true) == 0) // Copy if only file
            {
                // Start Background Process
                if (backgroundWorker1.IsBusy != true)
                {
                    backgroundWorker1.RunWorkerAsync(); // DoWork() else Block
                }

            }
            #endregion


            Debug.WriteLine("fullFilePath:" + fullPath.Substring(0, (fullPath.Length - 1)) + ".");
            Debug.WriteLine("cmdOutput:" + ProcessCreate.cmdOutput + ".");
        }

        private static string GetTheFullPathOfTheNode(TreeNode node)
        {
            string path = "/" + node.Text.Trim() + "/";

            while (node.Parent != null)
            {
                path = "/" + node.Parent.Text.Trim() + path;
                node = node.Parent;
            }
            return path;
        }

        public static void FromTempFolderToDraggedAnywhere(ItemDragEventArgs e) // Folders Are Not Supporting
        {
            string[] files = new string[] { /*Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +*/
                "platform-tools\\ADBGUITool\\" + e.Item.ToString().Substring(e.Item.ToString().IndexOf(" ")).Trim() };



            //   IfBlock:
            Debug.WriteLine("Files0" + files[0]);
            if (File.Exists(files[0]))
            {
                Debug.WriteLine("File Exists");
                FileTransfer.treeView.DoDragDrop(new DataObject(DataFormats.FileDrop, files), DragDropEffects.Move);
            }
            else
            {
                Debug.WriteLine("Copy Filed");
                // System.Threading.Thread.Sleep(1000); // Wait for creating file
                // Debug.WriteLine("aftersleep");
                // goto IfBlock;

                // MessageBox.Show(FileTransfer.fileTransferForm, "Copy Failed\n" + 
                //    e.Item.ToString().Substring(e.Item.ToString().IndexOf(" ")).Trim(), "ADB GUI Tool - File Transfer");
            }

            statusLabel.Text = ProcessCreate.cmdOutput.Substring(ProcessCreate.cmdOutput.LastIndexOf(":") + 2);
            transferForm.Refresh();
            System.Threading.Thread.Sleep(500);
            transferForm.Close();
            System.Threading.Thread.Sleep(100);
            FileTransfer.fileTransferForm.Refresh();
            //FileTransfer.fileTransferForm.ShowDialog();
        }

        private static void CreateStatusForm()
        {
            transferForm = new Form();
            tableLayoutPanel = new TableLayoutPanel();
            statusLabel = new Label();
            cancelButton = new Button();
            backgroundWorker1 = new BackgroundWorker();

            backgroundWorker1.WorkerSupportsCancellation = true;
            statusLabel.AutoSize = true;
            cancelButton.AutoSize = true;
            cancelButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            transferForm.TopMost = true;

            transferForm.Text = "ADB GUI Tool - File Transfer";
            statusLabel.Text = "Copying...\nYour files are being copied, but it doesn't see any progress." +
                "\nIt may take a long time.";// \nIf you want to cancel it, you can use Task Manager.";
            cancelButton.Text = "Cancel";

            transferForm.Width = 370;
            transferForm.Height = 130;
            tableLayoutPanel.Size = transferForm.Size;


            tableLayoutPanel.Controls.Add(statusLabel);
            tableLayoutPanel.Controls.Add(cancelButton);
            transferForm.Controls.Add(tableLayoutPanel);

            transferForm.Layout += TransferForm_Layout;
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
            cancelButton.Click += CancelButton_Click;

            transferForm.Show();
            transferForm.Refresh();
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
                // Done! - Report Result
                FromTempFolderToDraggedAnywhere(itemDragEventArgs);
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

                ProcessCreate.Command("adb pull " +
                    fullPath.Substring(0, (fullPath.Length - 1)) + " " + workingDirectory);
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
