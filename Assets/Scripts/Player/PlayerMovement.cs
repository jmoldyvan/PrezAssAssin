using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;

    // Add this variable to check if player movement is allowed
    public bool allowMovement = true;

    // Update is called once per frame
    void Update()
    {
        if (allowMovement) 
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        else
        {
            movement = Vector2.zero; // No movement if not allowed
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // You can use this function to toggle the movement
    public void ToggleMovement(bool allow)
    {
        allowMovement = allow;
    }
}