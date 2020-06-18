using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assignment.Core
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] Canvas menuCanvas = default;
        [SerializeField] TextMeshProUGUI titleTMP = default;
        [SerializeField] TextMeshProUGUI loadSceneBtnTMP = default;

        private const string GAME_TITLE = "ASSIGNMENT";
        private const string GAME_OVER_TEXT = "You died";
        private const string VICTORY_TEXT = "You are victorious";

        private GameManager gameManager;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        private void OnEnable()
        {
            gameManager.OnGameStateChange += OnGameStateChanged;
        }

        private void OnDisable()
        {
            gameManager.OnGameStateChange -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Start:
                    UpdateMenu(true, GAME_TITLE, "Play");
                    break;
                case GameState.Paused:
                    UpdateMenu(true, GAME_TITLE);
                    break;
                case GameState.GameOver:
                    UpdateMenu(true, GAME_OVER_TEXT);
                    break;
                case GameState.Victory:
                    UpdateMenu(true, VICTORY_TEXT);
                    break;
                case GameState.Running:
                default:
                    UpdateMenu(false, GAME_TITLE);
                    break;
            }
        }

        private void UpdateMenu(bool enabled, string title, string loadSceneBtnText = "Restart")
        {
            titleTMP.text = title;
            loadSceneBtnTMP.text = loadSceneBtnText;
            menuCanvas.enabled = enabled;
        }

        public void OnLoadSceneButtonClicked()
        {
            gameManager.LoadScene();
        }

        public void OnQuitButtonClicked()
        {
            Application.Quit();
        }
    }
}