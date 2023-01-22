using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace build_alpha_0._2.VFX
{
    public class ParticleSystem
    {
        private List<Particle> particles;
        private Clock clock;

        public ParticleSystem()
        {
            particles = new List<Particle>();
            clock = new Clock();
        }

        public void SpawnParticles(Vector2f spawnPosition, int count, float spawnAreaRange, Texture texture)
        {
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                clock.Restart();

                Particle particle = new Particle(
                    random.Next(2, 5), 
                    (float)random.NextDouble(),
                    (float)random.NextDouble(),
                    (float)random.NextDouble(),
                    new Vector2f(random.Next(-2, 2) * (float)random.NextDouble(), random.Next(-2, 2) * (float)random.NextDouble()),
                    texture);
                particle.SetSize(new Vector2f(random.Next(15, 25), random.Next(15, 25)));
                particle.SetPosition(new Vector2f(
                    spawnPosition.X + random.Next((int)-spawnAreaRange / 2, (int)spawnAreaRange / 2),
                    spawnPosition.Y + random.Next((int)-spawnAreaRange / 2, (int)spawnAreaRange / 2))); 
                
                particles.Add(particle);
            }
        }

        public void OnUpdate()
        {
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].OnUpdate();
                if (clock.ElapsedTime.AsSeconds() >= particles[i].LifeTime)
                {
                    particles[i].KillParticle();
                }
                if (!particles[i].IsLife)
                {
                    particles.RemoveAt(i);
                    i--;
                }
            }
        }
        public void OnRender(RenderTarget target)
        {
            foreach (var particle in particles)
                particle.OnRender(target);
        }
    }
}
