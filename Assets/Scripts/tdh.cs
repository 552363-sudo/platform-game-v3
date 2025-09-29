using UnityEngine;

public class MoveTowardsEnemy : MonoBehaviour
{
    public Transform target;       // The object we move towards
    public float speed = 5f;       // Movement speed

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Make sure Rigidbody settings prevent tunneling through
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }

    // Detect collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(gameObject.name + " collided with " + collision.gameObject.name);
    }
}