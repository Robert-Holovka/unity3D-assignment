using Assignment.Characters.Enemy;
using Assignment.ScriptableObjects;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Assignment.Core.Game.GameState;

namespace Assignment.Core.Game
{
    public class GameManager : MonoBehaviour, ILevelManager, ILevelEventHandler
    {
        [SerializeField] GameObject loadingCanvas;
        [SerializeField] Slider loadingBar;
        [SerializeField] Text loadingProgressText;

        public event UnityAction<GameState> OnGameStateChange;

        private GameState gameState;

        // Level goals
        private int enemiesKilledGoal;
        private int enemiesKilled;
        private int pickupsCollectedGoal;
        private int pickupsCollected;

        #region Unity Methods
        private void Start() => SetGameState(GameState.Start);

        private void Update()
        {
            bool isMenuLocked = (gameState != Running && gameState != Paused);

            if (!isMenuLocked && Input.GetKeyDown(KeyCode.Escape))
            {
                SetGameState((gameState == Paused) ? Running : Paused);
            }
        }
        #endregion

        #region Public Methods
        public void LoadLevel()
        {
            SetGameState(Loading);
            StartCoroutine(LoadLevelAsync());
        }

        public void QuitApplication()
        {
            Application.Quit();
        }

        public void OnEnemyKilled()
        {
            enemiesKilled++;
            CheckForVictory();
        }

        public void OnPickupCollected(ItemStats itemStats)
        {
            pickupsCollected++;
            CheckForVictory();
        }

        public void OnPlayerDeath()
        {
            SetGameState(GameOver);
        }
        #endregion

        #region Private Methods
        private void SetGameState(GameState gameState)
        {
            this.gameState = gameState;
            OnGameStateChange?.Invoke(gameState);
            Time.timeScale = (gameState == Running) ? 1f : 0f;

            loadingCanvas.SetActive(gameState == Loading);
        }

        private void OnLevelLoaded()
        {
            enemiesKilled = 0;
            pickupsCollected = 0;
            enemiesKilledGoal = FindObjectsOfType<EnemyHealth>().Length;
            pickupsCollectedGoal = FindObjectsOfType<Pickup>().Length;
        }

        private void CheckForVictory()
        {
            if (enemiesKilled == enemiesKilledGoal && pickupsCollected == pickupsCollectedGoal)
            {
                SetGameState(Victory);
            }
        }

        private IEnumerator LoadLevelAsync()
        {
            AsyncOperation loading = SceneManager.LoadSceneAsync("Sandbox");

            while (!loading.isDone)
            {
                UpdateLoadingUI(loading.progress);
                yield return null;
            }

            UpdateLoadingUI(0.95f);
            yield return null;
            UpdateLoadingUI(1f);
            yield return null;

            OnLevelLoaded();
            SetGameState(Running);
        }

        private void UpdateLoadingUI(float progress)
        {
            loadingBar.value = progress;
            loadingProgressText.text = $"{Mathf.RoundToInt(progress * 100f)} %";
        }
        #endregion
    }
}