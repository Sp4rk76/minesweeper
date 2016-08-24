using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    class Grid
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Case Case { get; set; }

        public Grid()
        {
            Case = new Case();
        }

    }
}
