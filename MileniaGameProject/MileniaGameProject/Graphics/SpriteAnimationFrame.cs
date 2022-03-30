using System;

namespace MileniaGameProject.Graphics
{
    /// <summary>
    /// combines sprite and timestamp to draw correct sprite in an animation
    /// </summary>
    public class SpriteAnimationFrame
    {
        private Sprite _sprite;
        
        public Sprite Sprite {
            get => _sprite;
            private set => _sprite = value ?? throw new ArgumentNullException("value", "The sprite cannot be null.");
        }
        
        public float TimeStamp { get; }

        public SpriteAnimationFrame(Sprite sprite, float timeStamp)
        {
            Sprite = sprite;
            TimeStamp = timeStamp;
        }
    }
}