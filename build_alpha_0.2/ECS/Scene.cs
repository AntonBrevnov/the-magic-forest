using SFML.Graphics;
using SFML.Window;
using System;

namespace build_alpha_0._2.ECS
{
    public abstract class Scene
    {
        // событие смены сцен
        public event EventHandler<SceneChagedEventArgs> OnSceneChanged;

        // менеджер сущностей и компонентов
        protected SystemManager systemManager;

        // окно приложения
        protected RenderWindow window;

        // сосотояние активности сцены
        private bool sceneIsActive;
        public bool SceneIsActive
        {
            get { return sceneIsActive; }
        }

        // уникальные идентификатор сцены
        private uint sceneID;
        public uint SceneID
        {
            get { return sceneID; }
            set { sceneID = value; }
        }

        public Scene(RenderWindow window)
        {
            systemManager = new SystemManager();
            this.window = window;
            sceneIsActive = false;
            sceneID = 0;
        }

        // стадартные методы сцены

        // стартовый метод сцены
        public abstract void OnStart();
        
        // методы обработки событий устроиств ввода
        public abstract void OnHandleKeyPressed(KeyEventArgs e);
        public abstract void OnHandleKeyReleased(KeyEventArgs e);
        public abstract void OnHandleButtonPressed(MouseButtonEventArgs e);
        public abstract void OnHandleButtonReleased(MouseButtonEventArgs e);
        public abstract void OnHandleMouseMoved(MouseMoveEventArgs e);
        public abstract void OnHandleWheelScrolled(MouseWheelScrollEventArgs e);

        // метод обновления сцены
        public abstract void OnUpdate(float deltaTime);
        // метод отрисовки сцены
        public abstract void OnRender(RenderTarget target);

        // активация сцены
        public void ActivateScene()
        {
            sceneIsActive = true;
            OnStart();
        }
        // деактивация сцены
        public void DeactivateScene()
        {
            sceneIsActive = false;
        }

        // метод смены текущей сцены на другую
        protected void ChangeScene(uint nextSceneID)
        {
            var args = new SceneChagedEventArgs();
            args.currentSceneID = sceneID;
            args.nextSceneID = nextSceneID;
            OnSceneChanged(this, args);
        }
    }
}
