using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monster_Hunter_v1._0.StateManager;
using Microsoft.Xna.Framework;

namespace Monster_Hunter_v1._0.GameStates
{
    public class BaseGameState : GameState
    {
        #region Field Region

        protected static Random random = new Random();

        protected Game1 GameRef;

        #endregion

        #region Constructor Region

        public BaseGameState(Game game) : base(game)
        {
            GameRef = (Game1)game;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        #endregion
    }
}
