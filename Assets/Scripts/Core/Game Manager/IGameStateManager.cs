using UnityEngine.Events;

namespace Assignment.Core.Game
{
    public interface IGameStateManager
    {
        event UnityAction<GameState> OnGameStateChange;
    }
}