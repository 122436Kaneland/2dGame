using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public string characterName;
    public bool isActive = false; // Is this character active and controllable?
    public float moveSpeed = 4f; // Normal movement speed
    public float sprintMultiplier = 2f; // Sprint multiplier (adjust as needed)
    public float dashDuration = 1f; // Duration of the dash in seconds
    public float dashCooldown = 5f; // Cooldown before dash can be used again

    private float currentDashTime = 0f; // Current time left for dash
    private float lastDashTime = -Mathf.Infinity; // Last time the dash was used
    private Vector3 offScreenPosition = new Vector3(-1000f, -1000f, 0f); // A position off-screen to hide inactive character

    private Animator animator; // Reference to the Animator component

    private void Start()
    {
        // Get the Animator component on the same GameObject
        animator = GetComponent<Animator>();

        if (isActive)
        {
            EnableCharacter();
        }
        else
        {
            DisableCharacter();
        }
    }

    private void Update()
    {
        if (isActive)
        {
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
        // Get the current horizontal and vertical inputs (WASD or arrow keys)
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Create a movement vector from horizontal and vertical input
        Vector2 input = new Vector2(horizontalInput, verticalInput);

        // Normalize the vector to ensure consistent movement speed, even diagonally
        if (input.magnitude > 0)
        {
            input.Normalize();  // Normalize the vector
        }

        // Check if the dash duration is active and reduce the dash time
        if (currentDashTime > 0)
        {
            // Dash movement (increased speed for dash duration)
            transform.Translate(input * moveSpeed * sprintMultiplier * Time.deltaTime);
            currentDashTime -= Time.deltaTime; // Decrease dash time
        }
        else
        {
            // Regular movement if dash is not active
            transform.Translate(input * moveSpeed * Time.deltaTime);
        }

        // Check if Shift key is pressed and dash is off cooldown
        if (Input.GetKey(KeyCode.LeftShift) && Time.time >= lastDashTime + dashCooldown)
        {
            // Start dash if Shift is pressed and cooldown is over
            if (currentDashTime <= 0)
            {
                currentDashTime = dashDuration; // Activate dash duration
                lastDashTime = Time.time; // Reset the last dash time
            }
        }

        // Update the Animator parameters
        UpdateAnimatorParameters(input);
    }

    // Method to update Animator parameters
    private void UpdateAnimatorParameters(Vector2 input)
    {
        // Set the DirectionX parameter based on horizontal input (1 for right, -1 for left)
        animator.SetFloat("DirectionX", input.x);

        // Set the DirectionY parameter based on vertical input (1 for up, -1 for down)
        animator.SetFloat("DirectionY", input.y);
    }

    public void EnableCharacter()
    {
        isActive = true;
        gameObject.SetActive(true);  // Ensure the character is visible
    }

    public void DisableCharacter()
    {
        isActive = false;
        gameObject.SetActive(false);  // Disable visibility of the character
        transform.position = offScreenPosition; // Optionally move off-screen
    }
}
