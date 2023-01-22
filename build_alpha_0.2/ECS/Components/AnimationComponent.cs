using build_alpha_0._2.VFX;
using SFML.Graphics;
using System;

namespace build_alpha_0._2.ECS.Components
{
    public class AnimationComponent : Component
    {
        private Animator animator;
        public Animator Animator
        {
            set { animator = value; }
        }

        public AnimationComponent() : base()
        {
            animator = null;
        }
        public AnimationComponent(uint id) : base(id)
        {
            animator = null;
        }
        public AnimationComponent(Animator animator) : base()
        {         
            this.animator = animator;
        }
        public AnimationComponent(Animator animator, uint id) : base(id)
        {         
            this.animator = animator;
        }

        public void AddAnimation(string name, Animation animation)
        {
            animator.AddAnimation(name, animation);
        }
        public void RemoveAnimation(string name)
        {
            animator.RemoveAnimation(name);
        }

        public void SetCurrentAnimation(string name)
        {
            animator.SetCurrentAnimation(name);
        }
        public void PlayCurrentAnimation()
        {
            animator.PlayCurrentAnimation();
        }
        public void PauseCurrentAnimation()
        {
            animator.PauseCurrentAnimation();
        }

        public override void OnUpdate(float deltaTime)
        {
            animator.OnUpdate(deltaTime);
        }

        public override void OnRender(RenderTarget target)
        {
        }
    }
}
