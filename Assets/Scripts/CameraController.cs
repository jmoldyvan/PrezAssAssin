using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{


    public float panSpeed = 20f;
    public float panBorderThickness = 20f;
    public float scrollSpeed = 20f;

    public float panSpeedOnTransition = .5f;
    private GameObject target;
    private bool shouldPan = false;

    public bool isPlayerControlEnabled = true;

    void Start()
    {
        // Set the initial camera position
        transform.position = new Vector3(20, 18, -15);
        Camera.main.orthographicSize = 15;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Prez");
        }

        if (!target)  // If Prez is not found, then try to find Player
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        if (!target)  // If Prez is not found, then try to find Player
        {
            target = null;
        }


        if (isPlayerControlEnabled){
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

            pos.x = Mathf.Clamp(pos.x, -10, 60);
            pos.y = Mathf.Clamp(pos.y, 0, 40);
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 1, 16);

            transform.position = pos;            
        }
        if (shouldPan)
        {
            Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * panSpeedOnTransition);

            // Set the target camera size here. For example, I'm using 10.
            float targetCameraSize = 6;

            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetCameraSize, Time.deltaTime * panSpeedOnTransition);
        }
            
    }


    public void StartTransitionPanning()
    {
        shouldPan = true;
        isPlayerControlEnabled = false;
    }

    public void StopTransitionPanning()
    {
        shouldPan = false;
    }

    public void SetAfterGunTransitionSiza(float newSize)
    {
        StopTransitionPanning();
        Camera.main.orthographicSize = newSize;
    }

    public void ZoomToTargetWithTag(string tag)
{
    GameObject target = GameObject.FindGameObjectWithTag(tag);
    if (target)
    {
        this.target = target;
        StartTransitionPanning();
    }
}
}
