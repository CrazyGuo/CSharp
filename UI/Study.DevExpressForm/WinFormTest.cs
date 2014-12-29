using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Study.DevExpressForm
{
    public partial class WinFormTest : Form
    {
        public WinFormTest()
        {
            InitializeComponent();
        }

        private void btn_bitmapDigital_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 300; i++)
            {
                Bitmap bmp = new Bitmap(400, 400);
                Graphics g = Graphics.FromImage(bmp);//利用该图片对象生成“画板”
                Color colorback = Color.Orange;
                Color pen = Color.Black;
                Font font = new Font("黑体", 150);//设置字体颜色
                SolidBrush brush = new SolidBrush(pen);//新建一个画刷,到这里为止,我们已经准备好了画板、画刷、和数据

                Pen p = new Pen(Color.Red, 16);//定义了一个红色,宽度为的画笔
                g.Clear(colorback); //设置黑色背景

                g.DrawString(i.ToString("000"), font, brush, 70, 100);

                bmp.Save(@"C:\oracle\temp\" + i.ToString("000") + ".bmp");//保存为输出流，否则页面上显示不出来
                
             }
        }
    }
}
