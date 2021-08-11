using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    public class Box : Obstacle
    {
        public Box(Map map, Vector2 mapPosition, Texture2D obstacleTexture) : base(map, mapPosition, obstacleTexture)
        {
        }
        
        public Rectangle CollisionBox
        {
            get
            {
                Rectangle box = new Rectangle((int) Math.Round(_mapPosition.X - _map.CameraPosition.X),
                    (int) Math.Round(_mapPosition.Y - _map.CameraPosition.Y), _obstacleTexture.Width,
                    _obstacleTexture.Height);
                return box;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_obstacleTexture, new Vector2((int) (_mapPosition.X - _map.CameraPosition.X),
                (int) (_mapPosition.Y - _map.CameraPosition.Y)), Color.White);
        }


     
    }
}