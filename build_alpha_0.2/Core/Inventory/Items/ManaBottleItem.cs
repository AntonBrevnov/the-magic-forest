using SFML.Graphics;

namespace build_alpha_0._2.Core.Inventory.Items
{
    public class ManaBottleItem : Item
    {
        public ManaBottleItem() : base()
        {
            Sprite.Texture = ResourcesManager.GetTexture("items");
            Sprite.TextureRect = new IntRect(48, 0, 24, 24);
            Sprite.Color = Color.White;
            itemId = ItemID.ManaBottle;
        }
    }
}
