using ADBGUIToolbyEvrenater.ProcessCreating;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace ADBGUIToolbyEvrenater.FileTransferring
{
    static class AddNodes
    {
        public static List<TreeNode> treeNodeDirList;
        public static List<TreeNode> treeNodeLinkList;

        public static void ToTreeView(List<string> folders, List<string> files, List<string> links,
           List<string> denieds)
        {
            #region Add folders to treeNodeFileList and TreeView
            treeNodeDirList = new List<TreeNode>();


            foreach (string folder in folders)
            {
                treeNodeDirList.Add(new TreeNode(folder));
            }
            foreach (TreeNode treeNode in treeNodeDirList)
            {
                AddDummyFileInDirectory(treeNode);
                if (treeNode.Text.Trim() == "storage") // Replace Internal and External Storage First. Trim Req.
                {
                    FileTransfer.treeView.Nodes.Insert(0, treeNode);
                }
                else
                {
                    FileTransfer.treeView.Nodes.Add(treeNode);
                }
            }
            #endregion

            #region Add links to treeNodeLinkList and TreeView

            treeNodeLinkList = new List<TreeNode>();
            foreach (string link in links)
            {
                treeNodeLinkList.Add(new TreeNode(link));
            }
            foreach (TreeNode treeNode in treeNodeLinkList)
            {
                // if (IsAccesibleDir(treeNode))     // LINKS ARE PROBLEM           
                AddDummyFileInDirectory(treeNode);

                if (treeNode.Text.Trim() == "sdcard") // Replace Internal Storage First.
                {
                    FileTransfer.treeView.Nodes.Insert(0, treeNode);
                }
                else
                {
                    FileTransfer.treeView.Nodes.Add(treeNode);
                }
            }
            #endregion

            #region Add files to treeNodeFileList and TreeView

            List<TreeNode> treeNodeFileList = new List<TreeNode>();
            foreach (string file in files)
            {
                treeNodeFileList.Add(new TreeNode(file));
            }

            foreach (TreeNode treeNode in treeNodeFileList)
            {
                FileTransfer.treeView.Nodes.Add(treeNode);
            }
            #endregion

            #region Add denieds to treeNodeDeniedList and TreeView

            List<TreeNode> treeNodeDeniedList = new List<TreeNode>();

            foreach (string denied in denieds)
            {
                treeNodeDeniedList.Add(new TreeNode(denied));
            }
            foreach (TreeNode treeNode in treeNodeDeniedList)
            {
                FileTransfer.treeView.Nodes.Add(treeNode);
            }
            #endregion

        }



        public static bool IsAccessibleDir(TreeNode treeNode)
        {
            Debug.WriteLine("IsAccesibleDir-in");
            ProcessCreate.Command("adb shell ls -l /" + treeNode.Text.Trim() + "/");

            if (ProcessCreate.cmdOutput.Contains("denied") || ProcessCreate.cmdOutput.Contains("Not a"))
            {
                Debug.WriteLine("IsAccesibleDir-out");
                return false;
            }
            else
            {
                Debug.WriteLine("IsAccesibleDir-out");
                return true;
            }
        }

        public static void ToSelectedSubDirectory(TreeNode addToThisNode)
        {
            // (Add Nodes To SelectedSubDirectory) - This method called from TreeNode Expand Event  
            // Get Parent Node
            // Get SubDirectory Content
            // Add SubDirectory Content to Parent Node

            TreeNode expandedFolderNode = addToThisNode;
            List<TreeNode> subFolderNodeList = new List<TreeNode>();
            List<TreeNode> subLinkNodeList = new List<TreeNode>();
            List<TreeNode> subFileNodeList = new List<TreeNode>();
            List<TreeNode> subDeniedNodeList = new List<TreeNode>();

            expandedFolderNode.Nodes.Clear(); // avoid adding multiple times

            #region Get Subdirectory Content

            try
            {
                GetDirectoryContents.GetFromHere(GetFullPath(addToThisNode));
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            #endregion

            #region Add Folder Name To TreeNode List From String Folder List
            foreach (string folder in GetDirectoryContents.folders)
            {
                subFolderNodeList.Add(new TreeNode(folder));
            }
            #endregion
            #region Add Link Name To TreeNode List From String Link List
            foreach (string link in GetDirectoryContents.links)
            {
                subLinkNodeList.Add(new TreeNode(link));
            }
            #endregion
            #region Add File Name To TreeNode List From String File List
            foreach (string file in GetDirectoryContents.files)
            {
                subFileNodeList.Add(new TreeNode(file));
            }
            #endregion
            #region Add Denied Name To TreeNode List From String Denied List
            foreach (string denied in GetDirectoryContents.denieds)
            {
                subDeniedNodeList.Add(new TreeNode(denied));
            }
            #endregion

            #region Add SubNodes To Parent Node And If The SubNodes Is A Folder, Add Dummy File
            foreach (TreeNode nodeFolder in subFolderNodeList)
            {
                expandedFolderNode.Nodes.Add(nodeFolder);
                AddDummyFileInDirectory(nodeFolder);
            }
            foreach (TreeNode nodeList in subLinkNodeList)
            {
                expandedFolderNode.Nodes.Add(nodeList);
                AddDummyFileInDirectory(nodeList);
            }
            foreach (TreeNode nodeFile in subFileNodeList)
            {
                expandedFolderNode.Nodes.Add(nodeFile);
            }
            foreach (TreeNode nodeDenied in subDeniedNodeList)
            {
                expandedFolderNode.Nodes.Add(nodeDenied);
            }
            #endregion

        }

        public static string GetFullPath(TreeNode node)
        {
            string path = "/" + node.Text.Trim() + "/";

            while (node.Parent != null)
            {
                path = "/" + node.Parent.Text.Trim() + path;
                node = node.Parent;
            }

            return path;
        }

        #region Add Dummy File In Directories For Expandable - Required Foreach Loop For TreeNode List
        public static void AddDummyFileInDirectory(TreeNode addToThisTreeNode)
        {
            if (addToThisTreeNode.Text.Trim() == addToThisTreeNode.Text.Trim())
            {
                TreeNode selectectedNode = addToThisTreeNode;
                selectectedNode.Nodes.Add("");
            }
        }
        #endregion



    }
}
