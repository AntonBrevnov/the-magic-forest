using SFML.Graphics;

namespace build_alpha_0._2.Core.Inventory.Items
{
    public class SwordItem : Item
    {
        public SwordItem() : base()
        {
            Sprite.Texture = ResourcesManager.GetTexture("items");
            Sprite.TextureRect = new IntRect(0, 0, 24, 24);
            Sprite.Color = Color.White;
            itemId = ItemID.Sword;
        }
    }
}
