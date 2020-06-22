using Assignment.ScriptableObjects;
using System.Collections;
using UnityEngine;

namespace Assignment.Characters.Player.Actions
{
    public abstract class PlayerAction : MonoBehaviour
    {
        [SerializeField] protected ActionStats actionStats;
        public abstract IEnumerator StartAction();
    }
}
