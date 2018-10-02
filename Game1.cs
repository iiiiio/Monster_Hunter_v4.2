using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monster_Hunter_v1._0.Components;
using Monster_Hunter_v1._0.GameStates;
using Monster_Hunter_v1._0.StateManager;
using Monster_Hunter_v1._0.TileEngine;
using System.Collections.Generic;

namespace Monster_Hunter_v1._0
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Dictionary<AnimationKey, Animation> playerAnimations = new Dictionary<AnimationKey, Animation>();
        Dictionary<AnimationKey, Animation> polisAnimations = new Dictionary<AnimationKey, Animation>();
        Dictionary<AnimationKey, Animation> civilianAnimations = new Dictionary<AnimationKey, Animation>();

        GameStateManager gameStateManager;

        TitleIntroState titleIntroState;
        MainMenuState mainMenuState;
        GamePlayState gamePlayState;
		GameOverState gameOverState;

        Rectangle screenRectangle;

        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        public Rectangle ScreenRectangle
        {
            get { return screenRectangle; }
        }

        public GameStateManager GameStateManager
        {
            get { return gameStateManager; }
        }

        public TitleIntroState TitleIntroState
        {
            get { return titleIntroState; }
        }

        public MainMenuState MainMenuState
        {
            get { return mainMenuState; }
        }

        public GamePlayState GamePlayState
        {
            get { return gamePlayState; }
        }

		public GameOverState GameOverState
		{
			get { return gameOverState; }
		}

		public Dictionary<AnimationKey, Animation> PlayerAnimations
        {
            get { return playerAnimations; }
        }

        public Dictionary<AnimationKey, Animation> PolisAnimations
        {
            get { return polisAnimations; }
        }

        public Dictionary<AnimationKey, Animation> CivAnimations
        {
            get { return civilianAnimations; }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            screenRectangle = new Rectangle(0, 0, 1280, 720);

            graphics.PreferredBackBufferWidth = ScreenRectangle.Width;
            graphics.PreferredBackBufferHeight = ScreenRectangle.Height;

            gameStateManager = new GameStateManager(this);
            Components.Add(gameStateManager);

            this.IsMouseVisible = true;

            titleIntroState = new TitleIntroState(this);
            mainMenuState = new MainMenuState(this);
            gamePlayState = new GamePlayState(this);
			gameOverState = new GameOverState(this);

            gameStateManager.ChangeState((TitleIntroState)titleIntroState, PlayerIndex.One);
        }

        protected override void Initialize()
        {
            Components.Add(new Xin(this));

            Animation animation = new Animation(3, 32, 32, 0, 0);
            polisAnimations.Add(AnimationKey.WalkDown, animation);

            animation = new Animation(3, 32, 32, 0, 32);
            polisAnimations.Add(AnimationKey.WalkLeft, animation);

            animation = new Animation(3, 32, 32, 0, 64);
            polisAnimations.Add(AnimationKey.WalkRight, animation);

            animation = new Animation(3, 32, 32, 0, 96);
            polisAnimations.Add(AnimationKey.WalkUp, animation);

			animation = new Animation(3, 32, 32, 0, 128);
			civilianAnimations.Add(AnimationKey.WalkDown, animation);

			animation = new Animation(1, 32, 32, 32, 128);
			civilianAnimations.Add(AnimationKey.IdleUp, animation);

			animation = new Animation(8, 48, 48, 0, 144);
			playerAnimations.Add(AnimationKey.WalkDown, animation);

			animation = new Animation(3, 48, 48, 0, 242);
			playerAnimations.Add(AnimationKey.IdleUp, animation);

			base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);          
        }

        protected override void UnloadContent()
        {  
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}

