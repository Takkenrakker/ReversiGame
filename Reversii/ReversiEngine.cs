using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Reversii
{
    class ReversiEngine
    {
        private int gameStatus;
        public int[,] GridContent;
        public const int GridSizeX = 6;
        public const int GridSizeY = 6;
        private GameControl ParentControl;
        private int currentTurn;
        private int redCount;
        private int blueCount;
        public ReversiEngine(GameControl o)
        {
            GridContent = new int[,] { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 1, 2, 0, 0 }, { 0, 0, 2, 1, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } };
            currentTurn = 1;
            gameStatus = 0;
            redCount = 2;
            blueCount = 2;
            ParentControl = o;
        }

        public void UpdateField(int tileX, int tileY)
        {
            if (CheckLegality(tileX, tileY, true))
            {
                GridContent[tileX, tileY] = currentTurn;
                currentTurn = (currentTurn == 1) ? 2 : 1;
                Debug.WriteLine("currentTurn:" +currentTurn);
            }
        }

        public void UpdateCount()
        {
            redCount = 0;
            blueCount = 0;
            for(int x = 0; x < GridSizeX; x++)
            {
                for(int y = 0; y < GridSizeY; y++)
                {
                    if(GridContent[x,y] != 0)
                    {
                        if(GridContent[x,y] == 1)
                        {
                            redCount++;
                            continue;
                        }
                        blueCount++;
                    }
                }
            }
        }

        public bool CheckLegality(int tileX, int tileY, bool fill)
        {
            bool legal = false;
            if (GridContent[tileX, tileY] != 0)
                return false;
            for (int rowDiff = -1; rowDiff <= 1; rowDiff++)
            {
                for (int colDiff = -1; colDiff <= 1; colDiff++)
                {
                    int r = tileX + rowDiff;
                    int c = tileY + colDiff;
                    if (r < 0 || c < 0 || r > GridSizeX - 1 || c > GridSizeY - 1)
                        continue;
                    if (GridContent[r, c] == ((currentTurn == 1) ? 2 : 1))
                    {
                        for (int i = 1; i < 10 && i != -1; i++)
                        {
                            r += rowDiff;
                            c += colDiff;
                            if (r < 0 || c < 0 || r > GridSizeX - 1 || c > GridSizeY - 1)
                                continue;
                            if (GridContent[r, c] == currentTurn)
                            {
                                legal = true;
                                if(fill)
                                {
                                    while(i>=0)
                                    {
                                        r -= rowDiff;
                                        c -= colDiff;
                                        GridContent[r, c] = currentTurn;
                                        i--;
                                    }
                                }
                                i--;
                            }
                        }
                    }
                }

            }
            return legal;
        }

        public int RedCount
        {
            get
            {
                return redCount;
            }
        }
        public int BlueCount
        {
            get
            {
                return blueCount;
            }
        }
        public bool CurrentTurn
        {
            get
            {
                return (currentTurn == 1) ? true:false;
            }
        }
        public string GameStatus
        {
            get
            {
                return "hi";
            }
        }
    }
}
