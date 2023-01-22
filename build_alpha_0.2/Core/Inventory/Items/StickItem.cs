using SFML.Graphics;

namespace build_alpha_0._2.Core.Inventory.Items
{
    public class StickItem : Item
    {
        public StickItem() : base()
        {
            Sprite.Texture = ResourcesManager.GetTexture("items");
            Sprite.TextureRect = new IntRect(240, 0, 24, 24);
            Sprite.Color = Color.White;
            itemId = ItemID.Stick;
        }
    }
}
