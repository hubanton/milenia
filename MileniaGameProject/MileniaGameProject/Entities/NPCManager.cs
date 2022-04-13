using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    /// <summary>
    /// manager of all npcs
    /// </summary>
    public class NPCManager : IGameEntity
    {
        public int DrawOrder
        {
            get
            {
                if (Talking)
                    return 422;
                return 5;
            }
        }

        public List<NPC> NPCs;

        private ContentManager _content;
        private Texture2D DialogBox;
        private SpriteFont NPCFont;
        public bool Talking;
        public NPCManager(ContentManager content, SpriteFont npcFont)
        {
            _content = content;
            NPCs = new List<NPC>();
            DialogBox = content.Load<Texture2D>("DialogTextbox");
            NPCFont = npcFont;
        }
        
        
        public void SpawnNPC(String npc, Map map, Vector2 mapPosition)
        {
            NPCs.Add(new NPC(map, mapPosition, _content.Load<Texture2D>(npc), NPCFont, DialogBox));
        }
        public void ClearList()
        {
            NPCs = new List<NPC>();
        }
        public void RemoveNPC(NPC npc)
        {
            //removes NPC from List of Building and maybe more?
        }

        public void Update(GameTime gameTime)
        {
            foreach (var npc in NPCs)
            {
                npc.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var npc in NPCs)
            {
                npc.Draw(gameTime, spriteBatch);
            }
        }

        /// <summary>
        /// check which npcs are in range to interact with and returns that npc or null if there is none
        /// </summary>
        /// <returns></returns>
        public NPC FindTalkableNPC()
        {
            foreach (var npc in NPCs)
            {
                if (npc.CanTalkTo)
                {
                    Talking = true;
                    return npc;
                }
            }
            return null;
        }
    }
}