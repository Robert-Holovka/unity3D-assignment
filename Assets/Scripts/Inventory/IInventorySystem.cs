using Assignment.ScriptableObjects;

namespace Assignment.Inventory
{
    public interface IInventorySystem
    {
        bool AddItem(ItemStats item, int amount);
        int ItemCount(ItemStats item);
        bool RemoveItem(ItemStats item);
    }
}