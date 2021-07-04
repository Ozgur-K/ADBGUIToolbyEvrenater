using System;
using System.Diagnostics;
using System.IO;

namespace ADBGUIToolbyEvrenater.ProcessCreating
{
    public static class ProcessCreate
    {
        private static Process process1 = new Process();

        private static string cmdOutputTextFile = /*Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                                                    +*/ "platform-tools\\ADBGUITool\\adb-temp-output.txt";
        private static string workingDirectory = /*Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                                                    + */"platform-tools\\";

        public static string cmdOutput = "CMD Output Unassigned";


        public static void Command(string command)
        {
            try
            {

                process1.StartInfo.FileName = "cmd.exe";
                process1.StartInfo.WorkingDirectory = workingDirectory;
                process1.StartInfo.RedirectStandardInput = true;
                process1.StartInfo.RedirectStandardOutput = true;
                process1.StartInfo.RedirectStandardError = true;
                process1.StartInfo.CreateNoWindow = true;
                process1.StartInfo.UseShellExecute = false;
                process1.StartInfo.ErrorDialog = true;
                process1.Start();

                FileInfo fileInfo = new FileInfo(cmdOutputTextFile);

                process1.StandardInput.AutoFlush = true;
                // https://docs.microsoft.com/en-us/troubleshoot/cpp/redirecting-error-command-prompt6
                process1.StandardInput.WriteLine(command);// + " 1> " + fileInfo.FullName + " 2>&1");
                // process1.StandardInput.WriteLine("exit");
                // process1.StandardInput.WriteLine("exit");
                process1.StandardInput.Flush();
                process1.StandardInput.Dispose();
                process1.StandardInput.Close();

                cmdOutput = process1.StandardOutput.ReadToEnd();
                process1.StandardOutput.Dispose();
                process1.StandardOutput.Close();

                cmdOutput += "\r\n" + process1.StandardError.ReadToEnd();
                process1.StandardError.Dispose();
                process1.StandardError.Close();


                //Debug.WriteLine("BEGIN" + cmdOutput + "END");
                string[] lines = cmdOutput.Split(Environment.NewLine,
                                                                 StringSplitOptions.RemoveEmptyEntries);
                string editedOutput = "";

                foreach (string line in lines)
                {
                    if (!line.Contains("Microsoft Windows") && !line.Contains("Microsoft Corporation")
                        && (!(line.IndexOf("C") == 0) && !(line.IndexOf(":") == 1)))
                    {
                        editedOutput += line + "\r\n";
                    }
                }
                //Debug.WriteLine("EDITEDBEGIN" + editedOutput + "EDITEDEND");
                // File.WriteAllText(cmdOutputTextFile + "original.txt", cmdOutput);
                cmdOutput = editedOutput;
                //File.WriteAllText(cmdOutputTextFile + "22.txt", cmdOutput);
                // Debug.WriteLine("EDITEdBEGIN" + cmdOutput + "EDITEDEND");
                Debug.WriteLine("BEGIN\r\n" + cmdOutput + "\r\nEND");






            }
            catch (Exception e)
            {
                // MessageBox.Show("Process Create Error - " + e.Message);
                Debug.WriteLine("Process Create Error - " + e.Message);
            }
        }

        public static void Command2(string command)
        {
            try
            {

                process1.StartInfo.FileName = "cmd.exe";
                process1.StartInfo.WorkingDirectory = workingDirectory;
                process1.StartInfo.RedirectStandardInput = true;
                process1.StartInfo.RedirectStandardOutput = true;
                process1.StartInfo.RedirectStandardError = true;
                process1.StartInfo.CreateNoWindow = true;
                process1.StartInfo.UseShellExecute = false;
                process1.StartInfo.ErrorDialog = true;
                process1.Start();

                FileInfo fileInfo = new FileInfo(cmdOutputTextFile);

                process1.StandardInput.AutoFlush = true;
                // https://docs.microsoft.com/en-us/troubleshoot/cpp/redirecting-error-command-prompt6
                process1.StandardInput.WriteLine(command);// + " 1> " + fileInfo.FullName + " 2>&1");
                // process1.StandardInput.WriteLine("exit");
                // process1.StandardInput.WriteLine("exit");
                process1.StandardInput.Flush();
                process1.StandardInput.Dispose();
                process1.StandardInput.Close();

                cmdOutput = process1.StandardOutput.ReadToEnd();
                process1.StandardOutput.Dispose();
                process1.StandardOutput.Close();

                cmdOutput += "\r\n" + process1.StandardError.ReadToEnd();
                process1.StandardError.Dispose();
                process1.StandardError.Close();


                //Debug.WriteLine("BEGIN" + cmdOutput + "END");
                string[] lines = cmdOutput.Split(Environment.NewLine,
                                                                 StringSplitOptions.RemoveEmptyEntries);
                string editedOutput = "";

                foreach (string line in lines)
                {
                    if (!line.Contains("Microsoft Windows") && !line.Contains("Microsoft Corporation")
                        && (!(line.IndexOf("C") == 0) && !(line.IndexOf(":") == 1)))
                    {
                        editedOutput += line + "\r\n";
                    }
                }
                //Debug.WriteLine("EDITEDBEGIN" + editedOutput + "EDITEDEND");
                // File.WriteAllText(cmdOutputTextFile + "original.txt", cmdOutput);
                cmdOutput = editedOutput;
                //File.WriteAllText(cmdOutputTextFile + "22.txt", cmdOutput);
                // Debug.WriteLine("EDITEDBEGIN" + cmdOutput + "EDITEDEND");






            }
            catch (Exception e)
            {
                // MessageBox.Show("Process Create Error - " + e.Message);
                Debug.WriteLine("Process Create Error - " + e.Message);
            }
        }
    }
}
