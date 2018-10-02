using System;
using System.Collections.Generic;
using System.Text;

namespace EnemyComponents.Traversal
{
    public class GraphNode
    {
        private static ulong m_Count = 0;

        public ulong ID { get; }

        public float X;
        public float Y;
		public string name;
        private int v1;
        private int v2;

        public GraphNode(float x, float y, string name)
        {
            ID = ++m_Count;
            X = x;
            Y = y;
			this.name = name;
        }

		public GraphNode(float x, float y)
		{
			ID = ++m_Count;
			X = x;
			Y = y;
		}

		public GraphNode(int v1, int v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

		public GraphNode()
		{

		}
    }
}
