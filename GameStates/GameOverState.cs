using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Monster_Hunter_v1._0.Components;
using Monster_Hunter_v1._0.StateManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monster_Hunter_v1._0.GameStates
{

	public interface IGameOverState : IGameState
	{
	}

	public class GameOverState : BaseGameState, IGameOverState
	{
		#region Field Region

		Rectangle backgroundDestination;
		SpriteFont font;
		Vector2 position;
		Vector2 position1;
		string message;
		string diedMessage;
		TimeSpan elapsed;

		#endregion

		#region Constructor Region

		public GameOverState(Game game) : base(game)
		{
			game.Services.AddService(typeof(IGameOverState), this);
		}

		#endregion

		#region Method Region

		public override void Initialize()
		{
			backgroundDestination = GameRef.ScreenRectangle;
			diedMessage = "YOU DIED";
			message = "PRESS SPACE TO RESTART / ESC TO QUIT";

			base.Initialize();
		}

		protected override void LoadContent()
		{
			font = content.Load<SpriteFont>(@"Fonts\InterfaceFont");

			Vector2 size = font.MeasureString(message);
			position = new Vector2((GameRef.ScreenRectangle.Width - size.X) / 2, GameRef.ScreenRectangle.Bottom - 50 - font.LineSpacing);

			Vector2 size1 = font.MeasureString(diedMessage);
			position1 = new Vector2((GameRef.ScreenRectangle.Width - size1.X) / 2, GameRef.ScreenRectangle.Top + 250 - font.LineSpacing);

			base.LoadContent();
		}

		public override void Update(GameTime gameTime)
		{
			PlayerIndex? index = null;

			elapsed += gameTime.ElapsedGameTime;

			if (Xin.CheckKeyReleased(Keys.Space))
			{
				GameRef.GamePlayState.ResetGame();
				manager.ChangeState((GamePlayState)GameRef.GamePlayState, index);
			}

			if (Xin.CheckKeyReleased(Keys.Escape))
			{
				Game.Exit();
			}

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			GameRef.SpriteBatch.Begin();

			Color color = new Color(1f, 1f, 1f) * (float)Math.Abs(Math.Sin(elapsed.TotalSeconds * 2));

			GameRef.SpriteBatch.DrawString(font, message, position, color);
			GameRef.SpriteBatch.DrawString(font, diedMessage, position1, Color.Red);

			GameRef.SpriteBatch.End();

			base.Draw(gameTime);
		}

		#endregion
	}
}

