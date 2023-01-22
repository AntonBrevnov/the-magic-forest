using SFML.System;
using SFML.Window;

namespace build_alpha_0._2.Core
{
    public class GraphicsSettings
    {
        private static Vector2u windowSize = new Vector2u(0, 0);
        public static Vector2u WindowSize 
        {
            get { return windowSize; }
            set { windowSize = value; }
        }

        private static Styles windowStyle = Styles.Default;
        public static Styles WindowStyle
        {
            get { return windowStyle; }
            set { windowStyle = value; }
        }

        private static bool isVertSync = false;
        public static bool IsVertSync
        {
            get { return isVertSync; }
            set { isVertSync = value; }
        }

        private static uint frameRate = 59;
        public static uint FrameRate
        {
            get { return frameRate; }
            set { frameRate = value; }
        }

        private static uint antialiasingLevel = 5;
        public static uint AntialiasingLevel
        {
            get { return antialiasingLevel; }
            set { antialiasingLevel = value; }
        }
    }
}
