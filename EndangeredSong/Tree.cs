using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EndangeredSong
{
    class Tree : HidingPlace
    {
        public Tree(int x, int y, int width, int height) : base(x, y, width, height)
        {
            
        }

        public Tree(int x, int y, int width, int height, int capacity) : base(x, y, width, height, capacity)
        {
        }

        new public void LoadContent(ContentManager content)
        {
            image = content.Load<Texture2D>("EmptyRock.png");
            filledHidingPlace = content.Load<Texture2D>("FilledRock.png");
        }
    }
}
