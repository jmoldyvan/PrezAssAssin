using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class ExitDoorBehavior : MonoBehaviour
{
    public GameObject Player;
    // public Animator animator;
    public GameObject BackToMainMenuPrefab;
    // public GameObject NextLevelPrefab;
    public GameObject CreditsTextPrefab;
    public Transform buttonsSpawnPoint;
    private float originalFixedDeltaTime;

    // This method could be used to initiate slow motion.
    public void DoSlowMotion(float slowDownFactor)
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = originalFixedDeltaTime * slowDownFactor;
    }

    void Awake()
    {
        originalFixedDeltaTime = Time.fixedDeltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject Playerplayer = GameObject.FindGameObjectWithTag("Player");
            PlayerMovement PlayerMovementScript = Playerplayer.GetComponent<PlayerMovement>();
            PlayerMovementScript.ToggleMovement(false);
            Player = other.gameObject;  // Set Player to the colliding object
            DoSlowMotion(0.1f);  // Initiating slow motion.
            StartCoroutine(ShrinkPlayer());
            PauseGame();
        }
    }

    IEnumerator ShrinkPlayer()
    {
        // Debug.Log("Shrinking player...");
        
        Vector3 originalScale = Player.transform.localScale;
        Vector3 targetScale = Vector3.zero;  // Target scale is zero (fully shrunk)
        float duration = 1.5f;  // Duration of 1 second
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float t = elapsedTime / duration;
            Player.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            // Debug.Log("Current Scale: " + Player.transform.localScale); // Add this line
            yield return null;
        }

        Player.transform.localScale = targetScale;  // Ensure the final scale is set correctly
        // Debug.Log("Player has been shrunk.");
        
        Destroy(Player);
        StartCoroutine(ActivateButtonsAfterDelay(3.5f));
    }

    void PauseGame()
    {
        GameObject[] tinyManObjects = GameObject.FindGameObjectsWithTag("TinyMan");

    foreach (GameObject tinyManObject in tinyManObjects)
    {
        if (tinyManObject  != null)
        {
            // Get the RandomMovement1 script component from the GameObject
            RandomMovement1 randomMovementScript = tinyManObject.GetComponent<RandomMovement1>();

            // If the RandomMovement1 script was found
            if (randomMovementScript != null)
            {
                // Call the TogglePause function, passing true to pause the object's movement
                randomMovementScript.TogglePause(true);
                Debug.Log("Paused movement for: " + tinyManObject.name);
            }
            else
            {
                Debug.LogError("RandomMovement1 script not found on GameObject with name: " + tinyManObject.name);
            }
        }
        else
        {
            Debug.LogError("GameObject with name: " + tinyManObject.name + " not found.");
        }
    }

    StartCoroutine(ActivateButtonsAfterDelay(3.5f));    
    }

    void Update()
    {
        if (Time.timeScale < 1f)
        {
            Time.timeScale += (1f / 2f) * Time.unscaledDeltaTime;  // 2 seconds to revert to normal speed.
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            Time.fixedDeltaTime = originalFixedDeltaTime * Time.timeScale;
        }
    }
        IEnumerator ActivateButtonsAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Find the Phase2Button 1 object
        GameObject phase2Button1 = GameObject.Find("Phase2Button 1");
        if (phase2Button1 != null)
        {
            // Access its child named Phase2Button
            Transform phase2Button = phase2Button1.transform.Find("Phase2Button");
            if (phase2Button != null)
            {
                // Get all children with the tag GameOverButtons and activate them
                foreach (Transform child in phase2Button)
                {
                    if (child.CompareTag("NextLevelButtons"))
                    {
                        child.gameObject.SetActive(true);
                    }
                    if (child.CompareTag("MainMenuButton"))
                    {
                        child.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}

