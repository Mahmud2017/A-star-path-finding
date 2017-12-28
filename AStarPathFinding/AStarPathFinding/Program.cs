using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarPathFinding
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] map = new string[10, 10];
            
            //draw the map
            for (int j = 0; j < map.GetLength(1); j++)
                for (int i = 0; i < map.GetLength(0); i++)
                    map[i, j] = ".";

            //set obstacles
            map[0, 2] = "X";
            map[1, 2] = "X";
            map[2, 2] = "X";
            map[3, 2] = "X";
            map[3, 1] = "X";
            map[9, 5] = "X";
            map[8, 6] = "X";
            map[7, 6] = "X";
            map[7, 7] = "X";
            map[7, 8] = "X";

            //set source
            map[1, 1] = "A";
            Node source = new Node { x = 1, y = 1 };

            //set destination
            map[8, 8] = "B";
            Node destination = new Node { x = 8, y = 8 };

            map = PathFinder.getPathPoints(map, source, destination);

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write("  " + map[i, j]);
                }
                Console.WriteLine("  ");
            }
            
            Console.ReadLine();
        }
    }
}
