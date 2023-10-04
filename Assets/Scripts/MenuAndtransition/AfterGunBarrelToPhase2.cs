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
    
    {
        Vector3 exitDoorPosition = new Vector3(72f, 21f, 0);
        Vector3 playerPosition = new Vector3(-3f, 12f, 1.5f); 
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
