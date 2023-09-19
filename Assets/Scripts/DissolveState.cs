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
        Debug.Log("DissolveState script started.");
        
        
        if (material != null)
        {
            material = GetComponent<SpriteRenderer>().material;
            Debug.Log("Successfully got the material.");
        }
        else
        {
            Debug.Log("Could not find material.");
        }
    }

    public void StartDissolve()
    {
        Debug.Log("StartDissolve called. Starting to dissolve.");
        isDissolving = true;
    }

    void Update()
    {
        
        
        if (isDissolving)
        {
            Debug.Log("In dissolve logic. Updating fade.");
            
            fade -= Time.deltaTime;

            if (fade <= 0f)
            {
                Debug.Log("Fade is zero or below. Stopping dissolve.");
                
                fade = 0f;
                isDissolving = false;
            }

            material.SetFloat("_Fade", fade);
        }
    }
}