using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MileniaGameProject.UserInput;

namespace MileniaGameProject.Entities
{
    public class MapManager : IGameEntity
    {
        public Map Map;
        
        private ContentManager _content;

        private ObstacleManager _obstacleManager;
        
        private InputController _inputController;

        public MapManager(ContentManager content, ObstacleManager obstacleManager)
        {
            _obstacleManager = obstacleManager;
            _content = content;
            _obstacleManager = new ObstacleManager(content);
        }

        public void LoadMap(String map, Character character)
        {
            Map = new Map(_content.Load<Texture2D>(map), character);
            _inputController = new InputController(character, Map);
            Random rand = new Random();
            _obstacleManager.SpawnObstacle("house", Map, new Vector2(800, 450), "Building", new Rectangle(0, 280, 572, 374));
        }

        public void Update(GameTime gameTime)
        {
            //Activates Input Listener for KeyboardControls
            _inputController.ProcessControls(gameTime, Map);
            
            Map.Update(gameTime);
            if ((int) Map.PlayerPosition.X > Map.MapTexture.Width - Map.Character.CharTexture.Width - 10)
            {
                Map.Character.Position = new Vector2(0, Milenia.DefaultHeight / 2);
                _obstacleManager.ClearList();
                LoadMap("map2nd", Map.Character);
                _inputController = new InputController(Map.Character, Map);
            }
            _obstacleManager.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Map.Draw(gameTime, spriteBatch);
            _obstacleManager.Draw(gameTime, spriteBatch);
        }
    }
}