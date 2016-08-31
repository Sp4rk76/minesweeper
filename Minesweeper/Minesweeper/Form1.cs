using System;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace Minesweeper
{
    public partial class Form1 : Form
    {
        
        private int MousePosX { get; set; }
        private int MousePosY { get; set; }
        //private int NbMines = 0;

        private Image mine, empty, val1, val2, val3, val4, val5, val6, cache, cache_pressed;
        //private Image[] images = new Image[9];
        private Square[,] grid;
        private bool call = true;
        private int nbMines, score;

        public Form1()
        {
            InitializeComponent();

            new Settings();

            gameTimer.Interval = 1000 / Settings.Speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();

            // Starting New game
            StartGame();
        }


        private void StartGame()
        { 
            lblGameOver.Visible = false;

            new Settings();
            grid = new Square[Settings.Width, Settings.Height];
            // Loading Grid (RANDOM)
            Array.Clear(grid, 0, grid.Length);
            // Generating Once (In Theory)
            for (int x = 0; x < Settings.Width; x++)
            {
                for (int y = 0; y < Settings.Height; y++)
                {
                    grid[x, y] = new Square();
                }
            }
        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            //Check for Game Over
            if (Settings.GameOver)
            {
                //Check if Enter is pressed
                if (Input.KeyPressed(Keys.Enter))
                {
                    StartGame();
                }
            }
            else
            {
                // Re-Checking the Grid Once to load Assets
                if( call ) {
                    updateGrid( );
                    call = false;
                }   
            }
            pbCanvas.Invalidate();
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
                // Generating Images
                for (int x = 0; x < Settings.Width; x++)
                {
                    for (int y = 0; y < Settings.Height; y++)
                    {

                        if (grid[x, y].caseState == CaseState.Hidden) {// case Hidden
                        e.Graphics.DrawImage( cache, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) );
                        }
                        else // case Discovered
                        {
                            if ( grid[x, y].Value == 0 ) {
                                e.Graphics.DrawImage( empty, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) );
                            }
                            if ( grid[x, y].Value == 1 ) {
                                e.Graphics.DrawImage( val1, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) );
                            }
                            if ( grid[x, y].Value == 2 ) {
                                e.Graphics.DrawImage( val2, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) );
                            }
                            if ( grid[x, y].Value == 3 ) {
                                e.Graphics.DrawImage( val3, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) );
                            }
                            if ( grid[x, y].Value == 4 ) {
                                e.Graphics.DrawImage( val4, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) );
                            }
                            if ( grid[x, y].Value == 5 ) {
                                e.Graphics.DrawImage( val5, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) );
                            }
                            if ( grid[x, y].Value == 6 ) {
                                e.Graphics.DrawImage( val6, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) );
                            }
                            if ( grid[x, y].caseType == CaseType.Mine ) {
                                e.Graphics.DrawImage( mine, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) );
                            }
                        } 
                    }
                }
            // Sinon, (gameOver)
            if (Settings.GameOver)
            {
                string gameOver = "Game over \nYour final score is: " + Settings.Score + "\nPress Enter to try again";
                lblGameOver.Text = gameOver;
                lblGameOver.Visible = true;
            }
        }

        private void pbCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            // Détecte le clic. Amélioration : Empêche le clic en dehors de la grille
            if ( e.X > 0 && e.X < pbCanvas.Size.Width && e.Y > 0 && e.Y < pbCanvas.Size.Height ) {
                MousePosX = (e.X / (pbCanvas.Size.Width / Settings.Width));
                MousePosY = (e.Y / (pbCanvas.Size.Height / Settings.Height));
                discoverSquare( grid, MousePosX, MousePosY );
                if ( grid[MousePosX, MousePosY].caseType == CaseType.Mine ) { // isMine()
                    Loose( );
                }
            }
        }

        private void pbCanvas_Load(object sender, EventArgs e)
        {
            // @TODO : Ajouter un foreach et parcourir un tableau d'images pour le chargement
            mine = resize_image(load_image("pictures/mine.png"), new Size(32, 32));
            empty = resize_image( load_image( "pictures/vide.png" ), new Size( 32, 32 ) );
            val1 = resize_image(load_image("pictures/val1.png"), new Size(32, 32));
            val2 = resize_image(load_image("pictures/val2.png"), new Size(32, 32));
            val3 = resize_image(load_image("pictures/val3.png"), new Size(32, 32));
            val4 = resize_image(load_image("pictures/val4.png"), new Size(32, 32));
            val5 = resize_image(load_image("pictures/val5.png"), new Size(32, 32));
            val6 = resize_image(load_image("pictures/val6.png"), new Size(32, 32));
            cache = resize_image(load_image("pictures/cache.png"), new Size(32, 32));
            cache_pressed = resize_image(load_image("pictures/cache_pressed.png"), new Size(32, 32));
        }

        public Image load_image(string path)
        {
            using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(path)))
                return Image.FromStream(ms);
                
        }

        public Image resize_image(Image originalImage, Size size)
        {
            return (Image)(new Bitmap(originalImage, size));
        }

        private void updateGrid( ) {
            for ( int rowPos = 0; rowPos < Settings.Width; rowPos++ ) {
                for ( int colPos = 0; colPos < Settings.Height; colPos++ ) {
                    if ( grid[rowPos, colPos].caseType == CaseType.Mine ) { // Mine
                        incrementValuesAroundMine( grid, rowPos, colPos );
                        nbMines++;
                    }
                }
            }
        }

        private void incrementValuesAroundMine( Square[,] grid, int rowPos, int colPos ) {
            try { grid[rowPos - 1, colPos + 0].Value += 1; } catch ( IndexOutOfRangeException ) { }
            try { grid[rowPos + 1, colPos + 0].Value += 1; } catch ( IndexOutOfRangeException ) { }
            try { grid[rowPos + 0, colPos + 1].Value += 1; } catch ( IndexOutOfRangeException ) { }
            try { grid[rowPos + 0, colPos - 1].Value += 1; } catch ( IndexOutOfRangeException ) { }
            try { grid[rowPos + 1, colPos + 1].Value += 1; } catch ( IndexOutOfRangeException ) { }
            try { grid[rowPos + 1, colPos - 1].Value += 1; } catch ( IndexOutOfRangeException ) { }
            try { grid[rowPos - 1, colPos + 1].Value += 1; } catch ( IndexOutOfRangeException ) { }
            try { grid[rowPos - 1, colPos - 1].Value += 1; } catch ( IndexOutOfRangeException ) { }
        }

        private void discoverSquare(Square[,] grid, int rowPos, int colPos) {
            if ( rowPos < 0 || rowPos >= Settings.Width || colPos< 0 || colPos >= Settings.Height ) {
                return;
            }
            Square square = grid[rowPos, colPos];
            if ( !(square.caseState == CaseState.Discovered) ) {
                square.caseState = CaseState.Discovered;
                if ( square.Value == 0 ) { // isEmpty()
                    // Recursivity on 8 squares around actual position
                    discoverSquare( grid, rowPos - 1, colPos + 0 );
                    discoverSquare( grid, rowPos + 1, colPos + 0 );
                    discoverSquare( grid, rowPos + 0, colPos + 1 );
                    discoverSquare( grid, rowPos + 0, colPos - 1 );
                    discoverSquare( grid, rowPos + 1, colPos + 1 );
                    discoverSquare( grid, rowPos + 1, colPos - 1 );
                    discoverSquare( grid, rowPos - 1, colPos + 1 );
                    discoverSquare( grid, rowPos - 1, colPos - 1 );
                }
            }           
        }

        private void Loose( ) {
            Settings.GameOver = true;
        }

        private void calculateScore( ) {

        }

        private void countMines( ) {
            for(int rowPos = 0; rowPos < Settings.Width; rowPos++ ) {
                for ( int colPos = 0; colPos < Settings.Height; colPos++ ) {
                    if ( grid[rowPos, colPos].caseType == CaseType.Mine ) {
                        nbMines++;
                    }
                }
            }
        }

    }

}
