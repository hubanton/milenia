using System;
using System.Collections.Specialized;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MileniaGameProject.Entities
{
    public class UserInterface : IGameEntity
    {
        private Texture2D _itemBarTexture;
        private Texture2D _itemSelectTexture;
        private Texture2D _statTexture;
        private Texture2D _hpTexture;
        private Texture2D _manaTexture;
        private Texture2D _expTexture;
        private int statX;
        private int statY;
        private int selectX;
        private int offsetX;
        private int itemX;
        private int itemY;
        private int statBarX;
        private int hpY;
        private int manaY;
        private int expY;

        public static int curInvSelection = 0;
        
        public int DrawOrder { get; } = 420;

        public UserInterface(Texture2D itemBarTexture, Texture2D itemSelectTexture, Texture2D statTexture, Texture2D hpTexture, Texture2D manaTexture, Texture2D expTexture)
        {
            _itemBarTexture = itemBarTexture;
            _itemSelectTexture = itemSelectTexture;
            _statTexture = statTexture;
            _expTexture = expTexture;
            _manaTexture = manaTexture;
            _hpTexture = hpTexture;
            itemX = (Milenia.DefaultWidth - itemBarTexture.Width) / 2;
            itemY = (Milenia.DefaultHeight - itemBarTexture.Height) - 15;
            selectX = itemX + 15;
            offsetX = 55;
            statX = 15;
            statY = (Milenia.DefaultHeight - statTexture.Height) - 15;
            statBarX = 179;
            hpY = (Milenia.DefaultHeight - statTexture.Height) + 24 - 15;
            manaY = (Milenia.DefaultHeight - statTexture.Height) + 72 - 15;
            expY = (Milenia.DefaultHeight - statTexture.Height) + 120 - 15;

        }
        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_itemBarTexture, new Vector2(itemX, itemY), Color.White);
            spriteBatch.Draw(_itemSelectTexture, new Vector2(selectX + curInvSelection * offsetX, itemY), Color.White);
            
            spriteBatch.Draw(_hpTexture, new Vector2(statBarX, hpY), new Rectangle(0, 0, (int) ((_hpTexture.Width) * ((double) Character.CurHealth / Character.MaxHealth)), _hpTexture.Height) ,Color.White);
            spriteBatch.Draw(_manaTexture, new Vector2(statBarX, manaY), new Rectangle(0, 0, (int) ((_manaTexture.Width) * ((double) Character.CurMana  / Character.MaxMana)), _manaTexture.Height), Color.White);
            spriteBatch.Draw(_expTexture, new Vector2(statBarX, expY), new Rectangle(0, 0, (int) ((_expTexture.Width) * ((double) Character.CurExperience  / Character.MaxExperience)), _expTexture.Height) ,Color.White);
            spriteBatch.Draw(_statTexture, new Vector2(statX, statY), Color.White);
            
        }
    }
}