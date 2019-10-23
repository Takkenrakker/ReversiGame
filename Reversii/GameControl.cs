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
        Label LblStatus;
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

            LblStatus = new Label();
            LblStatus.Text = "Turn: Red";
            LblStatus.ForeColor = Color.Red;
            LblStatus.Location = new Point(100, 25);
            Controls.Add(LblStatus);

            LblRedCount = new Label();
            LblRedCount.Text = "Red tiles: 2";
            LblRedCount.Location = new Point(300, 25);
            Controls.Add(LblRedCount);

            LblBlueCount = new Label();
            LblBlueCount.Text = "Blue tiles: 2";
            LblBlueCount.Location = new Point(400, 25);
            Controls.Add(LblBlueCount);

            HelpEnabled = true;

        }
        /// <summary>
        /// Schakelt tussen hulptekeningetjes of niet
        /// </summary>
        private void ShowHelp(object o, EventArgs ea)
        {
            HelpEnabled = !HelpEnabled;
            MainGrid.Refresh();
        }
        /// <summary>
        /// Reset het spel
        /// </summary>

        private void Restart(object o, EventArgs ea)
        {
            HelpEnabled = true;
            Controls.Remove(MainGrid);
            MainGrid = new GameGrid(this);
            MainGrid.Location = new Point(0, 100);
            Controls.Add(MainGrid);
            UpdateLabels();
        }
        /// <summary>
        /// Past de labels aan aan de nieuwe situatie
        /// </summary>
        public void UpdateLabels()
        {
            LblRedCount.Text = "Red tiles: " + MainGrid.RedCount;
            LblBlueCount.Text = "Blue tiles: " + MainGrid.BlueCount;
            if(MainGrid.TurnsAvailable)
            {
                LblStatus.Text = (MainGrid.RedsTurn) ? "Turn: Red" : "Turn: Blue";
                LblStatus.ForeColor = (MainGrid.RedsTurn) ? Color.Red : Color.Blue;
            }
            else if(MainGrid.RedCount == MainGrid.BlueCount)
            {
                LblStatus.Text = "Draw!";
                LblStatus.ForeColor = Color.Green;
            }
            else
            {
                LblStatus.Text = (MainGrid.RedCount > MainGrid.BlueCount) ? "Red won!" : "Blue won!";
                LblStatus.ForeColor = (MainGrid.RedCount > MainGrid.BlueCount) ? Color.Red : Color.Blue;
            }

        }
    }
}
