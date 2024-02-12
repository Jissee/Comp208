using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsAppTest.NetWork;

namespace Comp208
{
    public partial class Form1 : Form
    {
        private String ipAddress;
        private int portNum;
        public static TcpConnecter connector { get; private set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_StartGame(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {

                IPinput ipInput = new IPinput();
                ipInput.ShowDialog();
                if (ipInput.DialogResult == DialogResult.OK)
                {
                    ipAddress = ipInput.ip;
                    portNum = ipInput.port;
                    MessageBox.Show(ipAddress + " " + portNum);
                    /*IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(ipAddress), portNum);
                      connector = new TcpConnecter(endpoint);
                      connector.StartConnetion();
                      connector.SendMessage("CR", "");
                      MessageBox.Show("Game Started");*/
                }

            }
            else
            {
                MessageBox.Show("Please agree with the term and condition");
            }
        }
    }
}
