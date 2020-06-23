using Assignment.Core;
using Assignment.Core.Pooling;
using Assignment.ScriptableObjects;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assignment.Weapons
{
    [RequireComponent(typeof(AreaDamage))]
    [RequireComponent(typeof(Timer))]
    public class Grenade : MonoBehaviour, IPoolableObject
    {
        [SerializeField] ItemStats ammoType = default;

        public ItemStats AmmoType { get => ammoType; }

        private LayerMask damageableLayerMask;
        private Timer timer;
        private AudioSource explosionSFX;
        private ParticleSystem explosionVFX;
        private MeshRenderer meshRenderer;
        private UnityAction<GameObject> explodedCallback;

        private void Awake()
        {
            damageableLayerMask = LayerMask.NameToLayer("Damageable");
            explosionSFX = GetComponent<AudioSource>();
            explosionVFX = GetComponentInChildren<ParticleSystem>();
            meshRenderer = GetComponent<MeshRenderer>();
            timer = GetComponent<Timer>();
        }

        #region UNITY METHODS
        private void Start() => timer.StartTimer();
        private void OnEnable() => timer.OnTimerFinish += OnTimerFinished;
        private void OnDisable() => timer.OnTimerFinish -= OnTimerFinished;

        private void OnCollisionEnter(Collision collision)
        {
            if (((1 << collision.gameObject.layer) & (1 << damageableLayerMask)) != 0)
            {
                timer.EndEarly();
            }
        }
        #endregion

        public void OnObjectActivation(UnityAction<GameObject> explodedCallback)
        {
            this.explodedCallback = explodedCallback;
            meshRenderer.enabled = true;
            timer.StartTimer();
        }

        private void OnTimerFinished()
        {
            GetComponent<AreaDamage>().DealDamage();

            meshRenderer.enabled = false;
            explosionSFX.PlayOneShot(explosionSFX.clip);
            explosionVFX.Play();

            StartCoroutine(DeactivateObject(explosionSFX.clip.length));
        }

        private IEnumerator DeactivateObject(float time)
        {
            yield return new WaitForSeconds(time);
            gameObject.SetActive(false);
            explodedCallback(gameObject);
        }
    }
}