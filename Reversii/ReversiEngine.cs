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
        private string gameStatus;
        public int[,] GridContent;
        private const int gridSizeX = 6;
        private const int gridSizeY = 6;
        private GameControl ParentControl;
        private int currentTurn;
        private bool turnsAvailable;
        private int redCount;
        private int blueCount;
        public ReversiEngine(GameControl o)
        {
            GridContent = new int[gridSizeX, gridSizeY];

            //Plaats steentjes in het midden, ongeacht grid grootte
            GridContent[gridSizeX/2-1, gridSizeY/2-1] = 1; GridContent[gridSizeX/2, gridSizeY/2] = 1;
            GridContent[gridSizeX/2, gridSizeY/2-1] = 2; GridContent[gridSizeX/2-1, gridSizeY/2] = 2;

            currentTurn = 1;
            gameStatus = "";
            redCount = 2;
            blueCount = 2;
            ParentControl = o;
            turnsAvailable = true;
        }

        /// <summary>
        /// Plaats steentje en verander beurt. In de CheckLegality methode worden de juiste aanliggende steentjes ook omgedraait.
        /// </summary>
        public void UpdateField(int tileX, int tileY)
        {
            if (CheckLegality(tileX, tileY, true))
            {
                GridContent[tileX, tileY] = currentTurn;
                currentTurn = (currentTurn == 1) ? 2 : 1;
            }
        }

        /// <summary>
        /// Turf de hoeveelheid steentjes van elke kleur.
        /// </summary>
        public void UpdateCount()
        {
            redCount = 0;
            blueCount = 0;
            for(int x = 0; x < gridSizeX; x++)
            {
                for(int y = 0; y < gridSizeY; y++)
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
        
        /// <summary>
        /// Controleert of de gegeven tile wel legaal is om iets in te plaatsen. Als fill true is, dan wordt na het plaatsen van de steen ook de juiste aanliggende steentjes omgedraait.
        /// </summary>
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
                    if (r < 0 || c < 0 || r > gridSizeX - 1 || c > gridSizeY - 1)
                        continue;
                    if (GridContent[r, c] == ((currentTurn == 1) ? 2 : 1))
                    {
                        for (int i = 1; i < 20 && i != -1; i++)
                        {
                            r += rowDiff;
                            c += colDiff;
                            if (r < 0 || c < 0 || r > gridSizeX - 1 || c > gridSizeY - 1)
                                continue;
                            if (GridContent[r, c] == currentTurn)
                            {
                                legal = true;
                                turnsAvailable = true;
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

        public int GridSizeX
        {
            get
            {
                return gridSizeX;
            }
        }
        public int GridSizeY
        {
            get
            {
                return gridSizeY;
            }
        }
        public bool TurnsAvailable
        {
            get
            {
                return turnsAvailable;
            }
            set
            {
                turnsAvailable = value;
            }
        }
    }
}
