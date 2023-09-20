using System.Collections.Generic;
using UnityEngine;

public class TinyManAnimationHandler : MonoBehaviour
{
    public GameObject secretServicePrefab; // Drag your SecretService prefab here in the inspector.

    // List to store remaining TinyMan GameObjects.
    private List<GameObject> remainingTinyMen;

    // Method to start the TinyMan animations.
    public void StartRemainingTinyManAnimations()
    {
        remainingTinyMen = new List<GameObject>(GameObject.FindGameObjectsWithTag("TinyMan"));
        
        foreach (GameObject tinyMan in remainingTinyMen)
        {
            Animator animator = tinyMan.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("YourTinyManTrigger"); // Replace "YourTinyManTrigger" with the correct trigger name.
            }
        }
    }

    // Method to be called when TinyMan animation is complete. This will instantiate SecretService.
    public void OnTinyManAnimationComplete(GameObject tinyMan)
    {
        // Instantiate SecretService GameObject.
        GameObject secretService = Instantiate(secretServicePrefab, tinyMan.transform.position, tinyMan.transform.rotation);

        // Start the SecretService animation.
        Animator ssAnimator = secretService.GetComponent<Animator>();
        if (ssAnimator != null)
        {
            ssAnimator.SetTrigger("YourSecretServiceTrigger"); // Replace with the correct trigger name.
        }

        // Destroy the TinyMan GameObject.
        Destroy(tinyMan);
    }
}