using SFML.Graphics;
using SFML.Audio;
using System.Collections.Generic;
using System;
using SFML;

namespace build_alpha_0._2.Core
{
    public class ResourcesManager
    {
        private static Dictionary<string, Font> fontsList =
            new Dictionary<string, Font>();
        private static Dictionary<string, Texture> texturesList =
            new Dictionary<string, Texture>();
        private static Dictionary<string, Sound> soundsList =
            new Dictionary<string, Sound>();
        private static Dictionary<string, Music> musicList =
            new Dictionary<string, Music>();

        public static bool LoadFont(string path, string resourceName)
        {
            try 
            {
                Font font = new Font(path);
                fontsList.Add(resourceName, font);
                return true;
            }
            catch (LoadingFailedException)
            {
                return false;
            }
        }
        public static void RemoveFont(string resourceName)
        {
            try
            {
                fontsList.Remove(resourceName);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine($"Font {resourceName} is not found");
            }
        }
        public static Font GetFont(string resourceName)
        {
            try
            {
                return fontsList[resourceName];
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public static bool LoadTexture(string path, string resourceName)
        {
            try 
            {
                Texture texture = new Texture(path);
                texturesList.Add(resourceName, texture);
                return true;
            }
            catch (LoadingFailedException)
            {
                Texture texture = new Texture("resources/textures/debug_texture.png");
                texturesList.Add(resourceName, texture);
                return false;
            }            
        }
        public static void RemoveTexture(string resourceName)
        {
            try
            {
                texturesList.Remove(resourceName);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine($"Texture {resourceName} is not found");
            }
        }
        public static Texture GetTexture(string resourceName)
        {
            try
            {
                return texturesList[resourceName];
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public static bool LoadSound(string path, string resourceName)
        {
            try 
            {
                SoundBuffer buffer = new SoundBuffer(path);
                Sound sound = new Sound(buffer);
                sound.Volume = AudioSettings.SoundVolume;
                soundsList.Add(resourceName, sound);
                return true;
            }
            catch (LoadingFailedException)
            {
                return false;
            }
        }
        public static void RemoveSound(string resourceName)
        {
            try
            {
                soundsList.Remove(resourceName);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine($"Sound {resourceName} is not found");
            }
        }
        public static Sound GetSound(string resourceName)
        {
            try
            {
                return soundsList[resourceName];
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public static bool LoadMusic(string path, string resourceName)
        {
            try 
            {
                Music music = new Music(path);
                music.Volume = AudioSettings.MusicVolume;
                musicList.Add(resourceName, music);
                return true;
            }
            catch (LoadingFailedException)
            {
                return false;
            }
        }
        public static void RemoveMusic(string resourceName)
        {
            try
            {
                musicList.Remove(resourceName);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine($"Music {resourceName} is not found");
            }
        }
        public static Music GetMusic(string resourceName)
        {
            try
            {
                return musicList[resourceName];
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }
    }
}
