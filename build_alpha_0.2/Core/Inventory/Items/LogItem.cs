using SFML.Graphics;

namespace build_alpha_0._2.Core.Inventory.Items
{
    public class LogItem : Item
    {
        public LogItem() : base()
        {
            Sprite.Texture = ResourcesManager.GetTexture("items");
            Sprite.TextureRect = new IntRect(264, 0, 24, 24);
            Sprite.Color = Color.White;
            itemId = ItemID.Log;
        }
    }
}
