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
        private int currentTurn;
        public ReversiEngine()
        {
            GridContent = new int[,] { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 1, 2, 0, 0 }, { 0, 0, 2, 1, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } };
            currentTurn = 1;
            gameStatus = 0;
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
                    if (r < 0 || c < 0 || r > 5 || c > 5)
                        continue;
                    if (GridContent[r, c] == ((currentTurn == 1) ? 2 : 1))
                    {
                        for (int i = 1; i < 10 && i != -1; i++)
                        {
                            r += rowDiff;
                            c += colDiff;
                            if (r < 0 || c < 0 || r > 5 || c > 5)
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



        public string GameStatus
        {
            get
            {
                return "hi";
            }
        }
    }
}
