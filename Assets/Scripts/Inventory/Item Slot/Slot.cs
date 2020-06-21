using Assignment.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assignment.Inventory.ItemSlot
{
    public class Slot : MonoBehaviour, ISlot, IPointerClickHandler
    {
        [SerializeField] Image itemImage;
        [Header("Stack Size Text")]
        [SerializeField] Text stackText;
        [SerializeField] Color defaultTextColor;
        [SerializeField] Color slotFullTextColor;

        public event UnityAction<ISlot> OnStackSplit;
        public byte SlotID { get; set; }
        public bool IsEmpty => StoredItem == null;
        public int SlotCount { get; private set; }
        public IIconHandler MyIconHandler { get; private set; }
        public ItemStats StoredItem { get; private set; }

        #region UNITY METHODS
        private void Awake()
        {
            MyIconHandler = GetComponentInChildren<IIconHandler>();
        }

        private void Start()
        {
            DropAll();
        }
        #endregion

        #region PUBLIC METHODS

        public void DropAll()
        {
            SlotCount = 0;
            StoredItem = null;
            itemImage.enabled = false;
            UpdateStackText("");
        }

        /// <summary>
        /// Tries to add as much as possible.
        /// </summary>
        /// <param name="newItem">Item info</param>
        /// <param name="amount">Item stack size</param>
        /// <returns>Number of items added</returns>
        public int AddStackPortion(ItemStats newItem, int amount)
        {
            int storedNum = 0;
            int canStoreNum = HowManyCanIStore(newItem);
            if (canStoreNum > 0)
            {
                storedNum = (amount >= canStoreNum) ? canStoreNum : amount;
                AddAll(newItem, storedNum);
            }
            return storedNum;
        }

        /// <summary>
        /// Adds whole item stack or nothing.
        /// </summary>
        /// <param name="newItem">Item info</param>
        /// <param name="amount">Item stack size</param>
        /// <returns>True if operation was successful, false otherwise</returns>
        public bool AddAll(ItemStats newItem, int amount)
        {
            if (!CanAddItem(newItem, amount)) return false;

            if (StoredItem == null)
            {
                InitSlot(newItem, amount);
            }
            else
            {
                SlotCount += amount;
                UpdateStackText(SlotCount.ToString());
            }
            return true;
        }

        public bool RemoveStackPortion(int amount)
        {
            if (StoredItem == null) return false;
            if (amount > SlotCount) return false;

            if (amount == SlotCount)
            {
                DropAll();
            }
            else
            {
                SlotCount -= amount;
                UpdateStackText(SlotCount.ToString());
            }
            return true;
        }

        public int HowManyCanIStore(ItemStats newItem)
        {
            if (IsEmpty) return newItem.MaxStack;
            if (StoredItem.Type != newItem.Type) return 0;
            int n = StoredItem.MaxStack - SlotCount;
            return (n > 0) ? n : 0;
        }
        #endregion

        #region PRIVATE METHODS
        private void InitSlot(ItemStats newItem, int amount)
        {
            StoredItem = newItem;
            SlotCount = amount;
            UpdateStackText(SlotCount.ToString());
            itemImage.sprite = newItem.Icon;
            itemImage.enabled = true;
        }

        private void UpdateStackText(string text)
        {
            stackText.text = text;
            stackText.color = (IsEmpty || (SlotCount < StoredItem.MaxStack)) ? defaultTextColor : slotFullTextColor;
        }

        private bool CanAddItem(ItemStats newItem, int amount)
        {
            if (StoredItem == null) return true;
            if (newItem.Type != StoredItem.Type) return false;
            return (SlotCount + amount) <= StoredItem.MaxStack;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right
                && Input.GetKey(KeyCode.LeftShift))
            {
                if (SlotCount > 1)
                {
                    OnStackSplit?.Invoke(this);
                }
            }
        }
        #endregion
    }
}