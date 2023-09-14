using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loaders : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void NewGame()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 2));
    }
    public void SelectLevelSelectScreen()
    {
        Debug.Log("coroutine go1");
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void BackToMainMenu()
    {
        Debug.Log("coroutine go1");
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
    }
    public void QuitGame()
    {
        Debug.Log("QUITE");
        Application.Quit();
    }

    public IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        Debug.Log("start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
