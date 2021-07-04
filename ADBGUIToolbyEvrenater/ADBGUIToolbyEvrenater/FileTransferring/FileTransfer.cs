using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ADBGUIToolbyEvrenater.FileTransferring
{
    class FileTransfer
    {
        public static Form fileTransferForm;
        public static TreeView treeView;
        ContextMenuStrip cMS;
        Size size1 = new Size(300, 600);

        public FileTransfer()
        {
            fileTransferForm = new Form();
            treeView = new TreeView();
            cMS = new ContextMenuStrip();

            fileTransferForm.Text = "Loading...";

            cMS.Items.Add("Copy");
            cMS.Items.Add("Paste");
            cMS.Items.Add("Delete");

            treeView.ContextMenuStrip = cMS;
            fileTransferForm.Size = size1;
            treeView.Size = new Size(fileTransferForm.Size.Width, fileTransferForm.Size.Height - 50); // For 1366x768

            treeView.AllowDrop = true;

            fileTransferForm.Shown += new EventHandler(fileTransferForm_Load);
            fileTransferForm.Layout += new LayoutEventHandler(fileTransferForm_LayoutEvent);
            treeView.DragEnter += new DragEventHandler(treeView_DragEnter);
            treeView.DragDrop += new DragEventHandler(treeView_DragDrop);
            treeView.NodeMouseClick += new TreeNodeMouseClickEventHandler(treeView_NodeMouseClick);
            treeView.NodeMouseHover += new TreeNodeMouseHoverEventHandler(treeView_NodeMouseHover);
            treeView.MouseDown += new MouseEventHandler(treeView_MouseDown);
            treeView.AfterExpand += TreeView_AfterExpand;
            treeView.ItemDrag += TreeView_ItemDrag;
            cMS.ItemClicked += CMS_ItemClicked;
            treeView.KeyDown += TreeView_KeyDown;


            fileTransferForm.Controls.Add(treeView);

            fileTransferForm.ShowDialog();
        }

        public void fileTransferForm_Load(object sender, EventArgs e)
        {
            GetDirectoryContents.GetFromHere("/");

            AddNodes.ToTreeView(GetDirectoryContents.folders, GetDirectoryContents.files,
                GetDirectoryContents.links, GetDirectoryContents.denieds);


            fileTransferForm.Text = "File Transfer";
        }

        #region Events

        private void TreeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Delete))
                ContextMenuEvents.Delete(treeView.SelectedNode);
            if (e.KeyData.Equals(Keys.Control | Keys.C))
                ContextMenuEvents.Copy(treeView.SelectedNode);
            if (e.KeyData.Equals(Keys.Control | Keys.V))
                ContextMenuEvents.Paste(treeView.SelectedNode);
        }
        private void CMS_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                switch (e.ClickedItem.ToString().Trim())
                {
                    case "Delete":
                        ContextMenuEvents.Delete(treeView.SelectedNode);
                        break;
                    case "Paste":
                        ContextMenuEvents.Paste(treeView.SelectedNode);
                        break;
                    case "Copy":
                        ContextMenuEvents.Copy(treeView.SelectedNode);
                        break;
                }
            }
            else
            {
                Debug.WriteLine("treeNodeNull-FileTransfer-CMSItemClicked");
            }
        }

        private void TreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            treeView.SelectedNode = (TreeNode)e.Item;

            if (treeView.SelectedNode.GetNodeCount(true) == 0
                && !treeView.SelectedNode.Text.Contains("Inaccessible")
                && !treeView.SelectedNode.Text.Contains("Empty")
                && !treeView.SelectedNode.Text.Contains("No Devices Found"))
            {
                #region Cursor Setting
                [DllImport("kernel32.dll")]
                static extern IntPtr LoadLibrary(string dllToLoad);

                [DllImport("user32.dll")]
                static extern IntPtr LoadCursor(IntPtr hInstance, UInt16 lpCursorName);

                var lL = LoadLibrary("ole32.dll");
                var lC = LoadCursor(lL, 3);
                #endregion

                // fileTransferForm.Cursor = new Cursor(lC);

                fileTransferForm.Cursor = Cursors.WaitCursor;

                PullFilesWithDragDrop.From(treeView.SelectedNode, e);

                fileTransferForm.Cursor = Cursors.Default;

                //PullFilesWithDragDrop.FromTempFolderToDraggedAnywhere(e);
            }



            Debug.WriteLine("ItemDragEnd");
        }
        public void treeView_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            //treeView.SelectedNode = e.Node;
        }

        public void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Debug.WriteLine("TreeNodeMouseClickEvent, Clicked Node: " + e.Node.Text.Trim());
        }

        private void TreeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            treeView.SelectedNode = e.Node;

            Debug.WriteLine("AfterExpandEvent, seçili node: " + treeView.SelectedNode.Text.Trim() + " expanded");

            treeView.SelectedNode.Nodes.RemoveAt(0);

            AddNodes.ToSelectedSubDirectory(treeView.SelectedNode);
        }

        public void fileTransferForm_LayoutEvent(object sender, LayoutEventArgs e)
        {
            treeView.Size = new Size(fileTransferForm.Size.Width, fileTransferForm.Size.Height - 50); // For 1366x768
        }

        public void treeView_MouseDown(object sender, MouseEventArgs e)
        {

            Debug.WriteLine("MouseDownEvent");

        }
        private void treeView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            PushFilesWithDragDrop.ToPhone(e);
        }
        #endregion
    }
}

