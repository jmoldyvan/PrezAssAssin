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
    public void CreatePeople(int width, int height, int numberOfPeople)
    {
        var possibleLocationsForTinyMen = new List<Tuple<int, int>>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                possibleLocationsForTinyMen.Add(new Tuple(x,y));
            }
        } 

        Shuffle(possibleLocationsForTinyMen);
        
        for (int i = 0; i < numberOfPeople; i++)
        { 
            Instantiate(TinyMan, new Vector3(possibleLocationsForTinyMen[0].Item1, possibleLocationsForTinyMen[0].Item2,0));
            possibleLocationsForTinyMen.RemoveAt(0);
        }      
    }

    public void Shuffle<T>(this IList<T> list)
    {    
        var rng = new Random();
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
