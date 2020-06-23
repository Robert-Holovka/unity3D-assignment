using Assignment.Pickups;
using Assignment.ScriptableObjects;
using UnityEngine;

public class Pickup : MonoBehaviour, IPickupableItem
{
    [SerializeField] ItemStats pickupInfo = default;
    [SerializeField] int amount = 1;

    public ItemStats ItemInfo { get => pickupInfo; }
    public int Amount { get => amount; set => amount = value; }
    public void OnItemPicked() => Destroy(gameObject);
}