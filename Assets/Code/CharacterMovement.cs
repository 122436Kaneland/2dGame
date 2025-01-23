using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5f; // Adjust this for your character's movement speed
    private Animator animator;
    private Vector2 moveDirection;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get input for movement (horizontal and vertical)
        float horizontal = Input.GetAxis("Horizontal"); // Left/Right movement (A/D or Arrow keys)
        float vertical = Input.GetAxis("Vertical"); // Up/Down movement (W/S or Arrow keys)

        // Normalize movement direction to ensure consistent speed
        moveDirection = new Vector2(horizontal, vertical).normalized;

        // Update the Animator parameters based on movement
        animator.SetFloat("Speed", moveDirection.magnitude); // Use the magnitude of the direction vector (length)
        animator.SetFloat("Direction", vertical); // Use the vertical input for Direction (Up/Down)
    }
}