using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace EndangeredSong
{
    class ScreenManager:Sprite
    {
        private Texture2D mainMenu;
        private Texture2D gameOver;
        //private Texture2D gameWon;
        //private Texture2D instructions;
        private Texture2D activeScreen;
        private Rectangle rect;

        public ScreenManager(int x, int y, int width, int height)
        {
            this.pos.X = x;
            this.pos.Y = y;
            this.dim.X = width;
            this.dim.Y = height;
        }
        public void LoadContent(ContentManager content)
        {
            mainMenu = content.Load<Texture2D>("menubackground");
            gameOver = content.Load<Texture2D>("GameOverScreen");
            rect = new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y);
            activeScreen = mainMenu;
        }
        public void setToMenu()
        {
            activeScreen = mainMenu;
        }
        public void setToGameOver()
        {
            activeScreen = gameOver;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(activeScreen, rect, Color.White);
        }
        public void CenterElement(int height, int width)
        {
            rect = new Rectangle((width / 2) - this.activeScreen.Width, (height / 2) - this.activeScreen.Height, this.activeScreen.Width, this.activeScreen.Height);
        }
    }
}
