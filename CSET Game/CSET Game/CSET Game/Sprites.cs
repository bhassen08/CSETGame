using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CSET_Game
{
    abstract class Sprites
    {
        // Sprite Draw variables
        Texture2D spriteImage;
        Point currentFrame;
        Point sheetSize;
        protected Point frameSize;

        // Sprite Movement variables
        protected Vector2 speed;
        protected Vector2 position;

        // Sprite Framerate variables
        const int defaultMillisPerFrame = 16;
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame;

        // Collision Detection variables
        int collisionOffset;

        // Get direction from subclass
        public abstract Vector2 direction
        {
            get;
        }

        // Get collision
        public Rectangle rectangleCollision
        {
            get
            {
                return new Rectangle(
                    (int)position.X + collisionOffset,
                    (int)position.Y + collisionOffset,
                    frameSize.X - (collisionOffset * 2),
                    frameSize.Y - (collisionOffset * 2));
            }
        }

        // Constructors
        public Sprites(Texture2D spriteImage, Point currentFrame, Point sheetSize,
            Point frameSize, Vector2 speed, Vector2 position, int collisionOffset)
            : this(spriteImage, currentFrame, sheetSize, frameSize, speed, position,
            collisionOffset, defaultMillisPerFrame) 
        { 
        }

        public Sprites(Texture2D spriteImage, Point currentFrame, Point sheetSize,
            Point frameSize, Vector2 speed, Vector2 position, int collisionOffset,
            int millisecondsPerFrame)
        {
            this.spriteImage = spriteImage;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.frameSize = frameSize;
            this.speed = speed;
            this.position = position;
            this.collisionOffset = collisionOffset;
            this.millisecondsPerFrame = millisecondsPerFrame;
        }

        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
            // Update frames with regards to framerate
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                // Increment frame
                timeSinceLastFrame = 0;
                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    ++currentFrame.Y;
                    if (currentFrame.Y >= sheetSize.Y)
                        currentFrame.Y = 0;
                }

            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw Sprite
            spriteBatch.Draw(spriteImage, position,
                new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X, frameSize.Y),
                    Color.White, 0, Vector2.Zero, 1f,
                    SpriteEffects.None, 0);
        }
        
    }
}
