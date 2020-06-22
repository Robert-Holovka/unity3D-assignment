using UnityEngine;

namespace Assignment.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Level Info", menuName = "Level Info", order = 1)]
    public class LevelGoal : ScriptableObject
    {
        [SerializeField] ItemStats[] itemsToPick = default;

        public ItemStats[] ItemsToPick
        {
            get => itemsToPick;
        }

        public bool ContainsType(ItemStats itemInfo)
        {
            for (int i = 0, n = ItemsToPick.Length; i < n; i++)
            {
                if (itemInfo.Type == itemsToPick[i].Type) return true;
            }
            return false;
        }
    }
}