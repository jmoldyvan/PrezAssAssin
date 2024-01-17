using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrezAnim : MonoBehaviour
{
    public Animator prezDeathAnim;
    public IEnumerator PlayAnim()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        switch(sceneIndex)
        {
            case 3: prezDeathAnim.SetTrigger("Trigger 1"); break;
            case 5: prezDeathAnim.SetTrigger("Trigger 2"); break;
            case 7: prezDeathAnim.SetTrigger("Trigger 3"); break;
            case 9: prezDeathAnim.SetTrigger("Trigger 4"); break;
            case 11: prezDeathAnim.SetTrigger("Trigger 4"); break;
        }

        yield return null;

    }
    public void PlayAnimFunction()
    {
        
        StartCoroutine(PlayAnim());
    }
}
