using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public Loaders transition;
    public void Select (int levelName)
    {
        transition.StartCoroutine(transition.LoadLevel(levelName));
    }


}
