using Assignment.Characters;
using Assignment.ScriptableObjects;
using UnityEngine;

namespace Assignment.Weapons
{
    public class AreaDamage : MonoBehaviour
    {
        [SerializeField] ExplosiveStats explosive = default;
        [SerializeField] Color color = Color.red;

        public void DealDamage()
        {
            LayerMask layerMask = LayerMask.GetMask("Damageable");
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosive.Radius, layerMask);

            for (int i = 0, n = colliders.Length; i < n; i++)
            {
                IDamageable damageable = colliders[i].GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(explosive.Damage);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = color;
            Gizmos.DrawWireSphere(transform.position, explosive.Radius);
        }
    }
}