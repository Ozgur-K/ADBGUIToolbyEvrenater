using ADBGUIToolbyEvrenater.ProcessCreating;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ADBGUIToolbyEvrenater.FileTransferring
{
    class GetDirectoryContents
    {
        public static List<string> folders;
        public static List<string> files;
        public static List<string> links;
        public static List<string> denieds;

        public GetDirectoryContents()
        {

        }
        public static void GetFromHere(string getFrom)
        {
            ProcessCreate.Command("adb shell ls -l " + getFrom);
            string[] filesAndFoldersLines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                StringSplitOptions.RemoveEmptyEntries);

            folders = new List<string>();
            files = new List<string>();
            links = new List<string>();
            denieds = new List<string>();
            foreach (string line in filesAndFoldersLines)
            {
                if (line.IndexOf('d') == 0)
                {
                    folders.Add(line.Substring(line.LastIndexOf(" ") + 1));
                }
                else if (line.IndexOf('-') == 0)
                {
                    files.Add(line.Substring(line.LastIndexOf(" ") + 1));
                }
                else if (line.IndexOf('l') == 0 && line.IndexOf('r') == 1 && line.IndexOf('w') == 2)
                {
                    links.Add(line.Substring(line.LastIndexOf(":") + 4,
                        ((line.LastIndexOf(">") - 1) - line.LastIndexOf(":") - 4)).Trim());
                }
                else if (line.IndexOf('*') == 0)
                {
                    Debug.WriteLine("error-warning: " + line);
                }
                else if (line.IndexOf('l') == 0 && line.IndexOf('s') == 1 && line.IndexOf('t') == 2)
                {
                    denieds.Add("(Inaccessible) " + line.Substring(line.LastIndexOf("/") + 1,
                        line.LastIndexOf("'") - (line.LastIndexOf("/") + 1)));
                }
                else if (line.IndexOf('l') == 0 && line.IndexOf('s') == 1 && line.IndexOf(':') == 2)
                {
                    denieds.Add("(Inaccessible) " + line.Substring((line.IndexOf("/") + 1),
                                (line.LastIndexOf(":")) - (line.IndexOf("/") + 1)));

                    Debug.WriteLine("ıncondition" + line);
                    //SendError(line, files);
                }
                else if (line.Contains("total 0"))
                {
                    SendError("empty", files);
                }
                else
                {
                    SendError(line, files);
                    Debug.WriteLine("File Read Error: not dir, lstat, file, link or * (in GetDirectoryContent.GetFromHere)");
                    Debug.WriteLine("File Read Error: line: " + line + "lineEnd");
                }
            }
            // empty folder control - foreach outside method inside 
            if (filesAndFoldersLines.Length == 0)
            {
                Debug.WriteLine("InCondition-length:" +
                    "" + filesAndFoldersLines.Length + ".");
                SendError("empty", files);
            }
        }

        public static void SendError(string line, List<string> files)
        {
            if (line.Contains("no devices"))
            {
                FileTransfer.treeView.Nodes.Clear();
                FileTransfer.treeView.Nodes.Add("No Devices Found");
            }
            else if (line.Contains("Permission denied"))
            {
                files.Add("Inaccessible");
            }
            else if (line.Contains("empty"))
            {
                files.Add("Empty");
            }
        }
    }
}
