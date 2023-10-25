using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterGunBarrelToPhase2 : StateMachineBehaviour
{

    public GameObject ExitDoor1;
    public GameObject Player;
    public GameObject Phase2Button;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // Assuming "Image", "Image (1)", and "Circle" are direct children of the GameObject with the Animator.
            Transform parentTransform = animator.gameObject.transform;

            GameObject image = parentTransform.Find("Image")?.gameObject;
            GameObject image1 = parentTransform.Find("Image (1)")?.gameObject;
            GameObject circle = parentTransform.Find("circle")?.gameObject;
            GameObject Prez = GameObject.FindGameObjectWithTag("Prez");
            


            if (image != null) Destroy(image);
            if (image1 != null) Destroy(image1);
            if (circle != null) Destroy(circle);
            if (Prez != null) Destroy(Prez);

            SetCameraToMiddleOfLevel();


        }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    
    {Debug.Log("OnStateExit method called!");

        // 1. Locate the PlayerSpawnBoundary GameObject
        GameObject playerAndHatchSpawnObjects = GameObject.Find("PlayerAndHatchSpawnObjects");
        Debug.Log(playerAndHatchSpawnObjects);
        Transform playerSpawnBoundaryTransform = playerAndHatchSpawnObjects.transform.Find("PlayerSpawnBoundry");
        Debug.Log(playerSpawnBoundaryTransform);
        
        // 2. Find the 4 child objects

        Transform topLeft = playerSpawnBoundaryTransform.Find("PlayerCornerTopLeft");
        Debug.Log("topLeft: " + topLeft);
        Transform topRight = playerSpawnBoundaryTransform.Find("PlayerCornerTopRight");
        Debug.Log("topRight: " + topRight);
        Transform bottomLeft = playerSpawnBoundaryTransform.Find("PlayerCornerBottomLeft");
        Debug.Log("bottomLeft: " + bottomLeft);
        Transform bottomRight = playerSpawnBoundaryTransform.Find("PlayerCornerBottomRight");
        Debug.Log("bottomRight: " + bottomRight);

        
        // 3. Retrieve the x and y coordinates
        float PlayerSpawnXmin = bottomLeft.position.x;
        float PlayerSpawnXmax = topRight.position.x;
        float PlayerSpawnYmin = bottomLeft.position.y;
        float PlayerSpawnYmax = topLeft.position.y;

        float PlayerRandomXCord = Random.Range(PlayerSpawnXmin, PlayerSpawnXmax);
        float PlayerRandomYCord = Random.Range(PlayerSpawnYmin, PlayerSpawnYmax);
        Vector3 exitDoorPosition = new Vector3(72f, 21f, 0);

        Vector3 playerPosition = new Vector3(PlayerRandomXCord, PlayerRandomYCord, 1.5f); 
        Vector3 Phase2ButtonPosition = new Vector3(25f, 25f, -6); 
        Quaternion rotation = Quaternion.identity;

        GameObject createDoor = Instantiate(ExitDoor1, exitDoorPosition, rotation);
        GameObject createPlayer = Instantiate(Player, playerPosition, rotation);
        GameObject createPhase2Button = Instantiate(Phase2Button, Phase2ButtonPosition, rotation);
    }

    public void SetCameraToMiddleOfLevel()
    {            
        CameraController cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.isPlayerControlEnabled = false;
        cameraController.SetAfterGunTransitionSiza(24);
        cameraController.transform.position = new Vector3(33, 23, Camera.main.transform.position.z);
    }
}
