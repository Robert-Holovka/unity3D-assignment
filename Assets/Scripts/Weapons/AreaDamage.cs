using Assignment.Characters;
using UnityEngine;

namespace Assignment.Weapons
{
    public class AreaDamage : MonoBehaviour
    {
        [SerializeField] float damageAmount = 50f;
        [SerializeField] float radius = 5f;
        [SerializeField] Color color = Color.red;

        public void DealDamage()
        {
            LayerMask layerMask = LayerMask.GetMask("Damageable");
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);

            for (int i = 0, n = colliders.Length; i < n; i++)
            {
                IDamageable damageable = colliders[i].GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(damageAmount);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = color;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}