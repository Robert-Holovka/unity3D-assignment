using Assets.Scripts.Core;
using UnityEngine;

namespace Assignment.Zones
{
    [RequireComponent(typeof(ZoneTrigger))]
    public abstract class BaseZone : MonoBehaviour
    {
        protected ZoneTrigger zoneTrigger;
        protected AudioClipTranquilizer audioClipTranquilizer;
        protected bool playerInZone = false;

        private void Awake()
        {
            audioClipTranquilizer = GetComponent<AudioClipTranquilizer>();
            zoneTrigger = GetComponent<ZoneTrigger>();
        }

        private void OnEnable()
        {
            zoneTrigger.OnZoneEnter += OnZoneEntered;
            zoneTrigger.OnZoneLeave += OnZoneLeft;
        }

        private void OnDisable()
        {
            zoneTrigger.OnZoneEnter -= OnZoneEntered;
            zoneTrigger.OnZoneLeave -= OnZoneLeft;
        }

        protected abstract void OnZoneEntered(Collider other);
        protected abstract void OnZoneLeft();
    }
}