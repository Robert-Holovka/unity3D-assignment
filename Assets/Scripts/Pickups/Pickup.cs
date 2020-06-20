using Assignment.Pickups;
using Assignment.ScriptableObjects;
using UnityEngine;

public class Pickup : MonoBehaviour, IPickupable
{
    [SerializeField] PickupStats pickupInfo = default;
    [SerializeField] int amount = 1;

    public PickupStats PickupInfo { get => pickupInfo; }
    public int Amount { get => amount; }

    public void OnObjectPicked()
    {
        // TODO: animation
        Destroy(gameObject);
    }
}