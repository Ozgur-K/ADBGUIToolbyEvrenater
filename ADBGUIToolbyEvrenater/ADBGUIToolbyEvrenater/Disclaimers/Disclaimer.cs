using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADBGUIToolbyEvrenater.Disclaimers
{
    class Disclaimer
    {
        Form form1;
        FlowLayoutPanel flowLayoutPanel;
        Label disclaimerLabel;
        RichTextBox richTextBox;

        public Disclaimer()
        {
            form1 = new();
            flowLayoutPanel = new();
            disclaimerLabel = new();
            richTextBox = new();

            form1.SuspendLayout();

            form1.Text = "Disclaimer";
            disclaimerLabel.Text = "Disclaimer";
            richTextBox.Text = "No Responsibility\r\n\r\nUse At Your Own Risk\r\n\r\nevrenater@gmail.com";

            richTextBox.ReadOnly = true;

            flowLayoutPanel.Size = form1.Size;
            richTextBox.Width = form1.Width - 20;

            form1.SizeChanged += Form1_SizeChanged;

            form1.ResumeLayout();
            flowLayoutPanel.Controls.AddRange(new Control[] { disclaimerLabel, richTextBox});
            form1.Controls.Add(flowLayoutPanel);
            form1.ShowDialog();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            flowLayoutPanel.Size = form1.Size;
            richTextBox.Width = form1.Width - 20;
        }
    }
}
