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

        private Image mine, val1, val2, val3, val4, val5, val6, cache, cache_pressed;
        private Image[] images = new Image[9];
        //private Random random;
        private Case[,] grid;

        public Form1()
        {
            InitializeComponent();

            new Settings();

            gameTimer.Interval = 1000 / Settings.Speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();

            // Starting New game
            grid = new Case[19, 19];
            StartGame(grid);
        }


        private void StartGame(Case[,] grid)
        { 
            lblGameOver.Visible = false;

            new Settings();

            // Loading Grid (RANDOM)
            Array.Clear(grid, 0, grid.Length);
            // Generating Once (In Theory)
            for (int y = 0; y < 19; y++)
            {
                for (int x = 0; x < 19; x++)
                {
                    grid[y, x] = new Case();
                    //
                    // incrementCaseValues(grid[y,x], y, x);
                    if (grid[y, x] != null)
                    {
                        grid[y, x].Value = 1;
                        grid[y, x].caseType = CaseType.Normal;
                        if (grid[y, x].caseType == CaseType.Mine) // Mine (Value == -1)
                        {
                            // Increment Cases which surround a Mine
                            if( y != 0 && x != 0)
                            {
                                //grid[y - 1, x - 1].Value += 1;
                                //grid[y - 1, x].Value += 1;
                                //grid[y - 1, x + 1].Value += 1;
                                //grid[y, x - 1].Value += 1;
                                //grid[y, x + 1].Value += 1;
                                //grid[y + 1, x - 1].Value += 1;
                                //grid[y + 1, x].Value += 1;
                                //grid[y + 1, x + 1].Value += 1;
                            }    
                            
                        }
                    }
                    
                }
            }
        }
        
        private void initGrid(Case[,] grid)
        {
            
        }


        private void UpdateScreen(object sender, EventArgs e)
        {
            //Check for Game Over
            if (Settings.GameOver)
            {
                //Check if Enter is pressed
                if (Input.KeyPressed(Keys.Enter))
                {
                    StartGame(grid);
                }
            }
            else
            {
                // Re-Checking the Grid for any changement   
            }
            pbCanvas.Invalidate();
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            // BufferedGraphics bufferedGraphics ???;
          
            

            // if the game is running
            if (!Settings.GameOver)
            {
                // Generating Images
                for (int y = 0; y < 19; y++)
                {
                    for (int x = 0; x < 19; x++)
                    {

                            if (grid[y, x].caseState == CaseState.Hidden) // case Hidden
                            {
                                //e.Graphics.DrawImage(cache, new Point(x * 32, y * 32));
                            }
                            else // case Discovered
                            {
                                if(grid[y, x].Value == 1)
                                {
                                e.Graphics.DrawImage( val1, new Rectangle( x* (pbCanvas.Size.Width / 19), y*(pbCanvas.Size.Height / 19), (pbCanvas.Size.Width/19), (pbCanvas.Size.Height/19) ) );
                                }
                                if(grid[y,x].caseType == CaseType.Mine)
                                {
                                    e.Graphics.DrawImage(mine, new Point(x * 32, y * 32));
                                }
                            }
                        
                    }
                }
            }
            // Sinon, (gameOver)
            else
            {
                string gameOver = "Game over \nYour final score is: " + Settings.Score + "\nPress Enter to try again";
                lblGameOver.Text = gameOver;
                lblGameOver.Visible = true;
            }
        }

        private void pbCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            MousePosX = e.X;
            MousePosY = e.Y;
            Console.WriteLine("Mouse Clicked at ( {0} ; {1} )", MousePosX, MousePosY);
            
        }

        private void pbCanvas_Load(object sender, EventArgs e)
        {
            // @TODO : Ajouter un foreach et parcourir un tableau d'images pour le chargement
            mine = resize_image(load_image("pictures/mine.png"), new Size(32, 32));
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

    }

}
