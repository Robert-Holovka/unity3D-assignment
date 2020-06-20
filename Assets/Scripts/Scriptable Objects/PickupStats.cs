using UnityEngine;

namespace Assignment.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Pickup Info", menuName = "Pickup Info", order = 1)]
    public class PickupStats : ScriptableObject
    {
        [SerializeField] new string name = default;
        [SerializeField] string type = default;
        [SerializeField] Sprite icon = default;
        [SerializeField] int maxStack = 1;

        public string Name
        {
            get => name;
        }

        public string Type
        {
            get => type;
        }

        public int MaxStack
        {
            get => maxStack;
        }

        public Sprite Icon
        {
            get => icon;
        }

        public string GetTooltip(string key) => $"[{key}] Pick up {Name}";
    }
}