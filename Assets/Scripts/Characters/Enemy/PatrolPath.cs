using UnityEngine;

namespace Assignment.Characters.Enemy
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] float waypointGizmoRadius = 0.3f;
        [SerializeField] Color waypointColor = Color.red;
        [SerializeField] Color pathColor = Color.yellow;

        private void OnDrawGizmos()
        {
            for (int i = 0, n = transform.childCount; i < n; i++)
            {
                Vector3 currentChild = GetWaypoint(i);
                Vector3 nextChild = GetNextWaypoint(i, out _);
                DrawWaypoint(currentChild);
                DrawPath(currentChild, nextChild);
            }
        }

        public Vector3 GetWaypoint(int i) => transform.GetChild(i).position;
        public int GetNextWaypointIndex(int i) => (i + 1) % transform.childCount;

        public Vector3 GetNextWaypoint(int i, out int nextIndex)
        {
            nextIndex = GetNextWaypointIndex(i);
            return GetWaypoint(nextIndex);
        }

        public bool AtWaypoint(Vector3 position, int i)
        {
            Vector3 waypoint = GetWaypoint(i);
            position.y = 0f;
            waypoint.y = 0f;

            return Vector3.Distance(position, waypoint) <= Mathf.Epsilon;
        }

        private void DrawPath(Vector3 currentChild, Vector3 nextChild)
        {
            Gizmos.color = pathColor;
            Gizmos.DrawLine(currentChild, nextChild);
        }

        private void DrawWaypoint(Vector3 currentChild)
        {
            Gizmos.color = waypointColor;
            Gizmos.DrawSphere(currentChild, waypointGizmoRadius);
        }
    }
}