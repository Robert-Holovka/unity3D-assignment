using UnityEngine;

namespace Assignment.Characters.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] float mouseSensitivity = 5f;
        [SerializeField] float minPitchDegree = -70f;
        [SerializeField] float maxPitchDegree = 70f;

        private Rigidbody rigidBody;
        private float yaw;
        private float pitch;
        private new Camera camera;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
            camera = GetComponentInChildren<Camera>();
        }

        private void Update()
        {
            yaw = Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch += Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, minPitchDegree, maxPitchDegree);
        }

        private void FixedUpdate()
        {
            RotatePlayer();
            RotateCamera();
        }

        private void RotatePlayer()
        {
            Quaternion newRotation = transform.rotation * Quaternion.Euler(0f, yaw, 0f);
            rigidBody.MoveRotation(newRotation);
        }

        private void RotateCamera() => camera.transform.localRotation = Quaternion.Euler(-pitch, 0f, 0f);
    }
}