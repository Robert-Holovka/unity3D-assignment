using UnityEngine;

namespace Assignment.Core
{
    public class PersistableObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject[] persistableObjects = default;

        private void Awake()
        {
            if (FindObjectsOfType<PersistableObjectSpawner>().Length > 1)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            GameObject parentInScene = new GameObject("Persistable Objects");
            DontDestroyOnLoad(parentInScene);

            for (int i = 0, n = persistableObjects.Length; i < n; i++)
            {
                GameObject go = Instantiate(persistableObjects[i], Vector3.zero, Quaternion.identity);
                go.transform.parent = parentInScene.transform;
            }
        }
    }
}