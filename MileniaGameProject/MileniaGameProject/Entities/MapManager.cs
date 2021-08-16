using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MileniaGameProject.UserInput;

namespace MileniaGameProject.Entities
{
    public class MapManager : IGameEntity
    {
        public int DrawOrder => 0;
        
        public Map Map;
        
        private ContentManager _content;
        
        private InputController _inputController;

        public MapManager(ContentManager content)
        {
            _content = content;
        }

        public void LoadMap(String map, Character character)
        {
            Map = new Map(_content.Load<Texture2D>(map), character);
            _inputController = new InputController(character, Map);
        }

        public void Update(GameTime gameTime)
        {
            //Activates Input Listener for KeyboardControls
            _inputController.ProcessControls(gameTime, Map);
            
            Map.Update(gameTime);
            if ((int) Map.PlayerPosition.X > Map.MapTexture.Width - Map.Character.CharTexture.Width - 10)
            {
                Map.Character.Position = new Vector2(0, Milenia.DefaultHeight / 2);
                LoadMap("map2nd", Map.Character);
                _inputController = new InputController(Map.Character, Map);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Map.Draw(gameTime, spriteBatch);
        }
    }
}