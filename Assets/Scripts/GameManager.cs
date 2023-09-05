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
                hit.collider.gameObject.GetComponent<ClickEvent>().ObjectClicked();
            }
            else
            {
                Debug.Log("No object was hit.");
            }
        }
    }

    void Start()
    {
        SpawnPrez();
        CreatePeople(40, 40, 100);
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
    var possibleLocations = new List<Coordinate>();
    for (int i = 0; i < numberOfPeople; i++)
    {
        // Generate a random position within the boundary
        Vector3 randomPosition = new Vector3(
            Random.Range(0f, width),
            Random.Range(0f, height),
            5
        );

        // Perform a collision check using Physics2D.OverlapPoint
        Collider2D hitCollider = Physics2D.OverlapPoint(randomPosition, 1 << LayerMask.NameToLayer("YourLayerNameHere")); // replace "YourLayerNameHere" with the actual layer name

        // If hitCollider is null, it means there's no wall at that point
        if (hitCollider == null)
        {
            // The position is valid; you can instantiate the object there
            Instantiate(TinyMan, randomPosition, Quaternion.identity);
        }
        else
        {
            // Decrement i to make sure we try again
            i--;
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

    // Function to spawn a single Prez object
    void SpawnPrez()
    {
        Instantiate(Prez, new Vector3(Random.Range(0f, 30f), Random.Range(0f, 30f), 1), Quaternion.identity);
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