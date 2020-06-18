using UnityEngine;

namespace Assignment.Player.Mechanics
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] float speed = 5f;
        [SerializeField] float jumpForce = 100f;

        [Header("Camera")]
        [SerializeField] float mouseSensitivity = 5f;
        [SerializeField] float minPitchDegree = -80f;
        [SerializeField] float maxPitchDegree = 80f;

        private Rigidbody rigidBody;

        private Vector3 velocity = Vector3.zero;
        private bool isGrounded = true;
        private bool isJumping = false;
        private float yaw;
        private float pitch;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            UpdateVelocity();
            isJumping = Input.GetKeyDown(KeyCode.Space);
            yaw = Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch = Input.GetAxis("Mouse Y") * mouseSensitivity;
        }

        private void FixedUpdate()
        {
            Move();
            Jump();
            RotatePlayer();
            RotateCamera();
        }

        private void UpdateVelocity()
        {
            Vector3 horizontalMovement = Input.GetAxisRaw("Horizontal") * transform.right;
            Vector3 verticalMovement = Input.GetAxisRaw("Vertical") * transform.forward;
            Vector3 direction = (horizontalMovement + verticalMovement).normalized;
            velocity = direction * speed;
        }

        private void Move()
        {
            if (velocity == Vector3.zero) return;

            Vector3 offset = velocity * Time.fixedDeltaTime;
            rigidBody.MovePosition(transform.position + offset);
        }

        private void Jump()
        {
            if (isJumping && isGrounded)
            {
                rigidBody.AddRelativeForce(new Vector3(0f, jumpForce));
            }
        }

        private void RotatePlayer()
        {
            Quaternion newRotation = transform.rotation * Quaternion.Euler(0f, yaw, 0f);
            rigidBody.MoveRotation(newRotation);
        }

        private void RotateCamera()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ground"))
            {
                isGrounded = true;
                Debug.Log("Okinuo sam ovo");
            }
        }
    }
}