﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;

namespace Monster_Hunter_v1._0.TileEngine
{
    public class Camera
    {
        #region Field Region

        Vector2 position;
        float speed;

        #endregion

        #region Property Region

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public Matrix Transformation
        {
            get { return Matrix.CreateTranslation(new Vector3(-Position, 0f)); }
        }

        #endregion

        #region Constructor Region

        public Camera()
        {
            speed = 4f;
        }

        public Camera(Vector2 position)
        {
            speed = 4f;
            Position = position;
        }

        #endregion

        public void LockCamera(TiledMap map, Rectangle viewport)
        {
            position.X = MathHelper.Clamp(position.X, 0, map.WidthInPixels - viewport.Width);
            position.Y = MathHelper.Clamp(position.Y, 0, map.HeightInPixels - viewport.Height);
        }

        public void LockToSprite(TiledMap map, AnimatedSprite sprite, Rectangle viewport)
        {
            position.X = (sprite.Position.X + sprite.Width / 2) - (viewport.Width / 2);
            position.Y = (sprite.Position.Y + sprite.Height / 2) - (viewport.Height / 2);

            LockCamera(map, viewport);
        }
    }
}
