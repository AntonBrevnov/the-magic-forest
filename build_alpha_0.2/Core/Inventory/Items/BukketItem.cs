using SFML.Graphics;

namespace build_alpha_0._2.Core.Inventory.Items
{
    public class BukketItem : Item
    {
        public enum BukketState
        {
            Empty = 0, Full
        }
        public BukketState State { get; set; }

        public BukketItem() : base()
        {
            State = BukketState.Empty;
            Sprite.Texture = ResourcesManager.GetTexture("items");
            Sprite.TextureRect = new IntRect(96, 0, 24, 24);
            Sprite.Color = Color.White;
            itemId = ItemID.Bukket;
        }

        public override void OnUpdate()
        {
            Sprite.Position = Shape.Position;
            Hitbox.OnUpdate();
            switch (State)
            {
                case BukketState.Empty:
                    Sprite.TextureRect = new IntRect(96, 0, 24, 24);
                    break;
                case BukketState.Full:
                    Sprite.TextureRect = new IntRect(120, 0, 24, 24);
                    break;
            }
        }
    }
}
