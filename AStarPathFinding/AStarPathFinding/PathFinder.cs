using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarPathFinding
{
    public class PathFinder
    {
        private static List<Node> openList, closedList, pathList;        
        private static Node current = null;
        private static Node theSource, theDestination;
        private static int g = 0;
        private static string[,] map;

        internal static string[,] getPathPoints(string[,] theMap, Node source, Node destination)
        {
            theSource = source;
            theDestination = destination;
            map = theMap;

            //initialize all
            openList = new List<Node>();
            closedList = new List<Node>();
            pathList = new List<Node>();
            current = null;
            g = 0;

            //Add source to openlist
            openList.Add(theSource);

            //Start finding the paths
            return findThePaths();
        }

        private static string[,] findThePaths()
        {
            while (openList.Count > 0)
            {
                //Get the lowest F score
                var lowestFScore = openList.Min(l => l.f);
                current = openList.First(l => l.f == lowestFScore);

                //Add the current to the closed list
                closedList.Add(current);

                //Remove the current to the open list
                openList.Remove(current);

                //If the added element in the closed list is the destination then the path is found
                if (closedList.FirstOrDefault(l => l.x == theDestination.x && l.y == theDestination.y) != null)
                    break;

                var theNeighbours = findNeighbours(current.x, current.y);

                if (theNeighbours != null)
                {
                    g++;

                    foreach (var aNeighbour in theNeighbours)
                    {
                        // if this neighbour is already in the closed list then ignore it
                        if (closedList.FirstOrDefault(l => l.x == aNeighbour.x
                                && l.y == aNeighbour.y) != null)
                            continue;

                        //If this neighbour is not in the open list
                        if (openList.FirstOrDefault(l => l.x == aNeighbour.x && l.y == aNeighbour.y) == null)
                        {
                            //Calculate score & add the parent
                            aNeighbour.g = g;
                            aNeighbour.h = getHScore(aNeighbour);
                            aNeighbour.f = aNeighbour.g + aNeighbour.h;
                            aNeighbour.parent = current;

                            //Add the neighbour to the strting position of the open list
                            openList.Insert(0, aNeighbour);
                        }
                        else
                        {
                            //If the neighbour is in the open list then check for F score and if lower than update the parent
                            if (g + aNeighbour.h < aNeighbour.f)
                            {
                                aNeighbour.g = g;
                                aNeighbour.f = aNeighbour.g + aNeighbour.h;
                                aNeighbour.parent = current;
                            }
                        }
                    }
                }
            }

            while (current != null)
            {
                if(map[current.x, current.y] == ".")
                    map[current.x, current.y] = " ";

                current = current.parent;
            }

            return map;
        }

        private static List<Node> findNeighbours(int p1, int p2)
        {
            var proposedNodes = new List<Node>()
                {
                    new Node { x = p1, y = p2 - 1},
                    new Node { x = p1, y = p2 + 1},
                    new Node { x = p1 - 1, y = p2},
                    new Node { x = p1 + 1, y = p2}
                };

            return proposedNodes.Where(l => (l.x >= 0 && l.x <= map.GetLength(0) - 1) &&
                (l.y >= 0 && l.y <= map.GetLength(1) - 1) && (map[l.x, l.y] == "." || map[l.x, l.y] == "B")).ToList();

        }

        private static int getHScore(Node aNode)
        {
            return Math.Abs(aNode.x - theDestination.x) + Math.Abs(aNode.y - theDestination.y);
        }
    }
}
