using Assignment.Characters.Player.Manager;
using Assignment.Inventory;
using Assignment.Pickups;
using UnityEngine;
using UnityEngine.UI;

namespace Assignment.Characters.Player
{
    public class WorldInteraction : MonoBehaviour
    {
        [SerializeField] GameObject tooltipPanel = default;
        [SerializeField] float interactionRadius = 2f;
        [SerializeField] Color radiusColor = Color.blue;

        private readonly Vector3 cameraCenter = new Vector3(0.5f, 0.5f);

        private IInventorySystem inventorySystem;
        private IPlayerManager playerManager;
        private Camera playerCamera;
        private Text tooltipText;

        private void Awake()
        {
            tooltipText = tooltipPanel.GetComponentInChildren<Text>();
            playerCamera = GetComponentInChildren<Camera>();
            inventorySystem = GetComponentInChildren<IInventorySystem>();
            playerManager = GetComponent<IPlayerManager>();
        }

        private void Start() => tooltipPanel.SetActive(false);
        private void OnDisable() => tooltipPanel.SetActive(false);

        private void Update()
        {
            bool lookingAtPickup = FindNearbyPickup(out IPickupableItem pickup);
            UpdateTooltipUI(lookingAtPickup, pickup);

            if (lookingAtPickup && Input.GetKeyDown(KeyCode.E))
            {
                PickItem(pickup);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = radiusColor;
            Vector3 from = transform.position + Vector3.up * transform.localScale.y / 2;
            Vector3 to = from + Vector3.forward * interactionRadius;
            Gizmos.DrawLine(from, to);
        }

        private void PickItem(IPickupableItem pickup)
        {
            bool itemPicked = inventorySystem.AddItem(pickup.ItemInfo, pickup.Amount);

            if (itemPicked)
            {
                playerManager.OnPickupCollected(pickup.ItemInfo, pickup.Amount);
                pickup.OnItemPicked();
            }
            else
            {
                Debug.LogWarning($"Can't pick up item [{pickup.ItemInfo.Name}], inventory is full!");
            }
        }

        private void UpdateTooltipUI(bool active, IPickupableItem pickup)
        {
            tooltipPanel.SetActive(active);
            if (active)
            {
                tooltipText.text = pickup.ItemInfo.GetTooltip("E");
            }
        }

        private bool FindNearbyPickup(out IPickupableItem pickup)
        {
            Ray ray = playerCamera.ViewportPointToRay(cameraCenter);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, interactionRadius))
            {
                return hitInfo.collider.TryGetComponent(out pickup);
            }
            pickup = null;
            return false;
        }
    }
}