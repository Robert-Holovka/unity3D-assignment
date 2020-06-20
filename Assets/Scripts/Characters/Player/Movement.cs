using UnityEngine;

namespace Assignment.Characters.Player
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] float speed = 5f;
        [SerializeField] float jumpForce = 100f;

        private Rigidbody rigidBody;

        private Vector3 velocity = Vector3.zero;
        private bool isGrounded = true;
        private bool isJumping = false;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            UpdateVelocity();
            isJumping = Input.GetKeyDown(KeyCode.Space);
        }

        private void FixedUpdate()
        {
            Move();
            Jump();
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