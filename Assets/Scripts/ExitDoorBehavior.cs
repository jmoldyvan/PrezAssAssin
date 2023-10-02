using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ExitDoorBehavior : MonoBehaviour
{
    public GameObject Player;
    public Animator animator;
    public GameObject BackToMainMenuPrefab;
    public GameObject NextLevelPrefab;
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
        Debug.Log("Trigger entered by: " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            Player = other.gameObject;  // Set Player to the colliding object
            DoSlowMotion(0.1f);  // Initiating slow motion.
            StartCoroutine(ShrinkPlayer());
        }
    }

    IEnumerator ShrinkPlayer()
    {
        Vector3 originalScale = Player.transform.localScale;
        Vector3 targetScale = Vector3.zero;  // Target scale is zero (fully shrunk)
        float duration = 1.5f;  // Duration of 1 second
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;  // Use unscaled time so it works even when time is slowed down
            float t = elapsedTime / duration;
            Player.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            yield return null;
        }

        Player.transform.localScale = targetScale;  // Ensure the final scale is set correctly
        PauseGame();
    }

    void PauseGame()
    {
    GameObject[] EnemySecretServiceObjects = GameObject.FindGameObjectsWithTag("TinyMan");

    foreach (GameObject EnemySecretServiceObject in EnemySecretServiceObjects)
    {
        // Get the RandomMovement1 script component from the GameObject
        RandomMovement1 randomMovementScript = EnemySecretServiceObject.GetComponent<RandomMovement1>();

        // If the RandomMovement1 script was found
        if(randomMovementScript != null)
        {
            // Call the TogglePause function, passing true to pause the object's movement
            randomMovementScript.TogglePause(true);
        }
        else
        {
            Debug.LogError("RandomMovement1 script not found on GameObject with tag RandomMover.");
        }
    }

    Destroy(Player);
    animator.SetTrigger("LevelFinished");
    Invoke("SpawnUIElements", 3f);
    }

    void SpawnUIElements()
    {
        Instantiate(BackToMainMenuPrefab, buttonsSpawnPoint.position, Quaternion.identity);
        Instantiate(NextLevelPrefab, buttonsSpawnPoint.position + Vector3.right * 2f, Quaternion.identity);
        Instantiate(CreditsTextPrefab, buttonsSpawnPoint.position + Vector3.up * 2f, Quaternion.identity);
    }
    
    // This method could be used to revert slow motion effects gradually.
    void Update()
    {
        if (Time.timeScale < 1f)
        {
            Time.timeScale += (1f / 2f) * Time.unscaledDeltaTime;  // 2 seconds to revert to normal speed.
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            Time.fixedDeltaTime = originalFixedDeltaTime * Time.timeScale;
        }
    }
}