using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
public class ClickEventsForShootingTargets : MonoBehaviour
{

    public void ObjectClicked()
    {
        

        if (gameObject.tag == "TinyMan")
        {
            
            TriggerHeartAnimation();
        }
        else if (gameObject.tag == "Prez")
        {
            
            CickedOnPrez();
        }
    }

    void TriggerHeartAnimation()
    {
        
        GameManager.Instance.LoseHeart();
    }

    void CickedOnPrez()
    {
        GameObject[] TinyMans = GameObject.FindGameObjectsWithTag("TinyMan");

        foreach (GameObject obj in TinyMans)
        {
            RandomMovement randomMovement = obj.GetComponent<RandomMovement>();
            if (randomMovement != null)
            {
                randomMovement.TogglePause(true);  // To pause
                // randomMovement.TogglePause(false); // To resume
            }
        }

        GameObject prezClone = GameObject.FindGameObjectWithTag("Prez");
        RandomMovement PrezMovement = prezClone.GetComponent<RandomMovement>();
        
        PrezMovement.TogglePause(true);

        GameObject GameyObby = GameObject.FindGameObjectWithTag("GameObject");
        PrezAnim PrezKillCam = GameyObby.GetComponent<PrezAnim>();
        Debug.LogError(PrezKillCam);

        PrezKillCam.PlayAnimFunction();

        CameraController cameraPanToTarget = Camera.main.GetComponent<CameraController>();
        cameraPanToTarget.StartTransitionPanning(); 

        GameObject gunTransition = GameObject.FindGameObjectWithTag("GunTransition");
        Loaders playGunBarrelAnim = gunTransition.GetComponent<Loaders>();
        playGunBarrelAnim.TransitionPhaseFunction();
    }
}