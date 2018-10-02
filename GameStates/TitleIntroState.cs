using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monster_Hunter_v1._0.Components;
using Monster_Hunter_v1._0.StateManager;

namespace Monster_Hunter_v1._0.GameStates
{

    public interface ITitleIntroStates : IGameState
    {
    }

    public class TitleIntroState : BaseGameState, ITitleIntroStates
    {
        #region Field Region

        Texture2D background;
        Rectangle backgroundDestination;
        SpriteFont font;
        TimeSpan elapsed;
        Vector2 position;
        //Game1 main;
        string message;

        #endregion

        #region Constructor Region

        public TitleIntroState(Game game) : base(game)
        {
            game.Services.AddService(typeof(ITitleIntroStates), this);
        }

        #endregion

        #region Method Region

        public override void Initialize()
        {
            backgroundDestination = GameRef.ScreenRectangle;
            elapsed = TimeSpan.Zero;
            message = "PRESS SPACE TO CONTINUE";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            background = content.Load<Texture2D>(@"GameScreens\MH");
            font = content.Load<SpriteFont>(@"Fonts\InterfaceFont");

            Vector2 size = font.MeasureString(message);
            position = new Vector2((GameRef.ScreenRectangle.Width - size.X) / 2, GameRef.ScreenRectangle.Bottom - 50 - font.LineSpacing);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            PlayerIndex? index = null;
            elapsed += gameTime.ElapsedGameTime;

            if (Xin.CheckKeyReleased(Keys.Space) || Xin.CheckKeyReleased(Keys.Enter) || Xin.CheckMouseReleased(MouseButtons.Left))
            {
                manager.ChangeState((MainMenuState)GameRef.MainMenuState, index);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();

            GameRef.SpriteBatch.Draw(background, backgroundDestination, Color.White);

            Color color = new Color(1f, 1f, 1f) * (float)Math.Abs(Math.Sin(elapsed.TotalSeconds * 2));

            GameRef.SpriteBatch.DrawString(font, message, position, color);

            GameRef.SpriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion
    }
}
