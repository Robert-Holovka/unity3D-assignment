using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static Assignment.Core.GameState;

namespace Assignment.Core
{
    public class GameManager : MonoBehaviour
    {
        public event UnityAction<GameState> OnGameStateChange;

        private GameState gameState;

        void Start()
        {
            SetGameState(GameState.Start);
        }

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
            OnGameStateChange(gameState);

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