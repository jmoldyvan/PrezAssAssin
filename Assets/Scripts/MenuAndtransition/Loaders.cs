using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loaders : MonoBehaviour
{
    public Animator transition;
    public Animator phaseTransition;
    public float transitionTime = 1f;
    public float phaseTransitionTime = 3f;

    public void NewGame()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 2));
    }
    public void SelectLevelSelectScreen()
    {
        
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void BackToMainMenu()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
    }
    public void QuitGame()
    {
        
        Application.Quit();
    }

    public IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
    public IEnumerator TransitionPhase()
    {
                      
        yield return new WaitForSeconds(transitionTime);
        phaseTransition.SetTrigger("PhaseTrigger");
    }

    public void TransitionPhaseFunction()
    {
        
        StartCoroutine(TransitionPhase());
    }

}
