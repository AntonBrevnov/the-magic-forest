using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace build_alpha_0._2.ECS
{
    public class SceneManager
    {
        private List<Scene> scenes;
        
        public SceneManager()
        {
            scenes = new List<Scene>();
        }
               

        public void AddScene(Scene newScene, bool isStarted)
        {
            // поиск сцен c идентичным идентификатором
            foreach (var scene in scenes)
            {
                if (scene.SceneID == newScene.SceneID)
                {
                    Console.WriteLine($"The scene manager has this scene [id: {newScene.SceneID}");
                    return;
                }
            }

            // если идетичной сцены нет, то добавляем сцену в список
            newScene.SceneID = (uint)scenes.Count + 1;
            newScene.OnSceneChanged += ChangeScene;
            if (isStarted) 
                newScene.ActivateScene();
            scenes.Add(newScene);
        }

        public void RemoveScene(Scene oldScene)
        {
            // поиск сцен c идентичным идентификатором
            foreach (var scene in scenes)
            {
                if (scene.SceneID == oldScene.SceneID)
                {
                    scenes.Remove(oldScene);
                    return;
                }
            }
            Console.WriteLine($"The scene manager doesn't has this scene [id: {oldScene.SceneID}");
        }

        public void OnHandleKeyPressed(KeyEventArgs e)
        {
            foreach (var scene in scenes)
            {
                if (scene.SceneIsActive)
                {
                    scene.OnHandleKeyPressed(e);
                }
            }
        }
        public void OnHandleKeyReleased(KeyEventArgs e)
        {
            foreach (var scene in scenes)
            {
                if (scene.SceneIsActive)
                {
                    scene.OnHandleKeyReleased(e);
                }
            }
        }
        public void OnHandleButtonPressed(MouseButtonEventArgs e)
        {
            foreach (var scene in scenes)
            {
                if (scene.SceneIsActive)
                {
                    scene.OnHandleButtonPressed(e);
                }
            }
        }
        public void OnHandleButtonReleased(MouseButtonEventArgs e)
        {
            foreach (var scene in scenes)
            {
                if (scene.SceneIsActive)
                {
                    scene.OnHandleButtonReleased(e);
                }
            }
        }
        public void OnHandleMouseMoved(MouseMoveEventArgs e)
        {
            foreach (var scene in scenes)
            {
                if (scene.SceneIsActive)
                {
                    scene.OnHandleMouseMoved(e);
                }
            }
        }
        public void OnHandleWheelScrolled(MouseWheelScrollEventArgs e)
        {
            foreach (var scene in scenes)
            {
                if (scene.SceneIsActive)
                {
                    scene.OnHandleWheelScrolled(e);
                }
            }
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var scene in scenes)
            {
                if (scene.SceneIsActive)
                {
                    scene.OnUpdate(deltaTime);
                }
            }
        }

        public void OnRender(RenderTarget target)
        {
            foreach (var scene in scenes)
            {
                if (scene.SceneIsActive)
                {
                    scene.OnRender(target);
                }
            }
        }

        private void ChangeScene(object sender, SceneChagedEventArgs e)
        {
            scenes[(int)e.currentSceneID - 1].DeactivateScene();
            scenes[(int)e.nextSceneID - 1].ActivateScene();
        }
    }
}
