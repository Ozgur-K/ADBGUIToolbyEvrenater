using ADBGUIToolbyEvrenater.ProcessCreating;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ADBGUIToolbyEvrenater.DeviceDetecting
{
    public static class DetectDevice
    {
        static Form detectDeviceForm;
        static TableLayoutPanel flowLayoutPanel;
        static Label connectStatusLabel;
        static TextBox addressTextBox;
        static Button connectNetworkButton, connectUSBButton;

        public static string detectDeviceButtonText = "Detect Device";


        public static void ShowDetectForm()
        {
            detectDeviceForm = new Form();
            flowLayoutPanel = new TableLayoutPanel();
            connectStatusLabel = new Label();
            addressTextBox = new TextBox();
            connectNetworkButton = new Button();
            connectUSBButton = new Button();

            detectDeviceForm.SuspendLayout();

            detectDeviceForm.Text = "Detect Device";
            connectStatusLabel.Text = "Enter Address for Wireless Connecting:";
            addressTextBox.Text = "192.168.1.x:5555";
            addressTextBox.PlaceholderText = "192.168.1.x:5555";
            connectNetworkButton.Text = "Connect Over Network";
            connectUSBButton.Text = "Connect Over USB";

            connectStatusLabel.AutoSize = true;
            connectNetworkButton.AutoSize = true;
            connectNetworkButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            connectUSBButton.AutoSize = true;
            connectUSBButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            detectDeviceForm.Layout += DetectDeviceForm_Layout;
            connectStatusLabel.Layout += WirelessStatus_Layout;
            connectNetworkButton.Click += ConnectNetworkButton_Click;
            connectUSBButton.Click += ConnectUSBButton_Click;
            addressTextBox.KeyDown += AddressTextBox_KeyDown;

            flowLayoutPanel.Controls.AddRange(new Control[] { connectStatusLabel, addressTextBox, connectNetworkButton,
                                                                connectUSBButton});
            detectDeviceForm.Controls.Add(flowLayoutPanel);

            detectDeviceForm.ResumeLayout();

            detectDeviceForm.ShowDialog();
        }

        static void ConnectAndGetDeviceModelOverUSB()
        {
            try
            {
                connectUSBButton.Text = "Connecting...";
                connectUSBButton.Enabled = false;
                connectNetworkButton.Enabled = false;
                detectDeviceForm.Refresh();

                ProcessCreate.Command("adb kill-server");
                Debug.WriteLine("1" + ProcessCreate.cmdOutput);
                // System.Threading.Thread.Sleep(500);
                ProcessCreate.Command("adb usb");
                Debug.WriteLine("2" + ProcessCreate.cmdOutput);
                System.Threading.Thread.Sleep(1500);

                ProcessCreate.Command("adb shell getprop ro.product.model");
                if (ProcessCreate.cmdOutput.Contains("unauthorized"))
                {
                    ProcessCreate.Command("adb reconnect offline");
                    connectStatusLabel.Text = "Allow USB Debugging On Your Phone";
                    connectUSBButton.Text = "Connect Over USB";
                    detectDeviceButtonText = "Detect Device";
                    connectUSBButton.Enabled = true;
                    connectNetworkButton.Enabled = true;
                    detectDeviceForm.Refresh();
                }
                else if (ProcessCreate.cmdOutput.Contains("no devices/emulators")
                            || ProcessCreate.cmdOutput.Contains("device offline"))
                {
                    connectStatusLabel.Text = "No Devices Found";
                    connectUSBButton.Text = "Connect Over USB";
                    detectDeviceButtonText = "Detect Device";
                    connectUSBButton.Enabled = true;
                    connectNetworkButton.Enabled = true;
                    detectDeviceForm.Refresh();
                }
                else
                {
                    ProcessCreate.Command("adb devices -l");

                    if (ProcessCreate.cmdOutput.Contains("device product:"))
                    {
                        connectStatusLabel.Text = "Connected";
                        connectUSBButton.Text = "Connected";
                        connectUSBButton.Enabled = false;
                        connectNetworkButton.Enabled = false;
                        detectDeviceForm.Refresh();

                        ProcessCreate.Command("adb shell getprop ro.product.model");
                        Debug.WriteLine("connected-model:" + ProcessCreate.cmdOutput + "OverUSB");
                        string tempModelName = null;
                        if (ProcessCreate.cmdOutput.Contains("more than one device"))
                        {
                            tempModelName = "Model Name";
                        }
                        else
                        {
                            tempModelName = ProcessCreate.cmdOutput.Trim();
                        }
                        ProcessCreate.Command("adb root");
                        if (ProcessCreate.cmdOutput.Contains("already running") || ProcessCreate.cmdOutput.Contains("restarting"))
                            tempModelName += " - Root";
                        detectDeviceButtonText = tempModelName + " - Over USB ";

                        detectDeviceForm.Close();
                    }
                    else
                    {
                        connectStatusLabel.Text = ProcessCreate.cmdOutput;
                        connectUSBButton.Text = "Connect Over USB";
                        detectDeviceButtonText = "Detect Device";
                        connectUSBButton.Enabled = true;
                        connectNetworkButton.Enabled = true;
                        detectDeviceForm.Refresh();
                    }

                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }


        static void ConnectAndGetDeviceModelOverNetwork()
        {
            connectNetworkButton.Text = "Connecting Over Network...";
            connectUSBButton.Enabled = false;
            connectNetworkButton.Enabled = false;
            detectDeviceForm.Refresh();

            ProcessCreate.Command("adb connect " + addressTextBox.Text);
            if (ProcessCreate.cmdOutput.Contains("authenticate"))
            {
                connectStatusLabel.Text = "Allow USB Debugging On Your Phone";
                ProcessCreate.Command("adb reconnect offline");
                connectNetworkButton.Text = "Connect Over Network";
                detectDeviceButtonText = "Detect Device";
                connectUSBButton.Enabled = true;
                connectNetworkButton.Enabled = true;
                detectDeviceForm.Refresh();
            }
            else if (ProcessCreate.cmdOutput.Contains("cannot connect to"))
            {
                connectStatusLabel.Text = "No Devices Found";
                connectNetworkButton.Text = "Connect Over Network";
                detectDeviceButtonText = "Detect Device";
                connectUSBButton.Enabled = true;
                connectNetworkButton.Enabled = true;
                detectDeviceForm.Refresh();
            }
            else if (ProcessCreate.cmdOutput.Contains("cannot resolve host"))
            {
                connectStatusLabel.Text = "Cannot resolve host - The address should be like 192.168.1.20:5555";
                connectNetworkButton.Text = "Connect Over Network";
                detectDeviceButtonText = "Detect Device";
                connectUSBButton.Enabled = true;
                connectNetworkButton.Enabled = true;
                detectDeviceForm.Refresh();
            }
            else if (ProcessCreate.cmdOutput.Contains("connected to "))
            {
                ProcessCreate.Command("adb devices");
                if (ProcessCreate.cmdOutput.Contains("unauthorized"))
                {
                    connectStatusLabel.Text = "Allow USB Debugging On Your Phone";
                    ProcessCreate.Command("adb reconnect offline");
                    connectNetworkButton.Text = "Connect Over Network";
                    detectDeviceButtonText = "Detect Device";
                    connectUSBButton.Enabled = true;
                    connectNetworkButton.Enabled = true;
                    detectDeviceForm.Refresh();
                }
                else
                {
                    connectStatusLabel.Text = "Connected";
                    connectNetworkButton.Text = "Connected";
                    connectUSBButton.Enabled = false;
                    connectNetworkButton.Enabled = false;
                    detectDeviceForm.Refresh();

                    ProcessCreate.Command("adb shell getprop ro.product.model");
                    //more than one device wxeprion in modle name
                    Debug.WriteLine("connected-model:" + ProcessCreate.cmdOutput + "OverNetwoork");
                    string tempModelName = null;
                    if (ProcessCreate.cmdOutput.Contains("more than one device"))
                    {
                        tempModelName = "Model Name";
                    }
                    else
                    {
                        tempModelName = ProcessCreate.cmdOutput.Trim();
                    }
                    ProcessCreate.Command("adb root");
                    if (ProcessCreate.cmdOutput.Contains("already running") || ProcessCreate.cmdOutput.Contains("restarting"))
                        tempModelName += " - Root";
                    detectDeviceButtonText = tempModelName + " - Over Network ";

                    detectDeviceForm.Close();

                }
            }
            else
            {
                connectStatusLabel.Text = ProcessCreate.cmdOutput;
                connectNetworkButton.Text = "Connect Over Network";
                detectDeviceButtonText = "Detect Device";
                connectUSBButton.Enabled = true;
                connectNetworkButton.Enabled = true;
                detectDeviceForm.Refresh();
            }
        }

        #region Events
        private static void AddressTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.Equals(Keys.Enter))
                connectNetworkButton.PerformClick();
        }

        private static void ConnectUSBButton_Click(object sender, EventArgs e)
        {
            ConnectAndGetDeviceModelOverUSB();
        }

        private static void ConnectNetworkButton_Click(object sender, EventArgs e)
        {
            ConnectAndGetDeviceModelOverNetwork();
        }

        private static void WirelessStatus_Layout(object sender, LayoutEventArgs e)
        {
            if (!(connectStatusLabel.Width < 300))
                detectDeviceForm.Width = connectStatusLabel.Width + 50;
        }

        private static void DetectDeviceForm_Layout(object sender, LayoutEventArgs e)
        {
            Debug.WriteLine("resized");
            flowLayoutPanel.Size = detectDeviceForm.Size;
        }
        #endregion
    }
}
