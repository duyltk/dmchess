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
        int squareSize = 40;
        int mouseX, mouseY;
        int newMouseX, newMouseY;
        String Move = "lol";
        bool flag = false;
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
       

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            
            mouseX = e.X;
            mouseY = e.Y;
            
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        { 
            newMouseX = e.X;
            newMouseY = e.Y;
            Move = "" + (mouseY / squareSize) + (mouseX / squareSize) + (newMouseY / squareSize) + (newMouseX / squareSize) + (Program.chessBoard[(newMouseY / squareSize), (newMouseX / squareSize)]);
            
            String possibleMoveUser = MoveIllegal(mouseY, mouseX);
            
            if (possibleMoveUser.Contains(Move))
            {               
                Program.makeMove(Move);
                this.Refresh();
            }


        }
        private String MoveIllegal(int mouseY, int mouseX)
        {
            String possibleMoveUser = "";            
            switch (Program.chessBoard[mouseY / squareSize, mouseX / squareSize])
            {
                case "p":
                case "P":
                    possibleMoveUser = Program.possibleP(mouseY / squareSize * 8 + mouseX / squareSize);
                    break;
                case "r":
                case "R":
                    possibleMoveUser = Program.possibleR(mouseY / squareSize * 8 + mouseX / squareSize);
                    break;
                case "k":
                case "K":
                    possibleMoveUser = Program.possibleK(mouseY / squareSize * 8 + mouseX / squareSize);
                    break;
                case "b":
                case "B":
                    possibleMoveUser = Program.possibleB(mouseY / squareSize * 8 + mouseX / squareSize);
                    break;
                case "q":
                case "Q":
                    possibleMoveUser = Program.possibleQ(mouseY / squareSize * 8 + mouseX / squareSize);
                    break;
                case "a":
                case "A":
                    possibleMoveUser = Program.possibleA(mouseY / squareSize * 8 + mouseX / squareSize);
                    break;
            }
            return possibleMoveUser;
        }
    }
}
