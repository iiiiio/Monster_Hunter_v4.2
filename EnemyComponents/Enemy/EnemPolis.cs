using EnemyComponents.Traversal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monster_Hunter_v1._0.EnemyComponents.Behaviour;
using Monster_Hunter_v1._0.EnemyComponents.Weapons;
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
    class EnemPolis : Enemy
    {
        #region Field Region

        public PoliceBehaviour follow;

        // Bullet
        private Bullet m_bullet;
        private double m_firingRate;
        private double m_lastShot;

        private static int m_BulletCount = 0;

        List<DotBullet> bullets;
        #endregion

        #region Constructor Region


        public EnemPolis(Game game, Texture2D texture, Graph graph, Dictionary<AnimationKey, Animation> animation) : base(game, texture, graph, animation)
        {
        }

        #endregion

        #region Property Region
        //public List<DotBullet> BulletRef
        //{
        //    set { bullets = value; }
        //}

        #endregion

        #region Method Region

        public override void Initialize()
        {
            base.Initialize();
            follow = new PoliceBehaviour(Sprite, PathGraph);
            m_bullet = new DotBullet(Game);
            follow.Initialize();

            m_firingRate = 5.0; // number of bullets per second
            m_lastShot = 0;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Sprite.IsAnimating = true;

            follow.Update(gameTime);
            follow.PlayerPosition(Player.Position);

            base.Update(gameTime);
            
        }

        #endregion
    }
}
