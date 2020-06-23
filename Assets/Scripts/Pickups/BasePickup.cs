using Assignment.ScriptableObjects;
using UnityEngine;

namespace Assignment.Pickups
{
    public abstract class BasePickup : MonoBehaviour, IPickupableItem
    {
        [SerializeField] int amount = 1;

        public int Amount => amount;
        public abstract ItemStats ItemInfo { get; }
        public void OnItemPicked() => Destroy(gameObject);
    }
}