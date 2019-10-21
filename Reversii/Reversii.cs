using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace Reversii
{
    public partial class Reversii : Form
    {
        public Reversii()
        {
            GameGrid mainGrid = new GameGrid();
            mainGrid.Location = new Point(50, 50); mainGrid.Size = new Size(500, 500);
            Controls.Add(mainGrid);
            InitializeComponent();
        }
    }
}
