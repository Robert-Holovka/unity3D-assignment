using Assignment.Core.Pooling;
using Assignment.Inventory;
using Assignment.ScriptableObjects;
using Assignment.Scripts.Core.Pooling;
using Assignment.Weapons;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assignment.Characters.Player.Actions
{
    public class ThrowGrenadeAction : BasePlayerAction
    {
        [SerializeField] Text ammoAmountText = default;
        [Header("Throwing")]
        [SerializeField] float throwForce = 10f;
        [SerializeField] Transform startPoint = default;
        [SerializeField] Grenade grenade = default;

        private IInventorySystem inventorySystem;
        private IObjectPooler objectPooler;
        private int grenadeCount;

        private void Awake()
        {
            objectPooler = FindObjectOfType<ObjectPooler>();
            inventorySystem = FindObjectOfType<InventorySystem>();
        }
        private void OnEnable()
        {
            inventorySystem.OnItemAdd += OnItemAdded;
            inventorySystem.OnItemRemove += OnItemRemoved;
            grenadeCount = inventorySystem.ItemCount(grenade.AmmoType);
            UpdateAmmoText();
        }

        private void OnDisable()
        {
            inventorySystem.OnItemAdd -= OnItemAdded;
            inventorySystem.OnItemRemove -= OnItemRemoved;
        }

        public override IEnumerator StartAction()
        {
            if (grenadeCount != 0)
            {
                ThrowGrenade();
                inventorySystem.RemoveItem(grenade.AmmoType);
                yield return new WaitForSeconds(actionStats.ActionFrequency);
            }
            else
            {
                yield return null;
            }
        }

        private void ThrowGrenade()
        {
            GameObject throwable = objectPooler.SpawnObject(
                grenade.gameObject,
                startPoint.position,
                startPoint.rotation);
            throwable.GetComponent<Rigidbody>()
                .AddForce(startPoint.forward * throwForce, ForceMode.VelocityChange);
            throwable
                .GetComponent<IPoolableObject>()
                .OnObjectActivation(grenadeGO => objectPooler.ReturnToPool(grenadeGO));
        }

        private void OnItemAdded(ItemStats item, int amount)
        {
            if (item.Type == grenade.AmmoType.Type)
            {
                grenadeCount += amount;
                UpdateAmmoText();
            }
        }

        private void OnItemRemoved(ItemStats item, int amount)
        {
            if (item.Type == grenade.AmmoType.Type)
            {
                grenadeCount -= amount;
                UpdateAmmoText();
            }
        }

        private void UpdateAmmoText() => ammoAmountText.text = $"AMMO: {grenadeCount}";
    }
}