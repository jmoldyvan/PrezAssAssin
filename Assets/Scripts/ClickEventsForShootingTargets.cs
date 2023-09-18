using UnityEngine;

public class ClickEventsForShootingTargets : MonoBehaviour
{

    public void ObjectClicked()
    {
        Debug.Log("ObjectClicked called for " + gameObject.name);

        if (gameObject.tag == "TinyMan")
        {
            Debug.Log("Calling TriggerHeartAnimation for " + gameObject.name);
            TriggerHeartAnimation();
        }
        else if (gameObject.tag == "Prez")
        {
            Debug.Log("Calling StopGame for " + gameObject.name);
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