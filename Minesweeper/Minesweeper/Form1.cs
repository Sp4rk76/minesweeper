using System;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using Minesweeper.Properties;
using System.Diagnostics;

namespace Minesweeper
{
    public partial class Form1 : Form
    {
        private int MousePosX { get; set; }
        private int MousePosY { get; set; }
        //private int NbMines = 0;

        private Image mine, mine_exploded, empty, val1, val2, val3, val4, val5, val6, cache, flag;
        //private Image[] images = new Image[9];
        private Game game;
        private bool call = true;

        public Form1()
        {
            MousePosX = 1;
            MousePosY = 1;
           
            InitializeComponent();
            setButtonDependencies( );
            new Settings( );
            // @ TODO : To improve 
              pbMenu.Enabled = true;
              pbMenu.Visible = true;
            //

            SetGameTimer( );

            StartGame( );
        }

        private void buttonPlay_Click( object sender, EventArgs e ) {
            if ( Settings.GameOver ) {
                StartGame( );
            }
        }

        private void StartGame( ) {
            new Settings( );
            game = new Game( );
            
            // Game has Started -> Hide Menu
            pbMenu.Visible = false;
            pbMenu.Enabled = false;

            game.initGrid( );
            call = true;
            lblPlayerState.Text = "ALIVE";
            lblPlayerState.ForeColor = Color.ForestGreen;
        }

        private void pbMenu_Click( object sender, EventArgs e ) {

        }

        private void UpdateScreen( object sender, EventArgs e ) {
            //Check for Game Over
            if ( !Settings.GameOver ) {
                // Re-Checking the Grid Once to load Assets
                if ( call ) {
                    game.updateGrid( );
                    call=false;
                }
                lblScore.Text = (Settings.Score).ToString( );
                lblNbMines.Text = (Settings.NbMines).ToString( );
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
                        if ( game.grid[x, y].caseState == CaseState.Hidden ) { // square Hidden
                            //lblScore.Text = pbCanvas.Height.ToString(); // To test ...
                            e.Graphics.DrawImage( cache, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) );
                            if( game.grid[x, y].flag ) {
                                e.Graphics.DrawImage( flag, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) );
                            }
                        }
                        else if ( game.grid[x, y].caseState == CaseState.Exploded ) {
                            e.Graphics.DrawImage( mine_exploded, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) );
                        } 
                        else { // case Discovered
                            if ( game.grid[x, y].Value == 0 ) { e.Graphics.DrawImage( empty,new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) ); }
                            if ( game.grid[x, y].Value == 1 ) { e.Graphics.DrawImage( val1, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) ); }
                            if ( game.grid[x, y].Value == 2 ) { e.Graphics.DrawImage( val2, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) ); }
                            if ( game.grid[x, y].Value == 3 ) { e.Graphics.DrawImage( val3, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) ); }
                            if ( game.grid[x, y].Value == 4 ) { e.Graphics.DrawImage( val4, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) ); }
                            if ( game.grid[x, y].Value == 5 ) { e.Graphics.DrawImage( val5, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) ); }
                            if ( game.grid[x, y].Value == 6 ) { e.Graphics.DrawImage( val6, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) ); }
                            if ( game.grid[x, y].caseType == CaseType.Mine ) { e.Graphics.DrawImage( mine, new Rectangle( x * (pbCanvas.Size.Width / Settings.Width), y * (pbCanvas.Size.Height / Settings.Height), (pbCanvas.Size.Width / Settings.Width), (pbCanvas.Size.Height / Settings.Height) ) ); }
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
                lblPlayerState.ForeColor = Color.ForestGreen;
            }
        }

        private void pbCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if ( !Settings.GameOver ) { // If Game isRunning()
                // Détecte le clic. Amélioration : Empêche le clic en dehors de la grille
                MousePosX = (e.X / (pbCanvas.Size.Width / Settings.Width));
                MousePosY = (e.Y / (pbCanvas.Size.Height / Settings.Height));

                switch ( e.Button ) {
                    case (MouseButtons.Left):
                        //MouseButton = "Left_Click";
                        game.discoverSquare( game.grid, MousePosX, MousePosY );
                        game.TestWin( );
                        try {
                            if ( game.grid[MousePosX, MousePosY].caseType == CaseType.Mine ) { // isMine()
                                game.Loose( );
                                game.showAllMines( );
                                game.grid[MousePosX, MousePosY].caseState = CaseState.Exploded;
                            }
                        } catch ( IndexOutOfRangeException ie ) { ie.ToString( ); }
                        break;
                    /*
                    case (MouseButtons.Middle):
                        MouseButton = "Middle_Click";
                        break;
                    */
                    case (MouseButtons.Right):
                        //MouseButton = "Right_Click";
                        try {
                            game.grid[MousePosX, MousePosY].flag = !game.grid[MousePosX, MousePosY].flag;
                        } catch ( IndexOutOfRangeException ie ) { ie.ToString( ); }
                        break;

                }
            }
        }

        private void pbCanvas_Load(object sender, EventArgs e)
        {
            // @TODO : Ajouter un foreach et parcourir un tableau d'images pour le chargement
            mine          = resize_image(load_image("mine.png"),          new Size(32, 32));
            mine_exploded = resize_image(load_image("mine_exploded.png"), new Size(32, 32));
            empty         = resize_image(load_image("vide.png"),          new Size(32, 32));
            val1          = resize_image(load_image("val1.png"),          new Size(32, 32));
            val2          = resize_image(load_image("val2.png"),          new Size(32, 32));
            val3          = resize_image(load_image("val3.png"),          new Size(32, 32));
            val4          = resize_image(load_image("val4.png"),          new Size(32, 32));
            val5          = resize_image(load_image("val5.png"),          new Size(32, 32));
            val6          = resize_image(load_image("val6.png"),          new Size(32, 32));
            cache         = resize_image(load_image("cache.png"),         new Size(32, 32));
            flag          = resize_image(load_image("flag.png"),          new Size(32, 32));
        }

        public Image load_image(string path)
        {
            using (MemoryStream ms = new MemoryStream(File.ReadAllBytes("../../Resources/" + path)))
                return Image.FromStream(ms);
        }

        public Image resize_image(Image originalImage, Size size)
        {
            return (Image)(new Bitmap(originalImage, size));
        }

        private void buttonReplay_Click( object sender, EventArgs e ) {
            gameTimer.Dispose( ); // Forced Dispose;
            SetGameTimer( );
            StartGame( );
        }

        private void SetGameTimer( ) {
            gameTimer.Interval = 1000 / Settings.Speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start( );
        }

        private void setButtonDependencies( ) {
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
        }




        private void pbMenu_Paint( object sender, PaintEventArgs e ) {

        }

        






    }

}
