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
        private Texture2D gameWon;
        private Texture2D instructions1;
        private Texture2D instructions2;
        private Texture2D activeScreen;
        private Rectangle rect;
        private float timer = 0.8f;
        private bool onInstructions = false;
        private bool onMainMenu = true;
        private bool onMainGame = false;

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
            gameWon = content.Load<Texture2D>("winscreen");
            instructions1 = content.Load<Texture2D>("instruction1");
            instructions2 = content.Load<Texture2D>("instruction2");
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
        public void setToGameWon()
        {
            activeScreen = gameWon;
            onMainGame = false;
        }
        public void setToInstructions()
        {
            activeScreen = instructions1;
            onInstructions = true;
            onMainMenu = false;
        }
        public void setToMainGame()
        {
            onMainGame = true;
            onInstructions = false;
        }
        public bool isOnMainGame()
        {
            return this.onMainGame;
        }
        public void switchInstructions()
        {
            if (this.activeScreen == instructions1)
                this.activeScreen = instructions2;
            else
                this.activeScreen = instructions1;
        }
        public void Update(GameTime gameTime, Controls controls)
        {
            if(onMainMenu)
            {
                if (controls.onRelease(Keys.Space, Buttons.A))
                    this.setToInstructions();

            }
            else if(onInstructions)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                timer -= elapsed;
                if(timer <= 0)
                {
                    this.switchInstructions();
                    timer = 1;
                }
                if (controls.onRelease(Keys.Space, Buttons.A))
                    this.setToMainGame();
            }
            

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
