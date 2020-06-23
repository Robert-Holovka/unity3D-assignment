using Assignment.ScriptableObjects;
using System.Collections;
using UnityEngine;

namespace Assignment.Characters.Player.Actions
{
    public abstract class BasePlayerAction : MonoBehaviour
    {
        [SerializeField] protected ActionStats actionStats;
        public abstract IEnumerator StartAction();
    }
}