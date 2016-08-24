using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] grid = new int[5, 5];
            fill_grid(grid);
            display_grid(grid);
        }

        static void fill_grid(int[,] grid)
        {
            for(int y = 0; y < 5; y++)
            {
                for(int x = 0; x < 5; x++)
                {
                    grid[y,x] = 3;
                }
            }
        }

        static void display_grid(int[,] grid)
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    Console.Write(grid[y,x]);
                }
                Console.WriteLine();
            }
        }

    }

   
}
