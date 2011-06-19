using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Twitter2Reader
{
    public partial class frmAskForPin : Form
    {
        public frmAskForPin()
        {
            InitializeComponent();
        }
        public string pin = "";
        private void button1_Click(object sender, EventArgs e)
        {
            pin = textBox1.Text;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
