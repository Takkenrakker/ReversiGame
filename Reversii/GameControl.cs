using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Reversii
{

    public class GameControl : UserControl
    {
        GameGrid MainGrid;
        Button BtnHelp;
        Button BtnReset;
        Label LblRedCount;
        Label LblBlueCount;
        Label LblCurrentTurn;
        public bool HelpEnabled;
        public GameControl()
        {
            Size = new Size(500, 600);

            MainGrid = new GameGrid(this);
            MainGrid.Location = new Point(0, 100);
            Controls.Add(MainGrid);

            BtnHelp = new Button();
            BtnHelp.Text = "Help";
            BtnHelp.Location = new Point(20, 20);
            BtnHelp.Click += ShowHelp;
            Controls.Add(BtnHelp);

            BtnReset = new Button();
            BtnReset.Text = "Nieuw Spel";
            BtnReset.Location = new Point(200, 20);
            BtnReset.Click += Restart;
            Controls.Add(BtnReset);

            LblCurrentTurn = new Label();
            LblCurrentTurn.Text = "Turn: Red";
            LblCurrentTurn.ForeColor = Color.Red;
            LblCurrentTurn.Location = new Point(100, 20);
            Controls.Add(LblCurrentTurn);

            LblRedCount = new Label();
            LblRedCount.Text = "Red tiles: 2";
            LblRedCount.Location = new Point(300, 20);
            Controls.Add(LblRedCount);

            LblBlueCount = new Label();
            LblBlueCount.Text = "Blue tiles: 2";
            LblBlueCount.Location = new Point(400, 20);
            Controls.Add(LblBlueCount);

            HelpEnabled = true;

        }
        private void ShowHelp(object o, EventArgs ea)
        {
            HelpEnabled = !HelpEnabled;
            MainGrid.Refresh();
        }
        private void Restart(object o, EventArgs ea)
        {
            HelpEnabled = true;
            Controls.Remove(MainGrid);
            MainGrid = new GameGrid(this);
            MainGrid.Location = new Point(0, 100);
            Controls.Add(MainGrid);
            UpdateLabels();
        }
        public void UpdateLabels()
        {
            LblRedCount.Text = "Red tiles: " + MainGrid.RedCount;
            LblBlueCount.Text = "Blue tiles: " + MainGrid.BlueCount;
            LblCurrentTurn.Text = (MainGrid.RedsTurn) ? "Turn: Red" : "Turn: Blue";
            LblCurrentTurn.ForeColor = (MainGrid.RedsTurn) ? Color.Red : Color.Blue;
        }
    }
}
