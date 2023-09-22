using UnityEngine;

public class TinyManAnimationHandler : MonoBehaviour
{
    public GameObject secretServicePrefab; // Assign this in Unity Editor
    public Transform tinyManTransform;

    public void StartTinyManAnimations()
    {
        GameObject[] allTinyMen = GameObject.FindGameObjectsWithTag("TinyMan");
        foreach(GameObject tinyMan in allTinyMen)
        {
            Animator anim = tinyMan.GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetTrigger("StartAnimation");
            }
        }
    }

public void OnTinyManAnimationComplete()
{
    Vector3 position = tinyManTransform.position;
    Quaternion rotation = tinyManTransform.rotation;
    GameObject secretService = Instantiate(secretServicePrefab, position, rotation);

    Animator anim = secretService.GetComponent<Animator>();
    if (anim != null)
    {
        anim.SetTrigger("StartAnimation");
    }

    Destroy(gameObject);
}
}