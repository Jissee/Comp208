using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comp208
{
    internal class MainForm : Form
    {
        private MenuStrip menuStrip1;
        private ToolStripMenuItem wenjianToolStripMenuItem;
        private ToolStripMenuItem bainjiToolStripMenuItem;
        private ToolStripMenuItem shituToolStripMenuItem;
        private ToolStripMenuItem chuangkouToolStripMenuItem;
        private ToolStripMenuItem bangzhuToolStripMenuItem;
        private Button button1;
        private Button button2;
        private Button button3;
        private ComboBox comboBox1;
        private ListBox listBox1;
        private ProgressBar progressBar1;
        private ToolStripMenuItem guanyuToolStripMenuItem;

        public MainForm()
        {
            InitializeComponent();

            this.Icon = Util.MakeIcon(Properties.Resources.a);


        }

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.wenjianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bainjiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shituToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chuangkouToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bangzhuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guanyuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wenjianToolStripMenuItem,
            this.bainjiToolStripMenuItem,
            this.shituToolStripMenuItem,
            this.chuangkouToolStripMenuItem,
            this.bangzhuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(588, 32);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // wenjianToolStripMenuItem
            // 
            this.wenjianToolStripMenuItem.Name = "wenjianToolStripMenuItem";
            this.wenjianToolStripMenuItem.Size = new System.Drawing.Size(92, 28);
            this.wenjianToolStripMenuItem.Text = "wenjian";
            // 
            // bainjiToolStripMenuItem
            // 
            this.bainjiToolStripMenuItem.Name = "bainjiToolStripMenuItem";
            this.bainjiToolStripMenuItem.Size = new System.Drawing.Size(74, 28);
            this.bainjiToolStripMenuItem.Text = "bainji";
            // 
            // shituToolStripMenuItem
            // 
            this.shituToolStripMenuItem.Name = "shituToolStripMenuItem";
            this.shituToolStripMenuItem.Size = new System.Drawing.Size(68, 28);
            this.shituToolStripMenuItem.Text = "shitu";
            // 
            // chuangkouToolStripMenuItem
            // 
            this.chuangkouToolStripMenuItem.Name = "chuangkouToolStripMenuItem";
            this.chuangkouToolStripMenuItem.Size = new System.Drawing.Size(122, 28);
            this.chuangkouToolStripMenuItem.Text = "chuangkou";
            // 
            // bangzhuToolStripMenuItem
            // 
            this.bangzhuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.guanyuToolStripMenuItem});
            this.bangzhuToolStripMenuItem.Name = "bangzhuToolStripMenuItem";
            this.bangzhuToolStripMenuItem.Size = new System.Drawing.Size(102, 28);
            this.bangzhuToolStripMenuItem.Text = "bangzhu";
            // 
            // guanyuToolStripMenuItem
            // 
            this.guanyuToolStripMenuItem.Name = "guanyuToolStripMenuItem";
            this.guanyuToolStripMenuItem.Size = new System.Drawing.Size(175, 34);
            this.guanyuToolStripMenuItem.Text = "guanyu";
            this.guanyuToolStripMenuItem.Click += new System.EventHandler(this.guanyuToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(90, 109);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 45);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(309, 109);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(394, 166);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(101, 162);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 26);
            this.comboBox1.TabIndex = 4;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 18;
            this.listBox1.Location = new System.Drawing.Point(262, 209);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 94);
            this.listBox1.TabIndex = 5;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(101, 238);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(138, 23);
            this.progressBar1.TabIndex = 6;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(588, 323);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void guanyuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Copyright © TEAM NINETEEN COMP208 Co. Ltd.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new TradeManagerForm().Show();
        }
    }
}
