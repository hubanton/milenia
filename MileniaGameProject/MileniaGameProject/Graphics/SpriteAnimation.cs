using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MileniaGameProject.Entities;

namespace MileniaGameProject.Graphics
{
    /// <summary>
    /// an animation consisting of different frames shown at the appropriate frame
    /// </summary>
    public class SpriteAnimation
    {
        private readonly List<SpriteAnimationFrame> _frames = new List<SpriteAnimationFrame>();
        
        // used to get specific frame but is not used currently
        public SpriteAnimationFrame this[int index] => GetFrame(index);

        // used to get framecount but is not used currently
        public int FrameCount => _frames.Count;

        /// <summary>
        /// fetches current sprite based on PlaybackProgress
        /// </summary>
        public SpriteAnimationFrame CurrentFrame
        {

            get
            {
                int i = PlaybackProgress / Character.RUNNING_ANIMATION_FRAME_DURATION;
                if (i == _frames.Count)
                {
                    i -= 1;
                }

                return _frames[i];

            }

        }

        /// <summary>
        /// returns duration of animation
        /// </summary>
        public int Duration
        {

            get
            {

                if (!_frames.Any())
                    return 0;

                return _frames.Max(f => f.TimeStamp);

              }

        }

        private bool IsPlaying { get; set; }

        private int PlaybackProgress { get; set; }

        private bool ShouldLoop = true;

        public void AddFrame(Sprite sprite, int timeStamp)
        {
            SpriteAnimationFrame frame = new SpriteAnimationFrame(sprite, timeStamp);

            _frames.Add(frame);
        }

        /// <summary>
        /// updates PlaybackProgress to get appropriate frame
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if (!IsPlaying) return;
            
            PlaybackProgress += 1;

            if (PlaybackProgress <= Duration) return;
                
            if (ShouldLoop)
            {
                PlaybackProgress = 0;
            }
            else
            {
                IsPlaying = false;
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

        /// <summary>
        ///  used to create a simple animation but not used currently
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="startPos"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="offset"></param>
        /// <param name="frameCount"></param>
        /// <param name="frameLength"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static SpriteAnimation CreateSimpleAnimation(Texture2D texture, Point startPos, int width, int height, Point offset, int frameCount, int frameLength)
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

            anim.IsPlaying = true;
                
            return anim;
            
        } 
    }
}