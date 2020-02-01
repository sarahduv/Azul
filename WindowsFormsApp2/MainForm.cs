using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class MainForm : Form
    {
        PictureBox[] humanRow1;
        PictureBox[] humanRow2;
        PictureBox[] humanRow3;
        PictureBox[] humanRow4;
        PictureBox[] humanRow5;
        PictureBox[][] humanRows;

        public MainForm()
        {
            InitializeComponent();

            // All the pictureboxes are already there
            humanRow1 = new PictureBox[] { pendHumanR1C1 };
            humanRow2 = new PictureBox[] { pendHumanR2C1, pendHumanR2C2 };
            humanRow3 = new PictureBox[] { pendHumanR3C1, pendHumanR3C2, pendHumanR3C3 };
            humanRow4 = new PictureBox[] { pendHumanR4C1, pendHumanR4C2, pendHumanR4C3, pendHumanR4C4 };
            humanRow5 = new PictureBox[] { pendHumanR5C1, pendHumanR5C2, pendHumanR5C3, pendHumanR5C4, pendHumanR5C5 };
            humanRows = new PictureBox[][] { humanRow1, humanRow2, humanRow3, humanRow4, humanRow5 };

            foreach (var row in humanRows)
            {
                foreach (var picbox in row)
                {
                    picHumanBoard.Controls.Add(picbox);
                    picbox.Location = new Point(picbox.Location.X -picHumanBoard.Location.X, picbox.Location.Y -picHumanBoard.Location.Y);
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
