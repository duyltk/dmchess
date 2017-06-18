using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Program
    {
        static int globalDepth = 4;
        static int node = 0;
        static String[,] chessBoard2 ={
        {"r","k","b","q","a","b","k","r"},
        {"p","p","p","p","p","p","p","p"},
        {" "," "," "," "," "," "," "," "},
        {" "," "," "," "," "," "," "," "},
        {" "," "," "," "," "," "," "," "},
        {" "," "," "," "," "," "," "," "},
        {"P","P","P","P","P","P","P","P"},
        {"R","K","B","Q","A","B","K","R"}
        };

        static String[,] chessBoard ={
        {"r","k","b","q"," ","b","k","r"},
		{"p","p","p","p","a","p","p","p"},
		{" "," "," "," "," "," "," "," "},
		{" "," "," "," "," "," "," "," "},
		{" "," "," "," "," "," "," "," "},
		{" "," "," ","Q"," "," "," "," "},
		{"P","P","P","P","P","P","P","P"},
		{"R","K","B"," ","A","B","R","R"}
        };

        static int[,] pawnBoard ={//attribute to http://chessprogramming.wikispaces.com/Simplified+evaluation+function
        { 0,  0,  0,  0,  0,  0,  0,  0},
        {50, 50, 50, 50, 50, 50, 50, 50},
        {10, 10, 20, 30, 30, 20, 10, 10},
        { 5,  5, 10, 25, 25, 10,  5,  5},
        { 0,  0,  0, 20, 20,  0,  0,  0},
        { 5, -5,-10,  0,  0,-10, -5,  5},
        { 5, 10, 10,-20,-20, 10, 10,  5},
        { 0,  0,  0,  0,  0,  0,  0,  0}};

        static int[,] rookBoard ={
        { 0,  0,  0,  0,  0,  0,  0,  0},
        { 5, 10, 10, 10, 10, 10, 10,  5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        {-5,  0,  0,  0,  0,  0,  0, -5},
        { 0,  0,  0,  5,  5,  0,  0,  0}};

        static int[,] knightBoard ={
        {-50,-40,-30,-30,-30,-30,-40,-50},
        {-40,-20,  0,  0,  0,  0,-20,-40},
        {-30,  0, 10, 15, 15, 10,  0,-30},
        {-30,  5, 15, 20, 20, 15,  5,-30},
        {-30,  0, 15, 20, 20, 15,  0,-30},
        {-30,  5, 10, 15, 15, 10,  5,-30},
        {-40,-20,  0,  5,  5,  0,-20,-40},
        {-50,-40,-30,-30,-30,-30,-40,-50}};

        static int[,] bishopBoard ={
        {-20,-10,-10,-10,-10,-10,-10,-20},
        {-10,  0,  0,  0,  0,  0,  0,-10},
        {-10,  0,  5, 10, 10,  5,  0,-10},
        {-10,  5,  5, 10, 10,  5,  5,-10},
        {-10,  0, 10, 10, 10, 10,  0,-10},
        {-10, 10, 10, 10, 10, 10, 10,-10},
        {-10,  5,  0,  0,  0,  0,  5,-10},
        {-20,-10,-10,-10,-10,-10,-10,-20}};

        static int[,] queenBoard ={
        {-20,-10,-10, -5, -5,-10,-10,-20},
        {-10,  0,  0,  0,  0,  0,  0,-10},
        {-10,  0,  5,  5,  5,  5,  0,-10},
        { -5,  0,  5,  5,  5,  5,  0, -5},
        {  0,  0,  5,  5,  5,  5,  0, -5},
        {-10,  5,  5,  5,  5,  5,  0,-10},
        {-10,  0,  5,  0,  0,  0,  0,-10},
        {-20,-10,-10, -5, -5,-10,-10,-20}};

        static int[,] kingMidBoard ={
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-30,-40,-40,-50,-50,-40,-40,-30},
        {-20,-30,-30,-40,-40,-30,-30,-20},
        {-10,-20,-20,-20,-20,-20,-20,-10},
        { 20, 20,  0,  0,  0,  0, 20, 20},
        { 20, 30, 10,  0,  0, 10, 30, 20}};

        static int[,] kingEndBoard ={
        {-50,-40,-30,-20,-20,-30,-40,-50},
        {-30,-20,-10,  0,  0,-10,-20,-30},
        {-30,-10, 20, 30, 30, 20,-10,-30},
        {-30,-10, 30, 40, 40, 30,-10,-30},
        {-30,-10, 30, 40, 40, 30,-10,-30},
        {-30,-10, 20, 30, 30, 20,-10,-30},
        {-30,-30,  0,  0,  0,  0,-30,-30},
        {-50,-30,-30,-30,-30,-30,-30,-50}};

        static int kingPositionU;
        static int kingPositionL;
        static bool castlingUShort = true;
        static bool castlingULong = true;
        static bool castlingLShort = true;
        static bool castlingLLong = true;

        public static void Main(string[] args)
        {

            kingPositionU = 0;
            while (!"A".Equals(chessBoard[kingPositionU / 8, kingPositionU % 8]))
            {
                kingPositionU++;
            }
            kingPositionL = 0;
            while (!"a".Equals(chessBoard[kingPositionL / 8, kingPositionL % 8]))
            {
                kingPositionL++;
            }
            globalDepth = 4;
            drawChessBoard();
            System.Console.WriteLine(possibleMove());
            System.Console.WriteLine(alphaBeta(globalDepth, -1000000, 1000000, "", 0));
            System.Console.WriteLine(node);
            //Console.ReadLine();
            //flipboard();
        }
        public static void drawChessBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    System.Console.Write(chessBoard[i, j] + ";");
                }
                System.Console.WriteLine();
            }
        }
        public static String alphaBeta(int depth, int alpha, int beta, String move, int player)
        {
			String list = possibleMove();
			if (depth == 0 || list.Length == 0) { return move + (evaluation(list.Length, depth) * (player * 2 - 1)); }//The [*(player*2-1)] has been proven correct and necessary
																													  //list=sortMoves2(list);
			player = 1 - player;
			for (int i = 0; i < list.Length; i += 5)
			{
				makeMove(list.Substring(i, 5));
				node += 1;
				flipboard();
				String returnString = alphaBeta(depth - 1, alpha, beta, list.Substring(i, 5), player);
				int value = int.Parse(returnString.Substring(5));
				flipboard();
				undoMove(list.Substring(i, 5));
				if (player == 0)
				{
					if (value <= beta) { beta = value; if (depth == globalDepth) { move = returnString.Substring(i, 5); } }
				}
				else
				{
					if (value > alpha) { alpha = value; if (depth == globalDepth) { move = returnString.Substring(0, 5); } }
				}
				if (alpha >= beta)
				{
					
					if (player == 0) { return move + beta; } else { return move + alpha; }
				}
			}
			if (player == 0) { return move + beta; } else { return move + alpha; }
        }

        public static void makeMove(String move)
        {
            //[5]-P: Promotion Pawn. C: Castling.
            //!= Regularly, [PreviousRow, PreviousColume, NextRow, NextColume, capturedPiece]
            if (move[4] != 'P' && move[4] != 'C')
            {
                chessBoard[(int)Char.GetNumericValue(move[2]), (int)Char.GetNumericValue(move[3])] = chessBoard[(int)Char.GetNumericValue(move[0]), (int)Char.GetNumericValue(move[1])];
                chessBoard[(int)Char.GetNumericValue(move[0]), (int)Char.GetNumericValue(move[1])] = " ";
                //If move postion king .
                if ("A".Equals(chessBoard[(int)Char.GetNumericValue(move[2]), (int)Char.GetNumericValue(move[3])]))
                {
                    kingPositionU = 8 * (int)Char.GetNumericValue(move[2]) + (int)Char.GetNumericValue(move[3]);
                }
            }
            else if (move[4] == 'P')
            {
                //If pawm promotion
                //[0]ColumePrevious, [1]ColumeNext, [2]CapturePiece, [3]PromotionPiece
                chessBoard[1, (int)Char.GetNumericValue(move[0])] = " ";
                chessBoard[0, (int)Char.GetNumericValue(move[1])] = move[3].ToString();
            }
            else
            {
                //If castling [5]-C
                //[PreviousKingColume, PreviousRockColume, NextKingColume, NextRockColume]
                chessBoard[7, (int)Char.GetNumericValue(move[0])] = " ";
                chessBoard[7, (int)Char.GetNumericValue(move[1])] = " ";
                chessBoard[7, (int)Char.GetNumericValue(move[2])] = "A";
                chessBoard[7, (int)Char.GetNumericValue(move[3])] = "R";
                kingPositionU = 8 * 7 + (int)Char.GetNumericValue(move[2]);
            }
        }
        public static void undoMove(String move)
        {
            //[5]-P: Promotion Pawn. C: Castling.
            //!= Regularly, [PreviousRow, PreviousColume, NextRow, NextColume, capturedPiece]
            if (move[4] != 'P' && move[4] != 'C')
            {
                chessBoard[(int)Char.GetNumericValue(move[0]), (int)Char.GetNumericValue(move[1])] = chessBoard[(int)Char.GetNumericValue(move[2]), (int)Char.GetNumericValue(move[3])];
                chessBoard[(int)Char.GetNumericValue(move[2]), (int)Char.GetNumericValue(move[3])] = move[4].ToString();
                //If move postion king .
                if ("A".Equals(chessBoard[(int)Char.GetNumericValue(move[0]), (int)Char.GetNumericValue(move[1])]))
                {
                    kingPositionU = 8 * (int)Char.GetNumericValue(move[0]) + (int)Char.GetNumericValue(move[1]);
                }
            }
            else if (move[4] == 'P')
            {
                //If pawm promotion
                //[0]ColumePrevious, [1]ColumeNext, [2]CapturePiece, [3]PromotionPiece, P
                chessBoard[1, (int)Char.GetNumericValue(move[0])] = "P";
                chessBoard[0, (int)Char.GetNumericValue(move[1])] = move[2].ToString();
            }
            else
            {
                //If castling [5]-C
                //[PreviousKingColume, PreviousRockColume, NextKingColume, NextRockColume]
                chessBoard[7, (int)Char.GetNumericValue(move[0])] = "A";
                chessBoard[7, (int)Char.GetNumericValue(move[1])] = "R";
                chessBoard[7, (int)Char.GetNumericValue(move[2])] = " ";
                chessBoard[7, (int)Char.GetNumericValue(move[3])] = " ";
                kingPositionU = 8 * 7 + (int)Char.GetNumericValue(move[0]);
            }
        }
        public static void flipboard()
        {
            String temp;
            for (int i = 0; i < 32; i++)
            {
                int row = i / 8, col = i % 8;
                if (Char.IsUpper(chessBoard[row, col], 0))
                {
                    temp = chessBoard[row, col].ToLower();
                }
                else
                {
                    temp = chessBoard[row, col].ToUpper();
                }
                if (Char.IsUpper(chessBoard[7 - row, 7 - col], 0))
                {
                    chessBoard[row, col] = chessBoard[7 - row, 7 - col].ToLower();
                }
                else
                {
                    chessBoard[row, col] = chessBoard[7 - row, 7 - col].ToUpper();
                }
                chessBoard[7 - row, 7 - col] = temp;
            }
            int temp1 = kingPositionU;
            kingPositionU = 63 - kingPositionL;
            kingPositionL = 63 - temp1;

        }
        public static String possibleMove()
        {
            String list = "";
            for (int i = 0; i < 64; i++)
            {
                if (Char.IsUpper(chessBoard[i / 8, i % 8], 0))
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
                        case "R":
                            list += possibleR(i);
                            break;
                        case "K":
                            list += possibleK(i);
                            break;
                        case "P":
                            list += possibleP(i);
                            break;
                    }

                }
            }
            return list.Replace("....a", "");
        }
        public static String possibleA(int i)
        {
            String list = "", oldPiece;
            int row = i / 8, col = i % 8;
            for (int j = 0; j < 9; j++)
            {
                //J==4 is present king's position 
                if (j != 4)
                {
                    try
                    {//Check position around of the king is enemy or blank
                        if (Char.IsLower(chessBoard[row - 1 + j / 3, col - 1 + j % 3], 0) || " ".Equals(chessBoard[row - 1 + j / 3, col - 1 + j % 3]))
                        {
                            oldPiece = chessBoard[row - 1 + j / 3, col - 1 + j % 3];
                            chessBoard[row, col] = " ";
                            chessBoard[row - 1 + j / 3, col - 1 + j % 3] = "A";
                            int tempKing = kingPositionU;
                            kingPositionU = i - 9 + j * 8 / 3 + j % 3;
                            if (safeKing())
                            {
                                //Old Position [ROW, COL] => New Position [..., ...], where is OldPiece
                                list = list + row.ToString() + col.ToString() + (row - 1 + j / 3).ToString() + (col - 1 + j % 3).ToString() + oldPiece;
                            }
                            chessBoard[row, col] = "A";
                            chessBoard[row - 1 + j / 3, col - 1 + j % 3] = oldPiece;
                            kingPositionU = tempKing;
                        }
                    }
                    catch (Exception) { }
                }
            }
            //Castling Upper Long
            if ("A".Equals(chessBoard[7, 4]) && "R".Equals(chessBoard[7, 0]) && castlingULong && safeKing() && " ".Equals(chessBoard[7, 2]) && " ".Equals(chessBoard[7, 1]) && " ".Equals(chessBoard[7, 0]))
            {
                bool flag = true;
                for (int j = 1; j <= 3; j++)
                {
                    makeMove("747" + j.ToString() + " ");
                    //Check square where lower pieces can move to square king castling.
                    if (!safeKing())
                    {
                        flag = false;
                        undoMove("747" + j.ToString() + " ");
                        break;
                    }
                    undoMove("747" + j.ToString() + " ");
                }
                if (flag)
                {
                    list = list + "4023C";
                }
            }
            //Castling Upper Short
            if ("A".Equals(chessBoard[7, 4]) && "R".Equals(chessBoard[7, 7]) && castlingUShort && safeKing() && " ".Equals(chessBoard[7, 5]) && " ".Equals(chessBoard[7, 6]))
            {
                bool flag = true;
                for (int j = 5; j <= 6; j++)
                {
                    makeMove("747" + j.ToString() + " ");
                    //Check square where lower pieces can move to square king castling.
                    if (!safeKing())
                    {
                        flag = false;
                        undoMove("747" + j.ToString() + " ");
                        break;
                    }
                    undoMove("747" + j.ToString() + " ");
                }
                if (flag)
                {
                    list = list + "4765C";
                }
            }
            //Castling Lower Long
            if ("A".Equals(chessBoard[7, 3]) && "R".Equals(chessBoard[7, 7]) && castlingLLong && safeKing() && " ".Equals(chessBoard[7, 4]) && " ".Equals(chessBoard[7, 5]) && " ".Equals(chessBoard[7, 6]))
            {
                bool flag = true;
                for (int j = 4; j <= 6; j++)
                {
                    makeMove("747" + j.ToString() + " ");
                    //Check square where lower pieces can move to square king castling.
                    if (!safeKing())
                    {
                        flag = false;
                        undoMove("747" + j.ToString() + " ");
                        break;
                    }
                    undoMove("747" + j.ToString() + " ");
                }
                if (flag)
                {
                    list = list + "3754C";
                }
            }
            //Castling Lower Short
            if ("A".Equals(chessBoard[7, 3]) && "R".Equals(chessBoard[7, 0]) && castlingLShort && safeKing() && " ".Equals(chessBoard[7, 1]) && " ".Equals(chessBoard[7, 2]))
            {
                bool flag = true;
                for (int j = 1; j <= 2; j++)
                {
                    makeMove("747" + j.ToString() + " ");
                    //Check square where lower pieces can move to square king castling.
                    if (!safeKing())
                    {
                        flag = false;
                        undoMove("747" + j.ToString() + " ");
                        break;
                    }
                    undoMove("747" + j.ToString() + " ");
                }
                if (flag)
                {
                    list = list + "3012C";
                }
            }
            return list;
        }
        public static String possibleB(int i)
        {
            String list = "", oldPiece;
            int row = i / 8, col = i % 8;
            int distance = 1;
            for (int j = -1; j <= 1; j += 2)
            {
                for (int k = -1; k <= 1; k += 2)
                {
                    try
                    {
                        while (" ".Equals(chessBoard[row + distance * j, col + distance * k]))
                        {
                            oldPiece = chessBoard[row + distance * j, col + distance * k];
                            chessBoard[row, col] = " ";
                            chessBoard[row + distance * j, col + distance * k] = "B";
                            if (safeKing())
                            {
                                list = list + row.ToString() + col.ToString() + (row + distance * j).ToString() + (col + distance * k).ToString() + oldPiece;
                            }
                            chessBoard[row, col] = "B";
                            chessBoard[row + distance * j, col + distance * k] = oldPiece;
                            distance++;
                        }

                        if (Char.IsLower(chessBoard[row + distance * j, col + distance * k], 0))
                        {
                            oldPiece = chessBoard[row + distance * j, col + distance * k];
                            chessBoard[row, col] = " ";
                            chessBoard[row + distance * j, col + distance * k] = "B";
                            if (safeKing())
                            {
                                list = list + row.ToString() + col.ToString() + (row + distance * j).ToString() + (col + distance * k).ToString() + oldPiece;
                            }
                            chessBoard[row, col] = "B";
                            chessBoard[row + distance * j, col + distance * k] = oldPiece;
                        }
                    }
                    catch (Exception) { }
                    distance = 1;
                }
            }
            return list;
        }

        public static String possibleR(int i)
        {
            String list = "", oldPiece;
            int row = i / 8, col = i % 8;
            int distance = 1;
            for (int j = -1; j <= 1; j++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    if (k * j == 0 && j != k)
                    {
                        try
                        {
                            while (" ".Equals(chessBoard[row + distance * j, col + distance * k]))
                            {
                                oldPiece = " ";
                                chessBoard[row, col] = " ";
                                chessBoard[row + distance * j, col + distance * k] = "R";
                                if (safeKing())
                                {
                                    list = list + row.ToString() + col.ToString() + (row + distance * j).ToString() + (col + distance * k).ToString() + oldPiece;
                                }
                                chessBoard[row, col] = "R";
                                chessBoard[row + distance * j, col + distance * k] = oldPiece;
                                distance++;
                            }

                            if (Char.IsLower(chessBoard[row + distance * j, col + distance * k], 0))
                            {
                                oldPiece = chessBoard[row + distance * j, col + distance * k];
                                chessBoard[row, col] = " ";
                                chessBoard[row + distance * j, col + distance * k] = "R";
                                if (safeKing())
                                {
                                    list = list + row.ToString() + col.ToString() + (row + distance * j).ToString() + (col + distance * k).ToString() + oldPiece;
                                }
                                chessBoard[row, col] = "R";
                                chessBoard[row + distance * j, col + distance * k] = oldPiece;
                            }
                        }
                        catch (Exception) { }
                        distance = 1;
                    }
                }
            }
            return list;
        }
        public static String possibleQ(int i)
        {
            String list = "", oldPiece;
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
                                    list = list + row.ToString() + col.ToString() + (row + distance * j).ToString() + (col + distance * k).ToString() + oldPiece;
                                }
                                chessBoard[row, col] = "Q";
                                chessBoard[row + distance * j, col + distance * k] = oldPiece;
                                distance++;
                            }

                            if (Char.IsLower(chessBoard[row + distance * j, col + distance * k], 0))
                            {
                                oldPiece = chessBoard[row + distance * j, col + distance * k];
                                chessBoard[row, col] = " ";
                                chessBoard[row + distance * j, col + distance * k] = "Q";
                                if (safeKing())
                                {
                                    list = list + row.ToString() + col.ToString() + (row + distance * j).ToString() + (col + distance * k).ToString() + oldPiece;
                                }
                                chessBoard[row, col] = "Q";
                                chessBoard[row + distance * j, col + distance * k] = oldPiece;
                            }
                        }
                        catch (Exception) { }
                        distance = 1;
                    }
                }
            }
            return list;
        }

        public static String possibleK(int i)
        {
            String list = "", oldPiece;
            int row = i / 8, col = i % 8;
            for (int j = -2; j <= 2; j++)
            {
                for (int k = -2; k <= 2; k++)
                {
                    if (Math.Abs(j * k) == 2)
                    {
                        try
                        {
                            if (Char.IsLower(chessBoard[row + j, col + k], 0) || " ".Equals(chessBoard[row + j, col + k]))
                            {
                                oldPiece = chessBoard[row + j, col + k];
                                chessBoard[row, col] = " ";
                                chessBoard[row + j, col + k] = "K";
                                if (safeKing())
                                {
                                    list = list + row.ToString() + col.ToString() + (row + j).ToString() + (col + k).ToString() + oldPiece;
                                }
                                chessBoard[row, col] = "K";
                                chessBoard[row + j, col + k] = oldPiece;
                            }
                        }
                        catch (Exception) { }
                    }
                }
            }
            return list;
        }

        public static String possibleP(int i)
        {
            String list = "", oldPiece;
            int row = i / 8, col = i % 8;
            for (int j = -1; j <= 1; j += 2)
            {
                try // capture
                {
                    if (Char.IsLower(chessBoard[row - 1, col + j], 0) && i >= 16)
                    {
                        oldPiece = chessBoard[row - 1, col + j];
                        chessBoard[row, col] = " ";
                        chessBoard[row - 1, col + j] = "P";
                        if (safeKing())
                        {
                            list = list + row.ToString() + col.ToString() + (row - 1).ToString() + (col + j).ToString() + oldPiece;
                        }
                        chessBoard[row, col] = "P";
                        chessBoard[row - 1, col + j] = oldPiece;
                    }

                }
                catch (Exception) { }

                try // promotion and capture
                {
                    if (Char.IsLower(chessBoard[row - 1, col + j], 0) && i < 16)
                    {
                        String[] temp = { "Q", "R", "B", "K" };
                        for (int k = 0; k <= 4; k++)
                        {
                            oldPiece = chessBoard[row - 1, col + j];
                            chessBoard[row, col] = " ";
                            chessBoard[row - 1, col + j] = temp[k];
                            if (safeKing())
                            {
                                list = list + col.ToString() + (col + j).ToString() + oldPiece + temp[k] + "P";
                            }
                            chessBoard[row, col] = "P";
                            chessBoard[row - 1, col + j] = oldPiece;
                        }

                    }
                }
                catch (Exception) { }
            }
            try // go straight one distance
            {
                if (" ".Equals(chessBoard[row - 1, col]) && i >= 16)
                {
                    oldPiece = chessBoard[row - 1, col];
                    chessBoard[row, col] = " ";
                    chessBoard[row - 1, col] = "P";
                    if (safeKing())
                    {
                        list = list + row.ToString() + col.ToString() + (row - 1).ToString() + col.ToString() + oldPiece;
                    }
                    chessBoard[row, col] = "P";
                    chessBoard[row - 1, col] = oldPiece;
                }
            }
            catch (Exception) { }

            try // go straight two distance
            {
                if (" ".Equals(chessBoard[row - 2, col]) && i >= 48 && " ".Equals(chessBoard[row - 1, col]))
                {
                    oldPiece = chessBoard[row - 2, col];
                    chessBoard[row, col] = " ";
                    chessBoard[row - 2, col] = "P";
                    if (safeKing())
                    {
                        list = list + row.ToString() + col.ToString() + (row - 2).ToString() + col.ToString() + oldPiece;
                    }
                    chessBoard[row, col] = "P";
                    chessBoard[row - 2, col] = oldPiece;
                }
            }
            catch (Exception) { }

            try // tien 1 buoc va phong tot (promotion)
            {

                if (" ".Equals(chessBoard[row - 1, col]) && i < 16)
                {
                    String[] temp = { "Q", "R", "B", "K" };
                    for (int k = 0; k < 4; k++)
                    {
                        oldPiece = chessBoard[row - 1, col];
                        chessBoard[row, col] = " ";
                        chessBoard[row - 1, col] = temp[k];
                        if (safeKing())
                        {
                            list = list + col.ToString() + col.ToString() + oldPiece + temp[k] + "P";
                        }
                        chessBoard[row, col] = "P";
                        chessBoard[row - 1, col] = oldPiece;
                    }
                }
            }
            catch (Exception) { }
            return list;
        }
        //Don't work with
        public static Boolean safeKing()
        {
            int distance = 1;
            //Bishop & Queen diagonal
            for (int i = -1; i <= 1; i += 2)
            {
                for (int j = -1; j <= 1; j += 2)
                {
                    try
                    {
                        while (" ".Equals(chessBoard[kingPositionU / 8 + distance * i, kingPositionU % 8 + distance * j]))
                        {
                            distance++;
                        }
                        if ("b".Equals(chessBoard[kingPositionU / 8 + distance * i, kingPositionU % 8 + distance * j]) || "q".Equals(chessBoard[kingPositionU / 8 + distance * i, kingPositionU % 8 + distance * j]))
                        {
                            return false;
                        }
                    }
                    catch (Exception) { }
                    distance = 1;
                }
            }

            // Rock and Queen go strait
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i * j == 0 && i != j)
                        try
                        {
                            while (" ".Equals(chessBoard[kingPositionU / 8 + distance * i, kingPositionU % 8 + distance * j]))
                            {
                                distance++;
                            }
                            if ("r".Equals(chessBoard[kingPositionU / 8 + distance * i, kingPositionU % 8 + distance * j]) || "q".Equals(chessBoard[kingPositionU / 8 + distance * i, kingPositionU % 8 + distance * j]))
                            {
                                return false;
                            }
                        }
                        catch (Exception) { }
                    distance = 1;
                }
            }

            //Knight
            for (int i = -2; i <= 2; i++)
            {
                for (int j = -2; j <= 2; j++)
                {
                    if (Math.Abs(i * j) == 2)
                    {
                        try
                        {
                            if ("k".Equals(chessBoard[kingPositionU / 8 + i, kingPositionU % 8 + j]))
                            {
                                return false;
                            }
                        }
                        catch (Exception) { }
                    }
                }
            }
            //Pawn
            if (kingPositionU > 15)
            {
                for (int i = -1; i <= 1; i += 2)
                {
                    try
                    {
                        if ("p".Equals(chessBoard[kingPositionU / 8 - 1, kingPositionU % 8 + i]))
                        {
                            return false;
                        }
                    }
                    catch (Exception) { }
                }
            }

            //King
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    try
                    {
                        if (i != 0 || j != 0)
                        {
                            if ("a".Equals(chessBoard[kingPositionU + i, kingPositionU % 8 + j]))
                            {
                                return false;
                            }
                        }
                    }
                    catch (Exception) { }
                }
            }
            return true;
        }
		public static int evaluation(int listLength, int depth)
		{//list is only for checkmate checking
            
			int counter = 0, material = 0;//counter is in centipawns in evaluation() and all functions called by evaluation()
            for (int i = 1; i >= -1; i -= 2){
				material = materialScore();
                counter += i * material;
                counter += i * ratePositional(material);
                counter += i * rateAttack();
                if (i == 1)
                {
                    counter += rateMoveability(listLength, depth, material);
                    flipboard();
                }
            }
			
			if (listLength == -1)
			{
				counter -= rateMoveability(-1, depth, material);
			}
			else
			{
                counter -= rateMoveability(possibleMove().Length, depth, material);
			}

			flipboard();
			return -(counter + depth * 50);//
		}
		public static int materialScore()
		{
            int score = 0, count_bishop = 0;
            for (int i = 0; i < 64; i++){
				switch (chessBoard[i / 8, i % 8])
				{
					case "P":
						score += 100;
						break;
					case "K":
						score += 350;
						break;
					case "B":
						count_bishop += 1;
                        score += 350;
						break;
					case "R":
						score += 525;
						break;
					case "Q":
						score += 1000;
						break;
				} 
            }
            if (count_bishop > 1) score += 50;
            return score * 7;//at least 7
		}
		public static int rateAttack()
		{
			int counter = 0;
			int tempPositionC = kingPositionU;
			for (int i = 0; i < 64; i++)
			{//for each square in chessboard
				if (!" ".Equals(chessBoard[i / 8, i % 8]))
				{
					//if the square contains one of my pieces:
					switch (chessBoard[i / 8, i % 8])
					{
						case "P":
							{ kingPositionU = i; if (!safeKing()) { counter -= 50; } }
							break;
						case "R":
							{ kingPositionU = i; if (!safeKing()) { counter -= 500; } }
							break;
						case "K":
							{ kingPositionU = i; if (!safeKing()) { counter -= 300; } }
							break;
						case "B":
							{ kingPositionU = i; if (!safeKing()) { counter -= 300; } }
							break;
						case "Q":
							{ kingPositionU = i; if (!safeKing()) { counter -= 900; } }
							break;
					}
				}
			}
			kingPositionU = tempPositionC;
			if (!safeKing()) { counter -= 400; }//penalty for check
			return counter / 2;
		}
		public static int rateMoveability(int listLength, int depth, int material)
		{
			int counter = 0;
			counter += listLength * 15;//[posibleMoves().length()] will return 5 points per valid move
			if (listLength == 0)
			{//current side is in checkmate/stalemate
				if (!safeKing())
				{//if checkmate
					counter += -200000 * depth;//(depth+1)*(player*2-1);//the [*(depth+1)] is a way of favoring quicker checkMates
				}
				else
				{//if stalemate
					counter += -150000 * depth;//+1000*(globalDepth-depth)
				}
			}
			return counter;
		}
		public static int ratePositional(int material)
		{
			int score = 0;
			for (int i = 0; i < 64; i++)
			{//for each square in chessboard
				if (!" ".Equals(chessBoard[i / 8, i % 8]))
				{
					//if the square contains one of my pieces:
					switch (chessBoard[i / 8, i % 8])
					{
						case "P":
							score += pawnBoard[i / 8, i % 8];
							break;
						case "R":
							score += rookBoard[i / 8, i % 8];
							break;
						case "K":
							score += knightBoard[i / 8, i % 8];
							break;
						case "B":
							score += bishopBoard[i / 8, i % 8];
							break;
						case "Q":
							score += queenBoard[i / 8, i % 8];
							break;
						case "A":
							if (material >= 1900) 
                            { 
                                score += kingMidBoard[i / 8, i % 8]; 
                                score += possibleA(kingPositionU).Length * 10; 
                            } else 
                            { 
                                score += kingEndBoard[i / 8, i % 8]; 
                                score += possibleA(kingPositionU).Length * 30; 
                            }
							break;
					}
				}
			}
			return score;
		}
    }
}