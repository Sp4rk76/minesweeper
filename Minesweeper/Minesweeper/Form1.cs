using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form1 : Form
    {
        private Case[,] grid = new Case[18,18];
        private int NbMines;

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

            // Settings
            new Settings();

            // Loading Grid (RANDOM)
            
            for (int y = 0; y < 18; y++)
            {
                for (int x = 0; x < 18; x++)
                {
                    Array.Clear(grid, 0, grid.Length);
                    grid[y, x] = new Case();

                    lblScore.Text = Settings.Score.ToString();

                    // Checking the Grid
                    updateGrid();

                    // lblNbMines.Text = NbMines.ToString();

                    
                }
            }

        }

        private void updateGrid()
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
                    StartGame();
                }
            }
            else
            {
                // Re-Checking the Grid for any changement
                updateGrid();
            }

            pbCanvas.Invalidate();
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            // BufferedGraphics bufferedGraphics;

            Graphics canvas = e.Graphics;

            // if the game is running

            // Set brush & colours

            // Draw Grid, Case by Case (FILL)

            // Sinon, (gameOver)
                string gameOver = "Game over \nYour final score is: " + Settings.Score + "\nPress Enter to try again";
                lblGameOver.Text = gameOver;
                lblGameOver.Visible = true;
        }

        

        
    }
}
