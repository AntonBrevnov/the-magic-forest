using System.Collections.Generic;

namespace build_alpha_0._2.Core.Inventory
{
    using Items;

    public class InventoryController
    {
        private List<Item> items;
        public int MaxItemsCount { get; private set; }

        public InventoryController()
        {
            items = new List<Item>();
            MaxItemsCount = 0;
        }
        public InventoryController(int itemsCount)
        {
            items = new List<Item>();
            MaxItemsCount = itemsCount;
        }

        public bool AddItem(Item newItem)
        {
            if (MaxItemsCount == 0 || items.Count < MaxItemsCount)
            {
                items.Add(newItem);
                return true;
            }
            return false;
        }
        public void RemovaItem(Item item)
        {
            items.Remove(item);
        }
        public void RemoveItemAt(int index)
        {
            items.RemoveAt(index);
        }
        public void RemoveItemByID(ItemID itemID)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ItemId == itemID)
                {
                    items.RemoveAt(i);
                    return;
                }
            }
        }
        public void SwapItems(int firstIndex, int secondIndex)
        {
            Item temp = items[firstIndex];
            items[firstIndex] = items[secondIndex];
            items[secondIndex] = temp;
        }

        public Item GetItemAt(int itemID)
        {
            if (items.Count > itemID)
                return items[itemID];
            else return null;
        }

        public bool HasItem(ItemID itemID)
        {
            for (int i = 0; i < items.Count; i++)
                if (items[i].ItemId == itemID) return true;
            return false;
        }
        public bool HasItemOnCount(ItemID itemID, int count)
        {
            int itemsCount = 0;

            for (int i = 0; i < items.Count; i++)
                if (items[i].ItemId == itemID) itemsCount++;

            if (count <= itemsCount) return true;
            return false;
        }

        public void SaveInventory(string filePath)
        {
            DataWriter.SaveData(filePath, "items_count", items.Count);
            for (int i = 0; i < items.Count; i++)
            {
                DataWriter.SaveData(filePath, $"slot_{i}", (int)items[i].ItemId);
            }
        }

        public void LoadInventory(string filePath)
        {
            int itemsCount = int.Parse(DataReader.LoadData(filePath, "items_count"));
            if (itemsCount != 0)
            {
                for (int i = 0; i < itemsCount; i++)
                {
                    ItemID itemID = (ItemID)int.Parse(DataReader.LoadData(filePath, $"slot_{i}"));
                    switch (itemID)
                    {
                        case ItemID.None:
                            break;
                        case ItemID.Bukket:
                            AddItem(new BukketItem());
                            break;
                        case ItemID.Coal:
                            AddItem(new CoalItem());
                            break;
                        case ItemID.Crystal:
                            AddItem(new CrystalItem());
                            break;
                        case ItemID.HealthBottle:
                            AddItem(new HealthBottleItem());
                            break;
                        case ItemID.Iron:
                            AddItem(new IronItem());
                            break;
                        case ItemID.Log:
                            AddItem(new LogItem());
                            break;
                        case ItemID.ManaBottle:
                            AddItem(new ManaBottleItem());
                            break;
                        case ItemID.Pickaxe:
                            AddItem(new PickaxeItem());
                            break;
                        case ItemID.Stick:
                            AddItem(new StickItem());
                            break;
                        case ItemID.Sword:
                            AddItem(new SwordItem());
                            break;
                        case ItemID.Torch:
                            AddItem(new TorchItem());
                            break;
                    }
                }
            }
        }
    }
}
