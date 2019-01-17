using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using MonoGame.Extended.ViewportAdapters;


namespace Platformer
{
    class Sprite
    {
        public Vector2 position = Vector2.Zero;
        List<AnimatedTexture> animations = new List<AnimatedTexture>();
        List<Vector2> AnimationOffsets = new List<Vector2>();

        int currentAnimation = 0;

        SpriteEffects effects = SpriteEffects.None;
        

        public Sprite()
        {
        }

     
        public void Add(AnimatedTexture animation, int xOffset=0, int yOffset = 0)
        {
            animations.Add(animation);
            AnimationOffsets.Add(new Vector2(xOffset, yOffset));
        }

        public void Load(ContentManager content, string asset)
        {
            //texture = content.Load<Texture2D>(asset);
        }

        public void Update(float deltaTime)
        {
            animations[currentAnimation].UpdateFrame(deltaTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 Position, SpriteEffects effects = SpriteEffects.None)
        {
            //spriteBatch.Draw(texture, Position, null,  Color.White,  0, offSet, 1, effects, 0);
            animations[currentAnimation].DrawFrame(spriteBatch, Position + AnimationOffsets[currentAnimation]);
        }
        public void SetFlipped(bool state)
        {
            if (state == true)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                effects = SpriteEffects.None;
            }
                
        }
        
        public void Pause()
        {
            animations[currentAnimation].Pause();
        }

        public void Play()
        {
            animations[currentAnimation].Play();
        }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(new Point((int)position.X, (int)position.Y), animations[currentAnimation].FrameSize);
            }
        }
    }
}
