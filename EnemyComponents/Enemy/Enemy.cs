using EnemyComponents.Traversal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using Monster_Hunter_v1._0.PlayerComponents;
using Monster_Hunter_v1._0.TileEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter_v1._0.EnemyComponents.Enemy
{
    class Enemy :  DrawableGameComponent
    {
        #region Field Region

        private Game1 gameRef;

        private AnimatedSprite sprite;
        private Texture2D texture;

        private Player playerRef;
        private TiledMap map;

        public TiledMapObject[] pathNodes;

        private Graph pathGraph;
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

        public Player Player
        {
            get { return playerRef; }
            set { playerRef = value; }
        }

        public TiledMap OnMap
        {
            get { return map; }
            set { map = value; }
        }

        public Graph PathGraph
        {
            get { return pathGraph; }
            set { pathGraph = value; }
        }

        #endregion

        #region Constructor Region

        private Enemy(Game game) : base(game)
        {

        }

        public Enemy(Game game, Texture2D texture, Graph pathGraph, Dictionary<AnimationKey, Animation> animation) : base(game)
        {
            gameRef = (Game1)game;

            this.texture = texture;
            this.sprite = new AnimatedSprite(texture, animation);
            this.sprite.CurrentAnimation = AnimationKey.WalkDown;
			this.pathGraph = pathGraph;
        }

        #endregion

        #region Method Region

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
            base.Update(gameTime);
            Sprite.LockToMap(new Point(map.WidthInPixels, map.HeightInPixels));
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            sprite.Draw(gameTime, gameRef.SpriteBatch);
        }

		#endregion
	}
}
