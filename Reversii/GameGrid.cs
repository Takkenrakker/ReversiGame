using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;


namespace Reversii
{
    public class GameGrid : UserControl
    {
        ReversiEngine MainEngine;
        GameControl ParentControl;
        public GameGrid(GameControl o)
        {
            BackColor = Color.LightGray;
            Paint += Draw;
            MouseUp += Clicked;
            Size = new Size(500, 500);
            MainEngine = new ReversiEngine(o);
            ParentControl = o;
        }
        /// <summary>
        /// Vindt de aangeklikte tile, probeert het steentje om te draaien als het kan, update de count en labels, en refresht de control.
        /// </summary>
        public void Clicked(object o, MouseEventArgs mea)
        {
            Point tileClicked = new Point((int)(((float)mea.X / Size.Width) * MainEngine.GridSizeX), (int)(((float)mea.Y / Size.Height) * MainEngine.GridSizeY));
            MainEngine.UpdateField(tileClicked.X, tileClicked.Y);
            MainEngine.UpdateCount();
            Debug.WriteLine("" + tileClicked);
            ParentControl.UpdateLabels();
            Refresh();
        }
        public void Draw(object o, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            int gridSizeX = MainEngine.GridSizeX;
            int gridSizeY = MainEngine.GridSizeY;
            for (float i = 0; i < gridSizeX + 1; i++)
            {
                int pos = (int)((i / gridSizeX) * (Size.Width - 1));
                gr.DrawLine(Pens.Black, pos, 0, pos, Size.Height);
            }
            for(float i = 0; i < gridSizeY + 1; i++)
            {
                int pos = (int)((i / gridSizeY) * (Size.Height - 1));
                gr.DrawLine(Pens.Black, 0, pos, Size.Width, pos);
            }
            MainEngine.TurnsAvailable = false;
            for(float x = 0; x < gridSizeX; x++)
            {
                for(float y = 0; y < gridSizeY; y++)
                {
                    if (MainEngine.GridContent[(int)x, (int)y] == 1)
                        gr.FillEllipse(Brushes.Red, (x / gridSizeX) * Size.Width, (y / gridSizeY) * Size.Height, (Size.Width / gridSizeX) - 1, (Size.Height / gridSizeY) - 1);
                    else if (MainEngine.GridContent[(int)x, (int)y] == 2)
                        gr.FillEllipse(Brushes.Blue, (x / gridSizeX) * Size.Width, (y / gridSizeY) * Size.Height, (Size.Width / gridSizeX) - 1, (Size.Height / gridSizeY) - 1);
                    else
                    {
                        // Hulpcirkels
                        if (MainEngine.CheckLegality((int)x, (int)y, false) && ParentControl.HelpEnabled)
                            gr.DrawEllipse(Pens.Black, (x / gridSizeX) * Size.Width, (y / gridSizeY) * Size.Height, (Size.Width / gridSizeX) - 1, (Size.Height / gridSizeY) - 1);
                    }
                }
            }
        }
        public int RedCount
        {
            get
            {
                return MainEngine.RedCount;
            }
        }
        public int BlueCount
        {
            get
            {
                return MainEngine.BlueCount;
            }
        }

        public bool RedsTurn
        {
            get
            {
                return MainEngine.CurrentTurn;
            }
        }
        public bool TurnsAvailable
        {
            get
            {
                return MainEngine.TurnsAvailable;
            }
        }
    }
}
