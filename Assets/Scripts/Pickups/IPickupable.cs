using Assignment.ScriptableObjects;

namespace Assignment.Pickups
{
    public interface IPickupable
    {
        PickupStats PickupInfo { get; }
        void OnObjectPicked();
    }
}