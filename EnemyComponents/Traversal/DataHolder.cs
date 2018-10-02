using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter_v1._0.EnemyComponents.Traversal
{
	public class DataHolder
	{
		#region Field Region

		Vector2 position;
		string[] destinations;
		string[] weights;
		//string src;
		Dictionary<string, int> destWithWeight = new Dictionary<string, int>();

		#endregion

		#region Property Region

		public string[] Destinations
		{
			get { return destinations; }
			set { destinations = value; }

		}

		public string[] Weights
		{
			get { return weights; }
			set { weights = value; }

		}

		public Dictionary<string, int> DestinationWithWeights
		{
			get { return destWithWeight; }
			//set { src = value; }

		}

		public Vector2 Position
		{
			get { return position; }
			set { position = value; }
		}

		#endregion

		#region Method Region

		public void Organize(string dest, string weight)
		{
			int x = 0;

			if (Int32.TryParse(weight, out x))
			{
				// you know that the parsing attempt
				// was successful
				destWithWeight[dest] = x;
			}
			else
			{
				Debug.Print("Invalid Weight entered!!");
			}
			
		}

		#endregion
	}
}
