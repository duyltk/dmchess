using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Chess
{
    class Class1
    {
        public static void Main(string[] args)
        {
            Image img = Image.FromFile("FileName.gif");

            // Kích thước phần ảnh nhỏ mà bạn muốn hiển thị
            Bitmap b = new Bitmap(100, 100);

            // Draw lên Bitmap đó
            Graphics gBitMap = new Graphics.FromImage(b);

            // Tọa độ X,Y,W,H của phần ảnh nhỏ trên hình lớn.
            gBitMap.DrawImage(img, 0, 0, 100, 100, Xsrc, Ysrc, W, H);

            // Cuoi cung
            PictureBox.Image = b
        }
    }
}
