using UnityEngine;

namespace Assignment.Characters.Player.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float speed = 5f;
        [SerializeField] float jumpForce = 40f;

        private Rigidbody rigidBody;
        private CapsuleCollider playerCollider;
        private bool isJumping;
        private float distanceToGround;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
            playerCollider = GetComponent<CapsuleCollider>();
        }

        private void Start() => distanceToGround = playerCollider.bounds.extents.y;
        private void FixedUpdate() => Jump();

        private void Update()
        {
            Vector3 velocity = UpdateVelocity();
            Move(velocity);
            isJumping = Input.GetKeyDown(KeyCode.Space);
        }

        private Vector3 UpdateVelocity()
        {
            Vector3 horizontalMovement = Input.GetAxisRaw("Horizontal") * transform.right;
            Vector3 verticalMovement = Input.GetAxisRaw("Vertical") * transform.forward;
            Vector3 direction = (horizontalMovement + verticalMovement).normalized;
            return direction * speed;
        }

        private void Move(Vector3 velocity)
        {
            if (velocity == Vector3.zero) return;

            Vector3 offset = velocity * Time.deltaTime;
            rigidBody.MovePosition(transform.position + offset);
        }

        private void Jump()
        {
            if (isJumping && IsGrounded())
            {
                rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        private bool IsGrounded() => Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f);
    }
}