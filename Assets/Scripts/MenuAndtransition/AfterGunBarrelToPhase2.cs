using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    
    {
        Debug.Log("OnStateExit method called!");
        GameObject playerAndHatchSpawnObjects = GameObject.Find("PlayerAndHatchSpawnObjects");
        // Player spawn boundry

        // 1. Locate the PlayerSpawnBoundary GameObject
        Transform playerSpawnBoundaryTransform = playerAndHatchSpawnObjects.transform.Find("PlayerSpawnBoundry");
        Debug.Log(playerSpawnBoundaryTransform);
        
        // 2. Find the 4 child objects

        Transform PlayerTopLeft = playerSpawnBoundaryTransform.Find("PlayerCornerTopLeft");
        Transform PlayerTopRight = playerSpawnBoundaryTransform.Find("PlayerCornerTopRight");
        Transform PlayerBottomLeft = playerSpawnBoundaryTransform.Find("PlayerCornerBottomLeft");
        Transform PlayerBottomRight = playerSpawnBoundaryTransform.Find("PlayerCornerBottomRight");
        
        // 3. Retrieve the x and y coordinates
        float PlayerSpawnXmin = PlayerBottomLeft.position.x;
        float PlayerSpawnXmax = PlayerTopRight.position.x;
        float PlayerSpawnYmin = PlayerBottomLeft.position.y;
        float PlayerSpawnYmax = PlayerTopLeft.position.y;

        float PlayerRandomXCord = Random.Range(PlayerSpawnXmin, PlayerSpawnXmax);
        float PlayerRandomYCord = Random.Range(PlayerSpawnYmin, PlayerSpawnYmax);

        Vector3 playerPosition = new Vector3(PlayerRandomXCord, PlayerRandomYCord, 1.5f); 

        // ExitDoor(Hatch) spawn boundry

        // 1. Locate the HatchSpawnBoundary GameObject
        Transform HatchSpawnBoundaryTransform = playerAndHatchSpawnObjects.transform.Find("HatchSpawnBoundry");
        Debug.Log(HatchSpawnBoundaryTransform);
        
        // 2. Find the 4 child objects

        Transform HatchTopLeft = HatchSpawnBoundaryTransform.Find("HatchCornerTopLeft");
        Transform HatchTopRight = HatchSpawnBoundaryTransform.Find("HatchCornerTopRight");
        Transform HatchBottomLeft = HatchSpawnBoundaryTransform.Find("HatchCornerBottomLeft");
        Transform HatchBottomRight = HatchSpawnBoundaryTransform.Find("HatchCornerBottomRight");
        
        // 3. Retrieve the x and y coordinates
        float HatchSpawnXmin = HatchBottomLeft.position.x;
        float HatchSpawnXmax = HatchTopRight.position.x;
        float HatchSpawnYmin = HatchBottomLeft.position.y;
        float HatchSpawnYmax = HatchTopLeft.position.y;

        float HatchRandomXCord = Random.Range(HatchSpawnXmin, HatchSpawnXmax);
        float HatchRandomYCord = Random.Range(HatchSpawnYmin, HatchSpawnYmax);


        Vector3 exitDoorPosition = new Vector3(HatchRandomXCord, HatchRandomYCord, 0);  


        Vector3 Phase2ButtonPosition = new Vector3(25f, 25f, -6); 

        Quaternion rotation = Quaternion.identity;

        GameObject createDoor = Instantiate(ExitDoor1, exitDoorPosition, rotation);
        GameObject createPlayer = Instantiate(Player, playerPosition, rotation);
        GameObject createPhase2Button = Instantiate(Phase2Button, Phase2ButtonPosition, rotation);
        GameObject phase2Button1 = GameObject.Find("Phase2Button 1(Clone)");
        if (phase2Button1 != null)
        {
            // Access its child named Phase2Button
            Transform phase2Button = phase2Button1.transform.Find("Phase2Button");
            if (phase2Button != null)
            {
                // Get all children with the tag GameOverButtons and activate them
                foreach (Transform child in phase2Button)
                {
                    if (child.CompareTag("ReadyPhase2"))
                    {
                        child.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    public void SetCameraToMiddleOfLevel()
    {            
        CameraController cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.isPlayerControlEnabled = false;
        if(SceneManager.GetActiveScene().buildIndex == 3 )
        {
                    cameraController.SetAfterGunTransitionSiza(20);
                    cameraController.transform.position = new Vector3(30, 28, Camera.main.transform.position.z);
        }
        if(SceneManager.GetActiveScene().buildIndex == 5 )
        {
                    cameraController.SetAfterGunTransitionSiza(40);
        cameraController.transform.position = new Vector3(20, 24, Camera.main.transform.position.z);
        }
        if(SceneManager.GetActiveScene().buildIndex == 7 )
        {
                    cameraController.SetAfterGunTransitionSiza(30);
        cameraController.transform.position = new Vector3(30, 23, Camera.main.transform.position.z);
        }
        if(SceneManager.GetActiveScene().buildIndex == 9)
        {
            cameraController.SetAfterGunTransitionSiza(32);
        cameraController.transform.position = new Vector3(22, 27, Camera.main.transform.position.z);
        }
        if(SceneManager.GetActiveScene().buildIndex == 11)
        {
            cameraController.SetAfterGunTransitionSiza(50);
        cameraController.transform.position = new Vector3(20, 24, Camera.main.transform.position.z);
        }
    }
}
