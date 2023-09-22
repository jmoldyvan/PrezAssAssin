using UnityEngine;

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

        CameraController cameraPanToTarget = Camera.main.GetComponent<CameraController>();
        cameraPanToTarget.StartTransitionPanning(); 

        GameObject gunTransition = GameObject.FindGameObjectWithTag("GunTransition");
        Loaders playGunBarrelAnim = gunTransition.GetComponent<Loaders>();
        playGunBarrelAnim.TransitionPhaseFunction();
    }
}