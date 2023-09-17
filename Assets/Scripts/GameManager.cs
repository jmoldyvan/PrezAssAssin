using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Tilemaps;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject TinyMan;
    public GameObject Prez;
    public List<Image> Hearts;
    public TilemapCollider2D tilemapCollider;
    public Tilemap floorTilemap;
    public Tilemap wallTilemap;

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
        SpawnPrez(110, 50);
        CreatePeople(110, 55, 100);
    }

    public struct Coordinate
    {
        public int X, Y;

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public void CreatePeople(int width, int height, int numberOfPeople)
    {
        for (int i = 0; i < numberOfPeople; i++)
        {
            Vector3Int randomTilePosition = new Vector3Int(
                Random.Range(-15, width),
                Random.Range(0, height),
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
    void SpawnPrez(int width, int height)
    {
        for (int i = 0; i < 1; i++)  // We only want to spawn one Prez
        {
            Vector3Int randomTilePosition = new Vector3Int(
                Random.Range(-15, width),
                Random.Range(0, height),
                0
            );

            if (floorTilemap.HasTile(randomTilePosition) && !wallTilemap.HasTile(randomTilePosition))
            {
                Vector3 spawnPosition = floorTilemap.GetCellCenterWorld(randomTilePosition);
                Instantiate(Prez, spawnPosition, Quaternion.identity);
                return;  // Exit the loop once the Prez is spawned
            }
            else
            {
                i--;  // Decrement i to try again
            }
        }
    }

        public void LoseHeart(){
            Debug.Log("LoseHeart called in GameManager.");

            var fullHeart = Hearts.FirstOrDefault(x => x.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Default"));
            
            if (fullHeart != null) {
                Debug.Log("Full heart found.");
                fullHeart.gameObject.GetComponent<Animator>().Play("Flickering");
                fullHeart.sprite = Resources.Load<Sprite>("Images/Hearts/EmptyHeart");
            } else {
                Debug.Log("No full heart found.");
            }
        }

}