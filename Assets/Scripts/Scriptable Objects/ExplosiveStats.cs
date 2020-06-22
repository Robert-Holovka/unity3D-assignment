using UnityEngine;

namespace Assignment.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Explosive", menuName = "Explosive", order = 1)]
    public class ExplosiveStats : ScriptableObject
    {
        [SerializeField] float damage = 35f;
        [SerializeField] float radius = 5f;

        public float Damage
        {
            get => damage;
        }

        public float Radius
        {
            get => radius;
        }
    }
}