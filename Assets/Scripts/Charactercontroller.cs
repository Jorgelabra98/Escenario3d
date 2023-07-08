using UnityEngine;

public class Charactercontroller : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 5.0f;
    public float gravity = 9.81f;
    public float groundCheckDistance = 0.1f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;
    private Animator animator;
    private bool isGrounded;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Move the character based on the input and speed
        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput);
        movement = transform.TransformDirection(movement);
        movement *= speed;

        // Check if the character is on the ground
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        // Apply gravity to the character
        if (isGrounded)
        {
            moveDirection.y = 0.0f;
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move((movement + moveDirection) * Time.deltaTime);

        // Play the appropriate animation based on the movement
        if (isGrounded)
        {
            if (movement.magnitude > 0.0f)
            {
                animator.SetFloat("Speed", movement.magnitude);
            }
            else
            {
                animator.SetFloat("Speed", 0.0f);
            }

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
                animator.SetBool("Jump", true);
            }
            else
            {
                animator.SetBool("Jump", false);
            }
        }
        else
        {
            animator.SetBool("Jump", true);
        }
    }
}
