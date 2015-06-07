using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndangeredSong
{
    class Camera
    {
        public Matrix transform;
        Viewport view;
        public Vector2 center;
        public Camera(Viewport newView)
        {
            view = newView;
        }
        public void Update(GameTime gameTime, Player player, int screenWidth, int screenHeight)
        {
            center = new Vector2(player.getPosition().X + (player.getDimension().X / 2) - (screenWidth / 2), player.getPosition().Y + (player.getDimension().Y / 2) - (screenHeight / 2));
            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));
        }

        public void Update(GameTime gameTime, Menu menu, int screenWidth, int screenHeight)
        {
            center = new Vector2(menu.getPosition().X + (menu.getDimension().X / 2) - (screenWidth / 2), menu.getPosition().Y + (menu.getDimension().Y / 2) - (screenHeight / 2));
            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));
        }
    }
}
