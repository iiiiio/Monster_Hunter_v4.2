using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monster_Hunter_v1._0.TileEngine
{
    public enum AnimationKey
    {
        IdleLeft,
        IdleRight,
        IdleDown,
        IdleUp,
        WalkLeft,
        WalkRight,
        WalkDown,
        WalkUp,
        ThrowLeft,
        ThrowRight,
        DuckLeft,
        DuckRight,
        JumpLeft,
        JumpRight,
        Dying,
    }

    public class AnimatedSprite
    {
        #region Field Region

        Dictionary<AnimationKey, Animation> animations;
        AnimationKey currentAnimation;
        bool isAnimating;

        Texture2D texture;
        public Vector2 Position;
        Vector2 velocity;
        float speed = 200.0f;

        float positionWidth, positionHeight;
        private bool isColliding = false;
		private bool isActive = true;
		public float RotationAngle;
		Vector2 origin = new Vector2(0,0);
		#endregion

		#region Property Region

		public bool IsActive
		{
			get { return isActive; }
			set { isActive = value; }
		}

        public AnimationKey CurrentAnimation
        {
            get { return currentAnimation; }
            set { currentAnimation = value; }
        }

        public bool IsAnimating
        {
            get { return isAnimating; }
            set { isAnimating = value; }
        }

        public int Width
        {
            get { return animations[currentAnimation].FrameWidth; }
        }

        public int Height
        {
            get { return animations[currentAnimation].FrameHeight; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = MathHelper.Clamp(speed, 1.0f, 400.0f); }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

		public float VelocityX
		{
			get { return velocity.X; }
			set { velocity.X = value; }
		}

		public float VelocityY
		{
			get { return velocity.Y; }
			set { velocity.Y = value; }
		}

		public float PositionWidth
        {
            get { return positionWidth; }
        }

        public float PositionHeight
        {
            get { return positionHeight; }
        }

        public bool IsColliding
        {
            get { return isColliding; }
            set { isColliding = value; }
        }

		#endregion

		#region Constructor Region

		public AnimatedSprite(Texture2D sprite, Dictionary<AnimationKey, Animation> animation)
        {
            texture = sprite;
            animations = new Dictionary<AnimationKey, Animation>();

            foreach (AnimationKey key in animation.Keys)
                animations.Add(key, (Animation)animation[key].Clone());
		}

        #endregion

        #region Method Region

        public void ResetAnimation()
        {
            animations[currentAnimation].Reset();
        }

        public virtual void Update(GameTime gameTime)
        {
			if (isAnimating)
				animations[currentAnimation].Update(gameTime);

			positionWidth = Position.X + Width;
			positionHeight = Position.Y + Height;
            
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
			//spriteBatch.Draw(texture, Position, animations[currentAnimation].CurrentFrameRect, Color.White);
			origin = new Vector2(Width / 2, Height / 2);
			spriteBatch.Draw(texture, Position, animations[currentAnimation].CurrentFrameRect, Color.White, RotationAngle, origin, 1.0f, SpriteEffects.None, 1);
		}

        public void LockToMap(Point mapSize)
        {
            Position.X = MathHelper.Clamp(Position.X, 0, mapSize.X - Width);
            Position.Y = MathHelper.Clamp(Position.Y, 0, mapSize.Y - Height);
        }

        #endregion
    }
}
