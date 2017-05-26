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
        static void Main(string[] args)
        {
            kingPositionU = 0;
            while(!"A".Equals(chessBoard[kingPositionU/8,kingPositionU%8])){
                kingPositionU++;
            }
        }
        static string possibleMove()
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
                    }

                }
            }
            return list;
        }
        static string possibleA(int i)
        {
            string list = "";
            int row = i / 8, col = i % 8;
            for (int j = 0; j < 9; j++)
            {
                if (j != 4)
                {
                    if (Char.IsLower(chessBoard[row - 1 + j / 3, col - 1 + j % 3], 1) || " ".Equals(chessBoard[row - 1 + j / 3, col - 1 + j % 3]))
                    {

                    }
                }
            }
            return list;
        }
    }
}
