using Assignment.Pickups;

namespace Assignment.Inventory
{
    public interface IInventorySystem
    {
        bool AddItem(IPickupableItem pickup);
    }
}