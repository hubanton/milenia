using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    /// <summary>
    /// manager for all obstacles that need to be drawn after the player to be in the foreground
    /// (draworder background obstacle is bigger than draw order character)
    /// </summary>
    public class ForegroundObstacleManager : IGameEntity
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
        public static int TreeTwoX = 298;
        public static int TreeTwoY = 550;
        public static int TreeTwoWidth = 284;
        public static int TreeTwoHeight = 484;
        public static List<Rectangle> TreeTwoBounds = new List<Rectangle>();
        public static int PillarOneX = 0;
        public static int PillarOneY = 233;
        public static int PillarTwoX = 200;
        public static int PillarTwoY = 282;
        public static int PillarThreeX = 400;
        public static int PillarThreeY = 0;
        public static int PillarFourX = 600;
        public static int PillarFourY = 0;
        public static int PillarFiveX = 800;
        public static int PillarFiveY = 0;
        public static int PillarSixX = 1000;
        public static int PillarSixY = 282;
        public static int PillarSevenX = 1200;
        public static int PillarSevenY = 0;
        public static int PillarEightX = 1400;
        public static int PillarEightY = 0;
        public static int PillarWidth = 164;
        public static int PillarWholeHeight = 512;
        public static int PillarSmallOneHeight = 232;
        public static int PillarSmallTwoHeight = 279;
        public static List<Rectangle> PillarSmallBounds = new List<Rectangle>();
        public static List<Rectangle> PillarWholeBounds = new List<Rectangle>();

        public ForegroundObstacleManager(ContentManager content)
        {
            _content = content;
            Obstacles = new List<Obstacle>();
            ForestSpritesheet = _content.Load<Texture2D>("ForestSpritesheet");
            TreeOneBounds.Add(new Rectangle(116, 310, 64, 162));
            TreeTwoBounds.Add(new Rectangle(106, 310, 64, 162));
            PillarSmallBounds.Add(new Rectangle(0, 0, 164, 232));
            PillarWholeBounds.Add(new Rectangle(0, 371, 164, 142));
        }

        public void SpawnObstacle(String obstacle, Map map, Vector2 mapPosition, List<Rectangle> bounds)
        {
            switch (obstacle)
            {
                case("tree1"):
                    Obstacles.Add(new Asset(map, mapPosition, ForestSpritesheet, TreeOneX, TreeOneY, TreeOneWidth, TreeOneHeight, TreeOneBounds));
                    break;
                case("tree2"): 
                    Obstacles.Add(new Asset(map, mapPosition, ForestSpritesheet, TreeTwoX, TreeTwoY, TreeTwoWidth, TreeTwoHeight, TreeTwoBounds));
                    break;
                case("pillar1"):
                    Obstacles.Add(new Asset(map, mapPosition, ForestSpritesheet, PillarOneX, PillarOneY, PillarWidth, PillarSmallTwoHeight, PillarSmallBounds));
                    break;
                case("pillar2"): 
                    Obstacles.Add(new Asset(map, mapPosition, ForestSpritesheet, PillarTwoX, PillarTwoY, PillarWidth, PillarSmallOneHeight, PillarSmallBounds));
                    break;
                case("pillar3"): 
                    Obstacles.Add(new Asset(map, mapPosition, ForestSpritesheet, PillarThreeX, PillarThreeY, PillarWidth, PillarWholeHeight, PillarWholeBounds));
                    break;
                case("pillar4"): 
                    Obstacles.Add(new Asset(map, mapPosition, ForestSpritesheet, PillarFourX, PillarFourY, PillarWidth, PillarWholeHeight, PillarWholeBounds));
                    break;
                case("pillar5"): 
                    Obstacles.Add(new Asset(map, mapPosition, ForestSpritesheet, PillarFiveX, PillarFiveY, PillarWidth, PillarWholeHeight, PillarWholeBounds));
                    break;
                case("pillar6"): 
                    Obstacles.Add(new Asset(map, mapPosition, ForestSpritesheet, PillarSixX, PillarSixY, PillarWidth, PillarSmallOneHeight, PillarSmallBounds));
                    break;
                case("pillar7"): 
                    Obstacles.Add(new Asset(map, mapPosition, ForestSpritesheet, PillarSevenX, PillarSevenY, PillarWidth, PillarWholeHeight, PillarWholeBounds));
                    break;
                case("pillar8"): 
                    Obstacles.Add(new Asset(map, mapPosition, ForestSpritesheet, PillarEightX, PillarEightY, PillarWidth, PillarWholeHeight, PillarWholeBounds));
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