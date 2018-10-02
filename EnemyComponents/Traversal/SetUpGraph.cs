using EnemyComponents.Traversal;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter_v1._0.EnemyComponents.Traversal
{
	public class SetUpGraph
	{
		#region Field Region

		private TiledMapObject[] pathNodes;
		private Dictionary<string, DataHolder> nodesInfo = new Dictionary<string, DataHolder>();

		#endregion

		#region Constructor Region

		public SetUpGraph (TiledMapObject[] pathNodes)
		{
			this.pathNodes = pathNodes;
		}

		#endregion

		#region Method Region

		public Graph MakeGraph()
		{
			Graph graphHolder = new Graph();

			foreach (TiledMapObject obj in pathNodes)
			{
				if (obj.Type == "GraphNode" || obj.Type == "HideNode")
				{
					//Debug.Print("Graph: " + + " " + obj.Position.Y + " " + obj.Properties["Destination"] + " " + obj.Properties["Weight"]);
					DataHolder temp = new DataHolder();
					temp.Position = obj.Position;
					temp.Destinations = obj.Properties["Destination"].Split(',');
					temp.Weights = obj.Properties["Weight"].Split(',');

					if(temp.Destinations.Count() == temp.Weights.Count())
					{
						for (int i = 0; i < temp.Destinations.Count(); i++)
						{
							temp.Organize(temp.Destinations[i], temp.Weights[i]);
						}
					}
					else
					{
						Debug.Print("Error! Destinations and Weights are unequal!");
					}

					//temp.Source = obj.Name;
					 nodesInfo[obj.Name] = temp;

					GraphNode N = new GraphNode(obj.Position.X, obj.Position.Y, obj.Name);
					graphHolder.Nodes.Add(N); 
				}
			}

			foreach (GraphNode node in graphHolder.Nodes)
			{
				DataHolder temp = nodesInfo[node.name];

				foreach(KeyValuePair<string, int> destination in temp.DestinationWithWeights)
				{
					GraphNode tempNode = graphHolder.Nodes.Find(x => x.name.Contains(destination.Key));
					graphHolder.Connections.Add(new GraphConnection(node, tempNode, (ulong)destination.Value));
				}
			}

			//foreach (GraphConnection connection in graphHolder.Connections)
			//{
			//	Debug.Print("SRC: " + connection.Src.name + " DEST: " + connection.Dest.name + " COST: " + connection.Cost);
			//}

			return graphHolder;
		}

		#endregion


	}
}

//foreach (KeyValuePair<string, DataHolder> obj in nodesInfo)
//{
//	//graphHolder.Nodes.ElementAt(obj.Key);
//	//graphHolder.Connections.Add
//	Debug.Print("Key: " + obj.Key);
//	foreach (string stuff in obj.Value.Destinations)
//		Debug.Print("Dest: " + stuff);
//	foreach (string stuff in obj.Value.Weights)
//		Debug.Print("Weight: " + stuff);

//	//graphHolder.Nodes.Find();
//}
