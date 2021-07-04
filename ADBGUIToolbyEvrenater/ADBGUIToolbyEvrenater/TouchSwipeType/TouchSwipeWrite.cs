using ADBGUIToolbyEvrenater.ProcessCreating;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADBGUIToolbyEvrenater.TouchSwipeType
{
    class TouchSwipeWrite
    {
        Form form1;
        TableLayoutPanel tableLayoutPanel1, tableLayoutPanel2;
        Label resultLabel;
        TextBox inputTextBox;
        Button submitButton, homeButton, backButton, backspaceButton, cancelButton;
        PictureBox pictureBox1;
        BackgroundWorker backgroundWorker1, backgroundWorker2, backgroundWorker3, backgroundWorker4,
                            backgroundWorker5, backgroundWorker6, backgroundWorker7, backgroundWorker8;

        string workingDirectory;
        int clickX, clickY, dragStartX, dragStartY, dragEndX, dragEndY;

        public TouchSwipeWrite()
        {
            form1 = new();
            tableLayoutPanel1 = new();
            tableLayoutPanel2 = new();
            resultLabel = new();
            inputTextBox = new();
            submitButton = new();
            homeButton = new();
            backButton = new();
            backspaceButton = new();
            cancelButton = new();
            pictureBox1 = new();
            backgroundWorker1 = new();
            backgroundWorker2 = new();
            backgroundWorker3 = new();
            backgroundWorker4 = new();
            backgroundWorker5 = new();
            backgroundWorker6 = new();
            backgroundWorker7 = new();
            backgroundWorker8 = new();
            workingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                                                                                             + "\\platform-tools\\";

            form1.SuspendLayout();

            form1.Text = "Tap Swipe Type";
            resultLabel.Text = "Open the phone screen.\r\nFor accuracy, resize the window so that the red\r\n" +
                                "area is minimally visible on your screen.\r\n" +
                                 "1080px width supported.\r\n" +
                                 "Middle click on the image to refresh image.\r\n" +
                                 "Right click for drag.\r\n" +
                                 "Click the app icon to open an application.";
            inputTextBox.PlaceholderText = "Type Anything...";
            submitButton.Text = "Submit";
            homeButton.Text = "Home";
            backButton.Text = "Back";
            backspaceButton.Text = "Backspace";
            cancelButton.Text = "Cancel";

            resultLabel.AutoSize = true;
            submitButton.AutoSize = true;
            homeButton.AutoSize = true;
            backButton.AutoSize = true;
            backspaceButton.AutoSize = true;
            cancelButton.AutoSize = true;
            cancelButton.Enabled = false;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker2.WorkerSupportsCancellation = true;
            backgroundWorker3.WorkerSupportsCancellation = true;
            backgroundWorker4.WorkerSupportsCancellation = true;
            backgroundWorker5.WorkerSupportsCancellation = true;
            backgroundWorker6.WorkerSupportsCancellation = true;
            backgroundWorker7.WorkerSupportsCancellation = true;
            backgroundWorker8.WorkerSupportsCancellation = true;

            submitButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cancelButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            homeButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            backButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            backspaceButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.Dock = DockStyle.Right;
            tableLayoutPanel2.Dock = DockStyle.Left;
            form1.Width = tableLayoutPanel1.Width + tableLayoutPanel2.Width;
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Width = 180;
            tableLayoutPanel1.Width = 190;

            resultLabel.SizeChanged += ResultLabel_SizeChanged;
            submitButton.Click += SubmitButton_Click;
            homeButton.Click += HomeButton_Click;
            backButton.Click += BackButton_Click;
            backspaceButton.Click += BackspaceButton_Click;
            cancelButton.Click += CancelButton_Click;
            inputTextBox.KeyDown += InputTextBox_KeyDown;
            form1.SizeChanged += Form1_SizeChanged;
            form1.Shown += Form1_Shown;
            pictureBox1.MouseClick += PictureBox1_MouseClick;
            form1.Load += Form1_Load;
            pictureBox1.MouseUp += PictureBox1_MouseUp;
            pictureBox1.MouseDown += PictureBox1_MouseDown;
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
            backgroundWorker2.DoWork += BackgroundWorker2_DoWork;
            backgroundWorker2.RunWorkerCompleted += BackgroundWorker2_RunWorkerCompleted;
            backgroundWorker3.DoWork += BackgroundWorker3_DoWork;
            backgroundWorker3.RunWorkerCompleted += BackgroundWorker3_RunWorkerCompleted;
            backgroundWorker4.DoWork += BackgroundWorker4_DoWork;
            backgroundWorker4.RunWorkerCompleted += BackgroundWorker4_RunWorkerCompleted;
            backgroundWorker5.DoWork += BackgroundWorker5_DoWork;
            backgroundWorker5.RunWorkerCompleted += BackgroundWorker5_RunWorkerCompleted;
            backgroundWorker6.DoWork += BackgroundWorker6_DoWork;
            backgroundWorker6.RunWorkerCompleted += BackgroundWorker6_RunWorkerCompleted;
            backgroundWorker7.DoWork += BackgroundWorker7_DoWork;
            backgroundWorker7.RunWorkerCompleted += BackgroundWorker7_RunWorkerCompleted;
            backgroundWorker8.DoWork += BackgroundWorker8_DoWork;
            backgroundWorker8.RunWorkerCompleted += BackgroundWorker8_RunWorkerCompleted;

            tableLayoutPanel1.BackColor = System.Drawing.Color.Red;
            tableLayoutPanel1.Controls.Add(pictureBox1);
            tableLayoutPanel2.Controls.AddRange(new Control[] {resultLabel, inputTextBox, submitButton,
                                                    homeButton, backButton, backspaceButton, cancelButton});

            form1.Controls.AddRange(new Control[] { tableLayoutPanel1, tableLayoutPanel2 });
            form1.ResumeLayout();
            form1.ShowDialog();
        }

        private void SendTextInput()
        {
            submitButton.Enabled = false;
            cancelButton.Enabled = true;

            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.

                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void Home()
        {
            homeButton.Enabled = false;
            cancelButton.Enabled = true;

            if (backgroundWorker2.IsBusy != true)
            {
                // Start the asynchronous operation.

                backgroundWorker2.RunWorkerAsync();
            }
        }

        private void Back()
        {
            backButton.Enabled = false;
            cancelButton.Enabled = true;

            if (backgroundWorker3.IsBusy != true)
            {
                // Start the asynchronous operation.

                backgroundWorker3.RunWorkerAsync();
            }
        }

        private void Backspace()
        {
            backspaceButton.Enabled = false;
            cancelButton.Enabled = true;

            if (backgroundWorker4.IsBusy != true)
            {
                // Start the asynchronous operation.

                backgroundWorker4.RunWorkerAsync();
            }
        }

        private void GetScreenshot()
        {
            resultLabel.Text = "Open the phone screen.\r\nFor accuracy, resize the window so that the red\r\n" +
                                "area is minimally visible on your screen.\r\n" +
                                 "1080px width supported.\r\n" +
                                 "Taking screenshot...\r\n" +
                                 "Right click for drag.\r\n" +
                                 "Click the app icon to open an application.";

            if (backgroundWorker5.IsBusy != true)
            {
                // Start the asynchronous operation.

                backgroundWorker5.RunWorkerAsync();
            }
        }

        private void Click()
        {
            if (backgroundWorker6.IsBusy != true)
            {
                // Start the asynchronous operation.

                backgroundWorker6.RunWorkerAsync();
            }
        }

        private void Drag()
        {
            if (backgroundWorker7.IsBusy != true)
            {
                // Start the asynchronous operation.

                backgroundWorker7.RunWorkerAsync();
            }
        }

        private void BackgroundWorker8_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                resultLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!
            }
        }

        private void BackgroundWorker8_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.
            }
        }

        private void BackgroundWorker7_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                resultLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!
                GetScreenshot();
            }
        }

        private void BackgroundWorker7_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.
                ProcessCreate.Command("adb shell input swipe " + dragStartX + " " + dragStartY +
                                        " " + dragEndX + " " + dragEndY);
            }
        }

        private void BackgroundWorker6_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                resultLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!
                System.Threading.Thread.Sleep(100);
                GetScreenshot();
            }
        }

        private void BackgroundWorker6_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.
                ProcessCreate.Command("adb shell input tap " + clickX + " " + clickY);
            }
        }

        private void BackgroundWorker5_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                resultLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!
                try
                {
                    FileStream fileStream = new FileStream(workingDirectory + "test.png", FileMode.OpenOrCreate);

                    pictureBox1.Image = Image.FromStream(fileStream);

                    fileStream.Close();
                    resultLabel.Text = "Open the phone screen.\r\nFor accuracy, resize the window so that the red\r\n" +
                                        "area is minimally visible on your screen.\r\n" +
                                         "1080px width supported.\r\n" +
                                         "Middle click on the image to refresh image.\r\n" +
                                         "Right click for drag.\r\n" +
                                         "Click the app icon to open an application.";
                }
                catch (Exception error)
                {
                    Debug.WriteLine("Error picturebox image file" + error.Message);
                }
            }
        }

        private void BackgroundWorker5_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.

                ProcessCreate.Command("adb exec-out screencap -p > " + workingDirectory + "test.png");

            }
        }

        private void BackgroundWorker4_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                resultLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!

                backspaceButton.Enabled = true;
                cancelButton.Enabled = false;

                string[] lines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                                                                    StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    if (line.Contains("no devices"))
                    {
                        resultLabel.Text = "No Found Device";
                    }
                    else if (line.IndexOf('*') != 0)
                    {
                        resultLabel.Text += "\r\n" + line;
                    }
                }
                if (lines.Count() == 0)
                {
                    // Success
                    resultLabel.Text = "Touch, Swipe or Write";
                    System.Threading.Thread.Sleep(100);
                    GetScreenshot();
                }
                else
                {
                    // foreach in above
                }
            }
        }

        private void BackgroundWorker4_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.,
                ProcessCreate.Command("adb shell input keyevent 67");
            }
        }

        private void BackgroundWorker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                resultLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!

                backButton.Enabled = true;
                cancelButton.Enabled = false;

                string[] lines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                                                                    StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    if (line.Contains("no devices"))
                    {
                        resultLabel.Text = "No Found Device";
                    }
                    else if (line.IndexOf('*') != 0)
                    {
                        resultLabel.Text += "\r\n" + line;
                    }
                }
                if (lines.Count() == 0)
                {
                    // Success
                    resultLabel.Text = "Touch, Swipe or Write";
                    System.Threading.Thread.Sleep(100);
                    GetScreenshot();
                }
                else
                {
                    // foreach in above
                }
            }
        }

        private void BackgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.
                ProcessCreate.Command("adb shell input keyevent 4");
            }
        }

        private void BackgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                resultLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!

                homeButton.Enabled = true;
                cancelButton.Enabled = false;

                string[] lines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                                                                    StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    if (line.Contains("no devices"))
                    {
                        resultLabel.Text = "No Found Device";
                    }
                    else if (line.IndexOf('*') != 0)
                    {
                        resultLabel.Text += "\r\n" + line;
                    }
                }
                if (lines.Count() == 0)
                {
                    // Success
                    resultLabel.Text = "Touch, Swipe or Write";
                    System.Threading.Thread.Sleep(100);
                    GetScreenshot();
                }
                else
                {
                    // foreach in above
                }
            }
        }

        private void BackgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.
                ProcessCreate.Command("adb shell input keyevent 3");
            }
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                resultLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!

                submitButton.Enabled = true;
                cancelButton.Enabled = false;

                string[] lines = ProcessCreate.cmdOutput.Split(Environment.NewLine,
                                                                    StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    if (line.Contains("no devices"))
                    {
                        resultLabel.Text = "No Found Device";
                    }
                    else if (line.IndexOf('*') != 0)
                    {
                        resultLabel.Text += "\r\n" + line;
                    }
                }
                if (lines.Count() == 0)
                {
                    // Success
                    resultLabel.Text = "Touch, Swipe or Write";
                    System.Threading.Thread.Sleep(100);
                    GetScreenshot();
                }
                else
                {
                    // foreach in above
                }
            }
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                // Perform a time consuming operation and report progress.

                ProcessCreate.Command("adb shell input text '" + inputTextBox.Text + "'");
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            cancelButton.Enabled = false;


            DialogResult dialogResult = DialogResult.Yes;
            dialogResult = MessageBox.Show(form1, "You may need to reconnect your device over Wi-Fi " +
                                "or physically reconnect over USB after cancellation. I suggest clicking no and" +
                                "disconnecting the phone. Do you want to cancel?",
                                "Terminate", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult.Equals(DialogResult.No))
            {
            }
            else
            {
                ProcessCreate.Command2("adb kill-server");

                form1.Dispose();
                form1.Close();
                /*    if (backgroundWorker1.WorkerSupportsCancellation == true)
                    {
                        // Cancel the asynchronous operation.
                        // 
                        backgroundWorker1.CancelAsync();
                    }*/
            }
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            SendTextInput();
        }

        private void BackspaceButton_Click(object sender, EventArgs e)
        {
            Backspace();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Back();
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            Home();
        }

        private void ResultLabel_SizeChanged(object sender, EventArgs e)
        {
            if (resultLabel.Width > 300)
                tableLayoutPanel2.Width = resultLabel.Width + 20;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            tableLayoutPanel2.Width = form1.Width - tableLayoutPanel1.Width;

        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.Equals(Keys.Enter))
            {
                submitButton.PerformClick();
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            inputTextBox.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetScreenshot();
        }

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Middle))
            {
                GetScreenshot();

            }
            else if (e.Button.Equals(MouseButtons.Left))
            {
                clickX = e.X * 6;
                clickY = e.Y * 6;

                Click();

                //  Debug.WriteLine("click loacation" + e.Location);
                //  Debug.WriteLine("estimated x:" + (e.X * 6) + "estimated Y:" + (e.Y * 6));
            }
        }
        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Right))
            {
                dragStartX = e.X * 6;
                dragStartY = e.Y * 6;
            }
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Right))
            {
                dragEndX = e.X * 6;
                dragEndY = e.Y * 6;

                Drag();
            }
        }
    }
}
