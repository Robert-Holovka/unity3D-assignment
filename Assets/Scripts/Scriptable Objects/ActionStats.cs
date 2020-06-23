using UnityEngine;

namespace Assignment.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Player Action", menuName = "Player Action", order = 6)]
    public class ActionStats : ScriptableObject
    {
        [SerializeField] string actionType = default;
        [SerializeField] float actionFrequency = 0.5f;

        public string ActionType => actionType;
        public float ActionFrequency => actionFrequency;
    }
}