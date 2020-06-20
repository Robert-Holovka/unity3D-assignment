using Assignment.Core;
using UnityEngine;
using UnityEngine.AI;

namespace Assignment.Characters.Enemy
{
    [RequireComponent(typeof(Timer))]
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] float waypointDwellingTime = 3f;

        // Cached
        private NavMeshAgent navMeshAgent;
        private PatrolPath patrolPath;
        private Timer timer;

        private bool isWaiting;
        private int currentWaypointIndex;

        #region UNITY METHODS
        private void Awake()
        {
            patrolPath = transform.parent.GetComponentInChildren<PatrolPath>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            timer = GetComponent<Timer>();
        }

        private void Start()
        {
            navMeshAgent.SetDestination(patrolPath.GetWaypoint(currentWaypointIndex));
            timer.SetDuration(waypointDwellingTime);
        }

        private void OnEnable()
        {
            timer.OnTimerFinish += MoveNext;
        }

        private void OnDisable()
        {
            timer.OnTimerFinish -= MoveNext;
        }

        private void Update()
        {
            PatrolBehaviour();
        }
        #endregion

        private void PatrolBehaviour()
        {
            if (isWaiting) return;
            isWaiting = patrolPath.AtWaypoint(transform.position, currentWaypointIndex);
            if (isWaiting)
            {
                timer.StartTimer();
            }
        }

        private void MoveNext()
        {
            isWaiting = false;
            navMeshAgent.SetDestination(patrolPath.GetNextWaypoint(currentWaypointIndex, out int nextIndex));
            currentWaypointIndex = nextIndex;
        }
    }
}