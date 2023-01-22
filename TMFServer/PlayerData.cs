namespace TMFServer
{
    public class PlayerData
    {
        public float PositionX { get; set; } = 0;
        public float PositionY { get; set; } = 0;

        public int HealthPoint { get; set; } = 100;
        public int HungerPoint { get; set; } = 100;
        public int DrinkPoint { get; set; } = 100;

        public int ManaPoint { get; set; } = 100;
        public int DamagePoint { get; set; } = 15;
        public int UltimateDamagePoint { get; set; } = 30;
    }
}
