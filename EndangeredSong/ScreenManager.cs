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
        private Texture2D narrative1;
        private Texture2D narrative2;
        private bool firstDone = false;
        private bool secondDone = false;
        private bool thirdDone = false;
        private bool fourthDone = false;
        private bool fifthDone = false;
        private float animationTimer = 2;
        private float opacity1 = 0.0f;
        private float opacity2 = 0.0f;
        private float opacity3 = 0.0f;
        private float opacity4 = 0.0f;
        private float opacity5 = 0.0f;
        private float opacity6 = 0.0f;

        private bool firstWonDone = false;
        private bool secondWonDone = false;
        private bool thirdWonDone = false;
        private float opacityWon1 = 0.0f;
        private float opacityWon2 = 0.0f;
        private float opacityWon3 = 0.0f;
        private bool oscillation = false;
        SpriteFont font;

        private Texture2D mainMenu;
        private Texture2D gameOver;

        private bool onGameWon1 = false;
        private bool onGameWon2 = false;

        private Texture2D gameWon1;
        private Texture2D gameWon1a;
        private Texture2D gameWon1b;
        private Texture2D gameWon2;
        private Texture2D gameWon3;
        private Texture2D glow;
        private Texture2D instructions1;
        private Texture2D instructions2;
        private Texture2D instructions3;
        private Texture2D instructions4;
        private Texture2D activeScreen;
        private Rectangle rect;
        private float timer = 0.8f;
        private bool onInstructions = false;
        private bool onMainMenu = true;
        private bool onNarrative = false;
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
            narrative1 = content.Load<Texture2D>("narrative");
            gameOver = content.Load<Texture2D>("GameOverScreen");
            gameWon1 = content.Load<Texture2D>("winscreen1");
            gameWon1a = content.Load<Texture2D>("winscreen1a");
            gameWon1b = content.Load<Texture2D>("winscreen1b");
            gameWon2 = content.Load<Texture2D>("winscreen");
            glow = content.Load<Texture2D>("winningsunlight");
            instructions1 = content.Load<Texture2D>("instruction1");
            instructions2 = content.Load<Texture2D>("instruction2");
            instructions3 = content.Load<Texture2D>("instruction3");
            instructions4 = content.Load<Texture2D>("instruction4");
            font = content.Load<SpriteFont>("font");
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
        public void setToNarrative()
        {
            //This I have to look at later. It's INCOMPLETE
            activeScreen = narrative1;
            onMainMenu = false;
            onNarrative = true;
        }
        public void setToGameWon(int harmonians)
        {
            
            if (harmonians == 0)
            {
                animationTimer = 2;
                activeScreen = gameWon1;
                onGameWon1 = true;
            }
            else
            {
                animationTimer = 4;
                activeScreen = gameWon2;
                onGameWon2 = true;
            }
            onMainGame = false;
            onWinScreen = true;
        }
        public void setToInstructions()
        {
            activeScreen = instructions1;
            onInstructions = true;
            onMainMenu = false;
            onNarrative = false;
        }
        public void setToMainGame()
        {
            onMainGame = true;
            onInstructions = false;
            onNarrative = false;
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
        //This method might help in cleaaning up the code.
        /*
        public void increaseOpacity(float opacity, float time, bool done)
        {
            if (opacity <= 1.0f)
                opacity = opacity1 + 0.2f;
            else
            {
                time = 3;
                done = true;
            }
        }*/
        public void Update(GameTime gameTime, Controls controls)
        {
            if(onMainMenu)
            {
                if (controls.onRelease(Keys.Space, Buttons.A))
                    this.setToNarrative();

            }
            else if(onNarrative)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                animationTimer -= elapsed;
                if (animationTimer <= 0 && firstDone == false)
                {
                    if (opacity1 <= 1.0f)
                        opacity1 = opacity1 + 0.1f;
                    else
                    {
                        animationTimer = 1;
                        firstDone = true;
                    }
                }
                else if (animationTimer <= 0 && secondDone == false)
                {
                    if (opacity2 <= 1.0f)
                        opacity2 = opacity2 + 0.1f;
                    else
                    {
                        animationTimer = 1;
                        secondDone = true;
                    }
                }
                else if (animationTimer <= 0 && thirdDone == false)
                {
                    if (opacity3 <= 1.0f)
                        opacity3 = opacity3 + 0.1f;
                    else
                    {
                        animationTimer = 1;
                        thirdDone = true;
                    }
                }
                else if (animationTimer <= 0 && fourthDone == false)
                {
                    if (opacity4 <= 1.0)
                        opacity4 = opacity4 + 0.1f;
                    else
                    {
                        animationTimer = 1;
                        fourthDone = true;
                    }
                }
                else if (animationTimer <= 0 && fifthDone == false)
                {
                    if (opacity5 <= 1.0)
                        opacity5 = opacity5 + 0.1f;
                    else
                    {
                        animationTimer = 1;
                        fifthDone = true;
                    }
                }
                else if (animationTimer <= 0 && fifthDone == true)
                {
                    if (oscillation == false)
                    {
                        opacity6 = opacity6 + 0.02f;
                        if (opacity6 > 1.0)
                            oscillation = true;
                    }
                    else if(oscillation == true)
                    {
                        opacity6 = opacity6 - 0.02f;
                        if (opacity6 < 0)
                            oscillation = false;
                    }
                }
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
            else if(onGameWon1)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                animationTimer -= elapsed;
                if (animationTimer <= 0 && !firstWonDone)
                {
                    if(opacityWon1 <= 1.0)
                        opacityWon1 = opacityWon1 + 0.1f;
                    else
                    {
                        animationTimer = 2;
                        firstWonDone = true;
                    }
                }
                else if (animationTimer <= 0 && !secondWonDone)
                {
                    if (opacity2 <= 1.0)
                        opacityWon2 = opacityWon2 + 0.1f;
                    else
                    {
                        animationTimer = 2;
                        secondWonDone = true;
                    }
                }
                else if (animationTimer <= 0 && !thirdWonDone)
                {
                    if (opacity3 <= 1.0)
                        opacityWon3 = opacityWon3 + 0.1f;
                    else
                    {
                        animationTimer = 2;
                        thirdWonDone = true;
                    }
                }

            }
            else if (onGameWon2)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                animationTimer -= elapsed;
                if (oscillation == false)
                {
                    opacity6 = opacity6 + 0.005f;
                    if (opacity6 > 0.2)
                        oscillation = true;
                }
                else if (oscillation == true)
                {
                    opacity6 = opacity6 - 0.005f;
                    if (opacity6 < 0.1)
                        oscillation = false;
                }
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(activeScreen, rect, Color.White);
            if (onNarrative)
            {
                spriteBatch.DrawString(font, "There was once a time when the musical Harmonians were happy.", new Vector2(50, 50), Color.White * opacity1);
                spriteBatch.DrawString(font, "They would sing and travel together in herds.", new Vector2(80, 90), Color.White * opacity2);
                spriteBatch.DrawString(font, "However, greedy BIO Agents began hunting down these peaceful aliens.", new Vector2(110, 130), Color.White * opacity3);
                spriteBatch.DrawString(font, "As more Harmonians die off, the music is disappearing...", new Vector2(140, 170), Color.White* opacity4);
                spriteBatch.DrawString(font, "Please... save the Endangered Song...", new Vector2(170, 210), Color.White * opacity5);
                spriteBatch.DrawString(font, "Press SPACE to continue...", new Vector2(750, 550), Color.White * opacity6);
            }
            if (onGameWon1)
            {
                spriteBatch.Draw(gameWon1a, rect, Color.White * opacityWon1);
                spriteBatch.Draw(gameWon1b, rect, Color.White * opacityWon2);
            }
            if (onGameWon2)
                spriteBatch.Draw(glow, rect, Color.White * opacity6);
        }
        public void CenterElement(int height, int width)
        {
            rect = new Rectangle((width / 2) - this.activeScreen.Width, (height / 2) - this.activeScreen.Height, this.activeScreen.Width, this.activeScreen.Height);
        }
    }
}
