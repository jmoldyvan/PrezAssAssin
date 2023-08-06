using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    // Movement range
    public float minX = 0f;
    public float maxX = 30f;
    public float minY = 0f;
    public float maxY = 20f;

    // Movement speed
    public float moveSpeed = 5f;

    // Delay between movements
    public float moveDelay = 1f;

    // Coroutine to continuously move the object
    private IEnumerator Start()
    {
        while (true)
        {
            // Generate a random position within the movement range
            Vector3 targetPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);

            // Move the object smoothly to the target position
            yield return StartCoroutine(MoveToPosition(targetPosition));

            // Wait for a short duration before moving again
            yield return new WaitForSeconds(moveDelay);
        }
    }

    // Coroutine to smoothly move the object to the target position
    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        float distance = Vector3.Distance(transform.position, targetPosition);

        while (distance > 0.01f)
        {
            // Move towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Recalculate distance to target position
            distance = Vector3.Distance(transform.position, targetPosition);

            yield return null;
        }
    }
}
