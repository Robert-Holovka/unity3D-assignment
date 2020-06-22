using Assignment.ScriptableObjects;

namespace Assignment.Core.Game
{
    public interface ILevelEventHandler : IGameStateManager
    {
        void OnEnemyKilled();
        void OnPickupCollected(ItemStats itemStats);
        void OnPlayerDeath();
    }
}