using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace build_alpha_0._2.NPCOptions
{
    public class Healthable
    {
        protected int maxHealthPoint;
        protected int maxHungerPoint;
        protected int maxDrinkPoint;

        protected int healthPoint;
        public int HP
        {
            get { return healthPoint; }
        }
        protected int hungerPoint;
        public int HG
        {
            get { return hungerPoint; }
        }
        protected int drinkPoint;
        public int DP
        {
            get { return drinkPoint; }
        }

        private bool isLife;
        public bool IsLife
        {
            get { return isLife; }
        }

        Clock clock;

        public Healthable()
        {
            maxHealthPoint = 100;
            healthPoint = maxHealthPoint;
            maxHungerPoint = 100;
            hungerPoint = maxHungerPoint;
            maxDrinkPoint = 100;
            drinkPoint = maxDrinkPoint;

            isLife = true;

            clock = new Clock();
        }
        public Healthable(int maxHP, int maxHG, int maxDP)
        {
            maxHealthPoint = maxHP;
            healthPoint = maxHealthPoint;
            maxHungerPoint = maxHG;
            hungerPoint = maxHungerPoint;
            maxDrinkPoint = maxDP;
            drinkPoint = maxDrinkPoint;

            isLife = true;

            clock = new Clock();
        }

        public void SetMaximumValues(int maxHP, int maxHG, int maxDP)
        {
            maxHealthPoint = maxHP;
            healthPoint = maxHealthPoint;
            maxHungerPoint = maxHG;
            hungerPoint = maxHungerPoint;
            maxDrinkPoint = maxDP;
            drinkPoint = maxDrinkPoint;
        }
        public void Initialize(int HP, int HG, int DP)
        {
            healthPoint = HP;
            hungerPoint = HG;
            drinkPoint = DP;
        }

        public void CheckLifeState()
        {
            if (healthPoint <= 0) Kill();
            if (clock.ElapsedTime.AsSeconds() > 6 && clock.ElapsedTime.AsSeconds() < 7)
                if (hungerPoint >= 50 && drinkPoint >= 50)
                    AddHealthPoint(2);
            if (clock.ElapsedTime.AsSeconds() > 10 && clock.ElapsedTime.AsSeconds() > 11)
                SubHungerPoint(2);
            if (clock.ElapsedTime.AsSeconds() > 12 && clock.ElapsedTime.AsSeconds() > 13)
                SubDrinkPoint(2);

            if (clock.ElapsedTime.AsSeconds() > 15)
                clock.Restart();
        }

        public void AddHealthPoint(int value)
        {
            if (healthPoint + value < maxHealthPoint)
                healthPoint += value;
            else healthPoint = maxHealthPoint;
        }
        public void SubHealthPoint(int value)
        {
            if (healthPoint - value > 0)
                healthPoint -= value;
            else healthPoint = 0;
        }
        public void AddHungerPoint(int value)
        {
            if (hungerPoint + value < maxHungerPoint)
                hungerPoint += value;
            else hungerPoint = maxHungerPoint;
        }
        public void SubHungerPoint(int value)
        {
            if (hungerPoint - value > 0)
                hungerPoint -= value;
            else hungerPoint = 0;
        }
        public void AddDrinkPoint(int value)
        {
            if (drinkPoint + value < maxDrinkPoint)
                drinkPoint += value;
            else drinkPoint = maxDrinkPoint;
        }
        public void SubDrinkPoint(int value)
        {
            if (drinkPoint - value > 0)
                drinkPoint -= value;
            else drinkPoint = 0;
        }

        public void Kill()
        {
            isLife = false;
        }
    }
}
