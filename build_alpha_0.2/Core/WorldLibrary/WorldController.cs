using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;

namespace build_alpha_0._2.Core.WorldLibrary
{
    using Inventory.Items;
    using Inventory;
    using VFX;

    public enum WorldLocations
    {
        Forest = 0, Mine, House
    }

    public class WorldController
    {
        public WorldLocations Location { get; set; }

        private List<Tile> forestTilesList;
        private List<Tree> forestTreesList;
        public List<Tree> ForestTrees
        {
            get { return forestTreesList; }
        }
        private List<Tile> mineTilesList;
        private List<Ore> mineOresList;
        public List<Ore> MineOres
        {
            get { return mineOresList; }
        }
        private List<Tile> houseTilesList;

        public WorldController()
        {
            Location = WorldLocations.Forest;
            forestTilesList = new List<Tile>();
            forestTreesList = new List<Tree>();
            mineTilesList = new List<Tile>();
            mineOresList = new List<Ore>();
            houseTilesList = new List<Tile>();
        }

        public void Reset()
        {
            forestTilesList.Clear();
            forestTreesList.Clear();
            mineTilesList.Clear();
            mineOresList.Clear();
            houseTilesList.Clear();
        }

        public void GenerateWorld()
        {
            Random random = new Random();

            // генерация локации "Лес"
            for (int h = 0; h < 30; h++)
            {
                for (int w = 0; w < 30; w++)
                {
                    Tile tile = new Tile();
                    tile.Texture = ResourcesManager.GetTexture("tile_sheet");
                    tile.Position = new Vector2f(w * 30, h * 18);
                    tile.Size = new Vector2f(30, 18);
                    int animationID = random.Next(0, 40);

                    switch (animationID)
                    {
                        case 0:
                            tile.Animation = new Animation(tile.Sprite, 0, 0, 30, 18, 0.03f, 3, AnimationDirection.Vertival);
                            break;
                        case 1:
                            tile.Animation = new Animation(tile.Sprite, 30, 0, 30, 18, 0.03f, 3, AnimationDirection.Vertival);
                            break;
                        case 2:
                            tile.Animation = new Animation(tile.Sprite, 60, 0, 30, 18, 0.03f, 3, AnimationDirection.Vertival);
                            break;
                        case 3:
                            tile.Animation = new Animation(tile.Sprite, 90, 0, 30, 18, 0.03f, 3, AnimationDirection.Vertival);
                            break;
                        case 4:
                            tile.Animation = new Animation(tile.Sprite, 120, 0, 30, 18, 0.03f, 3, AnimationDirection.Vertival);
                            break;
                        case 5:
                            tile.Animation = new Animation(tile.Sprite, 150, 0, 30, 18, 0.03f, 3, AnimationDirection.Vertival);
                            break;
                        case 6:
                            tile.Animation = new Animation(tile.Sprite, 180, 0, 30, 18, 0.03f, 3, AnimationDirection.Vertival);
                            break;
                        case 7:
                            tile.Animation = new Animation(tile.Sprite, 210, 0, 30, 18, 0.03f, 3, AnimationDirection.Vertival);
                            break;
                        case 8:
                            tile.Animation = new Animation(tile.Sprite, 240, 18, 30, 18, 0.03f, 2, AnimationDirection.Vertival);
                            break;
                        case 9:
                            tile.Animation = new Animation(tile.Sprite, 270, 0, 30, 18, 0.03f, 2, AnimationDirection.Vertival);
                            break;
                        case 10:
                            tile.Animation = new Animation(tile.Sprite, 0, 54, 30, 18, 0.03f, 2, AnimationDirection.Vertival);
                            break;
                        case 11:
                            tile.Animation = new Animation(tile.Sprite, 30, 54, 30, 18, 0.03f, 2, AnimationDirection.Vertival);
                            break;
                        case 12:
                            tile.Animation = new Animation(tile.Sprite, 60, 54, 30, 18, 0.03f, 2, AnimationDirection.Vertival);
                            break;
                        case 13:
                            tile.Animation = new Animation(tile.Sprite, 90, 54, 30, 18, 0.03f, 2, AnimationDirection.Vertival);
                            break;
                        case 14:
                            tile.Animation = new Animation(tile.Sprite, 120, 54, 30, 18, 0.03f, 2, AnimationDirection.Vertival);
                            break;
                        case 15:
                            tile.Animation = new Animation(tile.Sprite, 150, 54, 30, 18, 0.03f, 2, AnimationDirection.Vertival);
                            break;
                        case 16:
                            tile.Animation = new Animation(tile.Sprite, 180, 54, 30, 18, 0.03f, 2, AnimationDirection.Vertival);
                            break;
                    }
                    if (animationID >= 17 && animationID < 40)
                        tile.Animation = new Animation(tile.Sprite, 210, 54, 30, 18, 0.03f, 2, AnimationDirection.Vertival);

                    forestTilesList.Add(tile);
                }
                for (int w = 0; w < 30; w++)
                {
                    Tile tile = new Tile();
                    tile.Texture = ResourcesManager.GetTexture("tile_sheet");
                    tile.Position = new Vector2f(w * 30 + 15, h * 18 + 9);
                    tile.Size = new Vector2f(30, 18);
                    int animationID = random.Next(0, 40);

                    switch (animationID)
                    {
                        case 0:
                            tile.Animation = new Animation(tile.Sprite, 0, 0, 30, 18, 0, 3, AnimationDirection.Vertival);
                            break;
                        case 1:
                            tile.Animation = new Animation(tile.Sprite, 30, 0, 30, 18, 0, 3, AnimationDirection.Vertival);
                            break;
                        case 2:
                            tile.Animation = new Animation(tile.Sprite, 60, 0, 30, 18, 0, 3, AnimationDirection.Vertival);
                            break;
                        case 3:
                            tile.Animation = new Animation(tile.Sprite, 90, 0, 30, 18, 0, 3, AnimationDirection.Vertival);
                            break;
                        case 4:
                            tile.Animation = new Animation(tile.Sprite, 120, 0, 30, 18, 0, 3, AnimationDirection.Vertival);
                            break;
                        case 5:
                            tile.Animation = new Animation(tile.Sprite, 150, 0, 30, 18, 0, 3, AnimationDirection.Vertival);
                            break;
                        case 6:
                            tile.Animation = new Animation(tile.Sprite, 180, 0, 30, 18, 0, 3, AnimationDirection.Vertival);
                            break;
                        case 7:
                            tile.Animation = new Animation(tile.Sprite, 210, 0, 30, 18, 0, 3, AnimationDirection.Vertival);
                            break;
                        case 8:
                            tile.Animation = new Animation(tile.Sprite, 240, 18, 30, 18, 0, 2, AnimationDirection.Vertival);
                            break;
                        case 9:
                            tile.Animation = new Animation(tile.Sprite, 270, 0, 30, 18, 0, 2, AnimationDirection.Vertival);
                            break;
                        case 10:
                            tile.Animation = new Animation(tile.Sprite, 0, 54, 30, 18, 0, 2, AnimationDirection.Vertival);
                            break;
                        case 11:
                            tile.Animation = new Animation(tile.Sprite, 30, 54, 30, 18, 0, 2, AnimationDirection.Vertival);
                            break;
                        case 12:
                            tile.Animation = new Animation(tile.Sprite, 60, 54, 30, 18, 0, 2, AnimationDirection.Vertival);
                            break;
                        case 13:
                            tile.Animation = new Animation(tile.Sprite, 90, 54, 30, 18, 0, 2, AnimationDirection.Vertival);
                            break;
                        case 14:
                            tile.Animation = new Animation(tile.Sprite, 120, 54, 30, 18, 0, 2, AnimationDirection.Vertival);
                            break;
                        case 15:
                            tile.Animation = new Animation(tile.Sprite, 150, 54, 30, 18, 0, 2, AnimationDirection.Vertival);
                            break;
                        case 16:
                            tile.Animation = new Animation(tile.Sprite, 180, 54, 30, 18, 0, 2, AnimationDirection.Vertival);
                            break;
                    }
                    if (animationID >= 17 && animationID < 40)
                        tile.Animation = new Animation(tile.Sprite, 210, 54, 30, 18, 0, 2, AnimationDirection.Vertival);

                    forestTilesList.Add(tile);
                }
            }
            for (int t = 0; t < 15; t++)
            {
                Tree tree = new Tree();
                tree.Texture = ResourcesManager.GetTexture("tree_sheet");
                tree.Position = new Vector2f(random.Next(30, 30 * 30 - 60), random.Next(18, 30 * 18 - 60));

                int animationID = random.Next(0, 8);

                switch (animationID)
                {
                    case 0:
                        tree.Size = new Vector2f(32, 60);
                        tree.TextureRect = new IntRect(0, 0, 32, 60);
                        tree.Animation = new Animation(tree.Sprite, 0, 0, 32, 60, 0, 2, AnimationDirection.Vertival);
                        break;
                    case 1:
                        tree.Size = new Vector2f(32, 60);
                        tree.TextureRect = new IntRect(34, 0, 32, 60);
                        tree.Animation = new Animation(tree.Sprite, 34, 0, 32, 60, 0, 2, AnimationDirection.Vertival);
                        break;
                    case 2:
                        tree.Size = new Vector2f(32, 60);
                        tree.TextureRect = new IntRect(68, 0, 32, 60);
                        tree.Animation = new Animation(tree.Sprite, 68, 0, 32, 60, 0, 2, AnimationDirection.Vertival);
                        break;
                    case 3:
                        tree.Size = new Vector2f(29, 60);
                        tree.TextureRect = new IntRect(108, 0, 29, 60);
                        tree.Animation = new Animation(tree.Sprite, 108, 0, 29, 60, 0, 2, AnimationDirection.Vertival);
                        break;
                    case 4:
                        tree.Size = new Vector2f(40, 60);
                        tree.TextureRect = new IntRect(137, 0, 40, 60);
                        tree.Animation = new Animation(tree.Sprite, 137, 0, 40, 60, 0, 2, AnimationDirection.Vertival);
                        break;
                    case 5:
                        tree.Size = new Vector2f(54, 60);
                        tree.TextureRect = new IntRect(188, 0, 54, 60);
                        tree.Animation = new Animation(tree.Sprite, 188, 0, 54, 60, 0, 2, AnimationDirection.Vertival);
                        break;
                    case 6:
                        tree.Size = new Vector2f(30, 60);
                        tree.TextureRect = new IntRect(242, 0, 30, 60);
                        tree.Animation = new Animation(tree.Sprite, 0, 0, 30, 60, 0, 2, AnimationDirection.Vertival);
                        break;
                    case 7:
                        tree.Size = new Vector2f(22, 60);
                        tree.TextureRect = new IntRect(274, 0, 22, 60);
                        tree.Animation = new Animation(tree.Sprite, 274, 0, 22, 60, 0, 2, AnimationDirection.Vertival);
                        break;
                }

                forestTreesList.Add(tree);
            }

            // генерация локации "Пещера"
            for (int h = 0; h < 30; h++)
            {
                for (int w = 0; w < 30; w++)
                {
                    Tile tile = new Tile();
                    tile.Texture = ResourcesManager.GetTexture("tile_sheet");
                    tile.Position = new Vector2f(w * 30, h * 18);
                    tile.Size = new Vector2f(30, 18);
                    tile.Animation = new Animation(tile.Sprite, 270, 54, 30, 18, 0, 1, AnimationDirection.Vertival);

                    mineTilesList.Add(tile);
                }
                for (int w = 0; w < 30; w++)
                {
                    Tile tile = new Tile();
                    tile.Texture = ResourcesManager.GetTexture("tile_sheet");
                    tile.Position = new Vector2f(w * 30 + 15, h * 18 + 9);
                    tile.Size = new Vector2f(30, 18);
                    tile.Animation = new Animation(tile.Sprite, 270, 54, 30, 18, 0, 1, AnimationDirection.Vertival);

                    mineTilesList.Add(tile);
                }
            }
            for (int o = 0; o < 30; o++)
            {
                Ore ore = new Ore();

                ore.Texture = ResourcesManager.GetTexture("tile_sheet");
                ore.Position = new Vector2f(30 * random.Next(0, 30), 18 * random.Next(0, 30));
                ore.Size = new Vector2f(30, 18);
                int animationID = random.Next(0, 3);

                switch (animationID)
                {
                    case 0:
                        ore.Animation = new Animation(ore.Sprite, 270, 72, 30, 18, 0, 1, AnimationDirection.Vertival);
                        break;
                    case 1:
                        ore.Animation = new Animation(ore.Sprite, 0, 90, 30, 18, 0, 1, AnimationDirection.Vertival);
                        break;
                    case 2:
                        ore.Animation = new Animation(ore.Sprite, 30, 90, 30, 18, 0, 1, AnimationDirection.Vertival);
                        break;
                }

                mineOresList.Add(ore);
            }

            // генерация локации "Дом"
            for (int h = 0; h < 6; h++)
            {
                for (int w = 0; w < 6; w++)
                {
                    Tile tile = new Tile();
                    tile.Texture = ResourcesManager.GetTexture("tile_sheet");
                    tile.Position = new Vector2f(w * 30, h * 18);
                    tile.Size = new Vector2f(30, 18);
                    tile.Animation = new Animation(tile.Sprite, 270, 36, 30, 18, 0, 1, AnimationDirection.Vertival);

                    houseTilesList.Add(tile);
                }
                for (int w = 0; w < 6; w++)
                {
                    Tile tile = new Tile();
                    tile.Texture = ResourcesManager.GetTexture("tile_sheet");
                    tile.Position = new Vector2f(w * 30 + 15, h * 18 + 9);
                    tile.Size = new Vector2f(30, 18);
                    tile.Animation = new Animation(tile.Sprite, 270, 36, 30, 18, 0, 1, AnimationDirection.Vertival);

                    houseTilesList.Add(tile);
                }
            }
        }
        public void SaveWorld(string worldFilesPath)
        {
            Directory.CreateDirectory($"{worldFilesPath}");
            using (StreamWriter file = new StreamWriter($"{worldFilesPath}/world_forest.cfg"))
            {
                foreach (var tile in forestTilesList)
                    file.WriteLine($"{tile.Position.X} {tile.Position.Y} {tile.Animation.GetFrameX()} {tile.Animation.GetFrameY()} {tile.Animation.GetFramesCount()}");
                file.Close();
            }
            using (StreamWriter file = new StreamWriter($"{worldFilesPath}/world_forest_tree.cfg"))
            {
                foreach (var tree in forestTreesList)
                    file.WriteLine($"{tree.Position.X} {tree.Position.Y} {tree.Size.X} {tree.Size.Y} {tree.Animation.GetFrameX()} {tree.Animation.GetFrameY()} {tree.Animation.GetFramesCount()}");
                file.Close();
            }
            using (StreamWriter file = new StreamWriter($"{worldFilesPath}/world_mine.cfg"))
            {
                foreach (var tile in mineTilesList)
                    file.WriteLine($"{tile.Position.X} {tile.Position.Y} {tile.Animation.GetFrameX()} {tile.Animation.GetFrameY()} {tile.Animation.GetFramesCount()}");
                file.Close();
            }
            using (StreamWriter file = new StreamWriter($"{worldFilesPath}/world_mine_ore.cfg"))
            {
                foreach (var ore in mineOresList)
                    file.WriteLine($"{ore.Position.X} {ore.Position.Y} {ore.Animation.GetFrameX()} {ore.Animation.GetFrameY()} {ore.Animation.GetFramesCount()}");
                file.Close();
            }
            using (StreamWriter file = new StreamWriter($"{worldFilesPath}/world_house.cfg"))
            {
                foreach (var tile in houseTilesList)
                    file.WriteLine($"{tile.Position.X} {tile.Position.Y} {tile.Animation.GetFrameX()} {tile.Animation.GetFrameY()} {tile.Animation.GetFramesCount()}");
                file.Close();
            }
            using (StreamWriter file = new StreamWriter($"{worldFilesPath}/world_data.cfg"))
            {
                file.WriteLine($"world_location {(int)Location}");
                file.Close();
            }
        }
        public void LoadWorld(string worldFilesPath)
        {
            var worldData = new List<string>();
            // выгрузка данных о лесе
            using(StreamReader file = new StreamReader($"{worldFilesPath}/world_forest.cfg"))
            {
                while (!file.EndOfStream)
                    worldData.Add(file.ReadLine());
                file.Close();
            }
            foreach(var data in worldData)
            {
                string[] dataString = data.Split(' ');

                Tile tile = new Tile();
                tile.Texture = ResourcesManager.GetTexture("tile_sheet");
                tile.Position = new Vector2f(float.Parse(dataString[0]), float.Parse(dataString[1]));
                tile.Size = new Vector2f(30, 18);
                tile.Animation = new Animation(
                    tile.Sprite, int.Parse(dataString[2]), int.Parse(dataString[3]),
                    30, 18, 1.5f, int.Parse(dataString[4]), AnimationDirection.Vertival);

                forestTilesList.Add(tile);
            }
            // выгрузка данных о деревьях
            worldData.Clear();
            using (StreamReader file = new StreamReader($"{worldFilesPath}/world_forest_tree.cfg"))
            {
                while (!file.EndOfStream)
                    worldData.Add(file.ReadLine());
                file.Close();
            }
            foreach (var data in worldData)
            {
                string[] dataString = data.Split(' ');

                Tree tree = new Tree();
                tree.Texture = ResourcesManager.GetTexture("tree_sheet");
                tree.Position = new Vector2f(float.Parse(dataString[0]), float.Parse(dataString[1]));
                tree.Size = new Vector2f(float.Parse(dataString[2]), float.Parse(dataString[3]));
                tree.Animation = new Animation(
                    tree.Sprite, int.Parse(dataString[4]), int.Parse(dataString[5]),
                    int.Parse(dataString[2]), int.Parse(dataString[3]), 1.5f, int.Parse(dataString[6]), AnimationDirection.Vertival);
                tree.SetMaximumValues(45, 0, 0);

                forestTreesList.Add(tree);
            }
            // выгрузка данных о пещере
            worldData.Clear();
            using (StreamReader file = new StreamReader($"{worldFilesPath}/world_mine.cfg"))
            {
                while (!file.EndOfStream)
                    worldData.Add(file.ReadLine());
                file.Close();
            }
            foreach (var data in worldData)
            {
                string[] dataString = data.Split(' ');

                Tile tile = new Tile();
                tile.Texture = ResourcesManager.GetTexture("tile_sheet");
                tile.Position = new Vector2f(float.Parse(dataString[0]), float.Parse(dataString[1]));
                tile.Size = new Vector2f(30, 18);
                tile.Animation = new Animation(
                    tile.Sprite, int.Parse(dataString[2]), int.Parse(dataString[3]),
                    30, 18, 1.5f, int.Parse(dataString[4]), AnimationDirection.Vertival);

                mineTilesList.Add(tile);
            }
            // выгрузка данных о руде
            worldData.Clear();
            using (StreamReader file = new StreamReader($"{worldFilesPath}/world_mine_ore.cfg"))
            {
                while (!file.EndOfStream)
                    worldData.Add(file.ReadLine());
                file.Close();
            }
            foreach (var data in worldData)
            {
                string[] dataString = data.Split(' ');

                Ore ore = new Ore();
                ore.Texture = ResourcesManager.GetTexture("tile_sheet");
                ore.Position = new Vector2f(float.Parse(dataString[0]), float.Parse(dataString[1]));
                ore.Size = new Vector2f(30, 18);
                ore.Animation = new Animation(
                    ore.Sprite, int.Parse(dataString[2]), int.Parse(dataString[3]),
                    30, 18, 0, int.Parse(dataString[4]), AnimationDirection.Vertival);
                ore.SetMaximumValues(45, 0, 0);

                mineOresList.Add(ore);
            }
            // выгрузка данных о доме
            worldData.Clear();
            using (StreamReader file = new StreamReader($"{worldFilesPath}/world_house.cfg"))
            {
                while (!file.EndOfStream)
                    worldData.Add(file.ReadLine());
                file.Close();
            }
            foreach (var data in worldData)
            {
                string[] dataString = data.Split(' ');

                Tile tile = new Tile();
                tile.Texture = ResourcesManager.GetTexture("tile_sheet");
                tile.Position = new Vector2f(float.Parse(dataString[0]), float.Parse(dataString[1]));
                tile.Size = new Vector2f(30, 18);
                tile.Animation = new Animation(
                    tile.Sprite, int.Parse(dataString[2]), int.Parse(dataString[3]),
                    30, 18, 1.5f, int.Parse(dataString[4]), AnimationDirection.Vertival);

                houseTilesList.Add(tile);
            }

            Location = (WorldLocations)int.Parse(DataReader.LoadData($"{worldFilesPath}/world_data.cfg", "world_location"));
        }

        public void OnUpdate(float deltaTime, ItemsWorldContainer itemsWorld)
        {
            switch (Location)
            {
                case WorldLocations.Forest:
                    foreach (var tile in forestTilesList)
                        tile.OnUpdate(deltaTime);
                    for (int i = 0; i < forestTreesList.Count; i++)
                    {
                        if (forestTreesList[i].IsLife)
                            forestTreesList[i].OnUpdate(deltaTime);
                        if (!forestTreesList[i].IsLife)
                        {
                            if (itemsWorld != null)
                            {
                                itemsWorld.SpawnItem(new LogItem(), new FloatRect(forestTreesList[i].Position.X, forestTreesList[i].Position.Y, 30, 30), 3);
                                itemsWorld.SpawnItem(new StickItem(), new FloatRect(forestTreesList[i].Position.X, forestTreesList[i].Position.Y, 30, 30), 2);
                            }
                            forestTreesList.RemoveAt(i);
                            i--;
                        }
                    }
                    break;
                case WorldLocations.Mine:
                    foreach (var tile in mineTilesList)
                        tile.OnUpdate(deltaTime);
                    for (int i = 0; i < mineOresList.Count; i++)
                    {
                        if (mineOresList[i].IsLife)
                            mineOresList[i].OnUpdate(deltaTime);
                        if (!mineOresList[i].IsLife)
                        {
                            if (itemsWorld != null)
                            {
                                if (mineOresList[i].TextureRect.Left == 270 && mineOresList[i].TextureRect.Top == 72)
                                    itemsWorld.SpawnItem(new CoalItem(), new FloatRect(mineOresList[i].Position.X, mineOresList[i].Position.Y, 30, 30), 3);
                                else if (mineOresList[i].TextureRect.Left == 0 && mineOresList[i].TextureRect.Top == 90)
                                    itemsWorld.SpawnItem(new IronItem(), new FloatRect(mineOresList[i].Position.X, mineOresList[i].Position.Y, 30, 30), 3);
                                else if (mineOresList[i].TextureRect.Left == 30 && mineOresList[i].TextureRect.Top == 90)
                                    itemsWorld.SpawnItem(new CrystalItem(), new FloatRect(mineOresList[i].Position.X, mineOresList[i].Position.Y, 30, 30), 3);
                            }
                                mineOresList.RemoveAt(i);
                            i--;
                        }
                    }
                    break;
                case WorldLocations.House:
                    foreach (var tile in houseTilesList)
                        tile.OnUpdate(deltaTime);
                    break;
            }
        }
        public void OnRender(RenderTarget target, Light light)
        {
            switch (Location)
            {
                case WorldLocations.Forest:
                    foreach (var tile in forestTilesList)
                        tile.OnRender(target, light);
                    foreach (var tree in forestTreesList)
                        tree.OnRender(target, light);
                    break;
                case WorldLocations.Mine:
                    foreach (var tile in mineTilesList)
                        tile.OnRender(target, light);
                    foreach (var ore in mineOresList)
                        ore.OnRender(target, light);
                    break;
                case WorldLocations.House:
                    foreach (var tile in houseTilesList)
                        tile.OnRender(target, light);
                    break;
            }
        }
    }
}
