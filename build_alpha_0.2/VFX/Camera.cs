using SFML.Graphics;
using SFML.System;
using System;

namespace build_alpha_0._2.VFX
{
    public class Camera
    {
        private View cameraView;

        private bool isShakingCamera;
        private float shakingAreaRange;
        private float shakingTime;

        private Random random;
        private Clock clock;

        public Camera()
        {
            cameraView = new View(new Vector2f(0, 0), new Vector2f(800, 600));
            isShakingCamera = false;
            shakingAreaRange = 0;
            shakingTime = 0;

            random = new Random();
            clock = new Clock();
        }
        public Camera(Vector2f center, Vector2f size)
        {
            cameraView = new View(center, size);
        }

        public View GetCameraView()
        {
            return cameraView;
        }

        public void SetViewport(Vector2f center, Vector2f size)
        {
            cameraView.Center = center;
            cameraView.Size = size;
        }

        public void SetZoom(float zoom)
        {
            cameraView.Zoom(zoom);
        }

        public void StartShake(float shakingAreaRange, float shakingTime)
        {
            clock.Restart();
            isShakingCamera = true;
            this.shakingAreaRange = shakingAreaRange;
            this.shakingTime = shakingTime;
        }
        public void CancelShake()
        {
            isShakingCamera = false;
            shakingAreaRange = 0;
            shakingTime = 0;
        }

        public void OnUpdate(Vector2f center)
        {
            cameraView.Center = center;

            if (isShakingCamera)
            {
                cameraView.Center = new Vector2f(
                    center.X + (random.Next((int)-shakingAreaRange / 2, (int)shakingAreaRange / 2) / (clock.ElapsedTime.AsSeconds() + 1)),
                    center.Y + (random.Next((int)-shakingAreaRange / 2, (int)shakingAreaRange / 2) / (clock.ElapsedTime.AsSeconds() + 1))
                    );

                if (clock.ElapsedTime.AsSeconds() >= shakingTime) isShakingCamera = false;
            }
        }
    }
}
