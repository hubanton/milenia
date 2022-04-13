
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    /// <summary>
    /// manager for all obstacles that need to be drawn before the player to be in the background
    /// (draworder background obstacle is smaller than draw order character)
    /// </summary>
    public class BackgroundObstacleManager : IGameEntity
    {
        public int DrawOrder => 1;
        
        public List<Obstacle> Obstacles;

        private ContentManager _content;
        private Texture2D ForestSpritesheet;

        
        public static int StoneOneX = 600;
        public static int StoneOneY = 550;
        public static int StoneOneWidth = 180;
        public static int StoneOneHeight = 200;
        public static List<Rectangle> StoneOneBounds = new List<Rectangle>();
        public static int BushOneX = 600;
        public static int BushOneY = 750;
        public static int BushOneWidth = 152;
        public static int BushOneHeight = 136;
        public static List<Rectangle> BushOneBounds = new List<Rectangle>();
        public static int StatueX = 1000;
        public static int StatueY = 550;
        public static int StatueWidth = 224;
        public static int StatueHeight = 488;
        public static List<Rectangle> StatueBounds = new List<Rectangle>();

        public BackgroundObstacleManager(ContentManager content)
        {
            _content = content;
            Obstacles = new List<Obstacle>();
            ForestSpritesheet = _content.Load<Texture2D>("ForestSpritesheet");
            StoneOneBounds.Add(new Rectangle(40, 0, 112, 50));
            BushOneBounds.Add(new Rectangle(0, 0, 152, 66));
            StatueBounds.Add(new Rectangle(0, 200, 224, 200));
        }

        public void SpawnObstacle(String obstacle, Map map, Vector2 mapPosition, List<Rectangle> bounds)
        {
            if (obstacle == null)
            {
                Obstacles.Add(new Asset(map, mapPosition, null, 0, 0, null, null,
                    bounds));
                return;
            }

            switch (obstacle)
            {
                case("stone1"): 
                    Obstacles.Add(new Asset(map, mapPosition, ForestSpritesheet, StoneOneX, StoneOneY, StoneOneWidth, StoneOneHeight, StoneOneBounds));
                    break;
                case("bush1"): 
                    Obstacles.Add(new Asset(map, mapPosition, ForestSpritesheet, BushOneX, BushOneY, BushOneWidth, BushOneHeight, BushOneBounds));
                    break;
                case("statue"): 
                    Obstacles.Add(new Asset(map, mapPosition, ForestSpritesheet, StatueX, StatueY, StatueWidth, StatueHeight, StatueBounds));
                    break;
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