using System.Collections.Generic;
using UnityEngine;

namespace Assignment.Core.Pooling
{
    [RequireComponent(typeof(ObjectPooler))]
    [ExecuteAlways]
    public class IPoolableObjectEditorRestriction : MonoBehaviour
    {
        private ObjectPooler objectPooler;

#if UNITY_EDITOR
        private void Update()
        {
            if (objectPooler == null)
            {
                objectPooler = FindObjectOfType<ObjectPooler>();
            }

            List<ObjectPooler.Pool> pools = objectPooler.Pools;
            if (pools == null || pools.Count == 0) return;

            pools.ForEach((pool) =>
            {
                GameObject prefab = pool.prefab;
                if (prefab == null) return;
                if (prefab.GetComponent<IPoolableObject>() == null)
                {
                    Debug.LogError($"GameObject '{pool.prefab.name}' is missing IPoolableObject component!");
                    pool.prefab = null;
                }
            });
        }
#endif
    }
}