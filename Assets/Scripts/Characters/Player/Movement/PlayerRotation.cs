using UnityEngine;

namespace Assignment.Characters.Player.Movement
{
    public class PlayerRotation : MonoBehaviour
    {
        [SerializeField] float mouseSensitivity = 5f;
        [SerializeField] float minPitchDegree = -70f;
        [SerializeField] float maxPitchDegree = 70f;

        private new Camera camera;
        private float pitch;

        private void Awake() => camera = GetComponentInChildren<Camera>();

        private void Update()
        {
            if (Time.timeScale == 0f) return;

            float yaw = Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;

            RotatePlayer(yaw);
            RotateCamera();
        }

        private void RotatePlayer(float yaw) => transform.Rotate(Vector3.up * yaw);

        private void RotateCamera()
        {
            pitch = Mathf.Clamp(pitch, minPitchDegree, maxPitchDegree);
            camera.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        }
    }
}