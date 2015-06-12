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
        private Texture2D instructions3;
        private Texture2D instructions4;
        private Texture2D activeScreen;
        private Rectangle rect;
        private float timer = 0.8f;
        private bool onInstructions = false;
        private bool onMainMenu = true;
        private bool onMainGame = false;
        private bool onWinScreen = false;

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
            instructions3 = content.Load<Texture2D>("instruction3");
            instructions4 = content.Load<Texture2D>("instruction4");
            rect = new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y);
            activeScreen = mainMenu;
        }
        public void setToMenu()
        {
            activeScreen = mainMenu;
            onMainGame = false;
        }
        public void setToGameOver()
        {
            activeScreen = gameOver;
            onMainGame = false;
        }
        public void setToGameWon()
        {
            activeScreen = gameWon;
            onMainGame = false;
            onWinScreen = true;
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
        public bool isOnWinScreen()
        {
            return this.onWinScreen;
        }
        public void switchInstructions()
        {
            if (this.activeScreen == instructions1)
                this.activeScreen = instructions2;
            else if (this.activeScreen == instructions2)
                this.activeScreen = instructions1;
            else return;
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
                {
                    if (this.activeScreen == instructions3)
                        this.activeScreen = instructions4;
                    else if (this.activeScreen == instructions4)
                        this.setToMainGame();
                    else
                        this.activeScreen = instructions3;
                }
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
