using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    class Player
    {
        public Vector2 Position = Vector2.Zero;
        Sprite sprite = new Sprite();
        SpriteEffects effects = SpriteEffects.None;
        public Player()
        {
        }


        public void Load(ContentManager content)
        {
            sprite.Load(content, "player_stand");
        }

        public void Update(float deltaTime)
        {
            sprite.Update(deltaTime);
            KeyboardState State = Keyboard.GetState();
            int speed = 50;
            if(State.IsKeyDown(Keys.Up) == true)
            {
                Position.Y -= speed * deltaTime;
                effects = SpriteEffects.None;
            }

            if (State.IsKeyDown(Keys.Down) == true)
            {
                Position.Y += speed * deltaTime;
                effects = SpriteEffects.FlipVertically;
            }

            if (State.IsKeyDown(Keys.Left) == true)
            {
                Position.X -= speed * deltaTime;
                effects = SpriteEffects.FlipHorizontally;
            }

            if (State.IsKeyDown(Keys.Right) == true)
            {
                Position.X += speed * deltaTime;
                effects = SpriteEffects.None;
                
            }

           
        }

        public void Draw(SpriteBatch spriteBatch)
        {
                sprite.Draw(spriteBatch, Position, effects);
           
        }
    }
}
