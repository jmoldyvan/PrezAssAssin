using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public void PlayerDeathFunction() 
    {
        GameObject PlayerObject = GameObject.FindGameObjectWithTag("Player");
        PlayerMovement PlayerMovementScript = PlayerObject.GetComponent<PlayerMovement>();
        PlayerMovementScript.ToggleMovement(false);
        if (PlayerObject != null)
        {
            Animator playerAnimator = PlayerObject.GetComponent<Animator>();
            if (playerAnimator != null)
            {
                    playerAnimator.SetTrigger("PlayerDeath");
            }
            else
            {
                Debug.LogError("Animator not found on the Player object.");
            }
        }
        else
        {
            Debug.LogError("Player object not found.");
        }
    }

       public void ZoomOutToViewWholeLevel()
    {
        CameraController cameraController = Camera.main.GetComponent<CameraController>();
        if (cameraController != null)
        {
            GameObject PlayerObject = GameObject.FindGameObjectWithTag("Player");
            Destroy(PlayerObject);
            cameraController.isPlayerControlEnabled = false;
            cameraController.StartTransitionPanning();
            cameraController.MoveToTarget(new Vector3(33, 23, Camera.main.transform.position.z));
        }        

    }
}
