using UnityEngine;

public class EnemyPatrolRaycast : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 1f;                 // Enemy speed
    public float rayDistance = 1f;           // How far down to check
    public LayerMask groundLayer;            // What counts as ground

    private Rigidbody2D rb;
    private bool movingRight = true;

    [Header("Raycast Offsets")]
    public Vector2 rayOffset = new Vector2(0.5f, 0f); // Half-width of sprite (adjust in Inspector)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    void FixedUpdate()
    {
        // Move enemy
        float moveDir = movingRight ? 0.5f : -0.5f;
        rb.linearVelocity = new Vector2(moveDir * speed, rb.linearVelocity.y);  

        // Choose ray origin (left or right bottom of sprite)
        Vector3 rayOrigin = transform.position + new Vector3(movingRight ? rayOffset.x : -rayOffset.x, rayOffset.y, 0);

        // Cast a ray straight down
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayDistance, groundLayer);

        // Debug ray (Scene view only)
        Debug.DrawRay(rayOrigin, Vector2.down * rayDistance, Color.red);

        // If no ground detected ? flip direction
        if (hit.collider == null)
        {
            Flip();
        }
    }

    private void Flip()
    {
        movingRight = !movingRight;

        // Flip sprite visually
        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }
}