using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    public class ObstacleManager : IGameEntity
    {
        public int DrawOrder => 4;
        
        public List<Obstacle> Obstacles;

        private ContentManager _content;
        
        public ObstacleManager(ContentManager content)
        {
            _content = content;
            Obstacles = new List<Obstacle>();
            
        }

        public void SpawnObstacle(String obstacle, Map map, Vector2 mapPosition, String type, Rectangle? bounds)
        {
            if (type.Equals("Box"))
            {
                Obstacles.Add(new Box(map, mapPosition, _content.Load<Texture2D>(obstacle)));
            } else if (type.Equals("Building"))
            {
                Obstacles.Add(new Building(map, mapPosition, _content.Load<Texture2D>(obstacle), bounds));
            }
        }

        public void RemoveObstacle(Obstacle obstacle)
        {
            //removes obstacle from List of Obstacles and maybe more?
        }
        
        public void Update(GameTime gameTime)
        {
            foreach (var obstacle in Obstacles)
            {
                obstacle.Update(gameTime);
            }
        }

        public void ClearList()
        {
            Obstacles.Clear();
        }
        
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var obstacle in Obstacles)
            {
                obstacle.Draw(gameTime, spriteBatch);
            }
        }
    }
}