using build_alpha_0._2.ECS;
using build_alpha_0._2.VFX;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace build_alpha_0._2.NPC
{
    public class DeerController
    {
        private List<Deer> deers;
        public List<Deer> Deers
        {
            get { return deers; }
        }

        public DeerController()
        {
            deers = new List<Deer>();
        }

        public void SpawnDeer(int count, Vector2f mapSize, ref SystemManager manager)
        {
            Random random = new Random();

            if (deers.Count + count < 8)
            { 
                for (int i = 0; i < count; i++)
                {
                    int randPointX = random.Next(10, (int)mapSize.X);
                    int randPointY = random.Next(10, (int)mapSize.Y);

                    Deer deer = new Deer(ref manager, deers.Count + i);
                    deer.Shape.Position = new Vector2f(randPointX, randPointY);
                    deers.Add(deer);
                }
            }
        }

        public void KillDeers()
        {
            deers.Clear();
        }

        public void OnUpdate(ref SystemManager manager)
        {
            for (int i = 0; i < deers.Count; i++)
            {
                deers[i].OnUpdate(ref manager);
                if (!deers[i].Healthable.IsLife)
                {
                    deers.RemoveAt(i);
                    i--;
                }
            }
        }

        public Deer GetDeer(int index)
        {
            return deers[index];
        }
        public int GetDeerCount()
        {
            return deers.Count;
        }

        public void OnRender(RenderTarget target, Light light)
        {
            for (int i = 0; i < deers.Count; i++)
            {
                deers[i].OnRender(target, light);
            }
        }
    }
}
