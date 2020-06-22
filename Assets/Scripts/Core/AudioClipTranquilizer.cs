using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Core
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioClipTranquilizer : MonoBehaviour
    {
        [Range(0, 1)]
        [SerializeField] float volumeTranquilizeRate = 0.25f;

        private AudioSource audioSource;

        private void Awake() => audioSource = GetComponent<AudioSource>();

        public void PlayAudio()
        {
            StopAllCoroutines();
            audioSource.volume = 1.0f;
            audioSource.Play();
        }

        public void StopAudio()
        {
            if (volumeTranquilizeRate == 0)
            {
                audioSource.Stop();
            }
            else
            {
                StartCoroutine(TranquilizeVolume(volumeTranquilizeRate));
            }
        }

        private IEnumerator TranquilizeVolume(float volumeTranquilizeRate)
        {
            while (audioSource.volume >= 0)
            {
                audioSource.volume -= volumeTranquilizeRate;
                yield return new WaitForSeconds(volumeTranquilizeRate);
            }
            audioSource.Stop();
        }
    }
}