using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DissolveOnAnim : StateMachineBehaviour
{
    // Rectangle coordinates
    public float xMin = -8f;
    public float xMax = 1f;
    public float yMin = 8f;
    public float yMax = 14.5f;

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Find and destroy objects within the rectangle
        GameObject[] allTinyMen = GameObject.FindGameObjectsWithTag("TinyMan");

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

        // Dissolve random objects
        int numberOfObjectsToLeave = 30;

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