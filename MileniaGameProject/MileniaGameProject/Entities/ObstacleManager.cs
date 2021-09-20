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
        private Texture2D ForestSpritesheet;

        public static int TreeOneX = 0;
        public static int TreeOneY = 550;
        public static int TreeOneWidth = 284;
        public static int TreeOneHeight = 484;
        public static List<Rectangle> TreeOneBounds = new List<Rectangle>();
        
        public ObstacleManager(ContentManager content)
        {
            _content = content;
            Obstacles = new List<Obstacle>();
            ForestSpritesheet = _content.Load<Texture2D>("ForestSpritesheet");
            TreeOneBounds.Add(new Rectangle(116, 230, 64, 242));
        }

        public void SpawnObstacle(String obstacle, Map map, Vector2 mapPosition, List<Rectangle> bounds)
        {
            switch (obstacle)
            {
                case("tree1"):
                    Obstacles.Add(new Asset(map, mapPosition, ForestSpritesheet, TreeOneX, TreeOneY, TreeOneWidth, TreeOneHeight, TreeOneBounds));
                    break;
                /*case("tree2"): 
                    Obstacles.Add(new Asset(map, mapPosition, _content.Load<Texture2D>(obstacle), 0, 550, 284, 484));
                    break;
                case("tree1"): 
                    Obstacles.Add(new Asset(map, mapPosition, _content.Load<Texture2D>(obstacle), 0, 550, 284, 484));
                    break;
                case("tree1"): 
                    Obstacles.Add(new Asset(map, mapPosition, _content.Load<Texture2D>(obstacle), 0, 550, 284, 484));
                    break;
                case("tree1"): 
                    Obstacles.Add(new Asset(map, mapPosition, _content.Load<Texture2D>(obstacle), 0, 550, 284, 484));
                    break;
                case("tree1"): 
                    Obstacles.Add(new Asset(map, mapPosition, _content.Load<Texture2D>(obstacle), 0, 550, 284, 484));
                    break;*/
                default:
                    Obstacles.Add(new Asset(map, mapPosition, _content.Load<Texture2D>(obstacle), 0, 0, null, null, bounds));
                    break;
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