using Assignment.Core.Pooling;
using Assignment.Inventory;
using Assignment.Scripts.Core.Pooling;
using Assignment.Weapons;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assignment.Characters.Player.Actions
{
    public class Throw : PlayerAction
    {
        [Range(0, 1)]
        [SerializeField] float viewPortY = 0.4f;
        [SerializeField]
        Camera playerCamera = default;
        private const float viewPortX = 0.5f;
        [SerializeField] Vector3 throwForce = Vector3.zero;
        [SerializeField] GameObject[] throwableObjects = default;
        [SerializeField] Text ammoAmountText = default;
        [SerializeField] InventorySystem inventorySystem = default;

        private IObjectPooler objectPooler;
        private GameObject selectedThrowable;

        private void Awake()
        {
            objectPooler = FindObjectOfType<ObjectPooler>().GetComponent<IObjectPooler>();
        }

        private void Start()
        {
            selectedThrowable = throwableObjects[0];
            UpdateAmmoText();
        }

        private void UpdateAmmoText()
        {
            int ammoInInventory = inventorySystem.ItemCount(selectedThrowable.GetComponent<Grenade>().GetAmmoType());
            ammoAmountText.text = $"AMMO: {ammoInInventory}";
        }

        public override IEnumerator StartAction()
        {
            GameObject throwable = objectPooler.SpawnObject(selectedThrowable, playerCamera.transform.position, playerCamera.transform.rotation);
            throwable.GetComponent<Rigidbody>().AddRelativeForce(throwForce);
            throwable.GetComponent<IPoolableObject>().OnObjectActivation();
            inventorySystem.RemoveItem(selectedThrowable.GetComponent<Grenade>().GetAmmoType());
            yield return new WaitForSeconds(actionStats.ActionFrequency);
        }
    }
}