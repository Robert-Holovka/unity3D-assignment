using Assignment.Characters;
using System.Collections;
using UnityEngine;

namespace Assignment.Zones
{
    public class HealingZone : BaseZone
    {
        [SerializeField] float healthPerSecond = 20f;

        private IRestorable restorable;

        public override void OnZoneEntered(Collider player)
        {
            playerInZone = true;
            if (restorable == null)
            {
                restorable = player.GetComponent<IRestorable>();
            }
            if (audioClipHandler != null)
            {
                audioClipHandler.PlayAudio();
            }

            StartCoroutine(RestoreHealth(healthPerSecond));
        }

        public override void OnZoneLeft()
        {
            if (audioClipHandler != null)
            {
                audioClipHandler.StopAudio();
            }
            playerInZone = false;
            StopAllCoroutines();
        }

        private IEnumerator RestoreHealth(float healthPerSecond)
        {
            while (playerInZone)
            {
                restorable.RestoreHealth(healthPerSecond);
                yield return new WaitForSeconds(1f);
            }
        }
    }
}