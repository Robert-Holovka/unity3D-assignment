using UnityEngine;
using UnityEngine.Events;

namespace Assignment.Core.Pooling
{
    public interface IPoolableObject
    {
        void OnObjectActivation(UnityAction<GameObject> callback);
    }
}