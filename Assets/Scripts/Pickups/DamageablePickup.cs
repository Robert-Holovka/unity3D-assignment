using Assignment.Characters;
using Assignment.ScriptableObjects;
using UnityEngine;

namespace Assignment.Pickups
{
    public class DamageablePickup : BasePickup, IDamageable
    {
        [SerializeField] DamageableItemStats pickupInfo = default;
        private float currentHealth;

        public override ItemStats ItemInfo => pickupInfo;
        private void Start() => currentHealth = pickupInfo.Health;

        public void TakeDamage(float damageAmount)
        {
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}