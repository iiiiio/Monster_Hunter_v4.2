using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monster_Hunter_v1._0.EnemyComponents.Weapons
{
    public class DotBullet : Bullet
    {
        private float lifeTime = 3;
        private float curentTime = 0;

        public DotBullet(Game game) : base(game)
        {
        }

        public DotBullet(Game game, Texture2D texture) : base(game, texture)
        {
        }
        
        public override Rectangle Bound()
        {
            Vector2 pos = Position - Origin;
            return new Rectangle((int)pos.X, (int)pos.Y, Texture.Width, Texture.Height);
        }

        public override void Initialize()
        {
            //texture = Game1.Assets[m_texture_name];
            //Origin = new Vector2(texture.Width / 2.0f, texture.Height / 2.0f);
            Rotation = 0.0f;
        }

        public override void Update(GameTime gameTime)
        {
            curentTime += (float) gameTime.TotalGameTime.TotalSeconds;
            if (curentTime >lifeTime  
                /*|| if it colide with the wall*/)
            {
                Alive = false;
                //Collider.RemoveAllOf(this);
            }
            /*if (Position.X < 0 || Position.X >= gameRef.ScreenRectangle.Width ||
                Position.Y < 0 || Position.Y >= gameRef.ScreenRectangle.Height)
            {
                Alive = false;
                //Collider.RemoveAllOf(this);
            }*/
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Alive)
                spriteBatch.Draw(Texture, Position, null, Color.White, Rotation, Origin, 1.0f, SpriteEffects.None, 1f);
        }

        public override Bullet Copy()
        {
            DotBullet copy = new DotBullet(GameRef);
            return copy;
        }
    }
}
