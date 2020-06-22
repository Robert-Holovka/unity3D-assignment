using Assignment.Characters;
using System.Collections;
using UnityEngine;

namespace Assignment.Zones
{
    public class DamageZone : BaseZone
    {
        [SerializeField] float damagePerSecond = 15f;

        private IDamageable damageable;

        public override void OnZoneEntered(Collider player)
        {
            playerInZone = true;
            if (damageable == null)
            {
                damageable = player.GetComponent<IDamageable>();
            }
            if (audioClipHandler != null)
            {
                audioClipHandler.PlayAudio();
            }

            StartCoroutine(ReduceHealth(damagePerSecond));
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

        private IEnumerator ReduceHealth(float healthPerSecond)
        {
            while (playerInZone)
            {
                damageable.TakeDamage(healthPerSecond);
                yield return new WaitForSeconds(1f);
            }
        }
    }
}