using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace EnemyComponents.Traversal
{
    public class Pathfinder
    {
        // None = Not seen
        // Opened = Seen and is in the Priority Queue (also called 'OpenSet' / 'OpenList')
        // Closed = Seen and has been taken out of the Priority Queue.
        public enum State {None = 0, Opened, Closed};

        public class NodeRecord : IComparable<NodeRecord>
        {
            public GraphNode self;
            public GraphNode parent;
            public ulong g; // cost so far
            public ulong h; // heuristic
            public State state; // default is None

            public NodeRecord(GraphNode self, ulong g, ulong h)
            {
                this.self = self;
                this.g = g;
                this.h = h;
            }

            public int CompareTo(NodeRecord rhs)
            {
                ulong f1 = this.g + this.h;
                ulong f2 = rhs.g + rhs.h;
                return (int)(f1 - f2);
            }

            public override string ToString()
            {
                return self.ID.ToString();
            }
        }

        public static LinkedList<GraphNode> AStarSearch(Graph graph, GraphNode start, GraphNode end)
        {
            // Create node records to store data used by Traversal
            Dictionary<GraphNode, NodeRecord> nodeRecords = new Dictionary<GraphNode, NodeRecord>();

            // Initialize g and h values
            foreach (GraphNode node in graph.Nodes)
                nodeRecords.Add(node, new NodeRecord(node, ulong.MaxValue, Manhattan(node, end)));
            nodeRecords[start] = new NodeRecord(start, 0, Manhattan(start, end));

            // Priority Queue for deciding which node to process
            PriorityQueue<NodeRecord> pq = new PriorityQueue<NodeRecord>();

            // Push the node record for start node to the priority queue
            NodeRecord startRecord = nodeRecords[start];
            pq.Push(startRecord);
            startRecord.state = State.Opened;

            NodeRecord endRecord = nodeRecords[end];
            NodeRecord curRecord, neighbourRecord;

            while (!pq.Empty())
            {
                curRecord = pq.Top();
                pq.Pop();
                curRecord.state = State.Closed;

                List<GraphConnection> connections = graph.GetConnections(curRecord.self);

                foreach (GraphConnection connection in connections)
                {
                    neighbourRecord = nodeRecords[connection.Dest];

                    // Ignore neighbours in Closed state
                    if (neighbourRecord.state == State.Closed)
                        continue;

                    // Update g
                    ulong gNew = curRecord.g + connection.Cost;
                    if (neighbourRecord.g > gNew)
                    {
                        neighbourRecord.g = gNew;
                        neighbourRecord.parent = curRecord.self;
                    }

                    // Update PQ
                    if (neighbourRecord.state == State.Opened)
                        pq.Update(neighbourRecord);
                    else
                    {
                        pq.Push(neighbourRecord);
                        neighbourRecord.state = State.Opened;
                    }
                }

            }

            return ConstructPath(nodeRecords, start, end);
        }


		private static ulong Manhattan(GraphNode current, GraphNode end)
        {
            return (ulong)(Math.Abs(current.X - end.X) + Math.Abs(current.Y - end.Y));
        }

        private static LinkedList<GraphNode> ConstructPath(Dictionary<GraphNode, NodeRecord> nodeRecords, GraphNode start, GraphNode end)
        {
            LinkedList<GraphNode> path = new LinkedList<GraphNode>();

            for (NodeRecord cur = nodeRecords[end]; cur.parent != null; )
            {
                path.AddFirst(cur.self);
                cur = nodeRecords[cur.parent];
            }
            path.AddFirst(start);

            return path;
        }
    }
}
