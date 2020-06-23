using Assignment.ScriptableObjects;
using UnityEngine.Events;

namespace Assignment.Inventory
{
    public interface IInventorySystem
    {
        event UnityAction<ItemStats, int> OnItemAdd;
        event UnityAction<ItemStats, int> OnItemRemove;
        bool AddItem(ItemStats item, int amount);
        int ItemCount(ItemStats item);
        bool RemoveItem(ItemStats item);
    }
}