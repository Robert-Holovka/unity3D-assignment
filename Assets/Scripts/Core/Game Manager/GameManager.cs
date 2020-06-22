using Assignment.Characters.Enemy;
using Assignment.ScriptableObjects;
using System;
using System.Collections;
using System.Linq;
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
        private int enemiesToKill;
        private int enemiesKilled;
        private int pickupsToCollect;
        private int pickupsCollected;
        private LevelGoal levelGoal;

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

        public bool OnPickupCollected(ItemStats itemStats, int amount)
        {
            if (levelGoal.ContainsType(itemStats))
            {
                pickupsCollected += amount;
                CheckForVictory();
                return true;
            }
            return false;
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

        private void OnLevelLoaded(string levelName)
        {
            levelGoal = Resources.Load<LevelGoal>(levelName);

            enemiesKilled = 0;
            pickupsCollected = 0;

            enemiesToKill = FindObjectsOfType<EnemyHealth>().Length;
            Pickup[] pickups = FindObjectsOfType<Pickup>();
            pickupsToCollect = pickups
                .Where(p => levelGoal.ContainsType(p.ItemInfo))
                .Sum(p => p.Amount);
        }

        private void CheckForVictory()
        {
            if (enemiesKilled == enemiesToKill && pickupsCollected == pickupsToCollect)
            {
                SetGameState(Victory);
            }
        }

        private IEnumerator LoadLevelAsync()
        {
            SetGameState(Loading);
            AsyncOperation loading = SceneManager.LoadSceneAsync("Sandbox");

            while (!loading.isDone)
            {
                UpdateLoadingUI(loading.progress);
                yield return null;
            }

            UpdateLoadingUI(0.95f);
            yield return null;

            OnLevelLoaded("Sandbox");

            UpdateLoadingUI(1f);
            yield return null;

            SetGameState(Running);
        }

        private void UpdateLoadingUI(float progress)
        {
            loadingBar.value = progress;
            loadingProgressText.text = $"{Mathf.RoundToInt(progress * 100f)} %";
        }

        public string GetPickupGoalText() => $"{pickupsCollected}/{pickupsToCollect}";
        #endregion
    }
}