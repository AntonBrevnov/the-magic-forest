using SFML.Graphics;

namespace build_alpha_0._2.Core.Inventory.Items
{
    class CoalItem : Item
    {
        public CoalItem() : base()
        {
            Sprite.Texture = ResourcesManager.GetTexture("items");
            Sprite.TextureRect = new IntRect(144, 0, 24, 24);
            Sprite.Color = Color.White;
            itemId = ItemID.Coal;
        }
    }
}
