using Assignment.Core.Game;
using Assignment.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Assignment.Characters.Player.Manager
{
    public class PlayerManager : MonoBehaviour, IPlayerManager
    {
        [SerializeField] Canvas playerCanvas = default;
        [SerializeField] GameObject inventoryPanel = default;
        [SerializeField] Text pickupGoalText = default;

        private ILevelEventHandler levelEventHandler;
        private WorldInteraction worldInteraction;
        private LeftClickInWorld leftClickInWorld;
        private bool isGameRunning = true;
        private bool inInventory = false;

        private void Awake()
        {
            levelEventHandler = FindObjectOfType<GameManager>();
            worldInteraction = GetComponent<WorldInteraction>();
            leftClickInWorld = GetComponent<LeftClickInWorld>();
        }
        private void OnEnable() => levelEventHandler.OnGameStateChange += OnGameStateChanged;
        private void OnDisable() => levelEventHandler.OnGameStateChange -= OnGameStateChanged;

        private void Start()
        {
            Cursor.visible = false;
            pickupGoalText.text = levelEventHandler.GetPickupGoalText();
            UpdatePlayerState(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab) && isGameRunning)
            {
                inInventory = !inInventory;
                UpdatePlayerState(inInventory);
            }
        }

        public void OnEnemyKilled() => levelEventHandler.OnEnemyKilled();

        public void OnPickupCollected(ItemStats item, int amount)
        {
            if (levelEventHandler.IsRequiredPickup(item, amount))
            {
                pickupGoalText.text = levelEventHandler.GetPickupGoalText();
            }
        }

        private void OnGameStateChanged(GameState gameState)
        {
            isGameRunning = gameState == GameState.Running;
            playerCanvas.gameObject.SetActive(isGameRunning);
            if (isGameRunning && !inInventory)
            {
                EnablePlayerComponents(true);
                Cursor.visible = false;
            }
            else
            {
                EnablePlayerComponents(false);
            }
        }

        private void UpdatePlayerState(bool inInventory)
        {
            inventoryPanel.SetActive(inInventory);
            Cursor.visible = inInventory;
            EnablePlayerComponents(!inInventory);
        }

        /// <summary>
        /// Controls only player components that are unaffected by Time.scaleTime
        /// </summary>
        /// <param name="enabled"></param>
        private void EnablePlayerComponents(bool enabled)
        {
            worldInteraction.enabled = enabled;
            leftClickInWorld.enabled = enabled;
        }
    }
}