using SFML.System;

namespace build_alpha_0._2.NPCOptions
{
    public class Fightable
    {
        private int maxDamagePoint;
        private int damagePoint;
        public int DM
        {
            get { return damagePoint; }
        }

        private int maxUltimateDamagePoint;
        private int ultimateDamagePoint;
        public int UDM
        {
            get { return ultimateDamagePoint; }
        }

        private int maxManaPoint;
        private int manaPoint;
        public int MN
        {
            get { return manaPoint; }
        }

        private Clock clockUltimate;
        private Clock clockAttack;
        private Clock clock;

        public Fightable()
        {
            maxDamagePoint = 15;
            damagePoint = maxDamagePoint;
            maxUltimateDamagePoint = 30;
            ultimateDamagePoint = maxUltimateDamagePoint;
            maxManaPoint = 100;
            manaPoint = maxManaPoint;

            clockUltimate = new Clock();
            clockAttack = new Clock();
            clock = new Clock();
        }

        public void SetMaximumValues(int maxMN, int maxDM, int maxUDM)
        {
            maxManaPoint = maxMN;
            manaPoint = maxManaPoint;
            maxDamagePoint = maxDM;
            damagePoint = maxDamagePoint;
            maxUltimateDamagePoint = maxUDM;
            ultimateDamagePoint = maxUltimateDamagePoint;
        }
        public void Initialize(int MN, int DM, int UDM)
        {
            manaPoint = MN;
            damagePoint = DM;
            ultimateDamagePoint = UDM;
        }

        public void CheckFightState()
        {
            if (clock.ElapsedTime.AsSeconds() > 4)
                AddManaPoint(2);
            if (clock.ElapsedTime.AsSeconds() > 8) clock.Restart();
        }

        public void AddManaPoint(int value)
        {
            if (manaPoint + value < maxManaPoint)
                manaPoint += value;
            else manaPoint = maxManaPoint;
        }
        public void SubManaPoint(int value)
        {
            if (manaPoint - value > 0)
                manaPoint -= value;
            else manaPoint = 0;
        }

        public void AttackHealthable(Healthable healthable)
        {
            if (clockAttack.ElapsedTime.AsSeconds() >= 1)
            {
                healthable.SubHealthPoint(damagePoint);
            }
            clockAttack.Restart();
        }
        public void UltimateAttackHealthable(Healthable healthable)
        {
            if (clockUltimate.ElapsedTime.AsSeconds() >= 15)
            {
                healthable.SubHealthPoint(ultimateDamagePoint);
                clockUltimate.Restart();
            }
            SubManaPoint(40);
        }
    }
}
