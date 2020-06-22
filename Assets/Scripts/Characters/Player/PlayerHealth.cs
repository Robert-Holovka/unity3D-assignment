using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assignment.Characters.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageable, IRestorable
    {
        [SerializeField] float maxHealth = 100f;
        [SerializeField] Image healthFillImage = default;

        public bool IsDead { get; set; }

        private float currentHealth;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                UpdateHealthUI();
            }
        }

        private void UpdateHealthUI()
        {
            healthFillImage.fillAmount = currentHealth / maxHealth;
        }

        private void Die()
        {
            throw new NotImplementedException();
        }

        public void RestoreHealth(float healthPoints)
        {
            currentHealth += healthPoints;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
            UpdateHealthUI();
        }
    }
}