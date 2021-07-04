using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ADBGUIToolbyEvrenater.Test
{
    class Class1
    {
        BackgroundWorker backgroundWorker1;
        Form form1;
        Label resultLabel;
        Button startAsyncButton, stopAsyncButton;
        FlowLayoutPanel flowLayoutPanel;
        public Class1()
        {
            backgroundWorker1 = new BackgroundWorker();
            form1 = new Form();
            resultLabel = new();
            startAsyncButton = new();
            stopAsyncButton = new Button();
            flowLayoutPanel = new FlowLayoutPanel();

            startAsyncButton.Text = "start";
            stopAsyncButton.Text = "stop";

            flowLayoutPanel.Controls.Add(resultLabel);
            flowLayoutPanel.Controls.Add(startAsyncButton);
            flowLayoutPanel.Controls.Add(stopAsyncButton);

            form1.Controls.Add(flowLayoutPanel);


            backgroundWorker1.WorkerSupportsCancellation = true;

            startAsyncButton.Click += startAsyncButton_Click;
            stopAsyncButton.Click += cancelAsyncButton_Click;
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;

            form1.Show();
        }


        private void startAsyncButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.

                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void cancelAsyncButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                // 
                backgroundWorker1.CancelAsync();
            }
        }

        // This event handler is where the time-consuming work is done.
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
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


        // This event handler deals with the results of the background operation.
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
    }
}
