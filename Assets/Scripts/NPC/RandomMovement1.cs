using System.Collections;
using UnityEngine;

public class RandomMovement1 : MonoBehaviour
{
    public FieldOfView fieldOfView;  
    public float rotationSpeed = 1f;
    public float radiusMultiplier = 1.5f;
    public float inactivityTime = 6f;
    public float backupTime = 2f;

    private Rigidbody2D rb;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Quaternion lastValidRotation;
    private float timer = 0f;
    private bool isPaused = false;
    private bool collided = false;
    private Vector2 backupDirection;
    private float moveSpeed;
    private float moveDelay;

    private Transform currentTarget;
    private bool isFollowingTarget = false;
    private Transform visibleTarget;
    public bool allowTracking = true;

    private enum State
    {
        Moving,
        BackingUp,
        HandlingCollision,
        Waiting,
        Paused,
        Tracking
    }

    private State currentState;

    void Start()
    {
        fieldOfView = GetComponentInChildren<FieldOfView>();
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        startPosition = transform.position;
        SetRandomTarget();
        currentState = State.Moving;
        float initialDelay = Random.Range(0f, 3f);  // Random initial delay between 0 to 3 seconds
        StartCoroutine(StartMovingAfterDelay(initialDelay));
        inactivityTime = Random.Range(4f, 8f);  // Random inactivity time between 2 to 10 seconds
        rotationSpeed = Random.Range(1f, 8f);  // Random inactivity time between 2 to 10 seconds
    }

    IEnumerator StartMovingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        currentState = State.Moving;
    }

    void FixedUpdate()
    {
        if (isPaused)
        {
            currentState = State.Paused;
        }

        if (fieldOfView.visibleTargets.Count > 0 && allowTracking)
        {
            visibleTarget  = fieldOfView.visibleTargets[0];
            MoveToVisibleTarget(visibleTarget);
        }

        else{
            switch (currentState)
            {
                case State.Moving:
                    MoveToTarget();
                    break;
                
                case State.BackingUp:
                    Backup();
                    break;

                case State.HandlingCollision:
                    ResolveCollision();
                    break;

                case State.Waiting:
                    // Do nothing, or handle any logic while waiting
                    break;

                case State.Paused:
                    // Stop any ongoing coroutines or other actions
                    break;
            }            
        }
        timer += Time.fixedDeltaTime;
        CheckInactivity();
        UpdateSpriteFlip();
    }

    void MoveToTarget()
    {
        float step = moveSpeed * Time.fixedDeltaTime;
        Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, step);
        rb.MovePosition(newPosition);
    }
    void MoveToVisibleTarget(Transform target)
    {
        moveSpeed = 5f;
        float step = moveSpeed * Time.fixedDeltaTime;
        Vector3 newPosition = Vector3.MoveTowards(transform.position, target.position, step);
        rb.MovePosition(newPosition);
    }
    void Backup()
    {
        float step = moveSpeed * Time.fixedDeltaTime;
        Vector3 newPosition = transform.position + (Vector3)backupDirection * step;
        rb.MovePosition(newPosition);
    }

    void ResolveCollision()
    {
        float step = moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(Vector3.MoveTowards(transform.position, startPosition, step));
    }

    IEnumerator RotateTowardsTargetCoroutine()
    {
        float angleToTarget = Mathf.Atan2(targetPosition.y - transform.position.y, targetPosition.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angleToTarget));
        float elapsedTime = -1f; // don't put this higher than 0
        float rotationDuration = 6f;  // Duration over which to perform the rotation - you can adjust this value

        while (elapsedTime < rotationDuration)
        {
            fieldOfView.transform.rotation = Quaternion.Lerp(fieldOfView.transform.rotation, targetRotation, elapsedTime/rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fieldOfView.transform.rotation = targetRotation;  // Ensure the final rotation is exactly as desired
    }

    void CheckInactivity()
    {
        if (timer >= inactivityTime)
        {
            timer = 0f;
            currentState = State.BackingUp;
            backupDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            StartCoroutine(BackupManeuver());
        }
    }

    void SetRandomTarget()
    {
        targetPosition = startPosition + new Vector3(Random.Range(-radiusMultiplier, radiusMultiplier), Random.Range(-radiusMultiplier, radiusMultiplier), 0);
        moveSpeed = Random.Range(0.5f, 3f);
        moveDelay = Random.Range(3f, 6f);
        
        StopCoroutine("RotateTowardsTargetCoroutine");  // Stop any existing rotation coroutine
        StartCoroutine(RotateTowardsTargetCoroutine());  // Start a new rotation coroutine
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            collided = true;
            currentState = State.HandlingCollision;
            Vector2 collisionNormal = collision.GetContact(0).normal;
            rb.MovePosition(rb.position - collisionNormal * 0.1f);
        }
    }

    private IEnumerator BackupManeuver()
    {
        yield return new WaitForSeconds(backupTime);
        currentState = State.Moving;
        SetRandomTarget();  // This will also set new random values for moveSpeed and moveDelay
    }

    public void TogglePause(bool pause)
    {
        isPaused = pause;
        if (pause)
        {
            currentState = State.Paused;
        }
        else
        {
            currentState = State.Moving;
            SetRandomTarget();  // This will also set new random values for moveSpeed and moveDelay
        }
    }

    void UpdateSpriteFlip()
    {
        Transform secretServiceTransform = transform.Find("SecretService");
        if (secretServiceTransform != null)
        {
            SpriteRenderer spriteRenderer = secretServiceTransform.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                float angle = fieldOfView.transform.eulerAngles.z;
                spriteRenderer.flipX = (angle > 90 && angle < 270);
            }
            else
            {
                Debug.LogError("No SpriteRenderer found on SecretService");
            }
        }
        else
        {
            Debug.LogError("No SecretService child found");
        }
    }
}