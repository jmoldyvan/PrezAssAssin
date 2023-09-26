using System.Collections;
using UnityEngine;

public class RandomMovement1 : MonoBehaviour
{
    public FieldOfView fieldOfView;  
    public float rotationSpeed = 5f;
    
    public float moveSpeed = 2f;
    public float moveDelay = 1f;
    public float radiusMultiplier = 1.5f;
    public float inactivityTime = 5f;

    private Rigidbody2D rb;
    private Vector3 startPosition;
    private Vector3 lastPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool collided = false;
    private bool hasBounced = false;
    private float timer = 0f;

    private bool inBackupMode = false;
    private Vector2 backupDirection;
    private float backupTime = 2f;
    private Quaternion lastValidRotation;
    public bool isPaused = false;

    void Start()
    {
        fieldOfView = GetComponentInChildren<FieldOfView>();

        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        startPosition = transform.position;
        lastPosition = startPosition;
        StartCoroutine(StartMovementPatterns());
    }

void FixedUpdate()
{
    if (isPaused)
        return;

    float angleToTarget = 0f;

    if (isMoving && !collided && !inBackupMode)
    {
        float step = moveSpeed * Time.fixedDeltaTime;
        Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, step);
        rb.MovePosition(newPosition);

        angleToTarget = Mathf.Atan2(targetPosition.y - transform.position.y, targetPosition.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion rotationToTarget = Quaternion.Euler(new Vector3(0, 0, angleToTarget));
        fieldOfView.transform.rotation = Quaternion.Lerp(fieldOfView.transform.rotation, rotationToTarget, Time.fixedDeltaTime * rotationSpeed);

        lastValidRotation = rotationToTarget;

        if (newPosition.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (newPosition.x < transform.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        if (transform.position != lastPosition)
        {
            timer = 0f;
        }
    }
    else
    {
        // Use the last valid rotation when not moving
        angleToTarget = lastValidRotation.eulerAngles.z;

        // Moved this part inside the else block
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angleToTarget));
        fieldOfView.transform.rotation = Quaternion.Lerp(fieldOfView.transform.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
    }

    if (collided)
    {
        float step = moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(Vector3.MoveTowards(transform.position, startPosition, step));
    }
    
    if (inBackupMode)
    {
        float step = moveSpeed * Time.fixedDeltaTime;
        Vector3 newPosition = transform.position + (Vector3)backupDirection * step;
        rb.MovePosition(newPosition);
    }

    lastPosition = transform.position;
    timer += Time.fixedDeltaTime;

    if (timer >= inactivityTime)
    {
        timer = 0f;
        inBackupMode = true;
        backupDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        StartCoroutine(BackupManeuver());
    }
}

    private IEnumerator StartMovementPatterns()
    {
        while (true)
        {
            if (collided)
            {
                yield return new WaitUntil(() => !collided);
                hasBounced = false;
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

            Vector2 collisionNormal = collision.GetContact(0).normal;
            rb.MovePosition(rb.position - collisionNormal * 0.1f);

            StartCoroutine(HandleCollision());
            hasBounced = true;
        }
    }

    private IEnumerator HandleCollision()
    {
        yield return new WaitUntil(() => Vector3.Distance(transform.position, startPosition) < 0.1f);
        collided = false;
        isMoving = true;
    }

    private IEnumerator BackupManeuver()
    {
        yield return new WaitForSeconds(backupTime);
        inBackupMode = false;
        StopCoroutine(StartMovementPatterns());
        StartCoroutine(StartMovementPatterns());
    }

    public void TogglePause(bool pause)
    {
        isPaused = pause;
        if (pause)
        {
            StopCoroutine(StartMovementPatterns());
        }
        else
        {
            StartCoroutine(StartMovementPatterns());
        }
    }
}