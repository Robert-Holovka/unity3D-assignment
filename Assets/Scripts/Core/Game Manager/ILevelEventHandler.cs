using Assignment.ScriptableObjects;

namespace Assignment.Core.Game
{
    public interface ILevelEventHandler : IGameStateManager
    {
        string GetPickupGoalText();
        void OnEnemyKilled();
        bool OnPickupCollected(ItemStats itemStats, int amount);
        void OnPlayerDeath();
    }
}