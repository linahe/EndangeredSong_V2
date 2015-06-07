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
		protected Texture2D image;

		public Sprite ()
		{
		}
	}
}

