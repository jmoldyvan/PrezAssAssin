using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Loaders transition;
    public Button[] LevelButtons;
    public void Select (int levelName)
    {
        transition.StartCoroutine(transition.LoadLevel(levelName));
    }

    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);
        for (int i = 0; i < LevelButtons.Length; i++)
        {
            if(i + 1 < levelReached)
                LevelButtons[i].interactable = false;
        }
    }

}
