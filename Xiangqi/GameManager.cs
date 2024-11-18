using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static Xiangqi.ChessTable;

namespace Xiangqi
{

    internal class GameManager
    {
        MatchGame matchGame;

        public readonly Bitmap empty_Item = new Bitmap(Xiangqi.Properties.Resources.empty_Item);

        public List<ChessItem> chessItemBlack = new List<ChessItem>();
        public List<ChessItem> chessItemRed = new List<ChessItem>();
        public static ChessItem[,] GameBoard;
        public static ChessItem[,] CircleTable;
        //need these for chess interactions

        public Rectangle chessLocation;

        public readonly Bitmap banCo = new Bitmap(Xiangqi.Properties.Resources.JanggiBrown);


        public GameManager(MatchGame matchGame)
        {
            this.matchGame = matchGame;
            GameBoard = new ChessItem[10, 9];
            CircleTable = new ChessItem[10, 9];

            //SIDES:
            //RED: 1
            //BLACK: 0

            //TYPES:
            //Advisor: 1
            //Cannon : 2
            //Elephant: 3
            //General: 4
            //Horse: 5
            //Rook: 6
            //Soldier: 7


            // Red pieces
            GameBoard[9, 3] = new Advisor(3, 9, 1, 1);
            chessItemRed.Add(GameBoard[9, 3]);

            GameBoard[9, 5] = new Advisor(5, 9, 1, 1);
            chessItemRed.Add(GameBoard[9, 5]);

            GameBoard[7, 1] = new Cannon(1, 7, 2, 1);
            chessItemRed.Add(GameBoard[7, 1]);

            GameBoard[7, 7] = new Cannon(7, 7, 2, 1);
            chessItemRed.Add(GameBoard[7, 7]);

            GameBoard[9, 2] = new Elephant(2, 9, 3, 1);
            chessItemRed.Add(GameBoard[9, 2]);

            GameBoard[9, 6] = new Elephant(6, 9, 3, 1);
            chessItemRed.Add(GameBoard[9, 6]);

            GameBoard[9, 4] = new General(4, 9, 4, 1);
            chessItemRed.Add(GameBoard[9, 4]);

            GameBoard[9, 1] = new Horse(1, 9, 5, 1);
            chessItemRed.Add(GameBoard[9, 1]);

            GameBoard[9, 7] = new Horse(7, 9, 5, 1);
            chessItemRed.Add(GameBoard[9, 7]);

            GameBoard[9, 0] = new Rook(0, 9, 6, 1);
            chessItemRed.Add(GameBoard[9, 0]);

            GameBoard[9, 8] = new Rook(8, 9, 6, 1);
            chessItemRed.Add(GameBoard[9, 8]);

            GameBoard[6, 0] = new Soldier(0, 6, 7, 1);
            chessItemRed.Add(GameBoard[6, 0]);

            GameBoard[6, 2] = new Soldier(2, 6, 7, 1);
            chessItemRed.Add(GameBoard[6, 2]);

            GameBoard[6, 4] = new Soldier(4, 6, 7, 1);
            chessItemRed.Add(GameBoard[6, 4]);

            GameBoard[6, 6] = new Soldier(6, 6, 7, 1);
            chessItemRed.Add(GameBoard[6, 6]);

            GameBoard[6, 8] = new Soldier(8, 6, 7, 1);
            chessItemRed.Add(GameBoard[6, 8]);




            // Black pieces 
            GameBoard[0, 3] = new Advisor(3, 0, 1, 0);
            chessItemBlack.Add(GameBoard[0, 3]);

            GameBoard[0, 5] = new Advisor(5, 0, 1, 0);
            chessItemBlack.Add(GameBoard[0, 5]);

            GameBoard[2, 1] = new Cannon(1, 2, 2, 0);
            chessItemBlack.Add(GameBoard[2, 1]);

            GameBoard[2, 7] = new Cannon(7, 2, 2, 0);
            chessItemBlack.Add(GameBoard[2, 7]);

            GameBoard[0, 2] = new Elephant(2, 0, 3, 0);
            chessItemBlack.Add(GameBoard[0, 2]);

            GameBoard[0, 6] = new Elephant(6, 0, 3, 0);
            chessItemBlack.Add(GameBoard[0, 6]);

            GameBoard[0, 4] = new General(4, 0, 4, 0);
            chessItemBlack.Add(GameBoard[0, 4]);

            GameBoard[0, 1] = new Horse(1, 0, 5, 0);
            chessItemBlack.Add(GameBoard[0, 1]);

            GameBoard[0, 7] = new Horse(7, 0, 5, 0);
            chessItemBlack.Add(GameBoard[0, 7]);

            GameBoard[0, 0] = new Rook(0, 0, 6, 0);
            chessItemBlack.Add(GameBoard[0, 0]);

            GameBoard[0, 8] = new Rook(8, 0, 6, 0);
            chessItemBlack.Add(GameBoard[0, 8]);

            GameBoard[3, 0] = new Soldier(0, 3, 7, 0);
            chessItemBlack.Add(GameBoard[3, 0]);

            GameBoard[3, 2] = new Soldier(2, 3, 7, 0);
            chessItemBlack.Add(GameBoard[3, 2]);

            GameBoard[3, 4] = new Soldier(4, 3, 7, 0);
            chessItemBlack.Add(GameBoard[3, 4]);

            GameBoard[3, 6] = new Soldier(6, 3, 7, 0);
            chessItemBlack.Add(GameBoard[3, 6]);

            GameBoard[3, 8] = new Soldier(8, 3, 7, 0);
            chessItemBlack.Add(GameBoard[3, 8]);
        }

        public void PlaceDefaultChessItems(PaintEventArgs e)
        {
            for (int i = 0; i<10; i++)
            {
                for (int j = 0; j<9; j++)
                {
                    if (GameBoard[i,j]==null)  GameBoard[i,j]=new EmptyLocation(j,i,0,-1);
                    CircleTable[i, j] = new EmptyLocation(j, i, 0, -1);
                }
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    GameBoard[i, j].Paint(e);
                    CircleTable[i, j].Paint(e);
                }
            }
        }

        public bool CheckAvailable(int x, int y)
        {
            bool result;
            for (int i = 0; i<chessItemBlack.Count; i++)
            {
                result = chessItemBlack[i].CheckAvailable(x, y);
                if (result == true) return true;
            }
            for (int i = 0; i < chessItemRed.Count; i++)
            {
                result = chessItemRed[i].CheckAvailable(x, y);
                if (result == true) return true;
            }
            return false;
        }


        ////Paint toàn bộ con cờ của GameBoard tại 1 thời điểm
        //public void PaintPawn(PaintEventArgs e)
        //{
        //    Graphics g = e.Graphics;
        //    for (int i = 0; i < 10; i++)
        //    {
        //        for (int j = 0; j < 9; j++)
        //        {
        //            GameBoard[i, j].PaintG(g);
        //            CircleTable[i,j].PaintG(g);
        //        }
        //    }
        //}

        ////Paint lại bàn cờ 
        //public void PaintBoard(PaintEventArgs e)
        //{
        //    Graphics g = e.Graphics;
        //    chessLocation.Width = 451;
        //    chessLocation.Height = 501;
        //    chessLocation.Location = new Point(225, 50);
        //    g.DrawImage(banCo, new Rectangle(225, 50, 451, 501));
        //}


    }
}