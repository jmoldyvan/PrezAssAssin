using UnityEngine;
using System.Collections;

public class Phase2ButtonHandler : MonoBehaviour
{
    public void OnPhase2ButtonPressed()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        GameObject actualbutton = GameObject.FindGameObjectWithTag("phasebutton");

        if (actualbutton != null)
        {
            // Destroy the "actualbutton" game object
            Destroy(actualbutton);
        }

        // Start the coroutine to enable PlayerMovement after 2 seconds
        StartCoroutine(EnablePlayerMovementAfterDelay(Player, 2f));

        GameObject[] allTinyMen = GameObject.FindGameObjectsWithTag("TinyMan");

        foreach (var tinyMan in allTinyMen)
        {
            Animator anim = tinyMan.GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetTrigger("StartAnimation");
            }
        }
    }

    IEnumerator EnablePlayerMovementAfterDelay(GameObject player, float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(3f);

        // Enable PlayerMovement
        PlayerMovement PlayerMovementScript = player.GetComponent<PlayerMovement>();
        if (PlayerMovementScript != null)
        {
            PlayerMovementScript.ToggleMovement(true);
        }
    }
}