using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.Diagnostics;


namespace EndangeredSong
{
    class Water : HidingPlace
    {

        //SpriteFont font;

        float frameRate = 1;
        const float timer = 1;
        int frames = 0;
        const int FRAMES = 3;
        Texture2D frame1;
        Texture2D frame2;
        Texture2D frame3;

        public Water(int x, int y, int width, int height) : base(x, y, width, height)
        {
            
        }
        public void LoadContent(ContentManager content)
        {
            frame1 = content.Load<Texture2D>("Water.png");
            frame2 = content.Load<Texture2D>("Water2.png");
            frame3 = content.Load<Texture2D>("Water3.png");
        }

        public void Draw(SpriteBatch sb)
        {
            if (frames == 0)
                sb.Draw(frame1, new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), Color.White);
            if (frames == 1)
                sb.Draw(frame2, new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), Color.White);
            if (frames == 2)
                sb.Draw(frame3, new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), Color.White);
            if (frames == 3)
                sb.Draw(frame2, new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), Color.White);
        }
        public void Update(Controls controls, GameTime gameTime, Player player)
        {
            if (controls.onPress(Keys.Space, Buttons.A) && this.intersects(player))
            {
                player.Hide();
            }
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            frameRate -= elapsed;
            if (frameRate < 0)
            {
                frames++;
                if (frames > 3)
                {
                    frames = 0;
                }
                frameRate = timer;
            }
        }

    }
}