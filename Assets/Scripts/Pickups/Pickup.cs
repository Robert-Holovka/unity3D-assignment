using Assignment.ScriptableObjects;
using UnityEngine;

namespace Assignment.Pickups
{
    public class Pickup : BasePickup
    {
        [SerializeField] protected ItemStats pickupInfo = default;
        public override ItemStats ItemInfo => pickupInfo;
    }
}