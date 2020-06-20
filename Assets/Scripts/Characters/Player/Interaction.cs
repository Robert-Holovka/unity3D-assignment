using Assignment.Pickups;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [SerializeField] GameObject tooltipPanel = default;
    [SerializeField] float interactionRadius = 1f;
    [SerializeField] Color radiusColor = Color.blue;

    private readonly Vector3 cameraCenter = new Vector3(0.5f, 0.5f);
    private new Camera camera;
    private Text tooltipText;

    private void Awake()
    {
        camera = GetComponentInChildren<Camera>();
        tooltipText = tooltipPanel.GetComponentInChildren<Text>();
        tooltipPanel.SetActive(false);
    }

    private void Update()
    {
        Ray ray = camera.ViewportPointToRay(cameraCenter);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, interactionRadius))
        {
            if (hitInfo.collider.TryGetComponent(out IPickupable pickup))
            {
                tooltipText.text = pickup.PickupInfo.GetTooltip("E");
                tooltipPanel.SetActive(true);
                return;
            }
        }
        tooltipPanel.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = radiusColor;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}