using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace build_alpha_0._2.VFX
{
    public enum AnimationDirection
    {
        Horizontal, Vertival
    }

    public class Animation
    {
        private enum AnimationType
        {
            None = 0,
            NumericFrames,
            TextureFrames
        }
        private AnimationType type;

        private List<IntRect> numericFrames;
        private List<Texture> textureFrames;

        private Sprite animatableSprite;
        private RectangleShape animatableShape;

        private int frameX, frameY;
        private float currentFrame, speed;

        private bool isPlaying;
        public bool IsPlaying
        {
            get { return isPlaying; }
        }

        public Animation()
        {
            type = AnimationType.None;
            numericFrames = new List<IntRect>();
            textureFrames = new List<Texture>();

            speed = 0;
            frameX = 0;
            frameY = 0;

            animatableSprite = null;
            animatableShape = null;

            currentFrame = 0;
            isPlaying = true;
        }

        public Animation(Sprite animatable, int x, int y, int width, int height, float speed, int count, AnimationDirection loadDirection)
        {
            type = AnimationType.NumericFrames;
            numericFrames = new List<IntRect>();

            this.speed = speed;
            frameX = x;
            frameY = y;

            animatableSprite = animatable;
            animatableShape = null;

            currentFrame = 0;
            isPlaying = true;

            if (loadDirection == AnimationDirection.Horizontal)
                for (int i = 0; i < count; i++)
                    numericFrames.Add(new IntRect(x + i * width, y, width, height));

            if (loadDirection == AnimationDirection.Vertival)
                for (int i = 0; i < count; i++)
                    numericFrames.Add(new IntRect(x, y + i * height, width, height));
        }

        public Animation(RectangleShape animatable, int x, int y, int width, int height, float speed, int count, AnimationDirection loadDirection)
        {
            type = AnimationType.NumericFrames;
            numericFrames = new List<IntRect>();

            this.speed = speed;
            frameX = x;
            frameY = y;

            animatableSprite = null;
            animatableShape = animatable;

            currentFrame = 0;
            isPlaying = true;

            if (loadDirection == AnimationDirection.Horizontal)
                for (int i = 0; i < count; i++)
                    numericFrames.Add(new IntRect(x + i * width, y, width, height));

            if (loadDirection == AnimationDirection.Vertival)
                for (int i = 0; i < count; i++)
                    numericFrames.Add(new IntRect(x, y + i * height, width, height));
        }
        
        public void SetAnimatable(Sprite animatable)
        {
            animatableSprite = animatable;
            animatableShape = null;
        }
        public void SetAnimatable(RectangleShape animatable)
        {
            animatableSprite = null;
            animatableShape = animatable;
        }
        public void SetSpeed(float speed)
        {
            this.speed = speed;
        }
        public void SetStartOfAnimationFrames(Vector2i startPosition)
        {
            frameX = startPosition.X;
            frameY = startPosition.Y;
        }

        public void AddFrame(IntRect frame)
        {
            if (type == AnimationType.None || type == AnimationType.NumericFrames)
            {
                type = AnimationType.NumericFrames;
                numericFrames.Add(frame);
            }
            else Console.WriteLine($"This animation has texture frames");
        }
        public void AddFrame(Texture frame)
        {
            if (type == AnimationType.None || type == AnimationType.TextureFrames)
            {
                type = AnimationType.TextureFrames;
                textureFrames.Add(frame);
            }
            else Console.WriteLine($"This animation has numeric frames");
        }

        public void OnUpdate(float deltaTime)
        {
            if (!isPlaying) 
                return;
            switch (type)
            {
                case AnimationType.NumericFrames:
                    if (numericFrames.Count == 0) return;

                    currentFrame += speed * deltaTime;
                    if (currentFrame >= numericFrames.Count)
                        currentFrame = 0;

                    if (animatableShape != null)
                        animatableShape.TextureRect = numericFrames[(int)currentFrame];
                    else if (animatableSprite != null)
                        animatableSprite.TextureRect = numericFrames[(int)currentFrame];
                    break;
                case AnimationType.TextureFrames:
                    if (textureFrames.Count == 0) return;

                    currentFrame += speed * deltaTime;
                    if (currentFrame >= textureFrames.Count)
                        currentFrame = 0;

                    if (animatableShape != null)
                        animatableShape.Texture = textureFrames[(int)currentFrame];
                    else if (animatableSprite != null)
                        animatableSprite.Texture = textureFrames[(int)currentFrame];
                    break;
            }
        }

        public void PlayAnimation()
        {
            isPlaying = true;
        }
        public void PauseAnimation()
        {
            isPlaying = false;
        }

        public int GetCurrentFrame()
        {
            return (int)currentFrame;
        }
        public int GetFramesCount()
        {
            if (type == AnimationType.None)
                return 0;
            else if (type == AnimationType.NumericFrames)
                return numericFrames.Count;
            else if (type == AnimationType.TextureFrames)
                return textureFrames.Count;
            return 0;
        }
        public int GetFrameX()
        {
            if (type == AnimationType.NumericFrames)
                return frameX;
            return 0;
        }
        public int GetFrameY()
        {
            if (type == AnimationType.NumericFrames)
                return frameY;
            return 0;
        }
    }
}
