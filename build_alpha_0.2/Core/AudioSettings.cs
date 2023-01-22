namespace build_alpha_0._2.Core
{
    public class AudioSettings
    {
        private static float musicVolume;
        public static float MusicVolume
        {
            get { return musicVolume; }
            set { musicVolume = value; }
        }

        private static float soundVolume;
        public static float SoundVolume
        {
            get { return soundVolume; }
            set { soundVolume = value; }
        }
    }
}
