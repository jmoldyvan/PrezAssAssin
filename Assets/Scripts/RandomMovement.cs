using System.Collections;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float minX = 0f;
    public float maxX = 30f;
    public float minY = 0f;
    public float maxY = 20f;
    public float moveSpeed = 2f;
    public float moveDelay = 1f;
    public float radiusMultiplier = 1.5f;  // radius for shuffling

    private Vector3 originalDirection;
    private bool collided = false;
    private Vector3 shuffleCenter;
    private int movementType;

    private void Start()
    {
        // Initialize the Z position to 1
        transform.position = new Vector3(transform.position.x, transform.position.y, 1);
        
        // Decide the movement type at start
        movementType = Random.Range(0, 2);  // Now only 0 and 1 are possible
        shuffleCenter = transform.position;
        originalDirection = GetRandomDirection();
        
        StartCoroutine(StartMovementPatterns());
    }

    private IEnumerator StartMovementPatterns()
    {
        while (true)
        {
            if (collided)
            {
                yield return new WaitUntil(() => !collided);
            }

            switch (movementType)
            {
                case 0:
                    yield return StartCoroutine(MoveRandomly());
                    break;
                case 1:
                    yield return StartCoroutine(Shuffle());
                    break;
                // Removed case for ZigZag
            }

            yield return new WaitForSeconds(moveDelay);
        }
    }

    private IEnumerator MoveRandomly()
    {
        Vector3 targetPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 1f);
        yield return StartCoroutine(MoveToPosition(targetPosition));
    }

    private IEnumerator Shuffle()
    {
        Vector3 targetPosition = shuffleCenter + new Vector3(Random.Range(-radiusMultiplier, radiusMultiplier), Random.Range(-radiusMultiplier, radiusMultiplier), 0f);
        yield return StartCoroutine(MoveToPosition(targetPosition));
    }

    // Removed ZigZag Coroutine

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        float distance = Vector3.Distance(transform.position, targetPosition);
        while (distance > 0.01f)
        {
            if (collided)
            {
                yield break;
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            distance = Vector3.Distance(transform.position, targetPosition);
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collided = true;
        StartCoroutine(HandleCollision());
    }

    private IEnumerator HandleCollision()
    {
        yield return new WaitForSeconds(1f);  // Pause for 1 second
        originalDirection = GetRandomDirection();
        collided = false;
    }

    private Vector3 GetRandomDirection()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
    }
}
