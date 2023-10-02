using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Tilemaps;

public class PlayerCollisionHandling : MonoBehaviour
{
    public GameObject EnemySecretService;
    public GameManager gameManager;
    // Start is called before the first frame update

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision entered by: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("TinyMan"))
        {
            EnemySecretService = collision.gameObject;
            GameManager.Instance.LoseHeart();
        }
    }
}
