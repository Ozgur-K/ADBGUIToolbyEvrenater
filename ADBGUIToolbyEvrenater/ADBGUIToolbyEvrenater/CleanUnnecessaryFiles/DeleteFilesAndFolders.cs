using System;
using System.Diagnostics;
using System.IO;

namespace ADBGUIToolbyEvrenater.CleanUnnecessaryFiles
{
    public static class DeleteFilesAndFolders
    {
        private static string workingDirectory = /*Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                + */"platform-tools\\ADBGUITool\\";

        public static void Delete()
        {
            string[] folders = Directory.GetDirectories(workingDirectory);
            string[] files = Directory.GetFiles(workingDirectory);

            try
            {
                foreach (string folder in folders)
                {
                    Directory.Delete(folder, true);
                }
                foreach (string file in files)
                {
                    File.Delete(file);
                    Debug.WriteLine("Deleting " + file);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}
