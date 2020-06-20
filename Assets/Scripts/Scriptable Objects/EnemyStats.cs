using UnityEngine;

namespace Assignment.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Enemy Stats", menuName = "Enemy", order = 1)]
    public class EnemyStats : ScriptableObject
    {
        [SerializeField] string type = null;
        [SerializeField] float maxHealth = 100f;
        [Tooltip("Percentage")]
        [Range(0, 1)]
        [SerializeField] float lowHealthThreshold = 0.25f;
        [SerializeField] Color defaultColor = Color.white;
        [SerializeField] Color lowHealthColor = Color.red;

        public string Type
        {
            get => type;
        }

        public float MaxHealth
        {
            get => maxHealth;
        }

        public float LowHealthThreshold
        {
            get => lowHealthThreshold;
        }

        public Color DefaultColor
        {
            get => defaultColor;
        }

        public Color LowHealthColor
        {
            get => lowHealthColor;
        }
    }
}