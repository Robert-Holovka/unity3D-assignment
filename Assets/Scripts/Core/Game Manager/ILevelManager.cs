namespace Assignment.Core.Game
{
    public interface ILevelManager : IGameStateManager
    {
        void LoadLevel();
        void QuitApplication();
    }
}