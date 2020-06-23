using UnityEngine;

namespace Assignment.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Enemy Stats", menuName = "Enemy", order = 5)]
    public class EnemyStats : ScriptableObject
    {
        [SerializeField] string type = null;
        [SerializeField] float maxHealth = 100f;
        [Tooltip("Percentage")]
        [Range(0, 1)]
        [SerializeField] float lowHealthThreshold = 0.3f;
        [SerializeField] Color defaultColor = Color.white;
        [SerializeField] Color lowHealthColor = Color.red;

        public string Type => type;
        public float MaxHealth => maxHealth;
        public float LowHealthThreshold => lowHealthThreshold;
        public Color DefaultColor => defaultColor;
        public Color LowHealthColor => lowHealthColor;
    }
}