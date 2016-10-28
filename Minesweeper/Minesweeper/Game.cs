using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    class Game
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Square[,] grid { get; set; }
        public Game()
        {
            grid = new Square[Settings.Width, Settings.Height];
        }
        /*
        public async void CallInitGrid( ) {
            await initGridAsync();
        }

        public Task<string> initGridAsync(){
            return Task.Run<string>( ( ) => initGrid( ) );
        }
        */
        public void initGrid( ) {
            // Loading Grid (RANDOM)
            Array.Clear( grid, 0, grid.Length );
            // Generating Once (In Theory)
            for ( int x = 0; x < Settings.Width; x++ ) {
                for ( int y = 0; y < Settings.Height; y++ ) {
                    grid[x, y] = new Square( );
                }
            }
            
            // @TODO : Put Loading animations HERE //

        }

        public void updateGrid( ) { // await initGrid ??
            for ( int rowPos = 0; rowPos < Settings.Width; rowPos++ ) {
                for ( int colPos = 0; colPos < Settings.Height; colPos++ ) {
                    if ( grid[rowPos, colPos].caseType == CaseType.Mine ) { // Mine
                        incrementValuesAroundMine( grid, rowPos, colPos );
                        (Settings.NbMines)++;
                    }
                }
            }
        }

        public void showAllMines( ) {
            for ( int rowPos = 0; rowPos < Settings.Width; rowPos++ ) {
                for ( int colPos = 0; colPos < Settings.Height; colPos++ ) {
                    if ( grid[rowPos, colPos].caseType == CaseType.Mine ) {
                        grid[rowPos, colPos].caseState = CaseState.Discovered;
                    }
                }
            }
        }

        public void discoverSquare( Square[,] grid, int rowPos, int colPos ) {
            if ( rowPos < 0 || rowPos >= Settings.Width || colPos < 0 || colPos >= Settings.Height ) {
                return;
            }
            Square square = grid[rowPos, colPos];
            if ( !(square.caseState == CaseState.Discovered) ) {
                square.caseState = CaseState.Discovered;
                switch ( square.Value ) { // isEmpty()
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
                        Settings.NbDiscoveredSquares++;
                        Settings.Score += 20;
                        break;
                    case 1:
                        Settings.Score += 100;
                        Settings.NbDiscoveredSquares++;
                        break;
                    case 2:
                        Settings.Score += 200;
                        Settings.NbDiscoveredSquares++;
                        break;
                    case 3:
                        Settings.Score += 300;
                        Settings.NbDiscoveredSquares++;
                        break;
                    case 4:
                        Settings.Score += 400;
                        Settings.NbDiscoveredSquares++;
                        break;
                    case 5:
                        Settings.Score += 500;
                        Settings.NbDiscoveredSquares++;
                        break;
                    case 6:
                        Settings.Score += 600;
                        Settings.NbDiscoveredSquares++;
                        break;
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





        public void Loose( ) {
            Settings.GameOver = true;
        }

        public void TestWin( ) {
            // nbCases - nbMines == nbCasesDecouvertes
            if ( ((Settings.Width * Settings.Height) - Settings.NbMines) == Settings.NbDiscoveredSquares )
                Settings.Win = true;
        }

    }
}
