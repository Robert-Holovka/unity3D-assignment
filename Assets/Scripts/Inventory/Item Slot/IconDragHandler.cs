using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assignment.Inventory.ItemSlot
{
    public class IconDragHandler : MonoBehaviour, IIconHandler
    {
        public event UnityAction<ISlot, List<GameObject>> OnItemDrop;

        private GraphicRaycaster graphicRaycaster;
        private GameObject playerCanvas;
        private Transform parent;
        private Vector3 iconInitialPos;

        private void Awake()
        {
            playerCanvas = GameObject.FindWithTag("PlayerCanvas");
            graphicRaycaster = playerCanvas.GetComponent<GraphicRaycaster>();
            iconInitialPos = transform.localPosition;
            parent = transform.parent;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            transform.SetParent(playerCanvas.transform);
            transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;

            transform.SetParent(parent);
            transform.localPosition = iconInitialPos;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            graphicRaycaster.Raycast(eventData, raycastResults);
            OnItemDrop?.Invoke(GetComponentInParent<ISlot>(), raycastResults.Select(rr => rr.gameObject).ToList());
        }
    }
}