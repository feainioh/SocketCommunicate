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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color TColor = ColorTranslator.FromHtml("#00dbde"); //Color.SteelBlue;
            Color FColor = ColorTranslator.FromHtml("#fc00ff");//Color.Gold;
            Brush b = new LinearGradientBrush(this.ClientRectangle, FColor, TColor, LinearGradientMode.Vertical);//窗口渐变色
            //b = new LinearGradientBrush(this.ClientRectangle,FColor,TColor,96.7f,true);
            g.FillRectangle(b, this.ClientRectangle);
        }
    }
}
