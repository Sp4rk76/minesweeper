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

        private Image mine, mine_exploded, empty, val1, val2, val3, val4, val5, val6, cache, cache_pressed, flag;
        //private Image[] images = new Image[9];
        private Square[,] grid;
        private bool call = true;
        private int nbMines, nbDiscoveredSquares, score;

        public Form1()
        {
            InitializeComponent();

            // Redefine labels' Parent
            var posLabel1 = this.PointToScreen(label1.Location);
            posLabel1 = pbPlayer1.PointToClient( posLabel1 );
            label1.Parent = pbPlayer1;
            label1.Location = posLabel1;
            label1.BackColor = Color.Transparent;

            var posLblScore = this.PointToScreen(lblScore.Location);
            posLblScore = pbPlayer1.PointToClient( posLblScore );
            lblScore.Parent = pbPlayer1;
            lblScore.Location = posLblScore;
            lblScore.BackColor = Color.Transparent;

            var posLabel2 = this.PointToScreen(label2.Location);
            posLabel2 = pbPlayer1.PointToClient( posLabel2 );
            label2.Parent = pbPlayer1;
            label2.Location = posLabel2;
            label2.BackColor = Color.Transparent;

            var posLblNbMines = this.PointToScreen(lblNbMines.Location);
            posLblNbMines = pbPlayer1.PointToClient( posLblNbMines );
            lblNbMines.Parent = pbPlayer1;
            lblNbMines.Location = posLblNbMines;
            lblNbMines.BackColor = Color.Transparent;

            var posLblPlayerName = this.PointToScreen(lblPlayerName.Location);
            posLblPlayerName = pbPlayer1.PointToClient( posLblPlayerName );
            lblPlayerName.Parent = pbPlayer1;
            lblPlayerName.Location = posLblPlayerName;
            lblPlayerName.BackColor = Color.Transparent;

            var posLblPlayerState = this.PointToScreen(lblPlayerState.Location);
            posLblPlayerState = pbPlayer1.PointToClient( posLblPlayerState );
            lblPlayerState.Parent = pbPlayer1;
            lblPlayerState.Location = posLblPlayerState;
            lblPlayerState.BackColor = Color.Transparent;

            new Settings();

            gameTimer.Interval = 1000 / Settings.Speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();

            // Starting New game
            StartGame( );
        }

        private void buttonPlay_Click( object sender, EventArgs e ) {
            if ( Settings.GameOver ) {
                StartGame( );
            }
        }

        private void StartGame()
        {
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

        private void UpdateScreen( object sender, EventArgs e ) {
            //Check for Game Over
            if ( !Settings.GameOver ) {
                // Re-Checking the Grid Once to load Assets
                if ( call ) {
                    updateGrid( );
                    call = false;
                }
                lblScore.Text = score.ToString( );
            }
            pbCanvas.Invalidate( );
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
                // Generating Images
                for (int x = 0; x < Settings.Width; x++)
                {
                    for (int y = 0; y < Settings.Height; y++)
                    {
                        if ( grid[x, y].caseState == CaseState.Hidden ) { // square Hidden
                            //lblScore.Text = pbCanvas.Height.ToString(); // To test ...
                            e.Graphics.DrawImage( cache, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) );
                            if( grid[x, y].flag ) {
                                e.Graphics.DrawImage( flag, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) );
                            }
                        }
                        else if ( grid[x, y].caseState == CaseState.Exploded ) {
                            e.Graphics.DrawImage( mine_exploded, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) );
                        } 
                        else { // case Discovered
                            if ( grid[x, y].Value == 0 ) { e.Graphics.DrawImage( empty, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) ); }
                            if ( grid[x, y].Value == 1 ) { e.Graphics.DrawImage( val1, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) ); }
                            if ( grid[x, y].Value == 2 ) { e.Graphics.DrawImage( val2, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) ); }
                            if ( grid[x, y].Value == 3 ) { e.Graphics.DrawImage( val3, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) ); }
                            if ( grid[x, y].Value == 4 ) { e.Graphics.DrawImage( val4, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) ); }
                            if ( grid[x, y].Value == 5 ) { e.Graphics.DrawImage( val5, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) ); }
                            if ( grid[x, y].Value == 6 ) { e.Graphics.DrawImage( val6, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) ); }
                            if ( grid[x, y].caseType == CaseType.Mine ) { e.Graphics.DrawImage( mine, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) ); }
                        //if ( grid[x, y].caseType == CaseType.Mine && grid[x, y].caseState == CaseState.Exploded ) { e.Graphics.DrawImage( mine_exploded, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) ); }
                        } 
                    }
                }
            // Sinon, (gameOver)
            if (Settings.GameOver) {
                string gameOver = "Game over \nYour final score is: " + Settings.Score + "\nPress Enter to try again";
                lblPlayerState.Text = "DEAD";
                lblPlayerState.ForeColor = Color.DarkRed;
            }
            if ( Settings.Win ) {
                lblPlayerState.Text = "You Won !";
            }
        }

        private void pbCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if ( !Settings.GameOver && !Settings.Win ) { // If Game isRunning()
                // Détecte le clic. Amélioration : Empêche le clic en dehors de la grille
                MousePosX = (e.X / (pbCanvas.Size.Width / Settings.Width));
                MousePosY = (e.Y / (pbCanvas.Size.Height / Settings.Height));

                switch ( e.Button ) {
                    case (MouseButtons.Left):
                        //MouseButton = "Left_Click";
                        discoverSquare( grid, MousePosX, MousePosY );
                        TestWin( );
                        try {
                            if ( grid[MousePosX, MousePosY].caseType == CaseType.Mine ) { // isMine()
                                Loose( );
                                showAllMines( );
                                grid[MousePosX, MousePosY].caseState = CaseState.Exploded;
                            }
                        } catch ( IndexOutOfRangeException ) { }
                        break;
                    /*
                    case (MouseButtons.Middle):
                        MouseButton = "Middle_Click";
                        break;
                    */
                    case (MouseButtons.Right):
                        //MouseButton = "Right_Click";
                        try {
                            grid[MousePosX, MousePosY].flag = !grid[MousePosX, MousePosY].flag;
                        } catch ( IndexOutOfRangeException ) { }
                        break;
                }

            }
        }

        private void pbCanvas_Load(object sender, EventArgs e)
        {
            // @TODO : Ajouter un foreach et parcourir un tableau d'images pour le chargement
            mine          = resize_image(load_image("pictures/mine.png"),          new Size(32, 32));
            mine_exploded = resize_image(load_image("pictures/mine_exploded.png"), new Size(32, 32));
            empty         = resize_image(load_image("pictures/vide.png"),          new Size(32, 32));
            val1          = resize_image(load_image("pictures/val1.png"),          new Size(32, 32));
            val2          = resize_image(load_image("pictures/val2.png"),          new Size(32, 32));
            val3          = resize_image(load_image("pictures/val3.png"),          new Size(32, 32));
            val4          = resize_image(load_image("pictures/val4.png"),          new Size(32, 32));
            val5          = resize_image(load_image("pictures/val5.png"),          new Size(32, 32));
            val6          = resize_image(load_image("pictures/val6.png"),          new Size(32, 32));
            cache         = resize_image(load_image("pictures/cache.png"),         new Size(32, 32));
            cache_pressed = resize_image(load_image("pictures/cache_pressed.png"), new Size(32, 32));
            flag          = resize_image(load_image("pictures/flag.png"),          new Size(32, 32));
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
            lblNbMines.Text = nbMines.ToString( );
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
                switch(square.Value ) { // isEmpty()
                    case 0:
                        // Recursivity on 8 squares around actual position
                        discoverSquare( grid, rowPos - 1, colPos + 0 );
                        discoverSquare( grid, rowPos + 1, colPos + 0 );
                        discoverSquare( grid, rowPos + 0, colPos + 1 );
                        discoverSquare( grid, rowPos + 0, colPos - 1 );
                        discoverSquare( grid, rowPos + 1, colPos + 1 );
                        discoverSquare( grid, rowPos + 1, colPos - 1 );
                        discoverSquare( grid, rowPos - 1, colPos + 1 );
                        discoverSquare( grid, rowPos - 1, colPos - 1 );
                        nbDiscoveredSquares++;
                        score += 20;
                        break;
                    case 1:
                        score += 100;
                        nbDiscoveredSquares++;
                        break;
                    case 2:
                        score += 200;
                        nbDiscoveredSquares++;
                        break;
                    case 3:
                        score += 300;
                        nbDiscoveredSquares++;
                        break;
                    case 4:
                        score += 400;
                        nbDiscoveredSquares++;
                        break;
                    case 5:
                        score += 500;
                        nbDiscoveredSquares++;
                        break;
                    case 6:
                        score += 600;
                        nbDiscoveredSquares++;
                        break;
                }
            }           
        }

        private void showAllMines( ) {
            for(int rowPos = 0; rowPos < Settings.Width; rowPos++ ) {
                for(int colPos = 0; colPos < Settings.Height; colPos++ ) {
                    if(grid[rowPos, colPos].caseType == CaseType.Mine ) {
                        grid[rowPos, colPos].caseState = CaseState.Discovered;
                    }
                }
            }
        }

        private void Loose( ) {
            Settings.GameOver = true;
        }

        private void TestWin( ) {
            // nbCases - nbMines == nbCasesDecouvertes
            if ( ((Settings.Width * Settings.Height) - nbMines) == nbDiscoveredSquares )
                Settings.Win = true;
        }

    }

}
