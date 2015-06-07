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
    class Menu
    {
        private Texture2D GUITexture;
        private Rectangle GUIRect;
        private string assetName;
        private Vector2 pos;
        private Vector2 dim;
        public Menu(int x, int y, int width, int height)
        {
            this.assetName = "menubackground";
            this.pos.X = x;
            this.pos.Y = y;
            this.dim.X = width;
            this.dim.Y = height;
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
            GUITexture = content.Load<Texture2D>(assetName);
            GUIRect = new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y);
        }
        public void Update()
        {

        }
      
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GUITexture, GUIRect, Color.White);
        }
        public void CenterElement(int height, int width)
        {
            GUIRect = new Rectangle((width / 2) - this.GUITexture.Width, (height / 2) - this.GUITexture.Height, this.GUITexture.Width, this.GUITexture.Height);
        }
    }
}
