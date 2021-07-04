using ADBGUIToolbyEvrenater.ProcessCreating;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace ADBGUIToolbyEvrenater.CustomCommands
{
    class CustomCommand
    {
        Form form1;
        TableLayoutPanel tableLayoutPanel1;
        RichTextBox richTextBox;
        TextBox inputTextBox;
        Button submitButton, cancelButton;
        BackgroundWorker backgroundWorker1;
        LinkedList<string> commandHistory;

        int cmdHistory;

        public CustomCommand()
        {
            form1 = new();
            tableLayoutPanel1 = new();
            richTextBox = new();
            inputTextBox = new();
            submitButton = new();
            cancelButton = new();
            backgroundWorker1 = new();
            commandHistory = new();

            form1.SuspendLayout();

            form1.Text = "Custom Command";
            inputTextBox.PlaceholderText = "adb devices";
            submitButton.Text = "Submit";
            cancelButton.Text = "Cancel";
            cmdHistory = 1;

            backgroundWorker1.WorkerSupportsCancellation = true;
            submitButton.AutoSize = true;
            cancelButton.AutoSize = true;
            cancelButton.Enabled = false;
            richTextBox.ReadOnly = true;

            submitButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cancelButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            inputTextBox.ScrollBars = ScrollBars.Both;
            richTextBox.ScrollBars = RichTextBoxScrollBars.Both;
            tableLayoutPanel1.Size = form1.Size;
            richTextBox.Width = tableLayoutPanel1.Width - 20;
            inputTextBox.Width = tableLayoutPanel1.Width - 20;
            richTextBox.WordWrap = false;

            inputTextBox.KeyDown += InputTextBox_KeyDown;
            submitButton.Click += SendCommandButton_Click;
            cancelButton.Click += CancelButton_Click;
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
            richTextBox.TextChanged += RichTextBox_TextChanged;
            form1.SizeChanged += Form1_SizeChanged;
            form1.Shown += Form1_Shown;

            tableLayoutPanel1.Controls.AddRange(new Control[] {richTextBox, inputTextBox, submitButton,
                                                    cancelButton});
            form1.Controls.Add(tableLayoutPanel1);
            form1.ResumeLayout();
            form1.ShowDialog();
        }

        private void Submit()
        {
            submitButton.Enabled = false;
            submitButton.Text = "Waiting For Output...";
            cancelButton.Enabled = true;

            if (backgroundWorker1.IsBusy != true)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }
        private void Cancel()
        {
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                backgroundWorker1.CancelAsync();
                DialogResult dialogResult = DialogResult.Yes;
                dialogResult = MessageBox.Show(form1, "You have to reconnect your device over Wi-Fi " +
                                    "or physically reconnect over USB after cancellation. I suggest clicking no and" +
                                    "disconnecting the phone. Do you want to cancel?",
                                    "Terminate", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult.Equals(DialogResult.No))
                {
                }
                else
                {
                    ProcessCreate.Command2("adb kill-server");
                }
            }
        }

        #region Events
        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.Equals(Keys.Enter))
            {
                submitButton.PerformClick();
               // Debug.WriteLine("cmdHistory" + cmdHistory + "commandcount" + commandHistory.Count);
            }
            else if (e.KeyData.Equals(Keys.Up))
            {
                if (cmdHistory <= commandHistory.Count) 
                {
                   // Debug.WriteLine("cmdHistory" + cmdHistory + "commandcount" + commandHistory.Count);
                   // inputTextBox.Text = commandHistory.ElementAt(commandHistory.Count - cmdHistory);
                   // ++cmdHistory;
                }
            }
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
            Cancel();
        }
        private void SendCommandButton_Click(object sender, EventArgs e)
        {
            Submit();
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                richTextBox.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                richTextBox.Text = "Error: " + e.Error.Message;
            }
            else
            {
                // Done!
                submitButton.Enabled = true;
                submitButton.Text = "Submit";
                cancelButton.Enabled = false;
                richTextBox.Text = ProcessCreate.cmdOutput;
                inputTextBox.Text = "";
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
                commandHistory.AddLast(inputTextBox.Text);
                ProcessCreate.Command(inputTextBox.Text);
            }
        }
        private void RichTextBox_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            richTextBox.SelectionStart = richTextBox.Text.Length;
            // scroll it automatically
            richTextBox.ScrollToCaret();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            tableLayoutPanel1.Size = form1.Size;
            richTextBox.Width = tableLayoutPanel1.Width - 20;
            inputTextBox.Width = tableLayoutPanel1.Width - 20;
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            inputTextBox.Focus();
        }
        #endregion
    }
}
