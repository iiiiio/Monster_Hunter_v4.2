using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Monster_Hunter_v1._0.PlayerComponents;
using Monster_Hunter_v1._0.TileEngine;

namespace Monster_Hunter_v1._0.EnemyComponents.Weapons
{
    public abstract class Bullet : DrawableGameComponent
    {
        #region Variable Region

        public bool Alive = true;
        public string Name;
        public Vector2 Position;
        public Vector2 Origin;
        public Vector2 Heading;
        private double moveSpeed;
        private float rotation;

        private Game1 gameRef;
        private Texture2D texture;

        #endregion

        #region Property Region
        public Game1 GameRef
        {
            get { return gameRef; }
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public double MoveSpeed
        {
            get { return moveSpeed; }
            set { moveSpeed = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        
        #endregion

        public Bullet(Game game) : base(game)
        {
            gameRef = (Game1)game;
        }

        public Bullet(Game game, Texture2D texture) : base(game)
        {
            gameRef = (Game1)game;
            this.texture = texture;
        }
        

        /*public void HitTarget(GameObject obj1, GameObject obj2)
        {
            if (obj1 == this)
                Console.WriteLine("{0} Hit {1}", obj1.Name, obj2.Name);
            else
                Console.WriteLine("{0} Hit {1}", obj2.Name, obj1.Name);

            obj1.Alive = false;
            obj2.Alive = false;
            Collider.RemoveAllOf(obj1);
            Collider.RemoveAllOf(obj2);
        }*/

        public abstract Bullet Copy();

        public abstract void Initialize();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

        public abstract Rectangle Bound();

        protected float HeadingToRotation(Vector2 heading)
        {
            // Calculate rotation relative to Vector(1, 0)
            return (float)Math.Atan2(heading.Y, heading.X);
        }
    }

    public static class BulletPool
    {
        public static List<Bullet> Bullets = new List<Bullet>();
        
        public static Bullet GetAvailableBullet()
        {
            for (int i = 0; i < Bullets.Count; i++)
            {
                if (!Bullets[i].Alive)
                {
                    return Bullets[i];
                }
            }

            // Not enough bullet
            Debug.Print("No bullet is available");
            return null;
        }

    }
}
