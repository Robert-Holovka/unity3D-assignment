using UnityEngine;

namespace Assignment.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Player Action", menuName = "Player Action", order = 1)]
    public class ActionStats : ScriptableObject
    {
        [SerializeField] string actionType;
        [SerializeField] float actionFrequency = 0.5f;

        public float ActionFrequency
        {
            get => actionFrequency;
        }

        public string ActionType
        {
            get => actionType;
        }
    }
}