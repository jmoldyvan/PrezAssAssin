// using System.Collections;
// using UnityEngine;

// public class RandomMovement : MonoBehaviour
// {
//     public float minX = 0f;
//     public float maxX = 30f;
//     public float minY = 0f;
//     public float maxY = 20f;
//     public float moveSpeed = 2f;

//     private Vector3 targetPosition;

//     void Start()
//     {
//         PickNewTarget();
//         StartCoroutine(MoveToTarget());
//     }

//     void Update()
//     {
//         // Empty for now, can add collision logic later
//     }

//     private IEnumerator MoveToTarget()
//     {
//         while (true)
//         {
//             float distance = Vector3.Distance(transform.position, targetPosition);
//             if (distance > 0.01f)
//             {
//                 transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
//             }
//             else
//             {
//                 PickNewTarget();
//             }
//             yield return null;
//         }
//     }

//     private void PickNewTarget()
//     {
//         targetPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), transform.position.z);
//     }
// }




















// using System.Collections;
// using UnityEngine;

// public class RandomMovement : MonoBehaviour
// {
//     public float minX = 0f;
//     public float maxX = 30f;
//     public float minY = 0f;
//     public float maxY = 20f;
//     public float moveSpeed = 2f;
//     public float moveDelay = 1f;
//     public float radiusMultiplier = 1.5f;
//     public float repulsionRadius = 1f;
//     public float repulsionForce = 2f;

//     private Vector3 originalDirection;
//     private bool collided = false;
//     private Vector3 shuffleCenter;
//     private int movementType;
//     private Vector3 lastCollisionNormal;

//     void Start()
//     {
//         transform.position = new Vector3(transform.position.x, transform.position.y, 1);
//         movementType = Random.Range(0, 2);
//         shuffleCenter = transform.position;
//         originalDirection = GetRandomDirection();
        
//         StartCoroutine(StartMovementPatterns());
//     }

//     void FixedUpdate()
//     {
//         // Fix the Z-axis position
//         transform.position = new Vector3(transform.position.x, transform.position.y, 1);

//         // Add repulsion logic
//         Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, repulsionRadius);
//         Vector3 repulsionVector = Vector3.zero;

//         foreach (var hitCollider in hitColliders)
//         {
//             if (hitCollider.gameObject != gameObject)
//             {
//                 Vector3 toCollider = hitCollider.transform.position - transform.position;
//                 repulsionVector -= toCollider.normalized / toCollider.magnitude;
//             }
//         }

//         transform.position += repulsionVector * repulsionForce * Time.deltaTime;
//     }

//     private IEnumerator StartMovementPatterns()
//     {
//         while (true)
//         {
//             if (collided)
//             {
//                 yield return new WaitUntil(() => !collided);
//             }

//             switch (movementType)
//             {
//                 case 0:
//                     yield return StartCoroutine(Shuffle());
//                     break;
//                 case 1:
//                     yield return StartCoroutine(Shuffle());
//                     break;
//             }

//             yield return new WaitForSeconds(moveDelay);
//         }
//     }

//     private IEnumerator Shuffle()
//     {
//         Vector3 targetPosition = shuffleCenter + new Vector3(Random.Range(-radiusMultiplier, radiusMultiplier), Random.Range(-radiusMultiplier, radiusMultiplier), 0f);
//         yield return StartCoroutine(MoveToPosition(targetPosition));
//     }

//     private IEnumerator MoveToPosition(Vector3 targetPosition)
//     {
//         float distance = Vector3.Distance(transform.position, targetPosition);
//         while (distance > 0.01f)
//         {
//             if (collided)
//             {
//                 yield break;
//             }
//             transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
//             distance = Vector3.Distance(transform.position, targetPosition);
//             yield return null;
//         }
//     }

//     private void OnCollisionEnter2D(Collision2D collision)
//     {
//         collided = true;
//         lastCollisionNormal = collision.contacts[0].normal;  // Store the collision normal
//         StartCoroutine(HandleCollision());
//         Debug.Log($"Collision detected with: {collision.gameObject.name}");
//     }

//     private IEnumerator HandleCollision()
//     {
//         yield return new WaitForSeconds(1f);
//         originalDirection = Vector3.Reflect(originalDirection, lastCollisionNormal);  // Reflect based on last collision
//         collided = false;
//     }

//     private Vector3 GetRandomDirection()
//     {
//         return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
//     }
// }








using System.Collections;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveDelay = 1f;
    public float radiusMultiplier = 1.5f;

    private Rigidbody2D rb;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool collided = false;
    private bool hasBounced = false; // New flag to prevent multiple bounces

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        startPosition = transform.position;
        StartCoroutine(StartMovementPatterns());
    }

    void FixedUpdate()
    {
        if (isMoving && !collided)
        {
            float step = moveSpeed * Time.fixedDeltaTime;
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, step);
            rb.MovePosition(newPosition);

            // Update sprite orientation
            if (newPosition.x > transform.position.x)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (newPosition.x < transform.position.x)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
        else if (collided)
        {
            float step = moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(Vector3.MoveTowards(transform.position, startPosition, step));
        }
    }

    private IEnumerator StartMovementPatterns()
    {
        while (true)
        {
            if (collided)
            {
                yield return new WaitUntil(() => !collided);
                hasBounced = false;  // Reset the bounce flag when no longer collided
            }

            moveSpeed = Random.Range(0.5f, 3f);
            moveDelay = Random.Range(0.5f, 2f);

            startPosition = transform.position;
            targetPosition = startPosition + new Vector3(Random.Range(-radiusMultiplier, radiusMultiplier), Random.Range(-radiusMultiplier, radiusMultiplier), 0);
            isMoving = true;

            yield return new WaitForSeconds(moveDelay);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") && !hasBounced)
        {
            collided = true;
            isMoving = false;

            // Move away from the wall slightly
            Vector2 collisionNormal = collision.GetContact(0).normal;
            rb.MovePosition(rb.position - collisionNormal * 0.1f);

            StartCoroutine(HandleCollision());

            hasBounced = true;  // Set the flag to true after the first bounce
        }
    }

    private IEnumerator HandleCollision()
    {
        yield return new WaitUntil(() => Vector3.Distance(transform.position, startPosition) < 0.1f);
        collided = false;
        isMoving = true;
    }
}