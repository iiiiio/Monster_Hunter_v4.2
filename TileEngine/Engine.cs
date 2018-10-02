using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;

namespace Monster_Hunter_v1._0.TileEngine
{
    public class Engine
    {
        #region Field Region

        private static Rectangle viewPortRectangle;

        private static int tileWidth = 32;
        private static int tileHeight = 32;

        private TiledMap map;

        private static float scrollSpeed = 500f;

        private static Camera camera;

        #endregion

        #region Property Region

        public static int TileWidth
        {
            get { return tileWidth; }
            set { tileWidth = value; }
        }

        public static int TileHeight
        {
            get { return tileHeight; }
            set { tileHeight = value; }
        }

        public TiledMap Map
        {
            get { return map; }
        }

        public static Rectangle ViewportRectangle
        {
            get { return viewPortRectangle; }
            set { viewPortRectangle = value; }
        }

        public static Camera Camera
        {
            get { return camera; }
        }

        #endregion

        #region Constructor Region

        public Engine(Rectangle viewPort)
        {
            ViewportRectangle = viewPort;
            camera = new Camera();

            TileWidth = 64;
            TileHeight = 64;
        }

        public Engine(Rectangle viewPort, int tileWidth, int tileHeight) : this(viewPort)
        {
            TileWidth = tileWidth;
            TileHeight = tileHeight;
        }

        #endregion

        #region Method Region

        public static Point VectorToCell(Vector2 position)
        {
            return new Point((int)position.X / tileWidth, (int)position.Y / tileHeight);
        }
        
        public void SetMap(TiledMap newMap)
        {
            map = newMap ?? throw new ArgumentNullException("newMap");
        }

        //public void Update(GameTime gameTime)
        //{
        //    Map.Update(gameTime);
        //}

        //public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        //{
        //    Map.Draw(gameTime, spriteBatch, camera);
        //}

        #endregion
    }
}
