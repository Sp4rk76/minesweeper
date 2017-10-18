namespace Minesweeper
{
    class Settings
    {
        private Difficulty difficulty;
        public static int Width { get; set; }

        public static int Height { get; set; }

        public static int Speed { get; set; }

        public static int Score { get; set; }

        public static bool GameOver { get; set; }

        public static bool Win { get; set; }

        public static int NbMines { get; set; }

        public static int NbDiscoveredSquares { get; set; }

        public Settings() // "difficulty" will be a Game Parameter in the future
        {
            difficulty = Difficulty.Normal;

            switch (difficulty)
            {
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
            NbMines = 0;
            NbDiscoveredSquares = 0;
            GameOver = false;
            Win = false;
        }
    }
}
