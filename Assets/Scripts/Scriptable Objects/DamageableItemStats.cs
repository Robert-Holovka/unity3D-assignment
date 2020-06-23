using UnityEngine;

namespace Assignment.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Damageable Item Info", menuName = "Damageable Item Info", order = 3)]
    public class DamageableItemStats : ItemStats
    {
        [SerializeField] float health = 100f;
        public float Health => health;
    }
}