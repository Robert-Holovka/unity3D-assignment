using UnityEngine;
using UnityEngine.Events;

namespace Assignment.Zones
{
    public class ZoneTrigger : MonoBehaviour
    {
        public UnityAction<Collider> OnZoneEnter;
        public UnityAction OnZoneLeave;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnZoneEnter?.Invoke(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnZoneLeave?.Invoke();
            }
        }
    }
}