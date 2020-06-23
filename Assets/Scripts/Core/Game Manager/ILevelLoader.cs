namespace Assignment.Core.Game
{
    public interface ILevelLoader : IGameStateManager
    {
        void LoadLevel();
        void QuitApplication();
    }
}