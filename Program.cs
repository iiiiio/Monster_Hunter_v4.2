//using EnemyComponents;
using System;
using System.Collections.Generic;

namespace Monster_Hunter_v1._0
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();
            //]Traversal();
        }

        //static void Main(string[] args)
        //{
        //    TestTraversal();
        //    // TestPQ();
        //}

        //static void TestTraversal()
        //{
        //    // Test the graph in Lecture 5 (See Lecture 5 to recall)
        //    Graph g = new Graph();
        //    // Nodes
        //    GraphNode A = new GraphNode(0, 1); // A
        //    GraphNode B = new GraphNode(1, 2); // B
        //    GraphNode C = new GraphNode(2, 2); // C
        //    GraphNode D = new GraphNode(2, 1); // D
        //    GraphNode E = new GraphNode(1, 0); // E
        //    GraphNode F = new GraphNode(2, 0); // F
        //    GraphNode G = new GraphNode(3, 0); // G
        //    g.Nodes.Add(A); // A
        //    g.Nodes.Add(B); // B
        //    g.Nodes.Add(C); // C
        //    g.Nodes.Add(D); // D
        //    g.Nodes.Add(E); // E
        //    g.Nodes.Add(F); // F
        //    g.Nodes.Add(G); // G
        //    // Connections
        //    g.Connections.Add(new GraphConnection(A, B, 4));
        //    g.Connections.Add(new GraphConnection(A, E, 9));
        //    g.Connections.Add(new GraphConnection(B, C, 4));
        //    g.Connections.Add(new GraphConnection(B, E, 3));
        //    g.Connections.Add(new GraphConnection(C, D, 2));
        //    g.Connections.Add(new GraphConnection(C, G, 2));
        //    g.Connections.Add(new GraphConnection(D, E, 2));
        //    g.Connections.Add(new GraphConnection(D, F, 1));
        //    g.Connections.Add(new GraphConnection(E, F, 1));
        //    g.Connections.Add(new GraphConnection(F, G, 1));

        //    GraphNode start = A;
        //    GraphNode end = G;

        //    LinkedList<GraphNode> path = Pathfinder.TraversalSearch(g, start, end);

        //    char[] NodeName = { '\0', 'A', 'B', 'C', 'D', 'E', 'F', 'G' };

        //    Console.WriteLine("Path from {0} to {1}: ", NodeName[start.ID], NodeName[end.ID]);
        //    foreach (GraphNode node in path)
        //        Console.Write(NodeName[node.ID] + " ");
        //    Console.WriteLine();

        //    Console.ReadLine();

        //}

        //static void TestPQ()
        //{
        //    PriorityQueue<int> pq = new PriorityQueue<int>();
        //    pq.Push(6);
        //    pq.Debug();
        //    pq.Push(2);
        //    pq.Debug();
        //    pq.Push(4);
        //    pq.Debug();
        //    pq.Push(5);
        //    pq.Debug();
        //    pq.Push(3);
        //    pq.Debug();

        //    pq.Pop();
        //    pq.Debug();
        //    pq.Pop();
        //    pq.Debug();
        //    pq.Pop();
        //    pq.Debug();

        //    Console.ReadLine();
        //}
    }
}
#endif

