using Assignment.ScriptableObjects;

namespace Assignment.Characters.Player.Manager
{
    public interface IPlayerManager
    {
        void OnEnemyKilled();
        void OnPickupCollected(ItemStats item, int amount);
    }
}