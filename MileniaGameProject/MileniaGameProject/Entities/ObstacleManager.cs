using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    public class ObstacleManager : IGameEntity
    {
        public List<Obstacle> Obstacles;

        private ContentManager _content;
        
        public ObstacleManager(ContentManager content)
        {
            _content = content;
        }

        public void SpawnObstacle(String obstacle)
        {
            //loads Obstacle per string obstacle
        }

        public void RemoveObstacle(Obstacle obstacle)
        {
            //removes obstacle from List of Obstacles and maybe more?
        }
        
        public void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new System.NotImplementedException();
        }
    }
}