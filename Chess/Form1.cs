using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
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
            
            
            GraphicsUnit unit = GraphicsUnit.Pixel;
            
            SolidBrush white = new SolidBrush(Color.PeachPuff);
            SolidBrush black = new SolidBrush(Color.SandyBrown);
            Image bm = new Bitmap(Properties.Resources.rock);
            int squareSize = 40;
            

            //g.FillRectangle(white, 20 / 8 + 20, 20 % 8 + 20, 40, 40);



            for (int i = 0; i < 64; i += 2)
            {
                g.FillRectangle(white, (i / 8) * squareSize, (i % 8 + (i / 8) % 2) * squareSize, squareSize, squareSize);
                g.FillRectangle(black, ((i + 1) / 8) * squareSize, ((i + 1) % 8 - ((i + 1) / 8) % 2) * squareSize, squareSize, squareSize);
            }

            for (int i = 0; i < 64; i++)
            {
                Rectangle rect = new Rectangle((i % 8) * squareSize, (i / 8) * squareSize, 40, 40);
                switch (Program.chessBoard[i / 8, i % 8])
                {
                    case "r":
                        bm = Properties.Resources.brock;
                        g.DrawImage(bm, rect, 0, 0, 200, 200, unit);
                        break;
                    case "k":
                        bm = Properties.Resources.bknight;   
                        g.DrawImage(bm, rect, 0, 0, 200, 200, unit);
                        break;
                    case "b":  
                        bm = Properties.Resources.bbishop;
                        g.DrawImage(bm, rect, 0, 0, 200, 200, unit);
                        break;
                    case "q": 
                        g.DrawImage(bm, rect, 0, 0, 200, 200, unit);
                        break;
                    case "a":
                        bm = Properties.Resources.bking;
                        g.DrawImage(bm, rect, 0, 0, 200, 200, unit);
                        break;
                    case "p":
                        
                        bm = Properties.Resources.bpawn;
                        g.DrawImage(bm, rect, 0, 0, 200, 200, unit);
                        break;
                    case "R":                       
                        bm = Properties.Resources.rock;
                        g.DrawImage(bm, rect, 0, 0, 200, 200, unit);
                        break;
                    case "K":
                        bm = Properties.Resources.knight;
                        g.DrawImage(bm, rect, 0, 0, 200, 200, unit);
                        break;
                    case "B":
                        bm = Properties.Resources.bishop;
                        g.DrawImage(bm, rect, 0, 0, 200, 200, unit);
                        break;
                    case "Q":
                        bm = Properties.Resources.queen;
                        g.DrawImage(bm, rect, 0, 0, 200, 200, unit);
                        break;
                    case "A":
                        bm = Properties.Resources.king;
                        g.DrawImage(bm, rect, 0, 0, 200, 200, unit);
                        break;
                    case "P":
                        bm = Properties.Resources.pawn;
                        g.DrawImage(bm, rect, 0, 0, 200, 200, unit);
                        break;
                }
            }


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
