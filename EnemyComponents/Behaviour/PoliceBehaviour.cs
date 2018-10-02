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
using MonoGame.Extended.Tiled;
using Monster_Hunter_v1._0.EnemyComponents.Weapons;

namespace Monster_Hunter_v1._0.EnemyComponents.Behaviour
{
    class PoliceBehaviour : Behaviour
    {
        #region Field Region
        
        private double m_firingRate;
        private double m_lastShot;
        Reaction polisReaction = Reaction.Patrol;

        private TiledMapObjectLayer pathLayer;
        private TiledMapObject[] path;
        #endregion

        #region Constructor Region

        public PoliceBehaviour(AnimatedSprite sprite, Graph pathGraph) : base(sprite, pathGraph)
        {
        }
        
        #endregion

        #region Method Region
        
        public void Initialize()
        {
            //m_bullet = new DotBullet();
            
            NodePosition = GetPatrolRoute();
        }

		public void Update(GameTime gameTime)
		{
			//LOSChase(gameTime);
			Action(gameTime);
			//return Position;
		}

		private void Action(GameTime gameTime)
        {
            Heading = pPosition - spriteRef.Position;

            if (Heading.LengthSquared() > 0)
                Heading.Normalize();

            float seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch (polisReaction)
            {
                case Reaction.Patrol:
                    PathNode(gameTime);

                    if (saw_Monster(500))
                        polisReaction = Reaction.Follow;

                    break;
                case Reaction.Follow:
                    spriteRef.Velocity = Heading * MaxVelocity * seconds;
                    //LOSChase(gameTime);

                    if (saw_Monster(100))
                        polisReaction = Reaction.Avoid;
                    else if (saw_Monster(400))
                        polisReaction = Reaction.Fire;
                    else if (!saw_Monster(600))
                    {
                        NodePosition.Clear();
                        NodePosition = GetPatrolRoute();
                        polisReaction = Reaction.Patrol;
                    }
                    break;
                case Reaction.Fire:
                    //fire bullet
                    FireBullet(gameTime);
                    polisReaction = Reaction.Follow;

                    break;
                case Reaction.Avoid:
                    spriteRef.Velocity = -Heading * MaxVelocity * seconds;
                    if (!saw_Monster(200))
                        polisReaction = Reaction.Follow;

                    break;
                case Reaction.Death:
                    break;
            }

			Move();
		}

        private List<Vector2> GetPatrolRoute ()
        {
            List<Vector2> route = new List<Vector2>();
            HashSet<GraphNode> DestNode = new HashSet<GraphNode>();

			// Set Node A as default
			GraphNode nearest = pathGraph.GetNode("A");
			float minDist = float.MaxValue;

            //get the nearest position
            foreach (GraphNode node in pathGraph.Nodes)
            {
                Vector2 nodePos = new Vector2(node.X, node.Y);

                Vector2 dist = nodePos - spriteRef.Position;
                if (dist.LengthSquared() < minDist)
                {
                    nearest = node;
                    minDist = dist.LengthSquared();
                }
                
            }
            //Debug.Print("Path" + nearest.name);
            route.Add(new Vector2(nearest.X, nearest.Y));
            DestNode.Add(nearest);

            
            List<GraphNode> neighbours = pathGraph.GetNeighbours(nearest);
            GraphNode chosenNeigbour;

            for (int i = 1; i < 5;i++)
            {
                do
                {
                    Random r = new Random();
                    chosenNeigbour = neighbours[r.Next(0, neighbours.Count)];
                } while (DestNode.Contains(chosenNeigbour));

                //neighbours.Add(chosenNeigbour);
                route.Add(new Vector2(chosenNeigbour.X, chosenNeigbour.Y));
                DestNode.Add(chosenNeigbour);

                neighbours = pathGraph.GetNeighbours(chosenNeigbour);
            }

            int s = route.Count;

            for (int i =s-2; i >0;i--)
            {
                route.Add(route[i]);
            }
            return route;
        }

        public void FireBullet(GameTime gameTime)
        {
            double newtime = gameTime.TotalGameTime.TotalSeconds;
            double dt = newtime - m_lastShot;
            double secondsperbullet = 1.0 / m_firingRate;

            // valid fire (within firing rate)
            if (dt >= secondsperbullet)
            {
                m_lastShot = newtime;

                DotBullet bulletinstance = (DotBullet) BulletPool.GetAvailableBullet();
                bulletinstance.Initialize();
                bulletinstance.Position = spriteRef.Position;
                bulletinstance.Heading = pPosition - spriteRef.Position;
                bulletinstance.Heading.Normalize();
                bulletinstance.Alive = true;
            }
        }

        #endregion

    }
}
