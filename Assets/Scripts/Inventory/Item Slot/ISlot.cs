using Assignment.ScriptableObjects;
using UnityEngine.Events;

namespace Assignment.Inventory.ItemSlot
{
    public interface ISlot
    {
        event UnityAction<ISlot> OnStackSplit;
        IIconHandler MyIconHandler { get; }
        byte SlotID { get; set; }
        bool IsEmpty { get; }
        int SlotCount { get; }
        ItemStats StoredItem { get; }
        int AddStackPortion(ItemStats newItem, int amount);
        bool AddAll(ItemStats newItem, int amount);
        bool RemoveStackPortion(int amount);
        int SpaceLeft(ItemStats newItem);
        void DropAll();
    }
}