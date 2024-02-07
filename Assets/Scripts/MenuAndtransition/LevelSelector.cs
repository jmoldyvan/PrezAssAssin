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
        for (int i = 0; i < LevelButtons.Length; i++)
        {
            LevelButtons[i].interactable = false;
        }
    }

}
