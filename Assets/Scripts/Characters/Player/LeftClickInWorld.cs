using Assignment.Characters.Player.Actions;
using System.Collections;
using UnityEngine;

namespace Assignment.Characters.Player
{
    public class LeftClickInWorld : MonoBehaviour
    {
        [SerializeField] BasePlayerAction[] actions = default;

        private BasePlayerAction selectedAction;
        private bool canStart = true;

        private void Start() => selectedAction = actions[0];

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && canStart)
            {
                StartCoroutine(StartAction());
            }
        }

        private IEnumerator StartAction()
        {
            canStart = false;
            yield return StartCoroutine(selectedAction.StartAction());
            canStart = true;
        }
    }
}