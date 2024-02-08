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
    public static GameManager Instance { get; private set; }
    public GameObject TinyMan;
    public GameObject Prez;
    public List<Image> Hearts;
    public TilemapCollider2D tilemapCollider;
    public Tilemap floorTilemap;
    public Tilemap wallTilemap;
    private int lostHeartsCount = 0;
    private PlayerDeath playerDeathScript;
    [SerializeField] public Sprite[] sceneSprites;
    [SerializeField] public Sprite[] tinymansceneSprites;
        public PrezSpawner prezSpawner;
        public TinyManSpawner tinyManSpawner;

        public GameObject Phase2Button;
        public bool canClickTinyMan = true;
    public string nextLevel = "Level02";
    public int LevelToUnlock = 2;

    private Vector3Int floorTilemapRange = new Vector3Int(-90, 250, 0);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

       public void Start()
    {
        SpawnPrez();
        SpawnTinyManBasedOnScene();
    }

    private void SpawnPrez()
    {
        GameObject prezInstance = prezSpawner.SpawnPrez(floorTilemapRange, floorTilemap, wallTilemap);
    }

    private void SpawnTinyManBasedOnScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        int numberOfPeople = 0;

        switch(sceneIndex)
        {
            case 3: numberOfPeople = 20; break;
            case 5: numberOfPeople = 100; break;
            case 7:numberOfPeople = 200; break;
            case 9: numberOfPeople = 200; break;
            case 11: numberOfPeople = 300; break;
        }

        if(numberOfPeople > 0)
        {
            tinyManSpawner.SpawnTinyMan(numberOfPeople, floorTilemapRange, floorTilemap, wallTilemap);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canClickTinyMan)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int layerMask = 1 << 6; 

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, layerMask);

            if (hit.collider != null)
            {
                // Debug.Log("This object was clicked: " + hit.collider.gameObject.name);
                hit.collider.gameObject.GetComponent<ClickEventsForShootingTargets>().ObjectClicked();
            }
            else
            {
                Debug.Log("No object was hit.");
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

    public void LoseHeart(){
        // Debug.Log("LoseHeart called in GameManager.");

        var fullHeart = Hearts.FirstOrDefault(x => x.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Default"));
        
        if (fullHeart != null) {
            // Debug.Log("Full heart found.");
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
                            Vector3 Phase2ButtonPosition = new Vector3(25f, 25f, -6);   
        Quaternion rotation = Quaternion.identity;
        GameObject createPhase2Button = Instantiate(Phase2Button, Phase2ButtonPosition, rotation);
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
                Vector3 Phase2ButtonPosition = new Vector3(25f, 25f, -6); 
        Quaternion rotation = Quaternion.identity;
        GameObject createPhase2Button = Instantiate(Phase2Button, Phase2ButtonPosition, rotation);
        StartCoroutine(ActivateButtonsAfterDelay(5f));

    }

        IEnumerator ActivateButtonsAfterDelay(float delay)
    {
        // Vector3 Phase2ButtonPosition = new Vector3(25f, 25f, -6); 
        // Quaternion rotation = Quaternion.identity;
        // GameObject createPhase2Button = Instantiate(Phase2Button, Phase2ButtonPosition, rotation);
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Find the Phase2Button 1 object
        GameObject phase2Button1 = GameObject.Find("Phase2Button 1(Clone)");
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

    public void WinLevel()
    {
        PlayerPrefs.SetInt("levelReached", LevelToUnlock);
    }
}