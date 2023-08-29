using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{

    public float panSpeed = 20f;
    public float panBorderThickness = 20f;
    public float scrollSpeed = 20f;

    void Start()
    {
        // Set the initial camera position
        transform.position = new Vector3(20, 18, -15);
        Camera.main.orthographicSize = 15;
    }


    // Update is called once per frame
    void Update()
    {
        
        Vector3 pos = transform.position;

        if(Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        if(Input.mousePosition.y <= panBorderThickness)
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        if(Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if(Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize -= scroll * scrollSpeed * 100f * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -10, 40);
        pos.y = Mathf.Clamp(pos.y, 0, 40);
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 1, 16);

        transform.position = pos;

    }
}


// For those who want to make 2D "Scrolling":
// instead of:

// "pos.y"

// justwrite:

// "Camera.main.orthographicSize"

// For example:

// Camera.main.orthographicSize -= scroll * scrollSpeed * 100f * Time.deltaTime;

// or:

// Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minCrop, maxCrop);