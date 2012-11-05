using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CSET_Game
{
    class PlayerSprite: Sprites
    {


        public override Vector2 direction
        {
            get
            {
                Vector2 inputDirection = Vector2.Zero;

                // If player pressed arrow keys, move the sprite
                if (Keyboard.GetState(  ).IsKeyDown(Keys.Left))
                    inputDirection.X -= 1;
                if (Keyboard.GetState(  ).IsKeyDown(Keys.Right))
                    inputDirection.X += 1;
                if (Keyboard.GetState(  ).IsKeyDown(Keys.Up))
                    inputDirection.Y -= 1;
                if (Keyboard.GetState(  ).IsKeyDown(Keys.Down))
                    inputDirection.Y += 1;

                // If player pressed the gamepad thumbstick, move the sprite
                GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
                if(gamepadState.ThumbSticks.Left.X != 0)
                    inputDirection.X += gamepadState.ThumbSticks.Left.X;
                if(gamepadState.ThumbSticks.Left.Y != 0)
                    inputDirection.Y -= gamepadState.ThumbSticks.Left.Y;

                return inputDirection * speed;
            }
        }


        // Constructors
        public PlayerSprite(Texture2D spriteImage, Point currentFrame, Point sheetSize,
            Point frameSize, Vector2 speed, Vector2 position, int collisionOffset)
            : base(spriteImage, currentFrame, sheetSize, frameSize, speed, position,
            collisionOffset) 
        { 
        }

        public PlayerSprite(Texture2D spriteImage, Point currentFrame, Point sheetSize,
            Point frameSize, Vector2 speed, Vector2 position, int collisionOffset,
            int millisecondsPerFrame)
            : base(spriteImage, currentFrame, sheetSize, frameSize, speed, position,
            collisionOffset, millisecondsPerFrame)
        {
        }


        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            // Move sprite with respect to direction
            position += direction;

            // Don't let player sprite move off screen
            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
            if (position.X > clientBounds.Width - frameSize.X)
                position.X = clientBounds.Width - frameSize.X;
            if (position.Y > clientBounds.Height - frameSize.Y)
                position.Y = clientBounds.Height - frameSize.Y;
            
            base.Update(gameTime, clientBounds);
        }

        
    }
}
