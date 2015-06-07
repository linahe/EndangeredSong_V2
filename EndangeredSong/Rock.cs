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
    class Rock : HidingPlace
    {
        public Rock(int x, int y, int width, int height) : base(x, y, width, height)
        {
            
        }

        public Rock(int x, int y, int width, int height, int capacity) : base(x, y, width, height, capacity)
        {
        }

        new public void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("EmptyRock.png");
            filledHidingPlace = content.Load<Texture2D>("FilledRock.png");
        }
    }
}