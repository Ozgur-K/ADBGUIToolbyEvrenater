using ADBGUIToolbyEvrenater.ProcessCreating;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;

namespace ADBGUIToolbyEvrenater.AppUninstall
{
    public static class AppUninstaller
    {
        static Form appUninstallerForm;
        static TabPage tabPage1, tabPage2;
        static TabControl tabControl1;
        static ListView listView1, listView2;
        static Button uninstallButton, disableButton, searchButton1, cancelButton1, cancelButton2;
        static Label downloadStatusLabel, systemStatusLabel;
        static TableLayoutPanel tableLayoutPanel1, tableLayoutPanel2;
        static TextBox searchBox;
        static FlowLayoutPanel flowLayoutPanel;
        static BackgroundWorker backgroundWorker1, backgroundWorker2, backgroundWorker3, backgroundWorker4;

        static string packageName1, packageName2;

        public static void CreateForm()
        {
            appUninstallerForm = new Form();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tabControl1 = new TabControl();
            listView1 = new ListView();
            downloadStatusLabel = new Label();
            uninstallButton = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            listView2 = new ListView();
            disableButton = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            systemStatusLabel = new Label();
            searchBox = new TextBox();
            searchButton1 = new Button();
            flowLayoutPanel = new FlowLayoutPanel();
            backgroundWorker1 = new BackgroundWorker(); // Show Downloaded Apps
            backgroundWorker2 = new BackgroundWorker(); // Uninstall Downloaded Apps
            backgroundWorker3 = new BackgroundWorker(); // Show System Apps
            backgroundWorker4 = new BackgroundWorker(); // Disable System Apps
            cancelButton1 = new Button();               // Cancel Uninstalling
            cancelButton2 = new Button();               // Cancel Disabling

            appUninstallerForm.SuspendLayout();

            appUninstallerForm.Text = "App Uninstaller";
            tabPage1.Text = "Downloaded Apps";
            tabPage2.Text = "System Apps";
            downloadStatusLabel.Text = "Apps (Don't change tab while loading.)";
            systemStatusLabel.Text = "Apps (Required Rooted Debugging)";
            uninstallButton.Text = "Uninstall";
            disableButton.Text = "Disable";
            searchBox.PlaceholderText = "Type An App Name...";
            searchButton1.Text = "Search";
            cancelButton1.Text = "Cancel";
            cancelButton2.Text = "Cancel";

            tabPage1.TabIndex = 0;
            tabPage2.TabIndex = 1;
            tabControl1.SelectedIndex = 0;
            flowLayoutPanel.Height = searchButton1.Height + 5;

            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker2.WorkerSupportsCancellation = true;
            backgroundWorker3.WorkerSupportsCancellation = true;
            backgroundWorker4.WorkerSupportsCancellation = true;
            downloadStatusLabel.AutoSize = true;
            systemStatusLabel.AutoSize = true;
            cancelButton1.AutoSize = true;
            cancelButton2.AutoSize = true;
            cancelButton1.Enabled = false;
            cancelButton2.Enabled = false;


            uninstallButton.Dock = DockStyle.Bottom;
            cancelButton1.Dock = DockStyle.Bottom;
            cancelButton2.Dock = DockStyle.Bottom;
            disableButton.Dock = DockStyle.Bottom;
            cancelButton1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cancelButton2.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            listView1.View = View.Tile;
            listView2.View = View.Tile;

            appUninstallerForm.Layout += AppUninstallerForm_Layout;
            disableButton.Click += DisableButton_Click;
            uninstallButton.Click += UninstallButton_Click;
            tabPage1.Enter += TabPage1_Enter;
            tabPage2.Enter += TabPage2_Enter;
            searchButton1.Click += SearchButton1_Click;
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
            cancelButton1.Click += CancelButton1_Click;
            backgroundWorker2.DoWork += BackgroundWorker2_DoWork;
            backgroundWorker2.RunWorkerCompleted += BackgroundWorker2_RunWorkerCompleted;
            backgroundWorker3.DoWork += BackgroundWorker3_DoWork;
            backgroundWorker3.RunWorkerCompleted += BackgroundWorker3_RunWorkerCompleted;
            backgroundWorker4.DoWork += BackgroundWorker4_DoWork;
            backgroundWorker4.RunWorkerCompleted += BackgroundWorker4_RunWorkerCompleted;
            cancelButton2.Click += CancelButton2_Click;


            tableLayoutPanel1.Controls.Add(downloadStatusLabel);
            flowLayoutPanel.Controls.Add(searchBox);
            flowLayoutPanel.Controls.Add(searchButton1);
            //tableLayoutPanel1.Controls.Add(flowLayoutPanel);
            tableLayoutPanel1.Controls.Add(listView1);
            tableLayoutPanel2.Controls.Add(systemStatusLabel);
            tableLayoutPanel2.Controls.Add(listView2);
            //tableLayoutPanel2.Controls.Add(cancelButton2);


            tabPage1.Controls.Add(uninstallButton);
            tabPage1.Controls.Add(cancelButton1);
            tabPage1.Controls.Add(tableLayoutPanel1);
            tabPage2.Controls.Add(disableButton);
            tabPage2.Controls.Add(cancelButton2);
            tabPage2.Controls.Add(tableLayoutPanel2);


            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);

            appUninstallerForm.Controls.Add(tabControl1);

            appUninstallerForm.ResumeLayout();
            appUninstallerForm.ShowDialog();
        }

        private static void DetectDownloadedApps()
        {
            tabPage1.Text = "Loading...";
            downloadStatusLabel.Text = "Apps (Don't change tab while loading.)";
            appUninstallerForm.Refresh();
            listView1.Items.Clear();

            // Start Background Process
            if (backgroundWorker1.IsBusy != true)
            {
                backgroundWorker3.CancelAsync();
                backgroundWorker1.RunWorkerAsync(); // DoWork() else Block
            }
        }

        private static void DetectSystemApps()
        {

            tabPage2.Text = "Loading...";
            systemStatusLabel.Text = "Apps (Required Rooted Debugging)";
            appUninstallerForm.Refresh();
            listView2.Items.Clear();

            // Start Background Process
            if (backgroundWorker3.IsBusy != true)
            {
                backgroundWorker1.CancelAsync();
                backgroundWorker3.RunWorkerAsync(); // DoWork() else Block
            }
        }

        private static void Uninstall()
        {
            SelectedListViewItemCollection sLVIC = listView1.SelectedItems;
            Debug.WriteLine(sLVIC.Count);
            if (sLVIC.Count != 0)
            {
                packageName1 = listView1.SelectedItems[0].Text.Trim().
                                                    Substring(listView1.SelectedItems[0].Text.IndexOf(":") + 1);
                Debug.WriteLine("selectedItem" + packageName1 + ".");

                var dialogResult = MessageBox.Show(appUninstallerForm,
                                                    "Do you want to uninstall " + packageName1 + "?",
                                                    "Uninstall",
                                                    MessageBoxButtons.YesNo);
                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        Debug.WriteLine("yes");
                        uninstallButton.Text = "Uninstalling...";
                        uninstallButton.Enabled = false;
                        cancelButton1.Enabled = true;
                        appUninstallerForm.Refresh();

                        if (backgroundWorker2.IsBusy != true)
                        {
                            // Start the asynchronous operation.
                            backgroundWorker2.RunWorkerAsync();
                        }

                        break;
                    case DialogResult.No:
                        Debug.WriteLine("no");
                        break;
                }
            }
            else
            {
                Debug.WriteLine("Error: ");// + e.Message);
                uninstallButton.Text = "Select An App";
                uninstallButton.Enabled = false;
                appUninstallerForm.Refresh();
                System.Threading.Thread.Sleep(1000);
                uninstallButton.Text = "Uninstall";
                uninstallButton.Enabled = true;
                cancelButton1.Enabled = false;
                appUninstallerForm.Refresh();

            }
        }

        private static void Disable()
        {
            SelectedListViewItemCollection sLVIC = listView2.SelectedItems;
            Debug.WriteLine(sLVIC.Count);
            if (sLVIC.Count != 0)
            {
                packageName2 = listView2.SelectedItems[0].Text.Trim().
                                                    Substring(listView2.SelectedItems[0].Text.IndexOf(":") + 1);
                Debug.WriteLine("selectedItem" + packageName2 + ".");

                var dialogResult = MessageBox.Show(appUninstallerForm,
                                                    "Do you want to disable " + packageName2 + "?",
                                                    "Disable",
                                                    MessageBoxButtons.YesNo);
                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        Debug.WriteLine("yes");
                        disableButton.Text = "Disabling...";
                        disableButton.Enabled = false;
                        cancelButton2.Enabled = true;
                        appUninstallerForm.Refresh();

                        if (backgroundWorker4.IsBusy != true)
                        {
                            // Start the asynchronous operation.
                            backgroundWorker4.RunWorkerAsync();
                        }


                        break;
                    case DialogResult.No:
                        Debug.WriteLine("no");
                        break;
                }

            }
            else
            {
                disableButton.Text = "Select An App";
                disableButton.Enabled = false;
                appUninstallerForm.Refresh();
                System.Threading.Thread.Sleep(1000);
                disableButton.Text = "Disable";
                disableButton.Enabled = true;
                cancelButton2.Enabled = false;
                appUninstallerForm.Refresh();

            }
        }

        private static void Search()
        {
            tabPage1.Text = "Loading...";
            downloadStatusLabel.Text = "Apps";
            appUninstallerForm.Refresh();
            listView1.Items.Clear();

            List<string> newCmdOutput = new();
            ProcessCreate.Command("adb shell");
            System.Threading.Thread.Sleep(1000);
            Debug.WriteLine("lşkjlkj");
            ProcessCreate.Command("pm list packages -3 | grep " + searchBox.Text);

            #region seperate if startswith * 
            string[] lines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                                                                             StringSplitOptions.RemoveEmptyEntries);

            List<string> lineWithoutAsterisk = new();

            foreach (string line in lines)
            {
                if (line.StartsWith("*"))
                {
                    // It's warning
                }
                else
                {
                    lineWithoutAsterisk.Add(line);
                }
            }
            newCmdOutput = lineWithoutAsterisk;
            #endregion

            // if contains package: or 0 line without error success


            if (ProcessCreate.cmdOutput.Contains("package:"))
            {
                foreach (string line in newCmdOutput)
                {
                    if (line.Contains("package:"))
                    {
                        listView1.Items.Add(line.Trim().Substring(line.IndexOf(":") + 1));
                    }
                }
            }
            else if (newCmdOutput.Count.Equals(0))
            {
                listView1.Items.Add("There are no third party apps.");
                appUninstallerForm.Refresh();
            }
            else
            {
                downloadStatusLabel.Text = null;
                foreach (string line in newCmdOutput)
                    downloadStatusLabel.Text += line;
            }

            tabPage1.Text = "Downloaded Apps";
            appUninstallerForm.Refresh();
        } // grep may be added

        private static void SearchButton1_Click(object sender, EventArgs e)
        {
            Search();
        }

        private static void TabPage2_Enter(object sender, EventArgs e)
        {
            DetectSystemApps();
        }

        private static void TabPage1_Enter(object sender, EventArgs e)
        {
            DetectDownloadedApps();
        }

        private static void UninstallButton_Click(object sender, EventArgs e)
        {
            Uninstall();
        }

        private static void DisableButton_Click(object sender, EventArgs e)
        {
            Disable();
        }

        private static void AppUninstallerForm_Layout(object sender, LayoutEventArgs e)
        {
            tabControl1.Height = appUninstallerForm.Height - 40;
            tabControl1.Width = appUninstallerForm.Width - 15;

            tabPage1.Size = tabControl1.Size;

            tableLayoutPanel1.Width = tabPage1.Width;
            tableLayoutPanel1.Height = (tabPage1.Height - 25);

            listView1.Width = tableLayoutPanel1.Width;
            listView1.Height = tableLayoutPanel1.Height - 50;




            tabPage2.Size = tabControl1.Size;

            tableLayoutPanel2.Width = tabPage2.Width;
            tableLayoutPanel2.Height = (tabPage2.Height - 25);

            listView2.Width = tableLayoutPanel2.Width;
            listView2.Height = tableLayoutPanel2.Height - 50;
        }

        private static void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                downloadStatusLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                Debug.WriteLine("inError");
                downloadStatusLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!

                List<string> newCmdOutput = new();
                #region seperate if startswith * 
                string[] lines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                                                                                 StringSplitOptions.RemoveEmptyEntries);

                List<string> lineWithoutAsterisk = new();

                foreach (string line in lines)
                {
                    if (line.StartsWith("*"))
                    {
                        // It's warning
                    }
                    else
                    {
                        lineWithoutAsterisk.Add(line);
                    }
                }
                newCmdOutput = lineWithoutAsterisk;
                #endregion
                // if contains package: or 0 line without error success
                if (ProcessCreate.cmdOutput.Contains("package:"))
                {
                    foreach (string line in newCmdOutput)
                    {
                        if (line.Contains("package:"))
                        {
                            listView1.Items.Add(line.Trim().Substring(line.IndexOf(":") + 1));
                        }
                    }
                }
                else if (newCmdOutput.Count.Equals(0))
                {
                    listView1.Items.Add("There are no third party apps.");
                    appUninstallerForm.Refresh();
                }
                else
                {
                    downloadStatusLabel.Text = null;
                    foreach (string line in newCmdOutput)
                        downloadStatusLabel.Text += line;
                }

                tabPage1.Text = "Downloaded Apps";
                appUninstallerForm.Refresh();
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
                // Perform a time consuming operation and report progress.

                ProcessCreate.Command("adb shell pm list packages -3");
            }
        }

        private static void CancelButton1_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                backgroundWorker1.CancelAsync();
                DialogResult dialogResult = DialogResult.Yes;
                if (backgroundWorker1.WorkerSupportsCancellation == true)
                {
                    // Cancel the asynchronous operation.
                    dialogResult = MessageBox.Show(appUninstallerForm, "You have to reconnect your device over Wi-Fi " +
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
        }

        private static void BackgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                downloadStatusLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                downloadStatusLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!

                List<string> editedCmdOutput = new();
                #region seperate if startswith * 
                string[] lines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                                                                                 StringSplitOptions.RemoveEmptyEntries);

                List<string> lineWithoutAsterisk = new();

                foreach (string line in lines)
                {
                    if (line.StartsWith("*"))
                    {
                        // It's warning
                    }
                    else
                    {
                        lineWithoutAsterisk.Add(line);
                    }
                }
                editedCmdOutput = lineWithoutAsterisk;
                #endregion
                downloadStatusLabel.Text = null;
                foreach (string line in editedCmdOutput)
                    downloadStatusLabel.Text += line;
                uninstallButton.Text = "Uninstall";
                uninstallButton.Enabled = true;
                cancelButton1.Enabled = false;
                appUninstallerForm.Refresh();
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
                // Perform a time consuming operation and report progress.

                ProcessCreate.Command("adb uninstall " + packageName1);
            }
        }

        private static void BackgroundWorker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                downloadStatusLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                downloadStatusLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!


                List<string> newCmdOutput = new();
                // Debug.WriteLine(ProcessCreate.cmdOutput);
                #region seperate if startswith * 
                string[] lines = new string[] { };
                lines = ProcessCreate.cmdOutput.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

                List<string> lineWithoutAsterisk = new();

                foreach (string line in lines)
                {
                    if (line.StartsWith("*"))
                    {
                        // It's warning line
                    }
                    else
                    {
                        lineWithoutAsterisk.Add(line);
                    }
                }
                newCmdOutput = lineWithoutAsterisk;
                #endregion

                // if contains package: or 0 line without error success


                if (ProcessCreate.cmdOutput.Contains("package:"))
                {
                    foreach (string line in newCmdOutput)
                    {
                        if (line.Contains("package:"))
                        {
                            listView2.Items.Add(line.Trim().Substring(line.IndexOf(":") + 1));
                        }
                    }
                }
                else
                {
                    systemStatusLabel.Text = "";
                    foreach (string line in newCmdOutput)
                        systemStatusLabel.Text += line;
                }

                tabPage2.Text = "System Apps";
                appUninstallerForm.Refresh();
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
                // Perform a time consuming operation and report progress.

                ProcessCreate.Command("adb shell pm list packages -s");
            }
        }

        private static void BackgroundWorker4_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                downloadStatusLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                downloadStatusLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!

                List<string> editedCmdOutput = new();
                #region seperate if startswith * 
                string[] lines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                                                                                 StringSplitOptions.RemoveEmptyEntries);

                List<string> lineWithoutAsterisk = new();

                foreach (string line in lines)
                {
                    if (line.StartsWith("*"))
                    {
                        // It's warning
                    }
                    else
                    {
                        lineWithoutAsterisk.Add(line);
                    }
                }
                editedCmdOutput = lineWithoutAsterisk;
                #endregion
                systemStatusLabel.Text = null;
                foreach (string line in editedCmdOutput)
                    systemStatusLabel.Text += line;
                disableButton.Text = "Disable";
                disableButton.Enabled = true;
                cancelButton2.Enabled = false;
                appUninstallerForm.Refresh();
            }
        }

        private static void BackgroundWorker4_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.

                ProcessCreate.Command("adb shell pm disable " + packageName2);
            }
        }

        private static void CancelButton2_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("clicked");
            if (backgroundWorker4.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                backgroundWorker4.CancelAsync();
                DialogResult dialogResult = DialogResult.Yes;
                if (backgroundWorker4.WorkerSupportsCancellation == true)
                {
                    // Cancel the asynchronous operation.
                    dialogResult = MessageBox.Show(appUninstallerForm, "You have to reconnect your device over Wi-Fi " +
                                        "or physically reconnect over USB after cancellation. Do you want to cancel?",
                                        "Terminate", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);


                    backgroundWorker4.CancelAsync();
                }
                if (dialogResult.Equals(DialogResult.No))
                {
                }
                else
                {
                    ProcessCreate.Command2("adb kill-server");
                }
            }
        }
    }
}
