﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Platformer
{
    class Sprite
    {
        
        public Vector2 offSet = Vector2.Zero;
        
        Texture2D texture;

        public Sprite()
        {
        }

        public void Load(ContentManager content, string asset)
        {
            texture = content.Load<Texture2D>(asset);
        }

        public void Update(float deltaTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch,Vector2 Position, SpriteEffects effects = SpriteEffects.None)
        {
            spriteBatch.Draw(texture, Position, null,  Color.White,  0, offSet, 1, effects, 0);
        }
    }
}
