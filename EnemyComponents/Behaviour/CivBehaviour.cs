using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Monster_Hunter_v1._0.PlayerComponents;
using Monster_Hunter_v1._0.TileEngine;
using EnemyComponents.Traversal;
using System.Diagnostics;

namespace Monster_Hunter_v1._0.EnemyComponents.Behaviour
{
    class CivBehaviour : Behaviour
    {
        #region Field Region
        Reaction civReaction = Reaction.Avoid;
		LinkedList<GraphNode> pathToTraverse;
		#endregion

		#region Constructor Region

		public CivBehaviour(AnimatedSprite sprite, Graph pathGraph) : base(sprite,pathGraph)
        {
        }

		#endregion

		#region Method Region

		public void Update(GameTime gameTime)
        {
			Action(gameTime);
		}

        private void Action(GameTime gameTime)
        {

			float seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch (civReaction)
            {
                case Reaction.Avoid:
					if (saw_Monster(300))
					{
						pathToTraverse = FindPath();
						civReaction = Reaction.Patrol;
					}
                    break;
                case Reaction.Hiding:
					break;
				case Reaction.Idle:
					spriteRef.CurrentAnimation = AnimationKey.IdleUp;
					break;
				case Reaction.Patrol:
					if (pathToTraverse.Count > 0)
						Heading = TraversePath();
					else
						Heading = Vector2.Zero;
					//civReaction = Reaction.Idle;
					break;
            }
			spriteRef.Velocity = Heading * MaxVelocity * seconds;
			Move();
        }

		private GraphNode GetNearestHideSpot()
		{
			GraphNode destination = new GraphNode();

			float minDist = float.MaxValue;

			foreach (GraphNode node in pathGraph.Nodes)
			{
				Vector2 nodePos = new Vector2(node.X, node.Y);

				Vector2 dist = nodePos - spriteRef.Position;
				if (dist.LengthSquared() < minDist)
				{
					destination = node;
					minDist = dist.LengthSquared();
				}

			}

			return destination;
		}

		private GraphNode GetFurthestHideSpot()
		{
			GraphNode destination = new GraphNode();

			float maxDist = -1f;

			foreach (GraphNode node in pathGraph.Nodes)
			{
				Vector2 nodePos = new Vector2(node.X, node.Y);

				Vector2 dist = nodePos - pPosition;
				if (dist.LengthSquared() > maxDist)
				{
					destination = node;
					maxDist = dist.LengthSquared();
				}

			}

			return destination;
		}

		private LinkedList<GraphNode> FindPath()
		{
			GraphNode start = GetNearestHideSpot();
			GraphNode end = GetFurthestHideSpot();

			Debug.Print(start.name + " " + end.name);

			LinkedList<GraphNode> path = Pathfinder.AStarSearch(pathGraph, start, end);
			return path;
		}

		private Vector2 TraversePath()
		{
			Vector2 position = new Vector2(pathToTraverse.First().X, pathToTraverse.First().Y);
			Vector2 direction = position - spriteRef.Position;

			Debug.Print(pathToTraverse.First().name + " " + position.ToString());

			if (direction.LengthSquared() > 0)
				direction.Normalize();

			Vector2 radius = spriteRef.Position - position;

			if (radius.LengthSquared() <= 3.0f)
			{
				Debug.Print("first is removed");
				spriteRef.Velocity = Vector2.Zero;
				pathToTraverse.RemoveFirst();
			}
				

			return direction;
		}

		#endregion
	}
}


// need to add connections first
// LinkedList<GraphNode> path = Pathfinder.AStarSearch(g, start, end);

//Heading = pPosition - spriteRef.Position;

            //if (Heading.LengthSquared() > 0)
            //    Heading.Normalize();
//spriteRef.Velocity = -Heading* MaxVelocity * seconds;