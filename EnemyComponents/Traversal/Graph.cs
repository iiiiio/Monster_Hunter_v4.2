using System;
using System.Collections.Generic;
using System.Text;

namespace EnemyComponents.Traversal
{
    public class Graph
    {
        public List<GraphNode> Nodes;
        public List<GraphConnection> Connections;

        public Graph()
        {
            Nodes = new List<GraphNode>();
            Connections = new List<GraphConnection>();
        }

        public List<GraphConnection> GetConnections(GraphNode src)
        {
            List<GraphConnection> connections = new List<GraphConnection>();

            foreach (GraphConnection edge in Connections)
            {
                if (edge.Dest == src)
                    connections.Add(new GraphConnection(src, edge.Src, edge.Cost));
                else if (edge.Src == src)
                    connections.Add(edge);
            }

            return connections;
        }

        public List<GraphNode> GetNeighbours(GraphNode src)
        {
            List<GraphNode> neighbours = new List<GraphNode>();

            foreach (GraphConnection edge in Connections)
            {
                if (edge.Src == src)
                    neighbours.Add(edge.Dest);
                else if (edge.Dest == src)
                    neighbours.Add(edge.Src);
            }

            return neighbours;
        }

        public GraphNode GetNode(string name)
        {
            return Nodes.Find(x => x.name.Contains(name));
        }

		public List<GraphNode> GetNodes()
		{
			return Nodes;
		}

	}
}
