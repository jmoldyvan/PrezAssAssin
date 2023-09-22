using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveState : MonoBehaviour
{
    Material material;
    bool isDissolving = false;
    float fade = 1f;

    void Start()
    {       
        material = GetComponent<SpriteRenderer>().material;
    }

    public void StartDissolve()
    {
        isDissolving = true;
    }

    void Update()
    {

        if (isDissolving)
        {
            fade -= Time.deltaTime;

            if (fade <= 0f)
            {
                fade = 0f;
                isDissolving = false;
                Destroy(gameObject);
            }
            material.SetFloat("_Fade", fade);
        }
    }
}