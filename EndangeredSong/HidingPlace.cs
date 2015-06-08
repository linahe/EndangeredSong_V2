using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.Diagnostics;
using System.Collections;


namespace EndangeredSong
{
    class HidingPlace : Sprite
    {
        protected int maxCapacity;
        protected int currentCapacity;
        protected Texture2D filledImage;
        protected Texture2D emptyImage;

        public HidingPlace(int x, int y, int width, int height)
	    {
            this.pos.X = x;
            this.pos.Y = y;
            this.dim.X = width;
            this.dim.Y = height;
            this.maxCapacity = 1;
            this.currentCapacity = 0;
	    }

        public HidingPlace(int x, int y, int width, int height, int capacity)
        {
            this.pos.X = x;
            this.pos.Y = y;
            this.dim.X = width;
            this.dim.Y = height;
            this.maxCapacity = capacity;
            this.currentCapacity = 0;
        }
        
        public void LoadContent(ContentManager content)
        {
            if (this.maxCapacity == 3)  //tree
            {
                emptyImage = content.Load<Texture2D>("emptyrightbigtree.png");
                filledImage = content.Load<Texture2D>("fullrightbigtree.png");
            }
            else    //rock
            {
                emptyImage = content.Load<Texture2D>("EmptyRock.png");
                filledImage = content.Load<Texture2D>("FilledRock.png");
            }
        }
        public int getMaxCapacity()
        {
            return this.maxCapacity;
        }
        public int getCurrentCapacity()
        {
            return this.currentCapacity;
        }
        public bool fill()
        {
            if (currentCapacity < maxCapacity)
            {
                currentCapacity++;
                return true;
            }
            else return false;
        }
        public bool empty()
        {
            if (currentCapacity > 0)
            {
                currentCapacity--;
                Console.WriteLine("Current capacity is: " + currentCapacity);
                return true;
            }
            else return false;
        }
        public bool isFull()
        {
            return this.currentCapacity == this.maxCapacity;
        }
        public bool isEmpty()
        {
            return this.currentCapacity == 0;
        }
        public void Draw(SpriteBatch sb)
        {
            if (this.isEmpty())
                sb.Draw(emptyImage, new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), Color.White);
            else
                sb.Draw(filledImage, new Rectangle((int)pos.X, (int)pos.Y, (int)dim.X, (int)dim.Y), Color.White);
        }
        public void Update(Controls controls, GameTime gameTime, Player player, ArrayList harmonians)
        {
            if (controls.onPress(Keys.Space, Buttons.A) && this.intersects(player))
            {
                player.HideHarmonians(this, harmonians);
            }

        }
       
    }
}