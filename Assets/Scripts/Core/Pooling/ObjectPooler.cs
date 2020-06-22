using Assignment.Scripts.Core.Pooling;
using System.Collections.Generic;
using UnityEngine;

namespace Assignment.Core.Pooling
{
    [RequireComponent(typeof(IPoolableObjectEditorRestriction))]
    public class ObjectPooler : MonoBehaviour, IObjectPooler
    {
        [System.Serializable]
        public class Pool
        {
            public GameObject prefab;
            public int poolSize;
        }
        public List<Pool> Pools;

        private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

        private void Start() => InitPools();

        #region Public Methods
        public void ReturnToPool(GameObject prefab)
        {
            if (!ContainsPool(prefab)) return;
            if (!IsObjectPoolable(prefab)) return;

            string poolTag = prefab.name;
            Queue<GameObject> pool = poolDictionary[poolTag];

            prefab.SetActive(false);
            pool.Enqueue(prefab);
        }

        public GameObject SpawnObject(GameObject prefab)
        {
            return SpawnObject(prefab, Vector3.zero);
        }

        public GameObject SpawnObject(GameObject prefab, Vector3 position)
        {
            return SpawnObject(prefab, position, Quaternion.identity);
        }

        public GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if (!ContainsPool(prefab))
            {
                return null;
            }
            if (IsPoolEmpty(prefab))
            {
                return null;
            }

            string poolTag = prefab.name;
            Queue<GameObject> pool = poolDictionary[poolTag];

            GameObject gameObject = pool.Dequeue();
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;
            gameObject.SetActive(true);

            return gameObject;
        }
        #endregion

        #region Private Methods
        private bool IsPoolEmpty(GameObject prefab)
        {
            string poolTag = prefab.name;
            Queue<GameObject> pool = poolDictionary[poolTag];
            if (pool.Count == 0)
            {
                Debug.LogError($"Pool for the type '{poolTag}' is empty.");
                return true;
            }
            return false;
        }

        private bool ContainsPool(GameObject prefab)
        {
            string poolTag = prefab.name;
            if (!poolDictionary.ContainsKey(poolTag))
            {
                Debug.LogError($"Pool for the type '{poolTag}' not found.");
                return false;
            }
            return true;
        }

        private void InitPools()
        {
            foreach (Pool pool in Pools)
            {
                if (pool.prefab == null)
                {
                    Debug.LogWarning("Pool is missing a prefab.");
                    continue;
                }

                GameObject parentInScene = CreateContainerInScene(pool);
                Queue<GameObject> pooledObjects = InstantiateObjects(pool, parentInScene);

                string poolTag = pool.prefab.name;
                poolDictionary[poolTag] = pooledObjects;
            }
        }

        private Queue<GameObject> InstantiateObjects(Pool pool, GameObject parentInScene)
        {
            Queue<GameObject> pooledObjects = new Queue<GameObject>();

            for (int i = 0, n = pool.poolSize; i < n; i++)
            {
                GameObject gameObject = Instantiate(pool.prefab);
                gameObject.transform.parent = parentInScene.transform;
                if (!IsObjectPoolable(gameObject))
                {
                    Debug.LogError($"GameObject '{pool.prefab.name}' is missing IPoolableObject component!");
                    break;
                }
                gameObject.SetActive(false);
                pooledObjects.Enqueue(gameObject);
            }

            return pooledObjects;
        }

        private GameObject CreateContainerInScene(Pool pool)
        {
            GameObject parentInScene = new GameObject();
            parentInScene.transform.parent = transform;
            parentInScene.name = pool.prefab.name;
            return parentInScene;
        }

        private bool IsObjectPoolable(GameObject gameObject) => gameObject.GetComponent<IPoolableObject>() != null;
        #endregion
    }
}