using UnityEngine;

public class SecretServiceHandler : MonoBehaviour
{
    public Animator animator; // Attach the Animator component in the inspector.

    void Start()
    {
        if (animator != null)
        {
            // Assuming you've set up an animation trigger called "YourSecretServiceTrigger" in the animator.
            animator.SetTrigger("StartAnimation"); 
        }
    }
}