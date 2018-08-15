using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OEESystem
{
    public partial class ReadLogs : Form
    {
        private bool isSaved = true;//用于判断是否保存过文本

        MyFunctions myf = new MyFunctions();

        public ReadLogs()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 新建NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isSaved)
            {
                if (MessageBox.Show("要保存现有文件吗", "提示对话框", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                   == DialogResult.Yes)
                {
                    保存SToolStripMenuItem_Click(sender, e);
                }
            }
                this.richTextBox1.ReadOnly = false;
                this.richTextBox1.Text = "";
            
        }

        private void 保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                this.richTextBox1.SaveFile(savefile.FileName, RichTextBoxStreamType.PlainText);
                isSaved = true;
                this.richTextBox1.ReadOnly = false;
                toolStripStatus_status.Text = "保存文本:"+savefile.FileName;
            }
        }

        private void 全选AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.SelectAll();
        }

        private void 粘贴PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Paste();
        }

        private void 复制CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Copy();
        }

        private void 剪切TToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Cut();
        }

        private void 重复RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Redo();
        }

        private void 撤消UToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Undo();
        }

        private void 新建NToolStripButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("要保存现有文件吗", "提示对话框", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
              == DialogResult.Yes)
            {
                保存SToolStripMenuItem_Click(sender, e);
            }

            else
            {
                this.richTextBox1.Text = "";
            }
        }

        private void 打开OToolStripButton_Click(object sender, EventArgs e)
        {

            OpenFileDialog myDlg = new OpenFileDialog();
            myDlg.CheckFileExists = true;
            myDlg.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (myDlg.ShowDialog() == DialogResult.OK)
            {
                StreamReader sReader = new StreamReader(myDlg.FileName, Encoding.UTF8);
                this.richTextBox1.Text = sReader.ReadToEnd();
                this.statusStrip1.Text = myDlg.FileName;
                this.richTextBox1.ReadOnly = true;
                toolStripStatus_status.Text = "打开文本:" + myDlg.FileName;
            }
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Copy();
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Paste();
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Cut();
        }

        private void 复制CToolStripButton_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Copy();
        }

        private void 粘贴PToolStripButton_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Paste();
        }

        private void 剪切UToolStripButton_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Cut();
        }

        private void 保存SToolStripButton_Click(object sender, EventArgs e)
        {
            保存SToolStripMenuItem_Click(sender, e);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isSaved)
            {
                if (MessageBox.Show("要保存现有文件吗", "提示对话框", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                  == DialogResult.Yes)
                {
                    保存SToolStripMenuItem_Click(sender, e);
                }
            }
        }

        private void 自定义CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.WordWrap == true)
            {
                richTextBox1.WordWrap = false;
            }
            else
            {
                richTextBox1.WordWrap = true;
            }
        }

        private void 字字颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.AllowFullOpen = true;
            colorDialog.FullOpen = true;
            colorDialog.ShowHelp = true;
            colorDialog.Color = Color.Black;//初始化当前文本框中的字体颜色，当用户在ColorDialog对话框中点击"取消"按钮恢复原来的值
            if (colorDialog.ShowDialog() != DialogResult.Cancel)
            {
                this.richTextBox1.SelectionColor = colorDialog.Color; //将当前选定的文字改变颜色
            }
            richTextBox1.Focus();

        }

        private void 字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
            font.Color = richTextBox1.ForeColor;
            font.AllowScriptChange = true;
            if (font.ShowDialog() != DialogResult.Cancel)
            {
                richTextBox1.SelectionFont = font.Font;//将当前选定的文字改变字体
            }
            richTextBox1.Focus();

        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            isSaved = false;
        }

        private void commenLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string comPath = Application.StartupPath + @"\log\Common\";
            OpenFileDialog myDlg = new OpenFileDialog();
            myDlg.InitialDirectory = comPath;
            myDlg.CheckFileExists = true;
            myDlg.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (myDlg.ShowDialog() == DialogResult.OK)
            {
                //this.richTextBox1.LoadFile(myDlg.FileName, RichTextBoxStreamType.PlainText);
                //this.statusStrip1.Text = myDlg.FileName;
                StreamReader sReader = new StreamReader(myDlg.FileName, Encoding.UTF8);
                this.richTextBox1.Text = sReader.ReadToEnd();
                this.statusStrip1.Text = myDlg.FileName;
                this.richTextBox1.ReadOnly = true;
                toolStripStatus_status.Text = "查看流程日志:" + myDlg.FileName;

            }
        }

        private void errorLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string errPath = Application.StartupPath + @"\log\Error\";
            OpenFileDialog myDlg = new OpenFileDialog();
            myDlg.InitialDirectory = errPath;
            myDlg.CheckFileExists = true;
            myDlg.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (myDlg.ShowDialog() == DialogResult.OK)
            {
                //this.richTextBox1.LoadFile(myDlg.FileName, RichTextBoxStreamType.PlainText);
                //this.statusStrip1.Text = myDlg.FileName;
                StreamReader sReader = new StreamReader(myDlg.FileName, Encoding.UTF8);
                this.richTextBox1.Text = sReader.ReadToEnd();
                this.statusStrip1.Text = myDlg.FileName;
                this.richTextBox1.ReadOnly = true;
                toolStripStatus_status.Text = "查看异常日志:" + myDlg.FileName;

            }
        }

        private void 上传异常文本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dir_path = Application.StartupPath + "\\" + GlobalVar.ErrorText + "\\";
            OpenFileDialog myDlg = new OpenFileDialog();
            myDlg.InitialDirectory = dir_path;
            myDlg.CheckFileExists = true;
            myDlg.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (myDlg.ShowDialog() == DialogResult.OK)
            {
                StreamReader sReader = new StreamReader(myDlg.FileName, Encoding.UTF8);
                this.richTextBox1.Text = sReader.ReadToEnd();
                this.statusStrip1.Text = myDlg.FileName;
                this.richTextBox1.ReadOnly = true;
            }
        }

        private void 上传文档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dir_path = Application.StartupPath +@"\log\";
            OpenFileDialog myDlg = new OpenFileDialog();
            myDlg.InitialDirectory = dir_path;
            myDlg.CheckFileExists = true;
            myDlg.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (myDlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileInfo file = new FileInfo(myDlg.FileName);
                    file.CopyTo(GlobalVar.gl_updateLog + @"\" + file.Name);
                }
                catch(Exception ex)
                {
                    myf.writeErrorLog("上传日志异常"+ex.ToString());
                }
            }
        }
    }
}
