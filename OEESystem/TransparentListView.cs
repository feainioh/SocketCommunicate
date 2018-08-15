using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace OEESystem
{
    public partial class TransparentListView : ListView
    {
        private int CurSelectIndex=-1;
        public TransparentListView()
        {
            SetStyle(ControlStyles.UserPaint|ControlStyles.DoubleBuffer |ControlStyles.OptimizedDoubleBuffer |ControlStyles.AllPaintingInWmPaint|ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();
        }
        //protected override void OnPaintBackground(PaintEventArgs pevent)
        //{
        //    base.OnPaintBackground(pevent);
        //    if (TopItem == null)
        //    {
        //        return;
        //    }
        //    //要画几个
        //    int CurIn = this.Height / 18 + 1;
        //    //第一项非空时才画
        //    if (TopItem != null && TopItem.Index >= 0)
        //    {
        //        CurSelectIndex = this.SelectedIndices[0];
        //        if (CurSelectIndex == -1 || CurSelectIndex < TopItem.Index || CurSelectIndex > (TopItem.Index + this.Height / 20))
        //        {

        //        }
        //        else
        //        {
        //            int temp = CurSelectIndex - TopItem.Index;
        //            pevent.Graphics.FillRectangle(Brushes.AliceBlue, 0, 21 * temp, 300, 21);
        //        }
        //        for (int i = 0; i < CurIn; i++)
        //        {
        //            if ((TopItem.Index + i) < this.Items.Count)
        //            {
        //                pevent.Graphics.DrawString(this.Items[TopItem.Index + i].Text, this.Font, Brushes.Black, 30, i * 21 + 1);
        //                pevent.Graphics.DrawString(this.Items[i].SubItems[0].Text, this.Font, new SolidBrush(this.ForeColor), 30 +  120, i * 21 + 1);
        //                pevent.Graphics.DrawString(this.Items[i].SubItems[1].Text, this.Font, new SolidBrush(this.ForeColor), 30 +  120+133, i * 21 + 1);
        //                pevent.Graphics.DrawString(this.Items[i].SubItems[2].Text, this.Font, new SolidBrush(this.ForeColor), 30 + 120 + 133+270, i * 21 + 1);
                        
        //            }
        //        }
        //    }
        //}
 
//         protected override void WndProc(ref Message m)
//         {
//             if (m.Msg == 0x0014 || m.Msg == 0x0015)
//             {
//                 this.Invalidate();
//             }
//             base.WndProc(ref m);
//         }
        //protected override void OnMouseWheel(MouseEventArgs e)
        // {
        //     base.OnMouseWheel(e);
        //     this.Invalidate();
//         // }
//         protected override void OnSelectedIndexChanged(EventArgs e)
//         {
//             this.Invalidate();
//             base.OnSelectedIndexChanged(e);
//         }
//         protected override void OnPaint(PaintEventArgs e)
//         {
//             if (TopItem != null && TopItem.Index >= 0)
//             {
//                 if (this.Focused)
//                 {
//                     Rectangle itemRect = this.GetItemRect(this.SelectedIndices[0]);
//                     e.Graphics.FillRectangle(Brushes.AliceBlue, itemRect);
//                 }
//                 int count = this.Height / 21 + 1;
//                 for (int i = 0; i < this.Items.Count ; i++)
//                 {
//                     e.Graphics.DrawString(this.Items[i].Text, this.Font, new SolidBrush(this.ForeColor), this.GetItemRect(i));
//                         
//                         
//                     
//                 }
//             }
//             base.OnPaint(e);
//         }


    }
}
