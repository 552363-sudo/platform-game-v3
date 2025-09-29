using UnityEngine;

public class EnemyEdgePatrol : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 0.05f;            // Movement speed
    public Transform groundCheck;       // Empty GameObject at the enemy’s feet
    public float groundCheckDistance = 0.5f;
    public LayerMask groundLayer;
    SpriteRenderer sr;
    Vector3 velocity;
    LayerMask groundLayerMask;
    bool result;
    bool isGrounded;

    private Rigidbody2D rb;
    private bool movingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        sr = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        bool check = ExtendedRayCollisionCheck(0, 0);


        Vector3 velocity = rb.linearVelocity;

        if ( check && movingRight)
        {
            velocity.x = 1;

        }
        else if ( check && movingRight)
        {
            velocity.x = -1;

        }

        rb.linearVelocity = velocity;
        isGrounded = ExtendedRayCollisionCheck(0, 0);


    }

    void FixedUpdate()
    {
        // Move horizontally
        rb.linearVelocity = new Vector2(speed * (movingRight ? 1 : -1), rb.linearVelocity.y);

        // Check if ground ahead exists
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

        if (groundInfo.collider == false)
        {
            Flip();
        }
    }

    public bool ExtendedRayCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 0.4f; // length of raycast
        bool hitSomething = false;

        // convert x and y offset into a Vector3 
        Vector3 offset = new Vector3(xoffs, yoffs, 0);

        //cast a ray downward starting at the sprite's position
        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position + offset, Vector2.down, rayLength, groundLayerMask);

        Color hitColor = Color.red;


        if (hit.collider != null)
        {
            print("Player has collided with Ground layer");
            hitColor = Color.green;
            hitSomething = true;
        }
        // draw a debug ray to show ray's position
        // You need to enable gizmos in th e editor to see these
        Debug.DrawRay(transform.position + offset, Vector2.down * rayLength, hitColor);
        return hitSomething;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If enemy hits a wall, flip too
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
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