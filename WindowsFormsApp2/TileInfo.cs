using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2;

namespace AzulGame
{
    class TileInfo
    {
        public TileKind kind;
        public PictureBox picBox;

        // Use -1 to signify NONE
        public int FactoryIndex;
    }
}
