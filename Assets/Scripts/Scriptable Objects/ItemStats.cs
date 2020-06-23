using UnityEngine;

namespace Assignment.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Item Info", menuName = "Item Info", order = 2)]
    public class ItemStats : ScriptableObject
    {
        [SerializeField] new string name = default;
        [SerializeField] string type = default;
        [SerializeField] Sprite icon = default;
        [SerializeField] int maxStack = 1;

        public string Name => name;
        public string Type => type;
        public int MaxStack => maxStack;
        public Sprite Icon => icon;
        public string GetTooltip(string key) => $"[{key}] Pick up {Name}";
    }
}