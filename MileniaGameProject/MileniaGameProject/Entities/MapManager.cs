﻿using System;
using System.Collections.Generic;
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

        public void LoadMap(String map, Character character, List<(Rectangle, String)> entryPoints, Vector2 cameraPosition)
        {
            Map = new Map(_content.Load<Texture2D>(map), character, entryPoints, cameraPosition);
            if (Map.MapTexture.Name == "TowerMap")
            {
                List<Rectangle> bounds = new List<Rectangle>();
                bounds.Add(new Rectangle(40, 600, 200, 340));
                bounds.Add(new Rectangle(400, 600, 200, 340));
                Milenia.BuildingManager.SpawnBuilding("Arch", Map, new Vector2(680, 600), bounds, Rectangle.Empty);
            }
            else
            {
                Milenia.BuildingManager.ClearList();
            }

            _inputController = new InputController(character, Map);
        }

        public void Update(GameTime gameTime)
        {
            //Activates Input Listener for KeyboardControls
            _inputController.ProcessControls(gameTime, Map);
            
            Map.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Map.Draw(gameTime, spriteBatch);
        }
    }
}