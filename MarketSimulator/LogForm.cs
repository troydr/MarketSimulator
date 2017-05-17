using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MarketSimulator
{
    public partial class LogForm : Form
    {
        public LogForm()
        {
            InitializeComponent();
        }


        public void DumpBufferIntoTextbox()
        {
            logTextBox.AppendText(MarketSimulator.console.ToString());
            MarketSimulator.console.Remove(0, MarketSimulator.console.Length);
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {

        }
    }
}
