using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarPathFinding
{
    class Node
    {
        public int x;
        public int y;
        public int f;
        public int g;
        public int h;
        public Node parent;
    }
}
