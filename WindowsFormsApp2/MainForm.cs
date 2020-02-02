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
        PictureBox[] pendHumanRow1;
        PictureBox[] pendHumanRow2;
        PictureBox[] pendHumanRow3;
        PictureBox[] pendHumanRow4;
        PictureBox[] pendHumanRow5;
        PictureBox[][] pendHumanRows;
        PictureBox[] pendCompRow1;
        PictureBox[] pendCompRow2;
        PictureBox[] pendCompRow3;
        PictureBox[] pendCompRow4;
        PictureBox[] pendCompRow5;
        PictureBox[][] pendCompRows;
        //
        PictureBox[,] doneHumanRows = new PictureBox[5,5];
        PictureBox[,] doneCompRows = new PictureBox[5,5];

        public MainForm()
        {
            InitializeComponent();

            // All the pictureboxes are already there
            pendHumanRow1 = new PictureBox[] { pendHumanR1C1 };
            pendHumanRow2 = new PictureBox[] { pendHumanR2C1, pendHumanR2C2 };
            pendHumanRow3 = new PictureBox[] { pendHumanR3C1, pendHumanR3C2, pendHumanR3C3 };
            pendHumanRow4 = new PictureBox[] { pendHumanR4C1, pendHumanR4C2, pendHumanR4C3, pendHumanR4C4 };
            pendHumanRow5 = new PictureBox[] { pendHumanR5C1, pendHumanR5C2, pendHumanR5C3, pendHumanR5C4, pendHumanR5C5 };
            pendHumanRows = new PictureBox[][] { pendHumanRow1, pendHumanRow2, pendHumanRow3, pendHumanRow4, pendHumanRow5 };
            pendCompRow1 = new PictureBox[] { pendCompR1C1 };
            pendCompRow2 = new PictureBox[] { pendCompR2C1, pendCompR2C2 };
            pendCompRow3 = new PictureBox[] { pendCompR3C1, pendCompR3C2, pendCompR3C3 };
            pendCompRow4 = new PictureBox[] { pendCompR4C1, pendCompR4C2, pendCompR4C3, pendCompR4C4 };
            pendCompRow5 = new PictureBox[] { pendCompR5C1, pendCompR5C2, pendCompR5C3, pendCompR5C4, pendCompR5C5 };
            pendCompRows = new PictureBox[][] { pendCompRow1, pendCompRow2, pendCompRow3, pendCompRow4, pendCompRow5 };

            foreach (var row in pendHumanRows)
            {
                foreach (var picbox in row)
                {
                    picHumanBoard.Controls.Add(picbox);
                    picbox.Location = new Point(picbox.Location.X -picHumanBoard.Location.X, picbox.Location.Y -picHumanBoard.Location.Y);
                }
            }
            foreach (var row in pendCompRows)
            {
                foreach (var picbox in row)
                {
                    picCompBoard.Controls.Add(picbox);
                    picbox.Location = new Point(picbox.Location.X - picHumanBoard.Location.X, picbox.Location.Y - picHumanBoard.Location.Y);
                }
            }


            var compTopLeft = new Point(picComp00Fake.Location.X - picCompBoard.Location.X, picComp00Fake.Location.Y - picCompBoard.Location.Y);
            var humanTopLeft = new Point(picHuman00Fake.Location.X - picHumanBoard.Location.X, picHuman00Fake.Location.Y - picHumanBoard.Location.Y);
            for (int row = 0; row < 5; ++row)
            {
                for (int col = 0; col < 5; ++col)
                {
                    // Create the computer 5 by 5
                    var cellPicBox = new PictureBox();
                    cellPicBox.Size = picComp00Fake.Size;
                    cellPicBox.Visible = true;
                    //cellPicBox.Image = AzulGame.Properties.Resources.CyanTile;
                    cellPicBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    cellPicBox.BackColor = Color.Transparent;
                    cellPicBox.Location = new Point(compTopLeft.X + (35 * col), compTopLeft.Y + (35 * row));
                    picCompBoard.Controls.Add(cellPicBox);
                    doneCompRows[col, row] = cellPicBox;

                    // Create the human 5 by 5
                    cellPicBox = new PictureBox();
                    cellPicBox.Size = picComp00Fake.Size;
                    cellPicBox.Visible = true;
                    //cellPicBox.Image = AzulGame.Properties.Resources.CyanTile;
                    cellPicBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    cellPicBox.BackColor = Color.Transparent;
                    cellPicBox.Location = new Point(humanTopLeft.X + (35 * col), humanTopLeft.Y + (35 * row));
                    picHumanBoard.Controls.Add(cellPicBox);
                    doneHumanRows[col, row] = cellPicBox;
                }
            }

            //AzulGame.Properties.Resources.BlackTile;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
