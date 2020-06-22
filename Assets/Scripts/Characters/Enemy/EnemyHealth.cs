using Assignment.ScriptableObjects;
using UnityEngine;

namespace Assignment.Characters.Enemy
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] EnemyStats baseStats;

        private MeshRenderer meshRenderer;
        private float currentHealth;
        private float lowHealthThreshold;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            currentHealth = baseStats.MaxHealth;
            lowHealthThreshold = baseStats.MaxHealth * baseStats.LowHealthThreshold;
            meshRenderer.material.color = baseStats.DefaultColor;
        }

        public void TakeDamage(float damageAmount)
        {
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }

            if (currentHealth <= lowHealthThreshold)
            {
                meshRenderer.material.color = baseStats.LowHealthColor;
            }
        }
    }
}