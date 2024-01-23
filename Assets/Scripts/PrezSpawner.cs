using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Tilemaps;
public class PrezSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prezPrefab;
    [SerializeField] private Sprite[] sceneSprites;

public GameObject SpawnPrez(Vector3Int floorTilemapRange, Tilemap floorTilemap, Tilemap wallTilemap)
{
    GameObject prezInstance = null;

    for (int i = 0; i < 10000; i++)
    {
        Vector3Int randomTilePosition = new Vector3Int(
            Random.Range(floorTilemapRange.x, floorTilemapRange.y),
            Random.Range(floorTilemapRange.z, 150),
            0
        );

        bool hasFloorTile = floorTilemap.HasTile(randomTilePosition);
        bool hasWallTile = wallTilemap.HasTile(randomTilePosition);

        Debug.Log($"Attempting to spawn at {randomTilePosition}. Floor tile present: {hasFloorTile}, Wall tile present: {hasWallTile}");

        if (hasFloorTile && !hasWallTile)
        {
            Vector3 spawnPosition = floorTilemap.GetCellCenterWorld(randomTilePosition);
            prezInstance = Instantiate(prezPrefab, spawnPosition, Quaternion.identity);
            break;
        }
    }

        if (prezInstance != null)
        {
            // Change the sprite for the Prez based on the current scene
            SpriteRenderer spriteRenderer = prezInstance.GetComponent<SpriteRenderer>();
            // Debug.LogError(prezInstance.GetComponent<SpriteRenderer>());
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            // Debug.LogError(sceneIndex);
            spriteRenderer.sprite = sceneSprites[sceneIndex];

            // GameObject UICanvas = GameObject.Find("UI Canvas");
            // Transform child = UICanvas.transform.Find("PauseMenu/PrezTargert");
            // GameObject PrezTargert = child.gameObject;
            // SpriteRenderer PrezTargertSpriteRenderer = PrezTargert.GetComponent<SpriteRenderer>();
            // PrezTargertSpriteRenderer.sprite = sceneSprites[sceneIndex];

        }
        else
        {
            Debug.LogError("Failed to instantiate Prez. Check tilemap range and presence of the Prez prefab.");
        }

        return prezInstance;
    }
}