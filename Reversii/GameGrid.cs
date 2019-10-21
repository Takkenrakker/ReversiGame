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
        public GameGrid()
        {
            BackColor = Color.LightGray;
            Paint += Draw;
            MouseUp += Clicked;
            Size = new Size(500, 500);
            MainEngine = new ReversiEngine();
        }
        public void Clicked(object o, MouseEventArgs mea)
        {
            Point tileClicked = new Point((int)(((float)mea.X / Size.Width) * 6), (int)(((float)mea.Y / Size.Height) * 6));
            MainEngine.UpdateField(tileClicked.X, tileClicked.Y);
            Debug.WriteLine("" + tileClicked);
            Refresh();
        }
        public void Draw(object o, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            for(float i = 0; i < 7; i++)
            {
                int pos = (int)((i / 6) * (Size.Width - 1));
                gr.DrawLine(Pens.Black, pos, 0, pos, Size.Height);
            }
            for(float i = 0; i < 7; i++)
            {
                int pos = (int)((i / 6) * (Size.Height - 1));
                gr.DrawLine(Pens.Black, 0, pos, Size.Width, pos);
            }
            for(float x = 0; x < 6; x++)
            {
                for(float y = 0; y < 6; y++)
                {
                    if (MainEngine.GridContent[(int)x, (int)y] == 1)
                        gr.FillEllipse(Brushes.Red, (x / 6) * Size.Width, (y / 6) * Size.Height, (Size.Width / 6) - 1, (Size.Height / 6) - 1);
                    else if (MainEngine.GridContent[(int)x, (int)y] == 2)
                        gr.FillEllipse(Brushes.Blue, (x / 6) * Size.Width, (y / 6) * Size.Height, (Size.Width / 6) - 1, (Size.Height / 6) - 1);
                    else if (MainEngine.CheckLegality((int)x, (int)y, false))
                        gr.DrawEllipse(Pens.Black, (x / 6) * Size.Width, (y / 6) * Size.Height, (Size.Width / 6) - 1, (Size.Height / 6) - 1);
                }
            }
        }
    }
}
