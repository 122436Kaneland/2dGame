using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    public CharacterController twig; // Reference to the Twig character controller
    public CharacterController leaf; // Reference to the Leaf character controller
    public GameObject smokeEffectPrefab; // Reference to the smoke effect prefab

    public float cooldownTime = 1f; // Cooldown time in seconds
    private float lastSwapTime = -Mathf.Infinity; // The time the last swap occurred (initialized to a large negative value)

    private void Update()
    {
        // Check if the "P" key is pressed and if the cooldown has passed
        if (Input.GetKeyDown(KeyCode.P) && Time.time >= lastSwapTime + cooldownTime)
        {
            SwitchCharacters();
        }
    }

    private void SwitchCharacters()
    {
        // Determine the position of the character who is being swapped
        Vector3 swapPosition;

        if (twig.isActive)
        {
            // Instantiate the smoke effect at Twig's position
            swapPosition = twig.transform.position;
            GameObject smokeEffect = Instantiate(smokeEffectPrefab, swapPosition, Quaternion.identity);
            Destroy(smokeEffect, .8f); // Destroy smoke effect after 1 second (adjust based on your animation duration)

            // Swap characters
            twig.DisableCharacter();
            leaf.EnableCharacter();
            leaf.transform.position = swapPosition; // Place Leaf where Twig was standing
        }
        else
        {
            // Instantiate the smoke effect at Leaf's position
            swapPosition = leaf.transform.position;
            GameObject smokeEffect = Instantiate(smokeEffectPrefab, swapPosition, Quaternion.identity);
            Destroy(smokeEffect, .8f); // Destroy smoke effect 

            // Swap characters
            leaf.DisableCharacter();
            twig.EnableCharacter();
            twig.transform.position = swapPosition; // Place Twig where Leaf was standing
        }

        // Update the last swap time
        lastSwapTime = Time.time;
    }
}