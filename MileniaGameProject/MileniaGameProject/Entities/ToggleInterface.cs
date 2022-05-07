using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MileniaGameProject.SupportFiles;
using MileniaGameProject.UserInput;

namespace MileniaGameProject.Entities
{
    /// <summary>
    /// class to draw und use toggleable menus like skill tree or inventory
    /// </summary>
    public class ToggleInterface : IGameEntity
    {
        private Texture2D _inventoryTexture;
        private Texture2D _skillTreeTexture;
        private Texture2D _blackBox;

        private Vector2 _treePos;
        private Vector2 _invPos;
        public int DrawOrder => 421;

        public ToggleInterface(Texture2D inventory, Texture2D skillTree, Texture2D blackBox)
        {
            _inventoryTexture = inventory;
            _skillTreeTexture = skillTree;
            _blackBox = blackBox;
            var invXPos = Milenia.DefaultWidth / 2 - _inventoryTexture.Width / 2;
            var invYPos = Milenia.DefaultHeight / 2 - _inventoryTexture.Height / 2;
            _invPos = new Vector2(invXPos, invYPos);
            var treeXPos = Milenia.DefaultWidth / 10;
            var treeYPos = Milenia.DefaultHeight / 10;
            _treePos = new Vector2(treeXPos, treeYPos);
        }
        public void Update(GameTime gameTime)
        {
            //TODO
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (InputController.GameState == GameState.Inventory || InputController.GameState == GameState.Skilltree || InputController.GameState == GameState.Talking)
            {
                spriteBatch.Draw(_blackBox, Vector2.Zero, Color.White * 0.5f);
            }
            
            if (InputController.GameState == GameState.Inventory)
            {
                spriteBatch.Draw(_inventoryTexture, _invPos, Color.White);
            } else if (InputController.GameState == GameState.Skilltree)
            {
                spriteBatch.Draw(_skillTreeTexture, _treePos, Color.White);
            }
        }
    }
}