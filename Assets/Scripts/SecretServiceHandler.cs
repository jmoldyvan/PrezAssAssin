using UnityEngine;

public class SecretServiceHandler : MonoBehaviour
{
    public Animator animator; // Attach the Animator component in the inspector.

    public CameraController cameraController;

    void Start()
    {
        if (animator != null)
        {
            // Assuming you've set up an animation trigger called "YourSecretServiceTrigger" in the animator.
            animator.SetTrigger("StartAnimation"); 
        }
    }
        private void Awake()
    {
        // Find and link the camera controller if not set.
        if (!cameraController)
        {
            cameraController = FindObjectOfType<CameraController>();
        }
    }

    public void TriggerCameraZoomToPlayer()
    {
        if (cameraController)
        {
            cameraController.ZoomToTargetWithTag("Player");
        }
    }
}