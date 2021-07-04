using ADBGUIToolbyEvrenater.ADBSideload;
using ADBGUIToolbyEvrenater.ApkInstalling;
using ADBGUIToolbyEvrenater.AppUninstall;
using ADBGUIToolbyEvrenater.BackupAndRestore;
using ADBGUIToolbyEvrenater.Battery;
using ADBGUIToolbyEvrenater.CleanUnnecessaryFiles;
using ADBGUIToolbyEvrenater.CustomCommands;
using ADBGUIToolbyEvrenater.DeviceDetecting;
using ADBGUIToolbyEvrenater.Disclaimers;
using ADBGUIToolbyEvrenater.DpiChanger;
using ADBGUIToolbyEvrenater.FastbootProcess;
using ADBGUIToolbyEvrenater.FileTransferring;
using ADBGUIToolbyEvrenater.ProcessCreating;
using ADBGUIToolbyEvrenater.Reboots;
using ADBGUIToolbyEvrenater.ResolutionChanger;
using ADBGUIToolbyEvrenater.ScreenRecord;
using ADBGUIToolbyEvrenater.Screenshot;
using ADBGUIToolbyEvrenater.Storage;
using ADBGUIToolbyEvrenater.TouchSwipeType;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ADBGUIToolbyEvrenater
{
    public partial class Form1 : Form
    {        
        public Form1()
        {
            InitializeComponent();
            if(File.Exists("Studio_Project.ico"))
                this.Icon = new Icon("Studio_Project.ico");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Shown += ShownEvent;

            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.detectDeviceButton, "Detect Device (Not Required For USB)");

        }

        private void detectDeviceButton_Click(object sender, EventArgs e)
        {
            DetectDevice.ShowDetectForm(); // Form.ShowDialog() method - program wait here for closing - 
                                           //I'm use this to be able to change the name of detectDeviceButton
            detectDeviceButton.Text = DetectDevice.detectDeviceButtonText;
            this.Refresh();
        }

        private void apkInstallerButton_Click(object sender, EventArgs e)
        {
            new ApkInstaller();
        }

        private void ShownEvent(object sender, EventArgs e)
        {

        }

        private void fileTransferButton_Click(object sender, EventArgs e)
        {
            new FileTransfer();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ProcessCreate.Command("adb kill-server");
            DeleteFilesAndFolders.Delete();
        }

        private void appUninstallerButton_Click(object sender, EventArgs e)
        {
            AppUninstaller.CreateForm();
        }

        private void backupRestoreButton_Click(object sender, EventArgs e)
        {
            new BackupRestore();
        }

        private void customCommandButton_Click(object sender, EventArgs e)
        {
            new CustomCommand();
        }

        private void rebootButton_Click(object sender, EventArgs e)
        {
            new Reboot();
        }

        private void sideloadButton_Click(object sender, EventArgs e)
        {
            new Sideload();
        }

        private void sdButton_Click(object sender, EventArgs e)
        {
            new SdToInternal();
        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void batteryButton_Click(object sender, EventArgs e)
        {
            new BatteryLevel();
        }

        private void dpiChangerButton_Click(object sender, EventArgs e)
        {
            new DPIChange();
        }

        private void resolutionCButton_Click(object sender, EventArgs e)
        {
            new ResolutionChange();
        }

        private void screenShotButton_Click(object sender, EventArgs e)
        {
            new ScreenCapture();
        }

        private void screenRecordButton_Click(object sender, EventArgs e)
        {
            new RecordVideo();
        }

        private void tapSwipeTypeButton_Click(object sender, EventArgs e)
        {
            new TouchSwipeWrite();
        }

        private void fastbootButton_Click(object sender, EventArgs e)
        {
            new Fastboot();
        }

        private void disclaimerButton_Click(object sender, EventArgs e)
        {
            new Disclaimer();
        }
    }
}
