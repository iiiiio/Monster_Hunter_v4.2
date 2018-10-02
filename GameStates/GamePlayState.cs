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
using MonoGame.Extended.Tiled.Graphics;
using Monster_Hunter_v1._0.PlayerComponents;
using Monster_Hunter_v1._0.TileEngine;
using Monster_Hunter_v1._0.StateManager;
using Monster_Hunter_v1._0.Collision;
using Monster_Hunter_v1._0.EnemyComponents.Enemy;
using Monster_Hunter_v1._0.EnemyComponents.Traversal;
using EnemyComponents.Traversal;

namespace Monster_Hunter_v1._0.GameStates
{
    public interface IGamePlayState : IGameState
    {
        void SetUpNewGame();
        void LoadExistingGame();
        void StartGame();
		void ResetGame();
    }

    public class GamePlayState : BaseGameState, IGamePlayState
    {
        #region Field Region

        Engine engine;

        private TiledMap map;
        private TiledMapRenderer mapRenderer;

        private TiledMapObjectLayer nodeLayer;
        private TiledMapObject[] nodes;

		private TiledMapObjectLayer pathLayer;
		private TiledMapObject[] pathNodes;

		private TiledMapObjectLayer hideLayer;
		private TiledMapObject[] hideNodes;

		Camera camera;
        Player player;

		List<EnemPolis> polis = new List<EnemPolis>();
		List<EnemCiv> civilians = new List<EnemCiv>();

		CollisionHandler collisionHandler;

		#endregion

		#region Constructor Region

		public GamePlayState(Game game) : base(game)
        {
            game.Services.AddService(typeof(IGamePlayState), this);
        }

        #endregion

        #region Method Region
        public override void Initialize()
        {
            engine = new Engine(GameRef.ScreenRectangle, 64, 64);

			base.Initialize();

			mapRenderer = new TiledMapRenderer(GraphicsDevice);

			player.OnMap = map;

		}

        protected override void LoadContent()
        {
            map = content.Load<TiledMap>(@"Levels\MHForestTest");

			nodeLayer = map.GetLayer<TiledMapObjectLayer>("Node");
            nodes = nodeLayer.Objects;

            pathLayer = map.GetLayer<TiledMapObjectLayer>("Path");
            pathNodes = pathLayer.Objects;

            hideLayer = map.GetLayer<TiledMapObjectLayer>("Hiding");
            hideNodes = hideLayer.Objects;

			SetUpGraph setUp = new SetUpGraph(pathNodes);
			Graph pathGraph = setUp.MakeGraph();

			setUp = new SetUpGraph(hideNodes);
			Graph hideGraph = setUp.MakeGraph();

			Texture2D spriteSheet = content.Load<Texture2D>(@"Sprites\MonsterSprite");
			//Texture2D healthBar = content.Load<Texture2D>(@"Misc\enemy_health_bar_000");
			Texture2D healthContainer = content.Load<Texture2D>(@"Misc\enemy_health_bar_000");
			Texture2D chargeIcon = content.Load<Texture2D>(@"Misc\IconBlue");
			Texture2D inviIcon = content.Load<Texture2D>(@"Misc\IconRed");
			SpriteFont font = content.Load<SpriteFont>(@"Fonts\HealthFont");
			player = new Player(GameRef, "iiio", false, spriteSheet, font, healthContainer,chargeIcon, inviIcon);

			collisionHandler = new CollisionHandler(player);

			Texture2D enemSprite = content.Load<Texture2D>(@"Sprites\PoliceSprite");
			foreach (TiledMapObject obj in nodes)
            {
				if (obj.Type == "PoliceNode")
				{
					//EnemPolis Polis = new EnemPolis(GameRef, enemSprite, pathGraph, GameRef.PolisAnimations);

					//Polis.Position = obj.Position;
					//Polis.Player = player;
					//Polis.OnMap = map;
					//Polis.Initialize();
					////Polis.BulletRef = bullets;
					//collisionHandler.LoadEntities(Polis.Sprite);
					//polis.Add(Polis);
				}
				else if (obj.Type == "CivilianNode")
				{
					EnemCiv Civ = new EnemCiv(GameRef, enemSprite, hideGraph, GameRef.CivAnimations);
					Civ.Position = obj.Position;
					Civ.Player = player;
					Civ.OnMap = map;
					Civ.Initialize();
					collisionHandler.LoadEntities(Civ.Sprite);
					civilians.Add(Civ);
				}
				else if (obj.Type == "PlayerNode")
                {
                    player.Position = obj.Position;
                }
			}

        }

        public override void Update(GameTime gameTime)
        {
			mapRenderer.Update(map, gameTime);
            camera.LockToSprite(map, player.Sprite, GameRef.ScreenRectangle);

            player.Update(gameTime);
            player.Sprite.Update(gameTime);

			if (polis != null)
			{
				foreach (var enemPolis in polis)
				{
					if (enemPolis.Sprite.IsActive)
					{
						enemPolis.Update(gameTime);
						enemPolis.Sprite.Update(gameTime);
					}
				}
			}

			if (civilians != null)
			{
				foreach (var enemCiv in civilians)
				{
					if (enemCiv.Sprite.IsActive)
					{
						enemCiv.Update(gameTime);
						enemCiv.Sprite.Update(gameTime);
					}
				}
			}

			PlayerIndex? index = null;

			if (player.Health <= 0)
			{	
				manager.PushState((GameOverState)GameRef.GameOverState, PlayerIndexInControl);
			}

			collisionHandler.Update();

			base.Update(gameTime);
        }
        
        public override void Draw(GameTime gameTime)
        {

            GameRef.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.Transformation);

            if (map != null && camera != null)
                mapRenderer.Draw(map, camera.Transformation);

            player.Sprite.Draw(gameTime, GameRef.SpriteBatch);
            player.UI(camera);

			if (polis != null)
			{
				foreach (var enemPolis in polis)
				{
					if(enemPolis.Sprite.IsActive)
						enemPolis.Sprite.Draw(gameTime, GameRef.SpriteBatch);
				}
			}

			if (civilians != null)
			{
				foreach (var enemCiv in civilians)
				{
					if (enemCiv.Sprite.IsActive)
						enemCiv.Sprite.Draw(gameTime, GameRef.SpriteBatch);
				}
			}

			GameRef.SpriteBatch.End();

            base.Draw(gameTime);
        }

        public void SetUpNewGame()
        {
            camera = new Camera();
        }

        public void LoadExistingGame()
        {

        }

        public void StartGame()
        {
			
		}

		public void ResetGame()
		{
			foreach (TiledMapObject obj in nodeLayer.Objects)
			{
				if (obj.Type == "PlayerNode")
				{
					player.Position = obj.Position;
				}
			}
			player.Reset();

			foreach (EnemCiv civ in civilians)
			{
				civ.Sprite.IsActive = true;
			}
		}
        #endregion 
        
    }

    
}

