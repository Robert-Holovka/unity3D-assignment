using Assignment.Characters.Enemy;
using Assignment.Pickups;
using Assignment.ScriptableObjects;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Assignment.Core.Game.GameState;

namespace Assignment.Core.Game
{
    public class GameManager : MonoBehaviour, ILevelLoader, ILevelEventHandler
    {
        [Tooltip("For easier testing")]
        [SerializeField] GameState startState = GameState.Start;
        [Header("UI")]
        [SerializeField] GameObject loadingCanvas = default;
        [SerializeField] Slider loadingBar = default;
        [SerializeField] Text loadingProgressText = default;

        public event UnityAction<GameState> OnGameStateChange;

        private GameState gameState;
        // Level goals
        private int enemiesToKill;
        private int enemiesKilled;
        private int pickupsToCollect;
        private int pickupsCollected;
        private LevelGoal levelGoal;

        #region Unity Methods
        private void Start()
        {
            SetGameState(startState);
            // For testing purposes
            if (startState != GameState.Start)
            {
                OnLevelLoaded(SceneManager.GetActiveScene().name);
            }
        }

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
        public void LoadLevel() => StartCoroutine(LoadLevelAsync());
        public void QuitApplication() => Application.Quit();
        public void OnPlayerDeath() => SetGameState(GameOver);
        public string GetPickupGoalText() => $"{pickupsCollected}/{pickupsToCollect}";

        public void OnEnemyKilled()
        {
            enemiesKilled++;
            CheckForVictory();
        }

        public bool IsRequiredPickup(ItemStats itemStats, int amount)
        {
            if (levelGoal.ContainsType(itemStats))
            {
                pickupsCollected += amount;
                CheckForVictory();
                return true;
            }
            return false;
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
            IPickupableItem[] pickups = FindObjectsOfType<Pickup>();
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
            AsyncOperation loading = SceneManager.LoadSceneAsync("World");

            // 0.0 - 0.9 (Unity Loading)
            while (!loading.isDone)
            {
                UpdateLoadingUI(loading.progress);
                yield return null;
            }
            // 0.9 - 1.0 (Unity Activation + custom initialization)
            UpdateLoadingUI(0.95f);
            yield return null;
            OnLevelLoaded("World");
            UpdateLoadingUI(1.0f);
            yield return null;

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