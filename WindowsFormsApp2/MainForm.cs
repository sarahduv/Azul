using AzulGame;
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
        //
        PictureBox[] factories;
        PictureBox[,,] factoryTiles = new PictureBox[5, 2, 2];
        //
        TileBag tileBag;

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
                    picbox.Location = new Point(picbox.Location.X - picHumanBoard.Location.X, picbox.Location.Y - picHumanBoard.Location.Y);
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
                    cellPicBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    cellPicBox.BackColor = Color.Transparent;
                    cellPicBox.Location = new Point(compTopLeft.X + (35 * col), compTopLeft.Y + (35 * row));
                    picCompBoard.Controls.Add(cellPicBox);
                    doneCompRows[col, row] = cellPicBox;

                    // Create the human 5 by 5
                    cellPicBox = new PictureBox();
                    cellPicBox.Size = picComp00Fake.Size;
                    cellPicBox.Visible = true;
                    cellPicBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    cellPicBox.BackColor = Color.Transparent;
                    cellPicBox.Location = new Point(humanTopLeft.X + (35 * col), humanTopLeft.Y + (35 * row));
                    picHumanBoard.Controls.Add(cellPicBox);
                    doneHumanRows[col, row] = cellPicBox;
                }
            }

            factories = new PictureBox[] { picFactory1, picFactory2, picFactory3, picFactory4, picFactory5 };
            for (int factoryIndex = 0; factoryIndex < factories.Length; factoryIndex++)
            {
                var factory = factories[factoryIndex];
                for (int row = 0; row < 2; ++row)
                {
                    for (int col = 0; col < 2; ++col)
                    {
                        var factoryTilePicBox = new PictureBox();
                        factoryTilePicBox.Size = picComp00Fake.Size;
                        factoryTilePicBox.Visible = true;
                        factoryTilePicBox.SizeMode = PictureBoxSizeMode.StretchImage;
                        factoryTilePicBox.BackColor = Color.Transparent;
                        factoryTilePicBox.Location = new Point(32 + (35 * col), 32 + (35 * row));
                        factory.Controls.Add(factoryTilePicBox);
                        factoryTiles[factoryIndex, col, row] = factoryTilePicBox;
                    }
                }
            }

            //AzulGame.Properties.Resources.BlackTile;
        }

        private void SetupBoard()
        {
            tileBag = new TileBag();
            for (int factoryIndex = 0; factoryIndex < factories.Length; factoryIndex++)
            {
                for (int row = 0; row < 2; ++row)
                {
                    for (int col = 0; col < 2; ++col)
                    {
                        var placedTileKind = tileBag.ShuffledTiles.Pop();
                        var picbox = factoryTiles[factoryIndex, col, row];
                        var tifo = new TileInfo();
                        tifo.FactoryIndex = factoryIndex;
                        tifo.kind = placedTileKind;
                        tifo.picBox = picbox;
                        picbox.Tag = tifo;
                        picbox.Image = Helpers.GetTileImage(placedTileKind, false);
                        picbox.MouseEnter += (sender, e) => {
                            HighlightFactoryTiles((PictureBox)sender, true);
                        };
                        picbox.MouseLeave += (sender, e) => {
                            HighlightFactoryTiles((PictureBox)sender, false);
                        };
                        picbox.MouseClick += SelectFactoryTiles;
                    }
                }
            }
        }

        List<PictureBox> selectedTiles = new List<PictureBox>();

        private void SelectFactoryTiles(object sender, MouseEventArgs e)
        {
            // Remove highlight/selection from previous selected tiles
            var prevSelectedTiles = selectedTiles.ToArray();
            selectedTiles.Clear();
            foreach (var tile in prevSelectedTiles)
            {
                HighlightFactoryTiles(tile, false);
            }

            var tifo = (TileInfo)((PictureBox)sender).Tag;
            for (int row = 0; row < 2; ++row)
            {
                for (int col = 0; col < 2; ++col)
                {
                    var factoryTile = factoryTiles[tifo.FactoryIndex, col, row];
                    var factoryTifo = (TileInfo)factoryTile.Tag;
                    if (factoryTifo.kind == tifo.kind)
                    {
                        selectedTiles.Add(factoryTile);
                        factoryTile.Image = factoryTile.Image.AddTint(Color.FromArgb(100, Color.Green));
                    }
                }
            }
        }

        private void HighlightFactoryTiles(PictureBox pb, bool isHighlit)
        {
            var tifo = (TileInfo)pb.Tag;
            for (int row = 0; row < 2; ++row)
            {
                for (int col = 0; col < 2; ++col)
                {
                    var factoryTile = factoryTiles[tifo.FactoryIndex, col, row];
                    var factoryTifo = (TileInfo)factoryTile.Tag;

                    // Skip over selected tiles. We are not changing their image.
                    if (selectedTiles.Contains(factoryTile))
                    {
                        continue;
                    }

                    if (factoryTifo.kind == tifo.kind)
                    {
                        factoryTile.Image = Helpers.GetTileImage(tifo.kind, isHighlit);
                    }
                }
            }
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            SetupBoard();
        }
    }

    static class Helpers
    {
        public static void EnableHoverFeedback(this PictureBox picbox)
        {
            picbox.MouseEnter += HighlightTile;
            picbox.MouseLeave += UnhighlightTile;
        }

        public static void DisableHoverFeedback(this PictureBox picbox)
        {
            picbox.MouseEnter -= HighlightTile;
            picbox.MouseLeave -= UnhighlightTile;
        }

        private static void UnhighlightTile(object sender, EventArgs e)
        {
            var pb = (PictureBox)sender;
            var tifo = (TileInfo)pb.Tag;
            pb.Image = Helpers.GetTileImage(tifo.kind, false);
        }

        private static void HighlightTile(object sender, EventArgs e)
        {
            var pb = (PictureBox)sender;
            var tifo = (TileInfo)pb.Tag;
            pb.Image = Helpers.GetTileImage(tifo.kind, true);
        }

        public static TileKind GetTile(this PictureBox picbox)
        {
            if (picbox.Tag == null)
            {
                return TileKind.None;
            }
            return (TileKind)picbox.Tag;
        }

        public static Image GetTileImage(
            TileKind inputKind, 
            bool highlighted)
        {
            if (inputKind == TileKind.Black)
                return highlighted 
                    ? AzulGame.Properties.Resources.BlackTileH
                    : AzulGame.Properties.Resources.BlackTile;
            else if (inputKind == TileKind.Blue)
                return highlighted 
                    ? AzulGame.Properties.Resources.BlueTileH
                    : AzulGame.Properties.Resources.BlueTile;
            else if (inputKind == TileKind.Cyan)
                return highlighted
                    ? AzulGame.Properties.Resources.CyanTileH
                    : AzulGame.Properties.Resources.CyanTile;
            else if (inputKind == TileKind.Red)
                return highlighted
                    ? AzulGame.Properties.Resources.RedTileH
                    : AzulGame.Properties.Resources.RedTile;
            else if (inputKind == TileKind.Yellow)
                return highlighted
                    ? AzulGame.Properties.Resources.YellowTileH
                    : AzulGame.Properties.Resources.YellowTile;
            throw new NotImplementedException();
        }

        public static Image AddTint(this Image img, Color clr)
        {
            var newImage = (Image)img.Clone();
            var g = Graphics.FromImage(newImage);
            g.FillRectangle(new SolidBrush(clr), 0, 0, img.Width, img.Height);
            //g.DrawRectangle(new Pen(clr, 80), 0, 0, img.Width, img.Height);
            g.Save();
            return newImage;
        }
    }

    class TileBag
    {
        public Stack<TileKind> ShuffledTiles;
        public TileBag()
        {
            List<TileKind> tiles = new List<TileKind>();
            for (int i = 0; i < 20; i++)
            {
                tiles.Add(TileKind.Black);
                tiles.Add(TileKind.Blue);
                tiles.Add(TileKind.Cyan);
                tiles.Add(TileKind.Yellow);
                tiles.Add(TileKind.Red);
            }
            ShuffledTiles = new Stack<TileKind>(tiles.OrderBy(x => Guid.NewGuid()).ToList());
        }
    }

    enum TileKind
    {
        None,
        Black,
        Blue,
        Cyan,
        Red,
        Yellow,
        FirstMove,
    }
}
