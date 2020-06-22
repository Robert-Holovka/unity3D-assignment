using Assignment.Core;
using Assignment.Core.Pooling;
using UnityEngine;

namespace Assignment.Weapons
{
    [RequireComponent(typeof(AreaDamage))]
    [RequireComponent(typeof(Timer))]
    public class Grenade : MonoBehaviour, IPoolableObject
    {
        private LayerMask layerMask;
        private Timer timer;

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
            gameObject.SetActive(false);
        }
    }
}