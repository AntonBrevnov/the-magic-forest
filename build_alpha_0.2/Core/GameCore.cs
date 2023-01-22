using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

using build_alpha_0._2.VFX;
using build_alpha_0._2.ECS;
using build_alpha_0._2.ECS.Scenes;

namespace build_alpha_0._2.Core
{
    public class GameCore
    {
        private RenderWindow window;

        private Clock frameClock;
        private float deltaTime;

        private SceneManager sceneManager;

        public GameCore()
        {
            // выгрузка данных о графике
            GraphicsSettings.WindowSize = new Vector2u(
                uint.Parse(DataReader.LoadData("data/graphics_settings.cfg", "window_width")),
                uint.Parse(DataReader.LoadData("data/graphics_settings.cfg", "window_height")));
            GraphicsSettings.WindowStyle = (Styles)int.Parse(DataReader.LoadData("data/graphics_settings.cfg", "window_style"));
            GraphicsSettings.IsVertSync = bool.Parse(DataReader.LoadData("data/graphics_settings.cfg", "is_vert_sync"));
            GraphicsSettings.FrameRate = uint.Parse(DataReader.LoadData("data/graphics_settings.cfg", "framerate"));
            GraphicsSettings.AntialiasingLevel = uint.Parse(DataReader.LoadData("data/graphics_settings.cfg", "antialiasing"));

            // настройка рендеринга
            ContextSettings settings = new ContextSettings();
            settings.AntialiasingLevel = GraphicsSettings.AntialiasingLevel;

            // выгрузка данных о звуке
            AudioSettings.MusicVolume = float.Parse(DataReader.LoadData("data/audio_settings.cfg", "music_volume"));
            AudioSettings.SoundVolume = float.Parse(DataReader.LoadData("data/audio_settings.cfg", "sound_volume"));

            // инициализация окна и установка настроек
            window = new RenderWindow(
                new VideoMode(GraphicsSettings.WindowSize.X, GraphicsSettings.WindowSize.Y),
                "The Magic Forest", GraphicsSettings.WindowStyle, settings);
            // ограничение кадров
            window.SetFramerateLimit(GraphicsSettings.FrameRate);
            // вертикальная синхронизация
            window.SetVerticalSyncEnabled(GraphicsSettings.IsVertSync);
            // установка невидимости курсора
            window.SetMouseCursorVisible(false);

            // установка событий приложения
            window.KeyPressed += keyPressedEvent;
            window.KeyPressed += keyReleasedEvent;
            window.MouseButtonPressed += mouseButtonPressedEvent;
            window.MouseButtonReleased += mouseButtonReleasedEvent;
            window.MouseMoved += mouseMovedEvent;
            window.MouseWheelScrolled += mouseWheelScrolledEvent;
            window.Closed += windowClose;

            // инициализация менеджера сцен
            sceneManager = new SceneManager();

            // инициализация таймера для расчёта дельты времени
            frameClock = new Clock();
            deltaTime = 0;
        }

        private void OnInitialize()
        {
            // инициализация данных

            // загрузка заднего фона для меню
            for (int i = 0; i < 34; i++)
                ResourcesManager.LoadTexture($"resources/textures/menu_background/{i + 1}.png", $"bg_texture_{i + 1}");
 
            // загрузка шрифта для граф. интерфейса
            ResourcesManager.LoadFont(DataReader.LoadData("data/locale_settings.cfg", "ui_text_font"), "ui_font");
            ResourcesManager.LoadFont("resources/fonts/samson.ttf", "names_font");

            // загрузка данных локализации
            Localization.LocaleType = DataReader.LoadData("data/locale_settings.cfg", "game_locale");
            // загрузка данных русского языка
            if (Localization.LocaleType == "rus")
            {
                Localization.AddParametr("menu_name_block_header", DataReader.LoadData("data/locale_main_menu.cfg", "ru_menu_name_block_header"));
                Localization.AddParametr("menu_name_block_save_button", DataReader.LoadData("data/locale_main_menu.cfg", "ru_menu_name_block_save_button"));
                Localization.AddParametr("menu_singleplayer_button", DataReader.LoadData("data/locale_main_menu.cfg", "ru_menu_singleplayer_button"));
                Localization.AddParametr("menu_multiplayer_button", DataReader.LoadData("data/locale_main_menu.cfg", "ru_menu_multiplayer_button"));
                Localization.AddParametr("menu_settings_button", DataReader.LoadData("data/locale_main_menu.cfg", "ru_menu_settings_button"));
                Localization.AddParametr("menu_exit_button", DataReader.LoadData("data/locale_main_menu.cfg", "ru_menu_exit_button"));
                
                Localization.AddParametr("graphics_block_header", DataReader.LoadData("data/locale_settings_menu.cfg", "ru_graphics_block_header"));
                Localization.AddParametr("first_resolution_button", DataReader.LoadData("data/locale_settings_menu.cfg", "ru_first_resolution_button"));
                Localization.AddParametr("second_resolution_button", DataReader.LoadData("data/locale_settings_menu.cfg", "ru_second_resolution_button"));
                Localization.AddParametr("third_resolution_button", DataReader.LoadData("data/locale_settings_menu.cfg", "ru_third_resolution_button"));
                Localization.AddParametr("fouth_resolution_button", DataReader.LoadData("data/locale_settings_menu.cfg", "ru_fouth_resolution_button"));
                Localization.AddParametr("is_fullscreen_button", DataReader.LoadData("data/locale_settings_menu.cfg", "ru_is_fullscreen_button"));
                Localization.AddParametr("is_windowed_button", DataReader.LoadData("data/locale_settings_menu.cfg", "ru_is_windowed_button"));
                Localization.AddParametr("fps_header", DataReader.LoadData("data/locale_settings_menu.cfg", "ru_fps_header"));
                
                Localization.AddParametr("audio_block_header", DataReader.LoadData("data/locale_settings_menu.cfg", "ru_audio_block_header"));
                Localization.AddParametr("music_header", DataReader.LoadData("data/locale_settings_menu.cfg", "ru_music_header"));
                Localization.AddParametr("sound_header", DataReader.LoadData("data/locale_settings_menu.cfg", "ru_sound_header"));
                
                Localization.AddParametr("game_block_header", DataReader.LoadData("data/locale_settings_menu.cfg", "ru_game_block_header"));
                Localization.AddParametr("game_locale_header", DataReader.LoadData("data/locale_settings_menu.cfg", "ru_game_locale_header"));

                Localization.AddParametr("accept_button", DataReader.LoadData("data/locale_settings_menu.cfg", "ru_accept_button"));

                Localization.AddParametr("choose_map_header", DataReader.LoadData("data/locale_choose_map_menu.cfg", "ru_choose_map_header"));
                Localization.AddParametr("create_map_header", DataReader.LoadData("data/locale_choose_map_menu.cfg", "ru_create_map_header"));
                Localization.AddParametr("name_world_header", DataReader.LoadData("data/locale_choose_map_menu.cfg", "ru_name_world_header"));
                Localization.AddParametr("create_button", DataReader.LoadData("data/locale_choose_map_menu.cfg", "ru_create_button"));

                Localization.AddParametr("hp_text", DataReader.LoadData("data/locale_game_process.cfg", "ru_health_text"));
                Localization.AddParametr("hg_text", DataReader.LoadData("data/locale_game_process.cfg", "ru_hunger_text"));
                Localization.AddParametr("dp_text", DataReader.LoadData("data/locale_game_process.cfg", "ru_drink_text"));
                Localization.AddParametr("mn_text", DataReader.LoadData("data/locale_game_process.cfg", "ru_mana_text"));
                Localization.AddParametr("dm_text", DataReader.LoadData("data/locale_game_process.cfg", "ru_damage_text"));
                Localization.AddParametr("udm_text", DataReader.LoadData("data/locale_game_process.cfg", "ru_ult_damage_text"));
            }
            // загрузка данных английского языка
            if (Localization.LocaleType == "eng")
            {
                Localization.AddParametr("menu_name_block_header", DataReader.LoadData("data/locale_main_menu.cfg", "en_menu_name_block_header"));
                Localization.AddParametr("menu_name_block_save_button", DataReader.LoadData("data/locale_main_menu.cfg", "en_menu_name_block_save_button"));
                Localization.AddParametr("menu_singleplayer_button", DataReader.LoadData("data/locale_main_menu.cfg", "en_menu_singleplayer_button"));
                Localization.AddParametr("menu_multiplayer_button", DataReader.LoadData("data/locale_main_menu.cfg", "en_menu_multiplayer_button"));
                Localization.AddParametr("menu_settings_button", DataReader.LoadData("data/locale_main_menu.cfg", "en_menu_settings_button"));
                Localization.AddParametr("menu_exit_button", DataReader.LoadData("data/locale_main_menu.cfg", "en_menu_exit_button"));

                Localization.AddParametr("graphics_block_header", DataReader.LoadData("data/locale_settings_menu.cfg", "en_graphics_block_header"));
                Localization.AddParametr("first_resolution_button", DataReader.LoadData("data/locale_settings_menu.cfg", "en_first_resolution_button"));
                Localization.AddParametr("second_resolution_button", DataReader.LoadData("data/locale_settings_menu.cfg", "en_second_resolution_button"));
                Localization.AddParametr("third_resolution_button", DataReader.LoadData("data/locale_settings_menu.cfg", "en_third_resolution_button"));
                Localization.AddParametr("fouth_resolution_button", DataReader.LoadData("data/locale_settings_menu.cfg", "en_fouth_resolution_button"));
                Localization.AddParametr("is_fullscreen_button", DataReader.LoadData("data/locale_settings_menu.cfg", "en_is_fullscreen_button"));
                Localization.AddParametr("is_windowed_button", DataReader.LoadData("data/locale_settings_menu.cfg", "en_is_windowed_button"));
                Localization.AddParametr("fps_header", DataReader.LoadData("data/locale_settings_menu.cfg", "en_fps_header"));

                Localization.AddParametr("audio_block_header", DataReader.LoadData("data/locale_settings_menu.cfg", "en_audio_block_header"));
                Localization.AddParametr("music_header", DataReader.LoadData("data/locale_settings_menu.cfg", "en_music_header"));
                Localization.AddParametr("sound_header", DataReader.LoadData("data/locale_settings_menu.cfg", "en_sound_header"));

                Localization.AddParametr("game_block_header", DataReader.LoadData("data/locale_settings_menu.cfg", "en_game_block_header"));
                Localization.AddParametr("game_locale_header", DataReader.LoadData("data/locale_settings_menu.cfg", "en_game_locale_header"));

                Localization.AddParametr("accept_button", DataReader.LoadData("data/locale_settings_menu.cfg", "en_accept_button"));

                Localization.AddParametr("choose_map_header", DataReader.LoadData("data/locale_choose_map_menu.cfg", "en_choose_map_header"));
                Localization.AddParametr("create_map_header", DataReader.LoadData("data/locale_choose_map_menu.cfg", "en_create_map_header"));
                Localization.AddParametr("name_world_header", DataReader.LoadData("data/locale_choose_map_menu.cfg", "en_name_world_header"));
                Localization.AddParametr("create_button", DataReader.LoadData("data/locale_choose_map_menu.cfg", "en_create_button"));

                Localization.AddParametr("hp_text", DataReader.LoadData("data/locale_game_process.cfg", "en_health_text"));
                Localization.AddParametr("hg_text", DataReader.LoadData("data/locale_game_process.cfg", "en_hunger_text"));
                Localization.AddParametr("dp_text", DataReader.LoadData("data/locale_game_process.cfg", "en_drink_text"));
                Localization.AddParametr("mn_text", DataReader.LoadData("data/locale_game_process.cfg", "en_mana_text"));
                Localization.AddParametr("dm_text", DataReader.LoadData("data/locale_game_process.cfg", "en_damage_text"));
                Localization.AddParametr("udm_text", DataReader.LoadData("data/locale_game_process.cfg", "en_ult_damage_text"));
            }

            // выгрзка данных пользователя
            GlobalData.AddTextVariable("player_name", DataReader.LoadData("data/player_settings.cfg", "player_name"));
            GlobalData.AddNumericVariable("player_level", double.Parse(DataReader.LoadData("data/player_settings.cfg", "player_level")));
            GlobalData.AddNumericVariable("player_max_hp", double.Parse(DataReader.LoadData("data/player_settings.cfg", "player_max_hp")));
            GlobalData.AddNumericVariable("player_max_hg", double.Parse(DataReader.LoadData("data/player_settings.cfg", "player_max_hg")));
            GlobalData.AddNumericVariable("player_max_dp", double.Parse(DataReader.LoadData("data/player_settings.cfg", "player_max_dp")));
            GlobalData.AddNumericVariable("player_max_mn", double.Parse(DataReader.LoadData("data/player_settings.cfg", "player_max_mn")));
            GlobalData.AddNumericVariable("player_max_dm", double.Parse(DataReader.LoadData("data/player_settings.cfg", "player_max_dm")));
            GlobalData.AddNumericVariable("player_max_udm", double.Parse(DataReader.LoadData("data/player_settings.cfg", "player_max_udm")));

            // загрузка контента игровых миров
            ResourcesManager.LoadTexture("resources/textures/world/tilesheet.png", "tile_sheet");
            ResourcesManager.LoadTexture("resources/textures/world/treesheet.png", "tree_sheet");
            ResourcesManager.LoadTexture("resources/textures/world/treehouse.png", "tree_house");
            ResourcesManager.LoadTexture("resources/textures/world/bed.png", "bed");
            ResourcesManager.LoadTexture("resources/textures/world/mineway.png", "mine_way");
            ResourcesManager.LoadTexture("resources/textures/world/stairs.png", "stairs");
            ResourcesManager.LoadTexture("resources/textures/world/table.png", "table");
            // загрузка контента для эффектов
            ResourcesManager.LoadTexture("resources/textures/effects/particle_1.png", "particle_1");
            ResourcesManager.LoadTexture("resources/textures/effects/particle_2.png", "particle_2");
            ResourcesManager.LoadTexture("resources/textures/effects/particle_3.png", "particle_3");
            ResourcesManager.LoadTexture("resources/textures/effects/particle_4.png", "particle_4");
            ResourcesManager.LoadTexture("resources/textures/effects/particle_5.png", "particle_5");
            ResourcesManager.LoadTexture("resources/textures/effects/particle_6.png", "particle_6");
            ResourcesManager.LoadTexture("resources/textures/effects/particle_7.png", "particle_7");
            ResourcesManager.LoadTexture("resources/textures/effects/particle_8.png", "particle_8");
            ResourcesManager.LoadTexture("resources/textures/effects/particle_9.png", "particle_9");
            ResourcesManager.LoadTexture("resources/textures/effects/particle_10.png", "particle_10");
            // загрузка контента сущностей
            ResourcesManager.LoadTexture("resources/textures/npc/player.png", "player");
            ResourcesManager.LoadTexture("resources/textures/npc/enemy_1.png", "enemy_1");
            ResourcesManager.LoadTexture("resources/textures/npc/animal_1.png", "animal_1");
            // загрузка контента пользовательского интерфейса
            ResourcesManager.LoadTexture("resources/textures/ui/ui_particles.png", "ui_particles");
            ResourcesManager.LoadTexture("resources/textures/ui/craft_recepts.png", "craft_slots");
            ResourcesManager.LoadTexture("resources/textures/ui/craft_access.png", "craft_access");
            ResourcesManager.LoadTexture("resources/textures/ui/cursor.png", "cursor");
            // загрузка контента предметов
            ResourcesManager.LoadTexture("resources/textures/items/items_list.png", "items");

            // загрузка звукового контента
            ResourcesManager.LoadSound("resources/sounds/nature/nature.ogg", "nature");
            ResourcesManager.LoadSound("resources/sounds/fight/player_attack.ogg", "plr_attack");
            ResourcesManager.LoadSound("resources/sounds/fight/wood_hit.ogg", "wood_hit");

            // добавление сцен
            // главное меню
            sceneManager.AddScene(new MainMenu(window), true);
            // меню настроек
            sceneManager.AddScene(new SettingsScene(window), false);
            // меню выбора карты
            sceneManager.AddScene(new ChooseMapScene(window), false);
            // одиночная игра
            sceneManager.AddScene(new SinglePlayerScene(window), false);
            // меню подключения к серверу
            sceneManager.AddScene(new ConnectionMenuScene(window), false);
            // сетевая игра
            sceneManager.AddScene(new MultiPlayerScene(window), false);
        }
        private void OnUpdate()
        {
            // обработка событий окна
            window.DispatchEvents();

            // расчёт дельты времени
            deltaTime = frameClock.ElapsedTime.AsMilliseconds();
            frameClock.Restart();
            deltaTime /= 600;

            // обновление сцен
            sceneManager.OnUpdate(deltaTime);
        }
        private void OnRender()
        {
            // отчистка экрана
            window.Clear(Color.White);

            // отрисовка сцен
            sceneManager.OnRender(window);

            // отображение отрисовки
            window.Display();
        }

        public void Run()
        {
            OnInitialize();
            while (window.IsOpen)
            {
                OnUpdate();
                OnRender();
            }
        }

        private void keyPressedEvent(object sender, KeyEventArgs e)
        {
            sceneManager.OnHandleKeyPressed(e);
        }
        private void keyReleasedEvent(object sender, KeyEventArgs e)
        {
            sceneManager.OnHandleKeyReleased(e);
        }
        private void mouseButtonPressedEvent(object sender, MouseButtonEventArgs e)
        {
            sceneManager.OnHandleButtonPressed(e);
        }
        private void mouseButtonReleasedEvent(object sender, MouseButtonEventArgs e)
        {
            sceneManager.OnHandleButtonReleased(e);
        }
        private void mouseMovedEvent(object sender, MouseMoveEventArgs e)
        {
            sceneManager.OnHandleMouseMoved(e);
        }
        private void mouseWheelScrolledEvent(object sender, MouseWheelScrollEventArgs e)
        {
            sceneManager.OnHandleWheelScrolled(e);
        }
        private void windowClose(object sender, EventArgs e)
        {
            window.Close();
        }
    }
}
