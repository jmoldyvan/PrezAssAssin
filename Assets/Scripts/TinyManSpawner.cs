using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Tilemaps;


public class TinyManSpawner : MonoBehaviour
{
    [SerializeField] private GameObject tinyManPrefab;
    [SerializeField] private Sprite[] sceneSprites;
    public class TinyManSpawn : MonoBehaviour
    {
    // Start is called before the first frame update
    public GameObject CreatePeople(int numberOfPeople)

    {
        GameObject prezInstance = null;

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
                        if (prezInstance != null)
        {
            // Change the sprite for the Prez based on the current scene
            SpriteRenderer spriteRenderer = prezInstance.GetComponent<SpriteRenderer>();
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            if (sceneIndex >= 0 && sceneIndex < sceneSprites.Length)
            {
                spriteRenderer.sprite = sceneSprites[sceneIndex];
            }
        }
                Instantiate(TinyMan, spawnPosition, Quaternion.identity);
            }
            else
            {
                i--;  // Decrement i to try again
            }
        }
    }
    }
}