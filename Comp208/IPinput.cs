using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comp208
{

    public partial class IPinput : Form
    {   
        public string ip { get; private set; }
        public int port { get; private set; }
        public IPinput()
        {
            InitializeComponent();
        }

        private void Confirm_click(object sender, EventArgs e)
        {
            ip = tB_IP.Text;
            port = int.Parse(tB_Port.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();

        }
    }
}
