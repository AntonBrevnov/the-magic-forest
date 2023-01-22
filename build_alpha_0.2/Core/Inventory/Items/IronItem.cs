using SFML.Graphics;

namespace build_alpha_0._2.Core.Inventory.Items
{
    class IronItem : Item
    {
        public IronItem() : base()
        {
            Sprite.Texture = ResourcesManager.GetTexture("items");
            Sprite.TextureRect = new IntRect(168, 0, 24, 24);
            Sprite.Color = Color.White;
            itemId = ItemID.Iron;
        }
    }
}
