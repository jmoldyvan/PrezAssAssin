using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{

    public GameObject TinyMan;

    // Start is called before the first frame update
    void Start()
    {
        CreatePeople(30,20,101);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // for the x size of the map
    //     for the y size of the map
    //         add this x y coordinate to the list of possible coordinates

    // for a certain number of people
    //     spawn a guy at a coordinate from the list of possible coordinates
    //     remove the coordinate from the list of possible coordinates or somehow skip over it so we don't use it again

    public struct Coordinate
{
    public int X;
    public int Y;

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }
}

    public void CreatePeople(int width, int height, int numberOfPeople)
    {
        var possibleLocationsForTinyMen = new List<Coordinate>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                possibleLocationsForTinyMen.Add(new Coordinate(x, y));
            }
        }

        ShuffleList(possibleLocationsForTinyMen);
        
        for (int i = 0; i < numberOfPeople; i++)
        {
            var position = new Vector3(possibleLocationsForTinyMen[0].X, possibleLocationsForTinyMen[0].Y, 0);
            Instantiate(TinyMan, position, Quaternion.identity);
            possibleLocationsForTinyMen.RemoveAt(0);
        }   
    }

    public void ShuffleList<T>(IList<T> list)
    {    
        var rng = new System.Random();
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
