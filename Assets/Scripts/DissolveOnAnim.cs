using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveOnAnim : StateMachineBehaviour
{
    public GameObject TinyMan;

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("DissolveOnAnim: OnStateExit Called");

        if(TinyMan == null) 
        {
            Debug.LogWarning("TinyMan GameObject is not assigned!");
            return;
        }

        DissolveState dissolveScript = TinyMan.GetComponent<DissolveState>();

        if (dissolveScript == null) 
        {
            Debug.LogWarning("No DissolveState script found on TinyMan!");
            return;
        }

        Debug.Log("DissolveOnAnim: Starting dissolve process");
        dissolveScript.StartDissolve();
    }
}