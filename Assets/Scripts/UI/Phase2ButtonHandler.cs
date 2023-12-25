using UnityEngine;

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

        PlayerMovement PlayerMovementScript = Player.GetComponent<PlayerMovement>();
        PlayerMovementScript.ToggleMovement(true);

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
}