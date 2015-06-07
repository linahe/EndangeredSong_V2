using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace EndangeredSong
{
    class Decor : Sprite
    {
        public Decor(int x, int y, int width, int height)
        {
            this.pos.X = x;
            this.pos.Y = y;
            this.dim.X = width;
            this.dim.Y = height;
        }
        public Vector2 getPosition()
        {
            return this.pos;
        }

        public void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("Flower.png");
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(image, new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), Color.White);
        }
        public void Update(Controls controls, GameTime gameTime)
        {
            //update capacity 
            //bluh
        }
    }
}