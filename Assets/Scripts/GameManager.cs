using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject TinyMan;
    public GameObject Prez;
    public List<Image> Hearts;
    public TilemapCollider2D tilemapCollider;
    public Tilemap floorTilemap;
    public Tilemap wallTilemap;
    private int lostHeartsCount = 0;
    private PlayerDeath playerDeathScript;
    [SerializeField] public Sprite[] sceneSprites;
        public PrezSpawner prezSpawner;

    private Vector3Int floorTilemapRange = new Vector3Int(-50, 150, 0);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int layerMask = 1 << 6; 

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, layerMask);

            if (hit.collider != null)
            {
                Debug.Log("This object was clicked: " + hit.collider.gameObject.name);
                hit.collider.gameObject.GetComponent<ClickEventsForShootingTargets>().ObjectClicked();
            }
            else
            {
                Debug.Log("No object was hit.");
            }
        }
    }

    void Start()
    {
         Debug.Log(SceneManager.GetActiveScene().buildIndex);
        GameObject prezInstance = prezSpawner.SpawnPrez(floorTilemapRange, floorTilemap, wallTilemap);
        if(SceneManager.GetActiveScene().buildIndex == 3 )
        {
            CreatePeople(20);
        }
        if(SceneManager.GetActiveScene().buildIndex == 5 )
        {
            CreatePeople(100);
        }
        if(SceneManager.GetActiveScene().buildIndex == 7 )
        {
            CreatePeople(200);
        }
        if(SceneManager.GetActiveScene().buildIndex == 9)
        {
            CreatePeople(200);
        }
        if(SceneManager.GetActiveScene().buildIndex == 11)
        {
            CreatePeople(300);
        }
        

    }

    public void CreatePeople(int numberOfPeople)
    {
        GameObject TinyMenSpawnBoundry = GameObject.Find("TinyMenSpawnBoundry");
        // Player spawn boundry

        // 1. Locate the TinyMenSpawnBoundry GameObject
        Transform TinyMenSpawnBoundryTransform = TinyMenSpawnBoundry.transform.Find("TinyMenSpawnBoundry");
        Debug.Log(TinyMenSpawnBoundryTransform);
        
        // 2. Find the 4 child objects

        Transform TinyMenTopLeft = TinyMenSpawnBoundryTransform.Find("TinyMenBoundryTopLeft");
        Transform TinyMenTopRight = TinyMenSpawnBoundryTransform.Find("TinyMenBoundryTopRight");
        
        // 3. Retrieve the x and y coordinates
        int TinyMenSpawnXmin = Mathf.FloorToInt(TinyMenTopLeft.position.x);
        int TinyMenSpawnXmax = Mathf.CeilToInt(TinyMenTopRight.position.x);
        int TinyMenSpawnYmax = Mathf.CeilToInt(TinyMenTopLeft.position.y);

        for (int i = 0; i < numberOfPeople; i++)
        {
            Vector3Int randomTilePosition = new Vector3Int(
                Random.Range(TinyMenSpawnXmin, TinyMenSpawnXmax + 1),
                Random.Range(-50, TinyMenSpawnYmax + 1),
                0
            );

            if (floorTilemap.HasTile(randomTilePosition) && !wallTilemap.HasTile(randomTilePosition))
            {
                Vector3 spawnPosition = floorTilemap.GetCellCenterWorld(randomTilePosition);
                Instantiate(TinyMan, spawnPosition, Quaternion.identity);
            }
            else
            {
                i--;  // Decrement i to try again
            }
        }
}

    public void ShuffleList<T>(IList<T> list)
    {
        var rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    // Function to spawn a single Prez object// Function to spawn a single Prez object
//     void SpawnPrez()
//     {
        
//         GameObject prezInstance = null;
// Debug.Log(prezInstance);
//         for (int i = 0; i < 1; i++)  // We only want to spawn one Prez
//         {
//             Vector3Int randomTilePosition = new Vector3Int(
//                 Random.Range(-50, 150),
//                 Random.Range(-50, 150),
//                 0
//             );

//             if (floorTilemap.HasTile(randomTilePosition) && !wallTilemap.HasTile(randomTilePosition))
//             {
//                 Vector3 spawnPosition = floorTilemap.GetCellCenterWorld(randomTilePosition);
//                 prezInstance = Instantiate(Prez, spawnPosition, Quaternion.identity);
//                Debug.Log($"Prez instantiated at position: {spawnPosition}");
//                 break;  // Exit the loop once the Prez is spawned
//             }
//         }

// if (prezInstance != null)
// {
//     Debug.Log("Prez instance is not null");
//     SpriteRenderer spriteRenderer = prezInstance.GetComponent<SpriteRenderer>();
//     if (spriteRenderer != null)
//     {
//         Debug.Log("SpriteRenderer found");
//         if (spriteRenderer.sprite == null)
//         {
//             Debug.Log("SpriteRenderer sprite is null");
//         }
//     }
//     else
//     {
//         Debug.Log("SpriteRenderer component not found on the instantiated Prez");
//     }
// }
// else
// {
//     Debug.Log("Prez instance is null after instantiation");
// }
//     }

    public void LoseHeart(){
        Debug.Log("LoseHeart called in GameManager.");

        var fullHeart = Hearts.FirstOrDefault(x => x.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Default"));
        
        if (fullHeart != null) {
            Debug.Log("Full heart found.");
            fullHeart.gameObject.GetComponent<Animator>().Play("Flickering");
            fullHeart.sprite = Resources.Load<Sprite>("Images/Hearts/EmptyHeart");

            lostHeartsCount++; // Increment the lost hearts count

            if (lostHeartsCount == 3) { // Check if lost hearts count is 3
                GameOver();
            }
        }
        }

    public void GameOver()
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
                randomMovementScript.allowTracking = false;
            }
        }
        

        GameObject PlayerObject = GameObject.FindGameObjectWithTag("Player");
        if(PlayerObject)
        {
            playerDeathScript = PlayerObject.GetComponent<PlayerDeath>();
            PlayerObject.GetComponent<Animator>().Play("PlayerDeath");
            playerDeathScript.PlayerDeathFunction();   

            StartCoroutine(ActivateButtonsAfterDelay(5f));
        }

        else
        {
            GameOverToNoPlayer();  
        }   

    }

    public void GameOverToNoPlayer()
    {
        CameraController cameraController = Camera.main.GetComponent<CameraController>();
        if (cameraController != null)
        {
            GameObject PrezObject = GameObject.FindGameObjectWithTag("Prez");
            Destroy(PrezObject);
            cameraController.isPlayerControlEnabled = false;
            cameraController.StartTransitionPanning();
            cameraController.MoveToTarget(new Vector3(33, 23, Camera.main.transform.position.z));
        }  
        StartCoroutine(ActivateButtonsAfterDelay(5f));

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
                    if (child.CompareTag("GameOverButtons"))
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
        // maybe play animation of player dying/ destroy player object/ StartTransitionPanning() camera 
        // instantiate try again button
        // instantiate main menu button
}