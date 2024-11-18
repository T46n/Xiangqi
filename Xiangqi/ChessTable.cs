using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xiangqi
{
    internal class ChessTable
    {
        public struct AvTy
        {
            private int availableFlag;
            private int typeFlag;

            public AvTy(int A, int B)
            {
                this.availableFlag = A;
                this.typeFlag = B;
            }

            public int getAvai()
            {
                return availableFlag;
            }

            public int getType()
            {
                return typeFlag;
            }

            public void setAvai(int x)
            {
                availableFlag = x;
            }

            public void setType(int x)
            {
                typeFlag = x;
            }
        }

        public List<List<AvTy>> cTable = new List<List<AvTy>>();


        public void UpdateCell(int avail, int type, int pX, int pY)
        {
            cTable[9-pY][pX].setAvai(avail);
            cTable[9-pY][pX].setType(type);
        }
        
        public void CreateTable() //restarting the game
        {
            for (int i = 0; i<10; i++)
            {
                cTable.Add(new List<AvTy>());
                for (int j = 0; j<9; j++)
                {
                    if (i == 0)
                    {
                        if (j == 0 || j == 8)
                        {
                            cTable[i].Add(new AvTy(0, 6));
                            continue;
                        }
                        else if (j == 1 || j == 7)
                        {
                            cTable[i].Add(new AvTy(0, 5));
                            continue;
                        }
                        else if (j == 2 || j == 6)
                        {
                            cTable[i].Add(new AvTy(0, 2));
                            continue;
                        }
                        else if (j == 3 || j == 5)
                        {
                            cTable[i].Add(new AvTy(0, 1));
                            continue;
                        }
                        else if (j == 4)
                        {
                            cTable[i].Add(new AvTy(0, 4));
                            continue;
                        }
                    }
                    else if (i == 9) {
                        if (j == 0 || j == 8)
                        {
                            cTable[i].Add(new AvTy(1, 6));
                            continue;
                        }
                        else if (j == 1 || j == 7)
                        {
                            cTable[i].Add(new AvTy(1, 5));
                            continue;
                        }
                        else if (j == 2 || j == 6)
                        {
                            cTable[i].Add(new AvTy(1, 2));
                            continue;
                        }
                        else if (j == 3 || j == 5)
                        {
                            cTable[i].Add(new AvTy(1, 1));
                            continue;
                        }
                        else if (j == 4)
                        {
                            cTable[i].Add(new AvTy(1, 4));
                            continue;
                        }
                    }
                    else if (i == 2)
                    {
                        if (j == 1 || j == 7)
                        {
                            cTable[i].Add(new AvTy(0, 3));
                            continue;
                        }
                    }
                    else if (i == 7)
                    {
                        if (j == 1 || j == 7)
                        {
                            cTable[i].Add(new AvTy(1, 3));
                            continue;
                        }
                    }
                    else if (i == 3)
                    {
                        if (j == 0 || j == 2 || j == 4 || j == 6 || j == 8)
                        {
                            cTable[i].Add(new AvTy(0, 7));
                            continue;
                        }
                    }
                    else if (i == 6)
                    {
                        if (j == 0 || j == 2 || j == 4 || j == 6 || j == 8)
                        {
                            cTable[i].Add(new AvTy(1, 7));
                            continue;
                        }
                    }
                    cTable[i].Add(new AvTy(-1, 0));
                }
            }
        }

        public void UpdateCellPosition(Label label, int x, int y)
        {
            int avai = cTable[9 - y][x].getAvai();
            int type = cTable[9 - y][x].getType();
            label.Text = (avai.ToString() + ", " + type.ToString());
        }

        public int[,] CellIsClicked(MouseEventArgs e)
        {
            for (int i = 0; i<10; i++)
            {
                for (int j = 0; j<9; j++)
                {
                    if (253 + (float)(j) * 399 / 8 - 419 / 16 <= e.Location.X && e.Location.X <= 253 + (float)(j) * 399 / 8 - 419 / 16 + 50 &&
                        527 - (float)(9-i) * 449 / 9 - 469 / 18 <= e.Location.Y && e.Location.Y <= 527 - (float)(9-i) * 449 / 9 - 469 / 18 + 50)
                    {
                        return new int[,] { { i, j } };
                    }
                }
            }
            return new int[,] { { -1, -1 } };
        }

    }
}