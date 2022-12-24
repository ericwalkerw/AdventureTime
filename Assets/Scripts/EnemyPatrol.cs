using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    // The points that the enemy will move between
    public Transform[] patrolPoints;

    // The speed at which the enemy will move
    public float moveSpeed = 2f;

    // The current point that the enemy is moving towards
    private int currentPoint;

    // The sprite renderer component
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Get the sprite renderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set the enemy's starting position to the first patrol point
        transform.position = patrolPoints[0].position;

        // Set the current point to the first patrol point
        currentPoint = 0;
    }

    void Update()
    {
        // Calculate the distance to the current patrol point
        float distance = Vector3.Distance(transform.position, patrolPoints[currentPoint].position);

        // If the distance is less than 0.1, move to the next patrol point
        if (distance < 0.1f)
        {
            currentPoint++;

            // If the enemy has reached the last patrol point, start again from the first point
            if (currentPoint >= patrolPoints.Length)
            {
                currentPoint = 0;
            }
        }

        // Move towards the current patrol point
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);

        // Flip the sprite if the enemy has changed direction
        if (transform.position.x > patrolPoints[currentPoint].position.x && spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
        }
        else if (transform.position.x < patrolPoints[currentPoint].position.x && !spriteRenderer.flipX)
        {
            spriteRenderer.flipX = true;
        }
    }
}
