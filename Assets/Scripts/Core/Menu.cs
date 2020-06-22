using Assignment.Core.Game;
using TMPro;
using UnityEngine;

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

        private ILevelManager levelManager;

        private void Awake() => levelManager = FindObjectOfType<GameManager>().GetComponent<ILevelManager>();
        private void OnEnable() => levelManager.OnGameStateChange += OnGameStateChanged;
        private void OnDisable() => levelManager.OnGameStateChange -= OnGameStateChanged;

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
                case GameState.Loading:
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

        public void OnLoadSceneButtonClicked() => levelManager.LoadLevel();

        public void OnQuitButtonClicked() => levelManager.LoadLevel();
    }
}