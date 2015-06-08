using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Audio;
namespace EndangeredSong
{
	abstract class Sprite
	{
		protected Vector2 pos;
		protected Vector2 dim;

		public Sprite ()
		{
		}
        public Vector2 getPosition()
        {
            return this.pos;
        }
        public Vector2 getDimension()
        {
            return this.dim;
        }
        public void setPosition(Vector2 newPos)
        {
            this.pos = newPos;
        }
        public void setDimensions(Vector2 newDim)
        {
            this.dim = newDim;
        }

        public bool intersects(Sprite s)
        {
            Vector2 Amax, Bmax, Amin, Bmin;
            Amax = new Vector2(this.pos.X + this.dim.X, this.pos.Y);
            Amin = new Vector2(this.pos.X, this.pos.Y + this.dim.Y);
            Bmax = new Vector2(s.getPosition().X + s.getDimension().X, s.getPosition().Y);
            Bmin = new Vector2(s.getPosition().X, s.getPosition().Y + s.getDimension().Y);

            return !(Amax.X < Bmin.X || Bmax.X < Amin.X || Amax.Y > Bmin.Y || Bmax.Y > Amin.Y);
        }
	}
}

