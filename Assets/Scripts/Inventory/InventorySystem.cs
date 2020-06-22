using Assignment.Inventory.ItemSlot;
using Assignment.Pickups;
using Assignment.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Assignment.Inventory
{
    [DisallowMultipleComponent]
    public class InventorySystem : MonoBehaviour, IInventorySystem
    {
        [SerializeField] byte numberOfSlots = 9;

        private List<ISlot> slots = new List<ISlot>();

        #region UNITY METHODS
        private void Awake() => GenerateSlots();
        private void OnEnable()
        {
            slots.ForEach(s =>
            {
                s.OnStackSplit += OnStackSplitted;
                s.MyIconHandler.OnItemDrop += OnItemDropped;
            });
        }
        private void OnDisable()
        {
            slots.ForEach(s =>
            {
                s.OnStackSplit -= OnStackSplitted;
                s.MyIconHandler.OnItemDrop -= OnItemDropped;
            });
        }
        #endregion

        public bool AddItem(ItemStats item, int amount)
        {
            bool success = false;

            int newAmount = amount;
            // Can I store new pickup?
            for (int i = 0, n = slots.Count; i < n; i++)
            {
                newAmount -= slots[i].HowManyCanIStore(item);
                if (newAmount <= 0)
                {
                    success = true;
                    break;
                }
            }

            if (!success) return false;

            // Add item
            newAmount = amount;
            for (int i = 0, n = slots.Count; i < n; i++)
            {
                newAmount -= slots[i].AddStackPortion(item, newAmount);
                if (newAmount == 0) break;
            }
            return success;
        }

        private void GenerateSlots()
        {
            UnityEngine.Object itemSlotPrefab = Resources.Load("Item Slot");

            for (int i = 0; i < numberOfSlots; i++)
            {
                GameObject itemSlot = Instantiate(itemSlotPrefab) as GameObject;

                itemSlot.transform.name = $"Item Slot {i + 1}";
                itemSlot.transform.SetParent(transform, false);
                ISlot slot = itemSlot.GetComponent<ISlot>();
                slot.SlotID = (byte)i;

                slots.Add(slot);
            }
        }

        private void OnItemDropped(ISlot from, List<GameObject> objectsHit)
        {
            // Outside inventory
            if (objectsHit.Count == 0)
            {
                from.DropAll();
                return;
            }
            for (int i = 0, n = objectsHit.Count; i < n; i++)
            {
                // Dropped on the inventory grid
                if (objectsHit[i].TryGetComponent(out IInventorySystem _))
                {
                    return;
                }
                // Dropped on inventory slot
                if (objectsHit[i].TryGetComponent(out ISlot to))
                {
                    MoveItem(from, to);
                    return;
                }
            }
            // Outside inventory
            from.DropAll();
        }

        private void MoveItem(ISlot fromSlot, ISlot toSlot)
        {
            // Item dropped on the initial slot
            if (fromSlot.SlotID == toSlot.SlotID) return;

            if (toSlot.IsEmpty)
            {
                MoveToEmptySlot(fromSlot, toSlot);
            }
            else if (toSlot.StoredItem.Type != fromSlot.StoredItem.Type)
            {
                SwapSlots(fromSlot, toSlot);
            }
            else
            {
                MergeSlots(fromSlot, toSlot);
            }
        }

        private void MergeSlots(ISlot fromSlot, ISlot toSlot)
        {
            if (fromSlot.HowManyCanIStore(fromSlot.StoredItem) == 0
                && toSlot.HowManyCanIStore(toSlot.StoredItem) == 0)
            {
                return;
            }

            int canMergeNum = toSlot.HowManyCanIStore(fromSlot.StoredItem);
            canMergeNum = (canMergeNum >= fromSlot.SlotCount) ? fromSlot.SlotCount : canMergeNum;
            fromSlot.RemoveStackPortion(canMergeNum);
            toSlot.AddStackPortion(fromSlot.StoredItem, canMergeNum);
        }

        private void MoveToEmptySlot(ISlot fromSlot, ISlot toSlot)
        {
            toSlot.AddAll(fromSlot.StoredItem, fromSlot.SlotCount);
            fromSlot.DropAll();
        }

        private void SwapSlots(ISlot fromSlot, ISlot toSlot)
        {
            ItemStats item1 = fromSlot.StoredItem;
            int amount1 = fromSlot.SlotCount;
            ItemStats item2 = toSlot.StoredItem;
            int amount2 = toSlot.SlotCount;

            fromSlot.DropAll();
            fromSlot.AddAll(item2, amount2);
            toSlot.DropAll();
            toSlot.AddAll(item1, amount1);
        }

        private void OnStackSplitted(ISlot slot)
        {
            ISlot emptySlot = slots.Find(s => s.IsEmpty);
            if (emptySlot != null)
            {
                int itemsToMoveNum = slot.SlotCount - slot.SlotCount / 2;
                slot.RemoveStackPortion(itemsToMoveNum);
                emptySlot.AddAll(slot.StoredItem, itemsToMoveNum);
            }
        }

        public int ItemCount(ItemStats item)
        {
            int count = 0;
            for (int i = 0, n = slots.Count; i < n; i++)
            {
                if (slots[i].IsEmpty) continue;
                if (slots[i].StoredItem.Type != item.Type) continue;
                count += slots[i].SlotCount;
            }

            return count;
        }

        public bool RemoveItem(ItemStats item)
        {
            for (int i = 0, n = slots.Count; i < n; i++)
            {
                if (slots[i].IsEmpty) continue;
                if (slots[i].StoredItem.Type != item.Type) continue;
                slots[i].RemoveStackPortion(1);
                return true;
            }

            return false;
        }
    }
}