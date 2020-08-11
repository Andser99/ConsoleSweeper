using System;
using System.Collections.Generic;
using System.Text;

namespace Minesweeper
{
    class MineField
    {
        private static Random rnd = new Random();
        private MineCell[,] MineArr;
        private int GameSize;
        private int MineCount;
        private int MinesLeft;
        private int ToReveal;
        public MineField(int width, int height, int mines)
        {
            MineArr = new MineCell[width, height];
            for (int i = 0; i < MineArr.GetLength(0); i++)
            {
                for (int j = 0; j < MineArr.GetLength(1); j++)
                {
                    MineArr[i, j] = new MineCell();
                }
            }
            MinesLeft = mines;
            MineCount = mines;
            GameSize = width * height;
            GenerateMines();

        }

        private void GenerateMines()
        {
            int ToPlace = MineCount;
            int FieldsLeft = GameSize+1;
            for (int i = 0; i < MineArr.GetLength(0); i++)
            {
                for (int j = 0; j < MineArr.GetLength(1); j++)
                {
                    if (rnd.NextDouble() <= (double)ToPlace / (double)FieldsLeft--)
                    {
                        MineArr[i, j].Settings |= MineCell.Flags.IsMine;
                        for (int m = -1; m <= 1; m++)
                        {
                            for (int n = -1; n <= 1; n++)
                            {
                                try
                                {
                                    MineArr[i+n, j+m].Neighbours++;
                                }
                                catch (IndexOutOfRangeException) { }
                            }
                        }
                        ToPlace--;
                        if (ToPlace == 0) break;
                    }
                }
            }
            DebugPrint();
        }
        public void DebugPrint(int toReveal = -1)
        {
            if (toReveal == -1)
            {
                toReveal = GameSize;
                for (int i = 0; i < MineArr.GetLength(0); i++)
                {
                    for (int j = 0; j < MineArr.GetLength(1); j++)
                    {
                        if ((MineArr[i, j].Settings & MineCell.Flags.IsPopped) != 0) ToReveal--;
                    }
                }
            }
            System.Console.Write(" ");
            for (int i = 0; i < MineArr.GetLength(0); i++)
            {
                System.Console.Write(i.ToString());
            }
            System.Console.WriteLine();
            for (int i = 0; i < MineArr.GetLength(0); i++)
            {
                for (int j = 0; j < MineArr.GetLength(1); j++)
                {
                    if (j == 0)
                    {
                        System.Console.Write(i.ToString());
                    }
                    System.Console.Write(MineArr[i,j].ToString());
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine($"Squares Left: {toReveal}");
        }
        public void PopMine(int x, int y)
        {
            if (MineArr[x,y].Pop())
            {
                //cycle through neighbours and clear empty fields
                if (MineArr[x,y].Neighbours == 0)
                {
                    PopSurrounding(x, y);
                }
                int ToReveal = GameSize;
                for (int i = 0; i < MineArr.GetLength(0); i++)
                {
                    for (int j = 0; j < MineArr.GetLength(1); j++)
                    {
                        if ((MineArr[i, j].Settings & MineCell.Flags.IsPopped) != 0) ToReveal--;
                    }
                }
                DebugPrint(ToReveal);
                if (ToReveal - MineCount <= 0)
                {
                    Victory();
                }
            }
            else
            {
                GameOver();
            }
        }

        private void PopSurrounding(int x, int y)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (x + i >= 0 && x + i < MineArr.GetLength(0) && y + j >= 0 && y + j < MineArr.GetLength(1))
                    {
                        if (MineArr[x + i, y + j].Neighbours == 0 && (MineArr[x + i, y + j].Settings & MineCell.Flags.IsPopped) == 0)
                        {
                            MineArr[x + i, y + j].Pop();
                            PopSurrounding(x + i, y + j);
                        }
                        else
                        {
                            MineArr[x + i, y + j].Pop();
                        }
                    }
                }
            }
        }

        public void GameOver()
        {
            DebugPrint();
            System.Console.WriteLine("Game Over!");
            //show all mines
        }

        public void Victory()
        {
            System.Console.WriteLine("Victory");
        }
    }
}
