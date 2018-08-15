namespace OEESystem
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip_Main = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel_StartWatch = new System.Windows.Forms.ToolStripButton();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.管理员登录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton_serial = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel_syncTime = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel_Time = new System.Windows.Forms.ToolStripLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel_Clients = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox_ServerPort = new OEESystem.TransparentTextBox();
            this.textBox_ServerIP = new OEESystem.TransparentTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Communication = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.listView_CennectClient = new System.Windows.Forms.ListView();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label_Version = new System.Windows.Forms.Label();
            this.serialPort_Server = new System.IO.Ports.SerialPort(this.components);
            this.toolStrip_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip_Main
            // 
            this.toolStrip_Main.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip_Main.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStrip_Main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel_StartWatch,
            this.toolStripSplitButton1,
            this.toolStripButton_serial,
            this.toolStripLabel_syncTime});
            this.toolStrip_Main.Location = new System.Drawing.Point(0, 0);
            this.toolStrip_Main.Name = "toolStrip_Main";
            this.toolStrip_Main.Size = new System.Drawing.Size(864, 29);
            this.toolStrip_Main.TabIndex = 0;
            this.toolStrip_Main.Text = "toolStrip1";
            // 
            // toolStripLabel_StartWatch
            // 
            this.toolStripLabel_StartWatch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripLabel_StartWatch.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLabel_StartWatch.Image")));
            this.toolStripLabel_StartWatch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripLabel_StartWatch.Name = "toolStripLabel_StartWatch";
            this.toolStripLabel_StartWatch.Size = new System.Drawing.Size(78, 26);
            this.toolStripLabel_StartWatch.Text = "开始监听";
            this.toolStripLabel_StartWatch.Click += new System.EventHandler(this.toolStripLabel_StartWatch_Click);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.管理员登录ToolStripMenuItem});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(32, 26);
            this.toolStripSplitButton1.Text = "管理员操作";
            // 
            // 管理员登录ToolStripMenuItem
            // 
            this.管理员登录ToolStripMenuItem.Name = "管理员登录ToolStripMenuItem";
            this.管理员登录ToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.管理员登录ToolStripMenuItem.Text = "管理员登录";
            this.管理员登录ToolStripMenuItem.Click += new System.EventHandler(this.管理员登录ToolStripMenuItem_Click);
            // 
            // toolStripButton_serial
            // 
            this.toolStripButton_serial.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_serial.Image = global::OEESystem.Properties.Resources.journal_48px_541726_easyicon_net;
            this.toolStripButton_serial.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_serial.Name = "toolStripButton_serial";
            this.toolStripButton_serial.Size = new System.Drawing.Size(23, 26);
            this.toolStripButton_serial.Text = "查看日志";
            this.toolStripButton_serial.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripLabel_syncTime
            // 
            this.toolStripLabel_syncTime.Name = "toolStripLabel_syncTime";
            this.toolStripLabel_syncTime.Size = new System.Drawing.Size(0, 26);
            // 
            // toolStripLabel_Time
            // 
            this.toolStripLabel_Time.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripLabel_Time.Name = "toolStripLabel_Time";
            this.toolStripLabel_Time.Size = new System.Drawing.Size(111, 26);
            this.toolStripLabel_Time.Text = "toolStripLabel1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 29);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel_Clients);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txt_Communication);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.listView_CennectClient);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(864, 472);
            this.splitContainer1.SplitterDistance = 571;
            this.splitContainer1.TabIndex = 1;
            // 
            // flowLayoutPanel_Clients
            // 
            this.flowLayoutPanel_Clients.AutoScroll = true;
            this.flowLayoutPanel_Clients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel_Clients.Location = new System.Drawing.Point(0, 33);
            this.flowLayoutPanel_Clients.Name = "flowLayoutPanel_Clients";
            this.flowLayoutPanel_Clients.Size = new System.Drawing.Size(571, 439);
            this.flowLayoutPanel_Clients.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.textBox_ServerPort);
            this.panel1.Controls.Add(this.textBox_ServerIP);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(571, 33);
            this.panel1.TabIndex = 1;
            // 
            // textBox_ServerPort
            // 
            this.textBox_ServerPort.BackColor = System.Drawing.Color.Transparent;
            this.textBox_ServerPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_ServerPort.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_ServerPort.Location = new System.Drawing.Point(268, 4);
            this.textBox_ServerPort.Name = "textBox_ServerPort";
            this.textBox_ServerPort.Size = new System.Drawing.Size(51, 26);
            this.textBox_ServerPort.TabIndex = 3;
            // 
            // textBox_ServerIP
            // 
            this.textBox_ServerIP.BackColor = System.Drawing.Color.Transparent;
            this.textBox_ServerIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_ServerIP.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_ServerIP.Location = new System.Drawing.Point(77, 4);
            this.textBox_ServerIP.Name = "textBox_ServerIP";
            this.textBox_ServerIP.Size = new System.Drawing.Size(123, 26);
            this.textBox_ServerIP.TabIndex = 2;
            this.textBox_ServerIP.Text = "255.255.255.255";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(0, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务端 IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(207, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "端口号:";
            // 
            // txt_Communication
            // 
            this.txt_Communication.BackColor = System.Drawing.Color.CadetBlue;
            this.txt_Communication.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Communication.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Communication.Location = new System.Drawing.Point(0, 241);
            this.txt_Communication.Name = "txt_Communication";
            this.txt_Communication.ReadOnly = true;
            this.txt_Communication.Size = new System.Drawing.Size(289, 193);
            this.txt_Communication.TabIndex = 11;
            this.txt_Communication.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(0, 221);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "服务器通信记录:";
            // 
            // listView_CennectClient
            // 
            this.listView_CennectClient.BackgroundImage = global::OEESystem.Properties.Resources.image___副本1;
            this.listView_CennectClient.BackgroundImageTiled = true;
            this.listView_CennectClient.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView_CennectClient.Dock = System.Windows.Forms.DockStyle.Top;
            this.listView_CennectClient.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView_CennectClient.Location = new System.Drawing.Point(0, 20);
            this.listView_CennectClient.Name = "listView_CennectClient";
            this.listView_CennectClient.Size = new System.Drawing.Size(289, 201);
            this.listView_CennectClient.TabIndex = 9;
            this.listView_CennectClient.UseCompatibleStateImageBehavior = false;
            this.listView_CennectClient.View = System.Windows.Forms.View.Details;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "设备连接列表:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label_Version);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 434);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(289, 38);
            this.panel2.TabIndex = 6;
            // 
            // label_Version
            // 
            this.label_Version.AutoSize = true;
            this.label_Version.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label_Version.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Version.Location = new System.Drawing.Point(0, 17);
            this.label_Version.Name = "label_Version";
            this.label_Version.Size = new System.Drawing.Size(40, 21);
            this.label_Version.TabIndex = 0;
            this.label_Version.Text = "label5";
            this.label_Version.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.label_Version.UseCompatibleTextRendering = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.BackgroundImage = global::OEESystem.Properties.Resources.BackGround;
            this.ClientSize = new System.Drawing.Size(864, 501);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip_Main);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "OEE稼动率采集系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip_Main.ResumeLayout(false);
            this.toolStrip_Main.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip_Main;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem 管理员登录ToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_Time;
        private System.Windows.Forms.ToolStripButton toolStripLabel_StartWatch;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_Clients;
        private System.Windows.Forms.Label label_Version;
        private System.Windows.Forms.ListView listView_CennectClient;
        private System.Windows.Forms.Label label4;
        private TransparentTextBox textBox_ServerIP;
        private TransparentTextBox textBox_ServerPort;
        private System.Windows.Forms.ToolStripButton toolStripButton_serial;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_syncTime;
        private System.Windows.Forms.RichTextBox txt_Communication;
        private System.IO.Ports.SerialPort serialPort_Server;


    }
}

