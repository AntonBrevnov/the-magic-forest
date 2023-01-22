using System.Collections.Generic;

namespace build_alpha_0._2.VFX
{
    public class Animator
    {
        private string currentAnimationName;
        private Dictionary<string, Animation> animationList;

        public Animator()
        {
            animationList = new Dictionary<string, Animation>();
        }

        public void AddAnimation(string name, Animation animation)
        {
            foreach (var anim in animationList)
            {
                if (anim.Key == name) return;
            }

            currentAnimationName = name;
            animationList.Add(name, animation);
        }
        public void RemoveAnimation(string name)
        {
            animationList.Remove(name);
        }
        public void SetCurrentAnimation(string name)
        {
            currentAnimationName = name;
        }
        public void PlayCurrentAnimation()
        {
            animationList[currentAnimationName].PlayAnimation();
        }
        public void PauseCurrentAnimation()
        {
            animationList[currentAnimationName].PauseAnimation();
        }
        public void OnUpdate(float deltaTime)
        {
            animationList[currentAnimationName].OnUpdate(deltaTime);
        }
    }
}
