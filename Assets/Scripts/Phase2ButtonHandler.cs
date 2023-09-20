using UnityEngine;
using UnityEngine.UI;

public class Phase2ButtonHandler : MonoBehaviour
{
    public TinyManAnimationHandler tinyManHandler; // Reference to TinyManAnimationHandler script.

    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        tinyManHandler.StartRemainingTinyManAnimations(); // Start animations for remaining TinyMen.
    }
}