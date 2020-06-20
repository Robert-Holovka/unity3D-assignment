using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assignment.Characters.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] Image healthFillImage = default;

        public bool IsDead { get; set; }
        private const float MAX_HEALTH = 100f;
        private float currentHealth = MAX_HEALTH;

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
            healthFillImage.fillAmount = currentHealth / MAX_HEALTH;
        }

        private void Die()
        {
            throw new NotImplementedException();
        }
    }
}