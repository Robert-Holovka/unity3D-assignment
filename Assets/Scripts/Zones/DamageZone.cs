using Assignment.Characters;
using System.Collections;
using UnityEngine;

namespace Assignment.Zones
{
    public class DamageZone : BaseZone
    {
        [SerializeField] float damagePerSecond = 15f;

        private IDamageable damageable;

        protected override void OnZoneEntered(Collider player)
        {
            playerInZone = true;
            if (damageable == null)
            {
                damageable = player.GetComponent<IDamageable>();
            }

            audioClipTranquilizer.PlayAudio();
            StartCoroutine(ReduceHealth(damagePerSecond));
        }

        protected override void OnZoneLeft()
        {
            playerInZone = false;
            audioClipTranquilizer.StopAudio();
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