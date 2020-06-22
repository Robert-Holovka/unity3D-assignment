using Assignment.Characters;
using System.Collections;
using UnityEngine;

namespace Assignment.Zones
{
    public class HealingZone : BaseZone
    {
        [SerializeField] float healthPerSecond = 20f;

        private IRestorable restorable;

        protected override void OnZoneEntered(Collider player)
        {
            playerInZone = true;
            if (restorable == null)
            {
                restorable = player.GetComponent<IRestorable>();
            }

            audioClipTranquilizer.PlayAudio();
            StartCoroutine(RestoreHealth(healthPerSecond));
        }

        protected override void OnZoneLeft()
        {
            playerInZone = false;
            audioClipTranquilizer.StopAudio();
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