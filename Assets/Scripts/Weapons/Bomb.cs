using Assignment.Core;
using Assignment.Core.Pooling;
using UnityEngine;

namespace Assignment.Weapons
{
    [RequireComponent(typeof(AreaDamage))]
    [RequireComponent(typeof(Timer))]
    public class Bomb : MonoBehaviour, IPoolableObject
    {
        private LayerMask layerMask;
        private Timer timer;

        #region UNITY METHODS
        private void Awake()
        {
            timer = GetComponent<Timer>();
            layerMask = LayerMask.NameToLayer("Damageable");
        }

        private void OnEnable()
        {
            timer.OnTimerFinish += OnTimerFinished;
        }

        private void OnDisable()
        {
            timer.OnTimerFinish -= OnTimerFinished;
        }

        void Start()
        {
            timer.StartTimer();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (((1 << collision.gameObject.layer) & (1 << layerMask)) != 0)
            {
                timer.EndEarly();
            }
        }
        #endregion

        private void OnTimerFinished()
        {
            Explode();
        }

        private void Explode()
        {
            Detonate();
            // TODO: particle effect
            Destroy(gameObject);
        }

        private void Detonate()
        {
            GetComponent<AreaDamage>().DealDamage();
        }

        public void OnObjectActivation()
        {
            throw new System.NotImplementedException();
        }
    }
}