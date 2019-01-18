using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    class Player
    {
        Sprite sprite = new Sprite();
        SoundEffect jumpSound;
        SoundEffectInstance jumpSoundInstance;
        Game1 game = null;
        bool isFalling = true;
        bool isJumping = false;
        bool autoJump = true;
        Vector2 velocity = Vector2.Zero;
        public Vector2 position = Vector2.Zero;

        public Vector2 Velocity
        {
            get { return velocity; }
        }

        public Rectangle Bounds
        {
            get { return sprite.Bounds; }
        }

        public bool IsJumping
        {
            get { return IsJumping; }
        }

        public void JumpOnCollision()
        {
            autoJump = true;
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public Player(Game1 game)
        {
            this.game = game;
            isFalling = true;
            isJumping = false;
            velocity = Vector2.Zero;
            position = Vector2.Zero;
        }


        public void Load(ContentManager content)
        {
            //sprite.Load(content, "player_stand");
            AnimatedTexture animation = new AnimatedTexture(Vector2.Zero, 0, 1, 1);
            animation.Load(content, "player_walk1", 12, 20);

            jumpSound = content.Load<SoundEffect>("SFX/Jump");
            jumpSoundInstance = jumpSound.CreateInstance();
            jumpSoundInstance.Volume = 0.5f;

            sprite.Add(animation, 0, -5);
        }

        public void Update(float deltaTime) 
        {
            UpdateInput(deltaTime);
            sprite.Update(deltaTime);

           
        }

        public void Draw(SpriteBatch spriteBatch)
        {
                sprite.Draw(spriteBatch, Position);
           
        }

        private void UpdateInput(float deltaTime)
        {
            bool wasMovingLeft = velocity.X < 0;
            bool wasMovingRight = velocity.X > 0;
            bool falling = isFalling;

            Vector2 acceleration = new Vector2(0, Game1.gravity);

           

            if(Keyboard.GetState().IsKeyDown(Keys.Left) == true) {
                acceleration.X -= Game1.acceleration;
                sprite.SetFlipped(true);
                sprite.Play();
            }
            else if (wasMovingLeft == true) {
                acceleration.X += Game1.friction;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right) == true)
            {
                acceleration.X += Game1.acceleration;
            }
            else if (wasMovingRight == true) {
                acceleration.X -= Game1.friction;
                sprite.SetFlipped(false);
                sprite.Play();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) == true && this.isJumping == false && falling == false || autoJump == true)
            {
                autoJump = false;
                acceleration.Y -= Game1.jumpImpulse;
                this.isJumping = true;
                jumpSoundInstance.Play();
            }

            velocity += acceleration * deltaTime;

            velocity.X = MathHelper.Clamp(velocity.X, -Game1.maxVelocity.X, Game1.maxVelocity.X);
            velocity.Y = MathHelper.Clamp(velocity.Y, -Game1.maxVelocity.Y, Game1.maxVelocity.Y);

            position += velocity * deltaTime;

            if ((wasMovingLeft && (velocity.X > 0)) || (wasMovingRight && (velocity.X < 0)))
            {
                velocity.X = 0;
                sprite.Pause();
            }

            int tx = game.PixelToTile(position.X);
            int ty = game.PixelToTile(position.Y);

            bool nx = (position.X) % Game1.tile != 0;
            bool ny = (position.Y) % Game1.tile != 0;
            bool cell = game.CellAtTileCoord(tx, ty) != 0;
            bool cellright = game.CellAtTileCoord(tx + 1, ty) != 0;
            bool celldown = game.CellAtTileCoord(tx, ty + 2) != 0;
            bool celldiag = game.CellAtTileCoord(tx + 1, ty + 1) != 0;

            if (this.velocity.Y > 0)
            {
                if ((celldown && !cell) || (celldiag && !cellright && nx))
                {
                    position.Y = game.TileTopixel(ty);
                    this.velocity.Y = 0;
                    this.isFalling = false;
                    this.isJumping = false;
                    ny = false;
                }
            }
            else if (this.velocity.Y < 0)
            {
                if ((cell && !celldown) || (cellright && !celldiag && nx))
                {
                    position.Y = game.TileTopixel(ty + 1);
                    this.velocity.Y = 0;
                    cell = celldown;
                    cellright = celldiag;
                    ny = false;
                }
            }
            if (this.velocity.X > 0)
            {
                if ((cellright && !cell) || (celldiag && !celldown && ny))
                {
                    position.X = game.TileTopixel(tx);
                    this.velocity.X = 0;
                }
          }
            else if (this.velocity.X < 0)
            {
                if ((cell && !cellright) || (celldown && !celldiag && ny))
                {
                    position.X = game.TileTopixel(tx + 1);
                    this.velocity.X = 0;
                }
            }
            this.isFalling = !(celldown || (nx && celldiag));
        }

        

    }
}
