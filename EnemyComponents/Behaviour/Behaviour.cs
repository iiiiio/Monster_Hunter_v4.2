
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
	public class Behaviour
	{
		#region Field Region

		public Vector2 Heading;
		public Vector2 pPosition;

		public enum Reaction
		{
			Hiding,
			Patrol,
			Fire,
			Death,
			Follow,
			Avoid,
			Idle
		}

		public float MaxVelocity = 100.0f; // pixels per second
		public float MaxSteerForce = 75.0f;
		public List<Vector2> NodePosition = new List<Vector2>();
		protected int i = 0;

		public AnimatedSprite spriteRef;

		protected Graph pathGraph;

		#endregion

		#region Constructor Region

		public Behaviour(AnimatedSprite sprite, Graph pathGraph)
		{
			Heading = new Vector2(1.0f, 0.0f);
			spriteRef = sprite;
			this.pathGraph = pathGraph;
		}

		#endregion

		#region Method Region

		public void PlayerPosition(Vector2 playerPostiion)
		{
			pPosition = playerPostiion;
		}

		protected bool saw_Monster(float radius)
		{
			Vector2 dp = pPosition - spriteRef.Position;
			float distance = dp.Length();
			return (distance < radius);
		}

		protected void Move()
		{
			spriteRef.Position = spriteRef.Position + spriteRef.Velocity;
		}

		protected void PathNode(GameTime gameTime)
		{
			if (i < 0 || i >  NodePosition.Count)
			{
				Debug.Print(spriteRef.Position.X + " " + spriteRef.Position.X);
				return;
			}
				

			Heading = NodePosition[i] - spriteRef.Position;

			if (Heading.Length() < 1.0f)
			{
				if (i < NodePosition.Count - 1)
					i++;
				else
					i = 0;
			}

			if (Heading.LengthSquared() > 0)
				Heading.Normalize();
			float seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
			spriteRef.Velocity = Heading * MaxVelocity * seconds;
		}

		#endregion
	}
}
