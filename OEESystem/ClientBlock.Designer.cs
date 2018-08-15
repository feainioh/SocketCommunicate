namespace OEESystem
{
    partial class ClientBlock
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.richTextBox_Conmunication = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox_IP = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_ClientName = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.richTextBox_Conmunication);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 67);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(182, 82);
            this.panel1.TabIndex = 1;
            // 
            // richTextBox_Conmunication
            // 
            this.richTextBox_Conmunication.BackColor = System.Drawing.Color.DarkOrchid;
            this.richTextBox_Conmunication.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_Conmunication.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_Conmunication.ForeColor = System.Drawing.SystemColors.WindowText;
            this.richTextBox_Conmunication.Location = new System.Drawing.Point(0, 0);
            this.richTextBox_Conmunication.Name = "richTextBox_Conmunication";
            this.richTextBox_Conmunication.ReadOnly = true;
            this.richTextBox_Conmunication.Size = new System.Drawing.Size(182, 82);
            this.richTextBox_Conmunication.TabIndex = 0;
            this.richTextBox_Conmunication.Text = "";
            this.richTextBox_Conmunication.TextChanged += new System.EventHandler(this.richTextBox_Conmunication_TextChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.OliveDrab;
            this.panel2.Controls.Add(this.textBox_IP);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 44);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(182, 23);
            this.panel2.TabIndex = 2;
            // 
            // textBox_IP
            // 
            this.textBox_IP.AutoSize = true;
            this.textBox_IP.Dock = System.Windows.Forms.DockStyle.Left;
            this.textBox_IP.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_IP.Location = new System.Drawing.Point(40, 0);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(50, 20);
            this.textBox_IP.TabIndex = 2;
            this.textBox_IP.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "终端:";
            // 
            // btn_ClientName
            // 
            this.btn_ClientName.BackColor = System.Drawing.Color.LawnGreen;
            this.btn_ClientName.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_ClientName.FlatAppearance.BorderSize = 0;
            this.btn_ClientName.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn_ClientName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ClientName.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ClientName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_ClientName.Location = new System.Drawing.Point(0, 0);
            this.btn_ClientName.Name = "btn_ClientName";
            this.btn_ClientName.Size = new System.Drawing.Size(182, 44);
            this.btn_ClientName.TabIndex = 0;
            this.btn_ClientName.Text = "ClientDevice";
            this.btn_ClientName.UseVisualStyleBackColor = false;
            this.btn_ClientName.Click += new System.EventHandler(this.btn_ClientName_Click);
            // 
            // ClientBlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_ClientName);
            this.DoubleBuffered = true;
            this.Name = "ClientBlock";
            this.Size = new System.Drawing.Size(182, 149);
            this.Load += new System.EventHandler(this.ClientBlock_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_ClientName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox_Conmunication;
        private System.Windows.Forms.Label textBox_IP;
    }
}
