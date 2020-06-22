using Assignment.ScriptableObjects;

namespace Assignment.Characters.Player.Manager
{
    public interface IPlayerManager
    {
        bool InInventory { get; }
        void OnEnemyKilled();
        void OnPickupCollected(ItemStats item, int amount);
    }
}