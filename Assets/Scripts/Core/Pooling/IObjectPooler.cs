using UnityEngine;

namespace Assignment.Scripts.Core.Pooling
{
    public interface IObjectPooler
    {
        GameObject SpawnObject(GameObject prefab);
        GameObject SpawnObject(GameObject prefab, Vector3 position);
        GameObject SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation);
    }
}