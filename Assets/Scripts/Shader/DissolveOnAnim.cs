using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
public class DissolveOnAnim : StateMachineBehaviour
{

        // Rectangle coordinates
        public float xMin = 0;
        public float xMax = 0;
        public float yMin = 0;
        public float yMax = 0;

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        int numberOfObjectsToLeave = 0;

        // Dissolve random objects
        if(SceneManager.GetActiveScene().buildIndex == 2 )
            {
                numberOfObjectsToLeave = 10;
            }
        if(SceneManager.GetActiveScene().buildIndex == 3 )
            {
                numberOfObjectsToLeave = 30;
            }
        if(SceneManager.GetActiveScene().buildIndex == 4 )
            {
                numberOfObjectsToLeave = 30;
            }
        if(SceneManager.GetActiveScene().buildIndex == 5)
            {
               numberOfObjectsToLeave = 60;
            }
        if(SceneManager.GetActiveScene().buildIndex == 6)
            {
                numberOfObjectsToLeave = 100;
            }

        // Find and destroy objects within the rectangle
        GameObject[] allTinyMen = GameObject.FindGameObjectsWithTag("TinyMan");
        GameObject ClearAreaForPlayer = GameObject.Find("ClearAreaForPlayer");
        // Player spawn boundry

        // 1. Locate the PlayerSpawnBoundary GameObject
        Transform ClearAreaForPlayerBoundryTransform = ClearAreaForPlayer.transform.Find("ClearAreaForPlayerBoundry");
        
        // 2. Find the 4 child objects

        Transform ClearAreaTopLeft = ClearAreaForPlayerBoundryTransform.Find("ClearAreaCornerTopLeft");
        Transform ClearAreaTopRight = ClearAreaForPlayerBoundryTransform.Find("ClearAreaCornerTopRight");
        Transform ClearAreaBottomLeft = ClearAreaForPlayerBoundryTransform.Find("ClearAreaCornerBottomLeft");
        Transform ClearAreaBottomRight = ClearAreaForPlayerBoundryTransform.Find("ClearAreaCornerBottomRight");

        float xMin = ClearAreaBottomLeft.position.x;
        float xMax = ClearAreaTopRight.position.x;
        float yMin = ClearAreaBottomLeft.position.y;
        float yMax = ClearAreaTopLeft.position.y;

        foreach (GameObject tinyMan in allTinyMen)
        {
            Vector3 position = tinyMan.transform.position;

            if (position.x >= xMin && position.x <= xMax && position.y >= yMin && position.y <= yMax)
            {
                Destroy(tinyMan);
            }
        }

        // Update the allTinyMen array after destroying the objects in the rectangle
        allTinyMen = GameObject.FindGameObjectsWithTag("TinyMan");

        if (allTinyMen.Length > numberOfObjectsToLeave)
        {
            int numberOfObjectsToDestroy = allTinyMen.Length - numberOfObjectsToLeave;

            var objectsToDestroy = allTinyMen.OrderBy(x => Random.value).Take(numberOfObjectsToDestroy);

            foreach (var tinyMan in objectsToDestroy)
            {
                DissolveState dissolveScript = tinyMan.GetComponent<DissolveState>();

                if (dissolveScript != null)
                {
                    dissolveScript.StartDissolve();
                }
            }
        }
    }
}