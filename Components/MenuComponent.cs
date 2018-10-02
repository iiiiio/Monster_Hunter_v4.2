using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Monster_Hunter_v1._0.Components
{
    public class MenuComponent
    {
        #region Fields

        SpriteFont spriteFont;
        readonly List<string> menuItems = new List<string>();
        int selectedIndex = -1;
        bool mouseOver;

        int width, height;

        Color normalColor = Color.White;
        Color hilightColor = Color.Red;

        Texture2D texture;

        Vector2 position;

        #endregion

        #region Properties

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = (int)MathHelper.Clamp(value, 0, menuItems.Count - 1);
            }
        }

        public Color NormalColor
        {
            get { return normalColor; }
            set { normalColor = value; }
        }

        public Color HilightColor
        {
            get { return hilightColor; }
            set { hilightColor = value; }
        }

        public bool MouseOver
        {
            get { return mouseOver; }
        }
        #endregion

        #region Constructors

        public MenuComponent(SpriteFont spriteFont, Texture2D texture)
        {
            this.mouseOver = false;
            this.spriteFont = spriteFont;
            this.texture = texture;
        }

        public MenuComponent(SpriteFont spriteFont, Texture2D texture, string[] menuItems) : this(spriteFont, texture)
        {
            selectedIndex = 0;

            foreach (string s in menuItems)
            {
                this.menuItems.Add(s);
            }

            MeasureMenu();
        }
        #endregion

        #region Methods

        public void SetMenuItems(string[] items)
        {
            menuItems.Clear();
            menuItems.AddRange(items);
            MeasureMenu();
            selectedIndex = 0;
        }

        private void MeasureMenu()
        {
            width = texture.Width;
            height = 0;

            foreach (string s in menuItems)
            {
                Vector2 size = spriteFont.MeasureString(s);

                if (size.X > width)
                    width = (int)size.X;

                height += texture.Height + 50;
            }

            height -= 50;
        }

        public void Update(GameTime gameTime)
        {
            Vector2 menuPosition = position;
            Point p = Xin.CurrentMouseState.Position;

            Rectangle buttonRect;
            mouseOver = false;

            for (int i = 0; i < menuItems.Count; i++)
            {
                buttonRect = new Rectangle((int)menuPosition.X, (int)menuPosition.Y, texture.Width, texture.Height);

                if (buttonRect.Contains(p))
                {
                    selectedIndex = i;
                    mouseOver = true;
                }

                menuPosition.Y += texture.Height + 40;
            }

            if(!mouseOver && Xin.CheckKeyReleased(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = menuItems.Count - 1;
            }
            else if (!mouseOver && (Xin.CheckKeyReleased(Keys.Down)))
            {
                selectedIndex++;
                if (selectedIndex > menuItems.Count - 1)
                    selectedIndex = 0;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Vector2 menuPosition = position;
            Color myColor;

            for (int i = 0; i < menuItems.Count; i++)
            {
                if (i == SelectedIndex)
                    myColor = HilightColor;
                else
                    myColor = NormalColor;

                spriteBatch.Draw(texture, menuPosition, Color.White);

                Vector2 textSize = spriteFont.MeasureString(menuItems[i]);

                Vector2 textPosition = menuPosition + new Vector2((int)(texture.Width - textSize.X), (int)(texture.Height - textSize.Y) / 2);
                spriteBatch.DrawString(spriteFont, menuItems[i], textPosition, myColor);

                menuPosition.Y += texture.Height + 40;
            }
        }

        #endregion

        #region Virtual methods
        #endregion
    }
}
