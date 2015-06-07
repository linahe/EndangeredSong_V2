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
    class Water : Sprite
    {

        Rectangle rect;
        //SpriteFont font;

        public Water(int x, int y, int width, int height)
        {
            this.pos.X = x;
            this.pos.Y = y;
            this.dim.X = width;
            this.dim.Y = height;
            this.rect = new Rectangle(x, y, width, height);
        }

        public Vector2 getPosition()
        {
            return this.pos;
        }
        public Vector2 getDimension()
        {
            return this.dim;
        }
        public void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("Water.png");
        }

        public Rectangle getRect()
        {
            return this.rect;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(image, new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), Color.White);
        }
        public void Update(Controls controls, GameTime gameTime, Player player)
        {
            Rectangle r = new Rectangle((int)player.getPosition().X, (int)player.getPosition().Y, (int)player.getDimension().X, (int)player.getDimension().Y);

            if (controls.onPress(Keys.Space, Buttons.A) && rect.Intersects(r))
            {
                player.Hide();
            }

        }

    }
}