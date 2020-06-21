using Assignment.ScriptableObjects;
using UnityEngine.Events;

namespace Assignment.Inventory.ItemSlot
{
    public interface ISlot
    {
        event UnityAction<ISlot> OnStackSplit;
        byte SlotID { get; set; }
        bool IsEmpty { get; }
        int SlotCount { get; }
        IIconHandler MyIconHandler { get; }
        ItemStats StoredItem { get; }
        int AddStackPortion(ItemStats newItem, int amount);
        bool AddAll(ItemStats newItem, int amount);
        bool RemoveStackPortion(int amount);
        int HowManyCanIStore(ItemStats newItem);
        void DropAll();
    }
}