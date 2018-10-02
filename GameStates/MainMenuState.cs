using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Monster_Hunter_v1._0.Components;
using Monster_Hunter_v1._0.StateManager;

namespace Monster_Hunter_v1._0.GameStates
{
    public interface IMainMenuState : IGameState
    {
    }

    public class MainMenuState : BaseGameState, IMainMenuState
    {
        #region Field Region

        Texture2D background;
        SpriteFont spriteFont;
        MenuComponent menuComponent;

        #endregion

        #region Property Region
        #endregion

        #region Constructor Region

        public MainMenuState(Game game) : base(game)
        {
            game.Services.AddService(typeof(IMainMenuState), this);
        }

        #endregion

        #region Method Region

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteFont = Game.Content.Load<SpriteFont>(@"Fonts\InterfaceFont");
            background = Game.Content.Load<Texture2D>(@"GameScreens\MH");

            Texture2D texture = Game.Content.Load<Texture2D>(@"Misc\wooden-button");

            string[] menuItems = { "NEW GAME", "CONTINUE", "OPTIONS", "EXIT" };

            menuComponent = new MenuComponent(spriteFont, texture, menuItems);

            Vector2 position = new Vector2();

            position.Y = 90;
            position.X = 1200 - menuComponent.Width;

            menuComponent.Position = position;

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            menuComponent.Update(gameTime);

            if (Xin.CheckKeyReleased(Keys.Space) || Xin.CheckKeyReleased(Keys.Enter) || menuComponent.MouseOver && Xin.CheckMouseReleased(MouseButtons.Left))
            {
                if (menuComponent.SelectedIndex == 0)
                {
                    Xin.FlushInput();
                    GameRef.GamePlayState.SetUpNewGame();
                    GameRef.GamePlayState.StartGame();
                    manager.PushState((GamePlayState)GameRef.GamePlayState, PlayerIndexInControl);
                }
                else if (menuComponent.SelectedIndex == 1)
                {
                    Xin.FlushInput();
                    GameRef.GamePlayState.LoadExistingGame();
                    GameRef.GamePlayState.StartGame();
                    manager.PushState((GamePlayState)GameRef.GamePlayState, PlayerIndexInControl);
                }
                else if (menuComponent.SelectedIndex == 2)
                {
                    Xin.FlushInput();
                }
                else if (menuComponent.SelectedIndex == 3)
                {
                    Game.Exit();
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();

            GameRef.SpriteBatch.Draw(background, GameRef.ScreenRectangle, Color.White);

            GameRef.SpriteBatch.End();

            base.Draw(gameTime);

            GameRef.SpriteBatch.Begin();

            menuComponent.Draw(gameTime, GameRef.SpriteBatch);

            GameRef.SpriteBatch.End();
        }

        #endregion
    }
}
