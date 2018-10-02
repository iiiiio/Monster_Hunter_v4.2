using System;
using System.Collections.Generic;
using System.Text;

namespace EnemyComponents.Traversal
{
    public class GraphConnection
    {
        public GraphNode Src { get; set; }
        public GraphNode Dest { get; set; }
        public ulong Cost; // for non-negative cost

        public GraphConnection(GraphNode src, GraphNode dest, ulong cost)
        {
            Src = src;
            Dest = dest;
            Cost = cost;
        }
    }
}
