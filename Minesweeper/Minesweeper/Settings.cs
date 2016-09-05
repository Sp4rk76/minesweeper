using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public enum Difficulty
    {
        Easy,
        Normal,
        Hard,
        Nightmare
    };
    class Settings
    {
        private Difficulty difficulty;
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Speed { get; set; }
        public static int Score { get; set; }
        public static bool GameOver { get; set; }
        public static bool Win { get; set; }

        public Settings() // "difficulty" will be a Game Parameter in the future
        {
            difficulty = Difficulty.Normal;
            switch( difficulty ) {
                case Difficulty.Easy:
                    Width = 10;
                    Height = 10;
                    break;
                case Difficulty.Normal:
                    Width = 20;
                    Height = 20;
                    break;
                case Difficulty.Hard:
                    Width = 40;
                    Height = 40;
                    break;
            }
            Speed = 16;
            Score = 0;
            GameOver = false;
            Win = false;
        }
    }
}
