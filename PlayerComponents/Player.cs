using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using Monster_Hunter_v1._0.Components;
using Monster_Hunter_v1._0.TileEngine;

namespace Monster_Hunter_v1._0.PlayerComponents
{
    class Player : DrawableGameComponent
    {
        #region Field Region

        private Game1 gameRef;

        private TiledMap map;
        private AnimatedSprite sprite;
        private Texture2D texture;

        private Texture2D healthBar;
		private Texture2D healthContainer;
		private Texture2D chargeIcon;
		private Texture2D inviIcon;
		private SpriteFont healthCounter;
		private float health = 100f;

        private float chargeMultiplier = 0.05f;
        private float chargeTime = 1f;
        private float coolDownTimeCharging = 4f;
        private bool isCharging = false;
        static float timeElapsedCharging;
        static Vector2 beforeChargeMotion = Vector2.Zero;
        static AnimationKey beforeChargeAnimation;

        private float inviFrames = 2f;
		private bool isInvi = false;
		static float timeElapsedInvi;
		private float coolDownTimeInvi = 5f;

		//private Vector2 position;

		#endregion

		#region Property Region

		public Vector2 Position
        {
            get { return sprite.Position; }
            set { sprite.Position = value; }
        }

        public AnimatedSprite Sprite
        {
            get { return sprite; }
        }

        public float Health
        {
            get { return health; }
            set { health = value; }
        }

        public float InviFrames
        {
            get { return inviFrames; }
            set { inviFrames = value; }
        }

        public TiledMap OnMap
        {
            get { return map; }
            set { map = value; }
        }

        #endregion

        #region Constructor Region

        private Player(Game game) : base(game)
        {

        }

		public Player(Game game, string name, bool gender, Texture2D texture, SpriteFont healthCounter,
			Texture2D healthCounterContainer, Texture2D chargeIcon, Texture2D inviIcon) : base(game)
		{
			gameRef = (Game1)game;

			this.texture = texture;
			this.healthCounter = healthCounter;
			healthContainer = healthCounterContainer;
			this.chargeIcon = chargeIcon;
			this.inviIcon = inviIcon;
			this.sprite = new AnimatedSprite(texture, gameRef.PlayerAnimations);
			this.sprite.CurrentAnimation = AnimationKey.WalkDown;
		}

		public Player(Game game, string name, bool gender, Texture2D texture, Texture2D healthBar, Texture2D healthContainer
			, Texture2D chargeIcon, Texture2D inviIcon) : base(game)
		{
			gameRef = (Game1)game;

			this.texture = texture;
			this.healthBar = healthBar;
			this.healthContainer = healthContainer;
			this.chargeIcon = chargeIcon;
			this.inviIcon = inviIcon;
			this.sprite = new AnimatedSprite(texture, gameRef.PlayerAnimations);
			this.sprite.CurrentAnimation = AnimationKey.WalkDown;
		}

		public Player(Game game, string name, bool gender, Texture2D texture, Texture2D healthBar, Texture2D healthContainer) : base(game)
        {
            gameRef = (Game1)game;

            this.texture = texture;
			this.healthBar = healthBar;
			this.healthContainer = healthContainer;
            this.sprite = new AnimatedSprite(texture, gameRef.PlayerAnimations);
            this.sprite.CurrentAnimation = AnimationKey.WalkDown;
        }

		public Player(Game game, string name, bool gender, Texture2D texture, Texture2D healthBar) : base(game)
		{
			gameRef = (Game1)game;

			this.texture = texture;
			this.healthBar = healthBar;
			this.sprite = new AnimatedSprite(texture, gameRef.PlayerAnimations);
			this.sprite.CurrentAnimation = AnimationKey.WalkDown;
		}
		#endregion

		#region Method Region

		public void SavePlayer()
        {

        }

        public static Player Load(Game game)
        {
            Player player = new Player(game);

            return player;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {  
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

			Move();
			Charge(gameTime);
			HealthDeplete(gameTime);
            Sprite.IsAnimating = true;
            Sprite.LockToMap(new Point(map.WidthInPixels, map.HeightInPixels));
			
			if (Sprite.Velocity == Vector2.Zero)
			{
				Sprite.CurrentAnimation = AnimationKey.IdleUp;
			}
			else
			{
				Sprite.CurrentAnimation = AnimationKey.WalkDown;
			}

			KeepHealth();

			if (!isCharging)
			{
				Sprite.Velocity *= (Sprite.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
			}
			else
			{
				Sprite.Velocity *= (Sprite.Speed * chargeMultiplier);
			}

			Sprite.Position = Sprite.Position + Sprite.Velocity;

			base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            sprite.Draw(gameTime, gameRef.SpriteBatch);
        }

        public void Move()
        {
			Sprite.Velocity = Vector2.Zero;

            if (Xin.CurrentKeyboardState.IsKeyDown(Keys.W) && Xin.CurrentKeyboardState.IsKeyDown(Keys.A))
            {
				Sprite.VelocityX = -1;
				Sprite.VelocityY = -1;
				Sprite.RotationAngle = MathHelper.ToRadians(135f);
				//Sprite.CurrentAnimation = AnimationKey.WalkLeft;
			}
            else if (Xin.CurrentKeyboardState.IsKeyDown(Keys.W) && Xin.CurrentKeyboardState.IsKeyDown(Keys.D))
            {
				Sprite.VelocityX = 1;
				Sprite.VelocityY = -1;
				Sprite.RotationAngle = MathHelper.ToRadians(225f);
				//Sprite.CurrentAnimation = AnimationKey.WalkRight;
			}
            else if (Xin.CurrentKeyboardState.IsKeyDown(Keys.S) && Xin.CurrentKeyboardState.IsKeyDown(Keys.A))
            {
				Sprite.VelocityX = -1;
				Sprite.VelocityY = 1;
				Sprite.RotationAngle = MathHelper.ToRadians(45f);
				//Sprite.CurrentAnimation = AnimationKey.WalkLeft;
			}
            else if (Xin.CurrentKeyboardState.IsKeyDown(Keys.S) && Xin.CurrentKeyboardState.IsKeyDown(Keys.D))
            {
				Sprite.VelocityX = 1;
				Sprite.VelocityY = 1;
				Sprite.RotationAngle = MathHelper.ToRadians(315f);
				//Sprite.CurrentAnimation = AnimationKey.WalkRight;
			}
            else if (Xin.CurrentKeyboardState.IsKeyDown(Keys.W))
            {
				Sprite.VelocityY = -1;
                //Sprite.CurrentAnimation = AnimationKey.WalkUp;
				Sprite.RotationAngle = MathHelper.ToRadians(180f);
			}
            else if (Xin.CurrentKeyboardState.IsKeyDown(Keys.S))
            {
				Sprite.VelocityY = 1;
				Sprite.RotationAngle = MathHelper.ToRadians(0f);
				//Sprite.CurrentAnimation = AnimationKey.WalkDown;
			}
            else if (Xin.CurrentKeyboardState.IsKeyDown(Keys.A))
            {
				Sprite.VelocityX = -1;
				Sprite.RotationAngle = MathHelper.ToRadians(90f);
				//Sprite.CurrentAnimation = AnimationKey.WalkLeft;
			}
            else if (Xin.CurrentKeyboardState.IsKeyDown(Keys.D))
            {
				Sprite.VelocityX = 1;
				Sprite.RotationAngle = MathHelper.ToRadians(270f);
				//Sprite.CurrentAnimation = AnimationKey.WalkRight;
			}

            if (Xin.CurrentKeyboardState.IsKeyDown(Keys.Space))
            {
                beforeChargeMotion = Sprite.Velocity;
                beforeChargeAnimation = Sprite.CurrentAnimation;
                isCharging = true;
            }

			if (Xin.CurrentKeyboardState.IsKeyDown(Keys.F))
			{
				isInvi = true;
			}

		}
        //Charge and move are linked
        public void Charge(GameTime gameTime)
        {

            if (beforeChargeMotion != null && isCharging && timeElapsedCharging < chargeTime)
            {
                timeElapsedCharging += (float)gameTime.ElapsedGameTime.TotalSeconds;

				Sprite.CurrentAnimation = beforeChargeAnimation;

				if (!beforeChargeMotion.Equals(Vector2.Zero))
				{
					beforeChargeMotion.Normalize();
					Sprite.Velocity = beforeChargeMotion;
				}
				else
					isCharging = false;
            }
			else if (!Sprite.Velocity.Equals(Vector2.Zero))
            {
				Sprite.Velocity.Normalize();
				//Sprite.Velocity *= (Sprite.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            if (timeElapsedCharging >= chargeTime)
            {
				isCharging = false;
                beforeChargeMotion = Vector2.Zero;
                timeElapsedCharging += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timeElapsedCharging > (chargeTime + coolDownTimeCharging))
                {
                    timeElapsedCharging = 0;
                }
            }
        }

        public void HealthDeplete(GameTime gameTime, int factor = 0)
        {
			if (!isInvi)
			{
				if (factor == 0)
					health -= 0.1f;
				else
					health -= 0.1f * factor;
			}
			else if(isInvi && timeElapsedInvi < inviFrames)
			{
				timeElapsedInvi += (float)gameTime.ElapsedGameTime.TotalSeconds;
			}

			if (timeElapsedInvi >= inviFrames)
			{
				isInvi = false;
				timeElapsedInvi += (float)gameTime.ElapsedGameTime.TotalSeconds;
				if (timeElapsedInvi > (inviFrames + coolDownTimeInvi))
				{
					timeElapsedInvi = 0;
				}
			}

		}

		public void KeepHealth()
		{
			if (health > 100.0f)
				health = 100.0f;
		}

		public void IncreaseHealth(int inc = 5)
		{
			health += inc;
		}

		public void Reset()
		{
			health = 100.0f;
		}

		public void UI(Camera camera)
        {

			if (health > 0 && healthBar != null && healthContainer != null)
            {
				Vector2 healthBarPos;

				healthBarPos.X = camera.Position.X + gameRef.ScreenRectangle.Width / 2 - healthBar.Width / 2;
				healthBarPos.Y = camera.Position.Y + gameRef.ScreenRectangle.Height - 100f;
				float healthWidth = healthBar.Width * (health / 100f);
				//Debug.Print("This ran!" + healthBar.Width + " " + healthBar.Height + " " + (int)healthWidth);
				gameRef.SpriteBatch.Draw(healthBar, new Rectangle((int)healthBarPos.X, (int)healthBarPos.Y, (int)healthWidth, healthBar.Height),
					new Rectangle(0, 0, healthBar.Width, healthBar.Height),
					Color.White);
				gameRef.SpriteBatch.Draw(healthContainer, new Rectangle((int)healthBarPos.X, (int)healthBarPos.Y, healthContainer.Width, healthContainer.Height), Color.White);

			}
			else if(health > 0 && healthCounter != null && healthContainer != null)
			{
				Vector2 healthCounterPos;
				string healthString = health.ToString("#.##");
				Vector2 size = healthCounter.MeasureString(healthString);
				healthCounterPos.X = camera.Position.X + gameRef.ScreenRectangle.Width / 2 - size.X;
				healthCounterPos.Y = camera.Position.Y + gameRef.ScreenRectangle.Height - 80f;
				
				Rectangle healthContainerPos = new Rectangle((int)camera.Position.X + gameRef.ScreenRectangle.Width / 2 - healthContainer.Width / 2 - 30,
					(int)healthCounterPos.Y - 15, healthContainer.Width, healthContainer.Height);

				gameRef.SpriteBatch.Draw(healthContainer, healthContainerPos, Color.White);
				
				gameRef.SpriteBatch.DrawString(healthCounter, healthString, healthCounterPos, Color.Black);
				
			}

			Vector2 iconPos;

			iconPos.X = camera.Position.X + 10f;
			iconPos.Y = camera.Position.Y + gameRef.ScreenRectangle.Height - 100f;

			float transparencyCharge = (chargeTime - timeElapsedCharging);
			float transparencyInvi = (inviFrames - timeElapsedInvi);

			if (chargeIcon != null && inviIcon != null)
			{
				gameRef.SpriteBatch.Draw(chargeIcon, iconPos, null, new Color(Color.Pink, transparencyCharge), 0f,Vector2.Zero, 0.07f, SpriteEffects.None, 0f);

				iconPos.X += 100f;
				gameRef.SpriteBatch.Draw(inviIcon, iconPos, null, new Color(Color.Pink, transparencyInvi), 0f, Vector2.Zero, 0.07f, SpriteEffects.None, 0f);
			}

		}

        #endregion
    }
}
