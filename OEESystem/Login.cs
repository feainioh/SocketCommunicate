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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color FColor = Color.GreenYellow;
            Color TColor = Color.Orange;
            Brush b = new LinearGradientBrush(this.ClientRectangle, FColor, TColor, LinearGradientMode.ForwardDiagonal);//窗口渐变色
            g.FillRectangle(b, this.ClientRectangle);
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if (textBox_password.Text.ToUpper() == GlobalVar.gl_password.ToUpper())
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("密碼錯誤", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox_password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox_password.Text.ToUpper() == GlobalVar.gl_password.ToUpper())
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("密碼錯誤", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}
