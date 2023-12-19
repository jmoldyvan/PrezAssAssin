using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TinyManSpawner : MonoBehaviour
{
    [SerializeField] private GameObject tinyManPrefab;
    [SerializeField] private Sprite[] tinymansceneSprites;

    public void SpawnTinyMan(int numberOfPeople, Vector3Int floorTilemapRange, Tilemap floorTilemap, Tilemap wallTilemap)
    {
        for (int i = 0; i < numberOfPeople; i++)
        {
            Vector3Int randomTilePosition = new Vector3Int(
                Random.Range(floorTilemapRange.x, floorTilemapRange.y),
                Random.Range(floorTilemapRange.z, floorTilemapRange.y), // Adjusted to use the range.y for the y-coordinate upper limit
                0
            );

            if (floorTilemap.HasTile(randomTilePosition) && !wallTilemap.HasTile(randomTilePosition))
            {
                Vector3 spawnPosition = floorTilemap.GetCellCenterWorld(randomTilePosition);
                GameObject tinyManInstance = Instantiate(tinyManPrefab, spawnPosition, Quaternion.identity);

                // Change the sprite for the Tinyman based on a random selection
                SpriteRenderer spriteRenderer = tinyManInstance.GetComponent<SpriteRenderer>();
                int randomNumber = Random.Range(0, tinymansceneSprites.Length); // Adjusted to use the length of the array
                spriteRenderer.sprite = tinymansceneSprites[randomNumber];
            }
            else
            {
                i--; // Decrement i to try again if a valid position is not found
            }
        }
    }
}