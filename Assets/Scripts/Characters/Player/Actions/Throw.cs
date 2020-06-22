using Assignment.Core.Pooling;
using Assignment.Scripts.Core.Pooling;
using System.Collections;
using UnityEngine;

namespace Assignment.Characters.Player.Actions
{
    public class Throw : PlayerAction
    {
        [Range(0, 1)]
        [SerializeField] float viewPortY = 0.4f;
        [SerializeField]
        Camera playerCamera;
        private const float viewPortX = 0.5f;
        [SerializeField] Vector3 throwForce = Vector3.zero;
        [SerializeField] GameObject[] throwableObjects;

        private IObjectPooler objectPooler;
        private GameObject selectedThrowable;

        private void Awake()
        {
            objectPooler = FindObjectOfType<ObjectPooler>().GetComponent<IObjectPooler>();
        }

        private void Start()
        {
            selectedThrowable = throwableObjects[0];
        }

        public override IEnumerator StartAction()
        {
            GameObject throwable = objectPooler.SpawnObject(selectedThrowable, playerCamera.transform.position, playerCamera.transform.rotation);
            throwable.GetComponent<Rigidbody>().AddRelativeForce(throwForce);
            throwable.GetComponent<IPoolableObject>().OnObjectActivation();
            yield return new WaitForSeconds(actionStats.ActionFrequency);
        }
    }
}