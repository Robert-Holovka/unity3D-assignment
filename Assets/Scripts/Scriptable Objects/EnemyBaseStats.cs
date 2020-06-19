using UnityEngine;

namespace Assignment.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Enemy Stats", menuName = "New Enemy", order = 1)]
    public class EnemyBaseStats : ScriptableObject
    {
        [SerializeField] string type;
        [SerializeField] float maxHealth;
        [Tooltip("Percentage")]
        [Range(0, 1)]
        [SerializeField] float lowHealthThreshold;
        [SerializeField] Color defaultColor;
        [SerializeField] Color lowHealthColor;

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