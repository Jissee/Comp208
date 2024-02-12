using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comp208
{
    internal class TradeManagerForm : Form
    {
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private ListView listView1;
        private int index = 0;

        public TradeManagerForm() 
        { 
            InitializeComponent();
            this.Icon = Util.MakeIcon(Properties.Resources.b);

            listView1.Columns.Add("index", 50, HorizontalAlignment.Center);
            listView1.Columns.Add("Seller", 50, HorizontalAlignment.Center);
            listView1.Columns.Add("StartTime", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Offer", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("Notes", 200);

            ListViewItem item = new ListViewItem();
            AddItem("Alice", "10", "COMP208", "I need a helper for the project.");
            AddItem("Bob", "12", "COMP201", "I hope someone can take the exam for me.");
            AddItem("Candy", "0", "COMP299", "FXXK! THIS IS A 404 MODULE");
            AddItem("David", "20", "Bilibili", "I have found a wonderful animation, who want to join with me?");
            AddItem("Emma", "12", "Genshin Impact", "A nice game that you must have a try.");


            /*
            listView1.Items.Add("Seller", "StartTime");
            listView1.Items.Add("Alice","12");
            listView1.Items.Add("Bob", "18");*/

        }

        private void AddItem(string name, string time, string offer, string note)
        {
            ListViewItem item = new ListViewItem();
            item.Text = index++.ToString();
            item.SubItems.Add(name);
            item.SubItems.Add(time);
            item.SubItems.Add(offer);
            item.SubItems.Add(note);
            this.listView1.Items.Add(item);
        }

        private void InitializeComponent()
        {
            this.listView1 = new System.Windows.Forms.ListView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(43, 44);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(897, 316);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(1017, 80);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(638, 323);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(630, 291);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "PublicTrade";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(630, 291);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "PrivateTrade";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // TradeManagerForm
            // 
            this.ClientSize = new System.Drawing.Size(1111, 654);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.tabControl1);
            this.Name = "TradeManagerForm";
            this.Text = "TradeManager";
            this.Load += new System.EventHandler(this.TradeManagerForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void TradeManagerForm_Load(object sender, EventArgs e)
        {

        }


    }
}
