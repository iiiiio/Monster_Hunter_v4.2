using EnemyComponents.Traversal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monster_Hunter_v1._0.EnemyComponents.Behaviour;
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
    class EnemCiv : Enemy
    {
        #region Field Region

        public CivBehaviour behave;

        #endregion

        #region Constructor Region

        public EnemCiv(Game game, Texture2D texture, Graph graph, Dictionary<AnimationKey, Animation> animation) : base(game, texture, graph, animation)
        {
        }

        #endregion

        #region Method Region

        public override void Initialize()
        {
            base.Initialize();
            behave = new CivBehaviour(Sprite,PathGraph);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            Sprite.IsAnimating = true;

            behave.Update(gameTime);
            behave.PlayerPosition(Player.Position);

            base.Update(gameTime);
        }


        #endregion
    }
}
