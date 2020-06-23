using UnityEngine;

namespace Assignment.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Explosive", menuName = "Explosive", order = 4)]
    public class ExplosiveStats : ScriptableObject
    {
        [SerializeField] float damage = 35f;
        [SerializeField] float radius = 5f;

        public float Damage => damage;
        public float Radius => radius;
    }
}