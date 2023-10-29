using UnityEngine;

public class Phase2ButtonHandler : MonoBehaviour
{
    public void OnPhase2ButtonPressed()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
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