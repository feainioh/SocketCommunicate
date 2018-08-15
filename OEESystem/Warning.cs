using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace OEESystem
{
    public partial class Warning : Form
    {
        MyFunctions myFunc = new MyFunctions();
        string message = "";
        public Warning()
        {
            InitializeComponent();
        }
        public Warning(string msg)
        {
            InitializeComponent();
            this.message = msg;
        }

        private void Warning_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color FColor = ColorTranslator.FromHtml("#ff5858"); //Color.SteelBlue;
            Color TColor = ColorTranslator.FromHtml("#f09819");//Color.Gold;
            Brush b = new LinearGradientBrush(this.ClientRectangle, FColor, TColor, LinearGradientMode.Vertical);//窗口渐变色
            g.FillRectangle(b, this.ClientRectangle);
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Warning_Load(object sender, EventArgs e)
        {

            transparentTextBox_msg.Text = message;
            myFunc.writeCommLog("超出权限操作:" + message.Replace("\r\n", ""));
        }
    }
}
