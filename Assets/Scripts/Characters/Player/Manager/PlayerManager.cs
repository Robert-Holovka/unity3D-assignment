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

        public bool InInventory { get; private set; } = false;

        private ILevelEventHandler levelEventHandler;
        private Movement playerMovement;
        private PlayerCamera playerCameraRotation;
        private Interaction playerInteraction;
        private LeftClickAction leftClickAction;
        private bool isGameRunning = true;

        private void Awake()
        {
            levelEventHandler = FindObjectOfType<GameManager>().GetComponent<ILevelEventHandler>();
            playerMovement = GetComponent<Movement>();
            playerCameraRotation = GetComponent<PlayerCamera>();
            playerInteraction = GetComponent<Interaction>();
            leftClickAction = GetComponent<LeftClickAction>();
        }

        private void Start()
        {
            Cursor.visible = false;
            pickupGoalText.text = levelEventHandler.GetPickupGoalText();
            UpdatePlayerState(false);
        }

        private void OnEnable() => levelEventHandler.OnGameStateChange += OnGameStateChanged;
        private void OnDisable() => levelEventHandler.OnGameStateChange -= OnGameStateChanged;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab) && isGameRunning)
            {
                InInventory = !InInventory;
                UpdatePlayerState(InInventory);
            }
        }

        private void UpdatePlayerState(bool inInventory)
        {
            inventoryPanel.SetActive(inInventory);
            Cursor.visible = inInventory;

            playerMovement.enabled = !inInventory;
            playerCameraRotation.enabled = !inInventory;
            playerInteraction.enabled = !inInventory;
            leftClickAction.enabled = !inInventory;
        }

        public void OnEnemyKilled() => levelEventHandler.OnEnemyKilled();
        public void OnPickupCollected(ItemStats item, int amount)
        {
            if (levelEventHandler.OnPickupCollected(item, amount))
            {
                pickupGoalText.text = levelEventHandler.GetPickupGoalText();
            }
        }

        public void OnGameStateChanged(GameState gameState)
        {
            isGameRunning = gameState == GameState.Running;
            playerCanvas.gameObject.SetActive(isGameRunning);
            if (isGameRunning && !InInventory)
            {
                Cursor.visible = false;
            }
        }
    }
}