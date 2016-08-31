﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    class Settings
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Speed { get; set; }
        public static int Score { get; set; }
        public static bool GameOver { get; set; }

        public Settings()
        {
            Width = 19;
            Height = 19;
            Speed = 16;
            Score = 0;
            GameOver = false;
        }
    }
}
