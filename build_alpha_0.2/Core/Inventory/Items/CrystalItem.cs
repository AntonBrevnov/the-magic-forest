using SFML.Graphics;

namespace build_alpha_0._2.Core.Inventory.Items
{
    class CrystalItem : Item
    {
        public CrystalItem() : base()
        {
            Sprite.Texture = ResourcesManager.GetTexture("items");
            Sprite.TextureRect = new IntRect(192, 0, 24, 24);
            Sprite.Color = Color.White;
            itemId = ItemID.Crystal;
        }
    }
}
