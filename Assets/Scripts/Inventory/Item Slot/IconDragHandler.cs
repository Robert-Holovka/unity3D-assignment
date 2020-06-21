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
        private Canvas canvas;
        private CanvasGroup canvasGroup;
        private GraphicRaycaster graphicRaycaster;
        private Transform parent;
        private Vector3 iconInitialPos;

        public event UnityAction<ISlot, List<GameObject>> OnItemDrop;

        private void Awake()
        {
            iconInitialPos = transform.localPosition;
            parent = transform.parent;
            canvas = FindObjectOfType<Canvas>();
            canvasGroup = GetComponent<CanvasGroup>();
            graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            transform.SetParent(canvas.transform);
            transform.SetAsLastSibling();
            canvasGroup.blocksRaycasts = false;
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

            canvasGroup.blocksRaycasts = true;
        }
    }
}