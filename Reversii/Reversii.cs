using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace Reversii
{
    public partial class Reversii : Form
    {
        private GameControl MainControl;
        public Reversii()
        {
            GameControl MainControl = new GameControl
            {
                Location = new Point(50, 50),
                Size = new Size(500, 600)
            };
            Controls.Add(MainControl);


            InitializeComponent();
        }
    }
}
