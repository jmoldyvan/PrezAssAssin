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
    public float moveSpeed = 2f;

    // Delay between movements
    public float moveDelay = 10f;

    private bool collided = false;
    private Vector3 originalDirection;

    private void Start()
    {
        originalDirection = GetRandomDirection();
        StartCoroutine(ChangeDirectionAndResume());
        StartCoroutine(StartMovementPatterns());
    }

    private IEnumerator StartMovementPatterns()
    {
        while (true)
        {
            if (!collided)
            {
                int randomMovementType = Random.Range(0, 3); // 0: Linear, 1: ZigZag, 2: Oscillating

                switch (randomMovementType)
                {
                    case 0:
                        yield return StartCoroutine(MoveRandomly());
                        break;
                    case 1:
                        yield return StartCoroutine(ZigZagMovement());
                        break;
                    case 2:
                        yield return StartCoroutine(OscillatingMovement());
                        break;
                }
            }

            // Wait for a short duration before moving again
            yield return new WaitForSeconds(moveDelay);
        }
    }

    private IEnumerator MoveRandomly()
    {
        while (true)
        {
            Vector3 targetPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 1f);
                    // Introduce a random pause
                if (ShouldPause())
                {
                    float pauseDuration = Random.Range(1f, 3f);
                    yield return new WaitForSeconds(pauseDuration);
                }
            yield return StartCoroutine(MoveToPosition(targetPosition));
        }
    }

    private IEnumerator ZigZagMovement()
    {
        Vector3 startPos = transform.position;
        Vector3[] zigZagPoints = GenerateZigZagPoints(startPos, 5, 3f); // Change the second parameter for more or fewer points

        int currentIndex = 0;

        while (true)
        {
            Vector3 targetPosition = new Vector3(zigZagPoints[currentIndex].x, zigZagPoints[currentIndex].y, 1f);
                    // Introduce a random pause
                if (ShouldPause())
                {
                    float pauseDuration = Random.Range(1f, 3f);
                    yield return new WaitForSeconds(pauseDuration);
                }
            float distance = Vector3.Distance(transform.position, targetPosition);

            while (distance > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                distance = Vector3.Distance(transform.position, targetPosition);
                yield return null;
            }

            currentIndex = (currentIndex + 1) % zigZagPoints.Length;
        }
    }

    private Vector3[] GenerateZigZagPoints(Vector3 startPoint, int pointCount, float distanceBetweenPoints)
    {
        Vector3[] points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 1f).normalized * distanceBetweenPoints;
            points[i] = startPoint + offset;
            startPoint = points[i];
        }

        return points;
    }

    private IEnumerator OscillatingMovement()
    {
        Vector3 startPos = transform.position;
        Vector3 initialDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 1f).normalized;
        Vector3 targetPosition = startPos + initialDirection * Random.Range(1f, 3f);

        while (true)
        {
            float distance = Vector3.Distance(transform.position, targetPosition);
                    // Introduce a random pause
                if (ShouldPause())
                {
                    float pauseDuration = Random.Range(1f, 3f);
                    yield return new WaitForSeconds(pauseDuration);
                }

            while (distance > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                distance = Vector3.Distance(transform.position, targetPosition);
                yield return null;
            }

            // Change direction and set a new target position
            Vector3 newDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 1f).normalized;
            targetPosition = new Vector3(transform.position.x + newDirection.x * Random.Range(1f, 3f),
            transform.position.y + newDirection.y * Random.Range(1f, 3f), 1f); // Set Z to 1f
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        float distance = Vector3.Distance(transform.position, targetPosition);

        while (distance > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            distance = Vector3.Distance(transform.position, targetPosition);

            yield return null;
        }
    }

private IEnumerator ChangeDirectionAndResume()
{
    while (true)
    {
        collided = false;

        float pauseDuration = Random.Range(1f, 2f);
        yield return new WaitForSeconds(pauseDuration);

        originalDirection = GetRandomDirection();
    }
}

    private Vector3 GetRandomDirection()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collided = true;

        Vector3 newDirection = GetRandomDirection();
        while (Vector3.Dot(newDirection, originalDirection) > 0.5f)
        {
            newDirection = GetRandomDirection();
        }

        originalDirection = newDirection;
    }

    // Helper function to determine if a pause should occur
        private bool ShouldPause()
        {
            return Random.value < 0.2f; // Adjust the probability as needed
        }

}