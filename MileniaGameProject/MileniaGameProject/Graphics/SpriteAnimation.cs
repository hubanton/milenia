using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MileniaGameProject.Entities;

namespace MileniaGameProject.Content.Graphics
{
    public class SpriteAnimation
    {
        private List<SpriteAnimationFrame> _frames = new List<SpriteAnimationFrame>();

        public int MaxWidth = 0;
        public SpriteAnimationFrame this[int index]
        {
            get
            {
                return GetFrame(index);

            }

        }

      

        public int FrameCount => _frames.Count;

        private bool _goBackwards;

        public SpriteAnimationFrame CurrentFrame
        {

            get
            {
                int i = (int) PlaybackProgress / Character.RUNNING_ANIMATION_LENGTH;
                if (i == _frames.Count)
                {
                    i -= 1;
                }

                return _frames[i];

            }

        }

        public float Duration
        {

            get
            {

                if (!_frames.Any())
                    return 0;

                return _frames.Max(f => f.TimeStamp);

              }

        }

        public bool IsPlaying { get; private set; }

        public float PlaybackProgress { get; private set; }

        public bool ShouldLoop { get; set; } = true;

        public void AddFrame(Sprite sprite, float timeStamp)
        {

            SpriteAnimationFrame frame = new SpriteAnimationFrame(sprite, timeStamp);

            _frames.Add(frame);
            if (frame.Sprite.Width > MaxWidth) MaxWidth = frame.Sprite.Width;

        }

        public void Update(GameTime gameTime)
        {
            if(IsPlaying)
            {

                if (_goBackwards)
                {
                    PlaybackProgress -= 1;

                    if (PlaybackProgress == 0)
                    {
                        if (ShouldLoop)
                            _goBackwards = false;
                        else
                            Stop();
                    }
                }
                else
                {

                    PlaybackProgress += 1;

                    if (PlaybackProgress > Duration + Character.RUNNING_ANIMATION_LENGTH - 1)
                    {
                        if (ShouldLoop)
                            _goBackwards = true;
                        else
                            Stop();
                    }
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {

            SpriteAnimationFrame frame = CurrentFrame;

            if (frame != null)
                frame.Sprite.Draw(spriteBatch, position);

        }

        public void Play()
        {

            IsPlaying = true;

        }

        public void Stop()
        {

            IsPlaying = false;
            PlaybackProgress = 0;

        }

        public SpriteAnimationFrame GetFrame(int index)
        {
            if (index < 0 || index >= _frames.Count)
                throw new ArgumentOutOfRangeException(nameof(index), "A frame with index " + index + " does not exist in this animation.");

            return _frames[index];

        }

        public void Clear()
        {

            Stop();
            _frames.Clear();

        }

        public static SpriteAnimation CreateSimpleAnimation(Texture2D texture, Point startPos, int width, int height, Point offset, int frameCount, float frameLength)
        {
            if (texture == null)
                throw new ArgumentNullException(nameof(texture));

            SpriteAnimation anim = new SpriteAnimation();

            for(int i = 0; i < frameCount; i++)
            {
                Sprite sprite = new Sprite(texture, startPos.X + i * offset.X, startPos.Y + i * offset.Y, width, height);
                anim.AddFrame(sprite, frameLength * i);

                if (i == frameCount - 1)
                    anim.AddFrame(sprite, frameLength * (i + 1));

            }

            return anim;

        } 
    }
}