using Assignment.Core;
using Assignment.Core.Pooling;
using Assignment.ScriptableObjects;
using System.Collections;
using UnityEngine;

namespace Assignment.Weapons
{
    [RequireComponent(typeof(AreaDamage))]
    [RequireComponent(typeof(Timer))]
    public class Grenade : MonoBehaviour, IPoolableObject
    {
        [SerializeField] ItemStats ammoType;
        private LayerMask layerMask;
        private Timer timer;
        private AudioSource explosionSFX;
        private ParticleSystem explosionVFX;
        private MeshRenderer meshRenderer;

        public ItemStats GetAmmoType()
        {
            return ammoType;
        }

        private void Awake()
        {
            timer = GetComponent<Timer>();
            layerMask = LayerMask.NameToLayer("Damageable");
            explosionSFX = GetComponent<AudioSource>();
            explosionVFX = GetComponentInChildren<ParticleSystem>();
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            timer.StartTimer();
        }

        private void OnEnable()
        {
            timer.OnTimerFinish += OnTimerFinished;
        }

        private void OnDisable()
        {
            timer.OnTimerFinish -= OnTimerFinished;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (((1 << collision.gameObject.layer) & (1 << layerMask)) != 0)
            {
                timer.EndEarly();
            }
        }

        private void OnTimerFinished()
        {
            Explode();
        }

        private void Explode()
        {
            Detonate();
            // TODO: particle effect

            OnObjectDeactivation();
        }

        private void Detonate()
        {
            GetComponent<AreaDamage>().DealDamage();
        }

        public void OnObjectActivation()
        {
            if (timer == null)
            {
                timer = GetComponent<Timer>();
            }
            if (layerMask.value == 0)
            {
                layerMask = LayerMask.NameToLayer("Damageable");
            }
            timer.StartTimer();
            //timer.OnTimerFinish += OnTimerFinished;
        }

        public void OnObjectDeactivation()
        {
            // timer.OnTimerFinish -= OnTimerFinished;
            meshRenderer.enabled = false;
            explosionSFX.PlayOneShot(explosionSFX.clip);
            explosionVFX.Play();

            StartCoroutine(DeactivateObject(explosionSFX.clip.length));
        }

        private IEnumerator DeactivateObject(float time)
        {
            yield return new WaitForSeconds(time);
            gameObject.SetActive(false);
        }
    }
}