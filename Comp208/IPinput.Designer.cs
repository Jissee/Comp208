namespace Comp208
{
    partial class IPinput
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tB_IP = new System.Windows.Forms.TextBox();
            this.tB_Port = new System.Windows.Forms.TextBox();
            this.b_confirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(108, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(108, 201);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 33);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port:";
            // 
            // tB_IP
            // 
            this.tB_IP.Location = new System.Drawing.Point(242, 127);
            this.tB_IP.Name = "tB_IP";
            this.tB_IP.Size = new System.Drawing.Size(223, 28);
            this.tB_IP.TabIndex = 2;
            this.tB_IP.Text = "10.24.201.125";
            // 
            // tB_Port
            // 
            this.tB_Port.Location = new System.Drawing.Point(242, 206);
            this.tB_Port.Name = "tB_Port";
            this.tB_Port.Size = new System.Drawing.Size(222, 28);
            this.tB_Port.TabIndex = 3;
            this.tB_Port.Text = "80";
            // 
            // b_confirm
            // 
            this.b_confirm.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.b_confirm.Location = new System.Drawing.Point(583, 108);
            this.b_confirm.Name = "b_confirm";
            this.b_confirm.Size = new System.Drawing.Size(166, 46);
            this.b_confirm.TabIndex = 4;
            this.b_confirm.Text = "Confirm";
            this.b_confirm.UseVisualStyleBackColor = true;
            this.b_confirm.Click += new System.EventHandler(this.Confirm_click);
            // 
            // IPinput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.b_confirm);
            this.Controls.Add(this.tB_Port);
            this.Controls.Add(this.tB_IP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "IPinput";
            this.Text = "IPinput";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tB_IP;
        private System.Windows.Forms.TextBox tB_Port;
        private System.Windows.Forms.Button b_confirm;
    }
}