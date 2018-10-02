using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;
using Monster_Hunter_v1._0.PlayerComponents;
using Monster_Hunter_v1._0.TileEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter_v1._0.Collision
{
    class CollisionHandler
    {
		#region Field Region

		private TiledMapObject[] colliders;
		Player player;
		List<AnimatedSprite> collidingEntities = new List<AnimatedSprite>();

		#endregion

		#region Property Region
		#endregion

		#region Constructor Region

		public CollisionHandler(Player player)
		{
			this.player = player;
		}

		public CollisionHandler(TiledMapObject[] colliders)
		{
			this.colliders = colliders;
		}

		public CollisionHandler(Player player, TiledMapObject[] colliders)
		{
			this.player = player;
			this.colliders = colliders;
		}

		#endregion

		#region  Method Region

		public void LoadEntities(AnimatedSprite entity)
        {
            collidingEntities.Add(entity);
        }

		public void Update()
		{
			float ax = player.Position.X;
			float ay = player.Position.Y;
			float aX = player.Position.X + player.Sprite.Width;
			float aY = player.Position.Y + player.Sprite.Height;

			for (int j = 0; j < collidingEntities.Count; j++)
			{
				AnimatedSprite entityHolder = collidingEntities.ElementAt(j);

				float bx = entityHolder.Position.X;
				float by = entityHolder.Position.Y;
				float bX = entityHolder.Position.X + entityHolder.Width;
				float bY = entityHolder.Position.Y + entityHolder.Height;

				bool collisionDetected = isColliding(ax, ay, aX, aY, bx, by, bX, bY);

				if (collisionDetected)
				{
					player.IncreaseHealth(3);
					entityHolder.IsActive = false;
					break;
				}
			}
			
		}

		public bool isColliding(float ax, float ay, float aX, float aY, float bx, float by, float bX, float bY)
        {
			return !(aX < bx || bX < ax || aY < by || bY < ay);
        }

		#endregion
	}
}


