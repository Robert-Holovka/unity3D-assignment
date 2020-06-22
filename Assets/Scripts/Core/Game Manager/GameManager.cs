using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static Assignment.Core.Game.GameState;

namespace Assignment.Core.Game
{
    public class GameManager : MonoBehaviour
    {
        public event UnityAction<GameState> OnGameStateChange;

        private GameState gameState;

        private void Start() => SetGameState(GameState.Start);

        private void Update()
        {
            bool isMenuLocked = (gameState != Running && gameState != Paused);

            if (!isMenuLocked && Input.GetKeyDown(KeyCode.Escape))
            {
                SetGameState((gameState == Paused) ? Running : Paused);
            }
        }

        private void SetGameState(GameState gameState)
        {
            this.gameState = gameState;
            OnGameStateChange?.Invoke(gameState);

            if (gameState == Paused)
            {
                Time.timeScale = 0f;
            }
            if (gameState == Running)
            {
                Time.timeScale = 1f;
            }
        }

        public void LoadScene()
        {
            // TODO: async loading + loading bar
            SceneManager.LoadScene("Sandbox");
            SetGameState(Running);
        }
    }
}