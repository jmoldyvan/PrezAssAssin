using UnityEngine;

public class Phase2ButtonHandler : MonoBehaviour
{
    public void OnPhase2ButtonPressed()
    {
        Debug.Log("Phase 2 Button Pressed!");
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