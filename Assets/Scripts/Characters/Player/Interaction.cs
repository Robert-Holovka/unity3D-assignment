using Assignment.Inventory;
using Assignment.Pickups;
using UnityEngine;
using UnityEngine.UI;

namespace Assignment.Characters.Player
{
    public class Interaction : MonoBehaviour
    {
        [SerializeField] GameObject tooltipPanel = default;
        [SerializeField] float interactionRadius = 1f;
        [SerializeField] Color radiusColor = Color.blue;

        private readonly Vector3 cameraCenter = new Vector3(0.5f, 0.5f);

        private IInventorySystem inventorySystem;
        private new Camera camera;
        private Text tooltipText;

        private void Awake()
        {
            camera = GetComponentInChildren<Camera>();
            tooltipText = tooltipPanel.GetComponentInChildren<Text>();
            inventorySystem = GetComponentInChildren<IInventorySystem>();
        }

        private void Start()
        {
            tooltipPanel.SetActive(false);
        }

        private void Update()
        {
            bool lookingAtPickup = FindNearbyPickup(out IPickupableItem pickup);
            UpdateTooltipUI(lookingAtPickup, pickup);

            if (lookingAtPickup && Input.GetKeyDown(KeyCode.E))
            {
                PickItem(pickup);
            }
        }

        private void PickItem(IPickupableItem pickup)
        {
            bool itemPicked = inventorySystem.AddItem(pickup);

            if (itemPicked)
            {
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
            Ray ray = camera.ViewportPointToRay(cameraCenter);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, interactionRadius))
            {
                return hitInfo.collider.TryGetComponent(out pickup);
            }
            pickup = null;
            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = radiusColor;
            Gizmos.DrawWireSphere(transform.position, interactionRadius);
        }
    }
}