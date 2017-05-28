using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Program
    {
        static string[,] chessBoard={
        {"r","k","b","q","a","b","k","r"},
        {"p","p","p","p","p","p","p","p"},
        {" "," "," "," "," "," "," "," "},
        {" "," "," "," "," "," "," "," "},
        {" "," "," "," "," "," "," "," "},
        {" "," "," "," "," "," "," "," "},
        {"P","P","P","P","P","P","P","P"},
        {"R","K","B","Q","A","B","K","R"}};
        
        static int kingPositionU;
        public static void Main(string[] args)
        {
            kingPositionU = 0;
            while(!"A".Equals(chessBoard[kingPositionU/8,kingPositionU%8])){
                kingPositionU++;
            }
        }
        public static string possibleMove()
        {
            string list = "";

            for (int i = 0; i < 64; i++)
            {
                if (Char.IsUpper(chessBoard[i / 8, i % 8], 1))
                {
                    switch (chessBoard[i / 8, i % 8])
                    {
                        case "A":
                            list += possibleA(i);
                            break;
                        case "B":
                            list += possibleB(i);
                            break;
                        case "Q":
                            list += possibleQ(i);
                            break;
                    }

                }
            }
            return list;
        }
        public static string possibleA(int i)
        {
            string list = "", oldPiece;
            int row = i / 8, col = i % 8;
            for (int j = 0; j < 9; j++)
            {
                //J==4 is present king's position 
                if (j != 4)
                {
                    try
                    {//Check position around of the king is enemy or blank
                        if (Char.IsLower(chessBoard[row - 1 + j / 3, col - 1 + j % 3], 1) || " ".Equals(chessBoard[row - 1 + j / 3, col - 1 + j % 3]))
                        {
                            oldPiece = chessBoard[row - 1 + j / 3, col - 1 + j % 3];
                            chessBoard[row, col] = " ";
                            chessBoard[row - 1 + j / 3, col - 1 + j % 3] = "A";
                            int tempKing = kingPositionU;
                            kingPositionU = i - 9 + j * 8 / 3 + j % 3;
                            if (safeKing())
                            {
                                //Old Position [ROW, COL] => New Position [..., ...], where is OldPiece
                                list = list + row + col + (row - 1 + j / 3) + (col - 1 + j % 3) + oldPiece;
                            }
                            chessBoard[row, col] = "A";
                            chessBoard[row - 1 + j / 3, col - 1 + j % 3] = oldPiece;
                            kingPositionU = tempKing;
                        }
                    }
                    catch (Exception e) { }
                }
            }
            return list;
        }
        public static string possibleB(int i)
        {
            string list = "";
            return list;
        }

        public static string possibleQ(int i)
        {
            string list = "", oldPiece;
            int row = i / 8, col = i % 8;
            int distance = 1;
            for (int j = -1; j <= 1; j++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    if (j != 0 || k != 0)
                    {
                        try
                        {
                            while (" ".Equals(chessBoard[row + distance * j, col + distance * k]))
                            {
                                oldPiece = " ";
                                chessBoard[row, col] = " ";
                                chessBoard[row + distance * j, col + distance * k] = "Q";
                                if (safeKing())
                                {
                                    list = list + row + col + (row + distance * j) + (col + distance * k) + oldPiece;
                                }
                                chessBoard[row, col] = "Q";
                                chessBoard[row + distance * j, col + distance * k] = oldPiece;
                                distance++;
                            }

                            if (Char.IsLower(chessBoard[row + distance * j, col + distance * k], 1))
                            {
                                oldPiece = " ";
                                chessBoard[row, col] = " ";
                                chessBoard[row + distance * j, col + distance * k] = "Q";
                                if (safeKing())
                                {
                                    list = list + row + col + (row + distance * j) + (col + distance * k) + oldPiece;
                                }
                                chessBoard[row, col] = "Q";
                                chessBoard[row + distance * j, col + distance * k] = oldPiece;
                            }
                        }
                        catch (Exception e) { }
                        distance = 1;
                    }
                }
            }
            return list;
        }
        //Don't work with
        static Boolean safeKing()
        {

            return true;
        }
    }
}
