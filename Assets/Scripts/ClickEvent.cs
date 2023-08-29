using UnityEngine;

public class ClickEvent : MonoBehaviour
{
    public void ObjectClicked()
    {
        Debug.Log("ObjectClicked called for " + gameObject.name);

        if (gameObject.tag == "TinyMan")
        {
            Debug.Log("Calling TriggerHeartAnimation for " + gameObject.name);
            TriggerHeartAnimation();
        }
        else if (gameObject.tag == "Prez")
        {
            Debug.Log("Calling StopGame for " + gameObject.name);
            StopGame();
        }
    }

    void TriggerHeartAnimation()
    {
        Debug.Log("TriggerHeartAnimation called.");
        GameManager.Instance.LoseHeart();
    }

    void StopGame()
    {
        Debug.Log("StopGame called.");
        Time.timeScale = 0;
    }
}