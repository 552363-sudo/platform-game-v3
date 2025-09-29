using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 1f;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    Vector3 velocity;
    LayerMask groundLayerMask;
    SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
        anim = GetComponent<Animator>();
        groundLayerMask = LayerMask.GetMask("Ground");
        sr = GetComponent<SpriteRenderer>();

    }
    public bool ExtendedRayCollisionCheck(float xoffs, float yoffs)
    {
        sr.flipX = true;

        float rayLength = 0.2f; // length of raycast
        bool hitSomething = false;

        // convert x and y offset into a Vector3 
        Vector3 offset = new Vector3(xoffs, yoffs, 0);

        //cast a ray downward 
        RaycastHit2D hit;


        hit = Physics2D.Raycast(transform.position + offset, -Vector2.up, rayLength, groundLayerMask);

        Color hitColor = Color.white;


        if (hit.collider != null)
        {
            print("Player has collided with Ground layer");
            hitColor = Color.green;
            hitSomething = true;
        }
        // draw a debug ray to show ray position
        // You need to enable gizmos in the editor to see these
        Debug.DrawRay(transform.position + offset, -Vector3.up * rayLength, hitColor);

        return hitSomething;

    }


    

    void Update()

    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            anim.SetBool("Jump", false); 
        }

        Vector3 velocity = rb.linearVelocity;

        anim.SetBool("Run", false);
        

        if (Input.GetKey(KeyCode.D))
        {
            velocity.x = 1;
            anim.SetBool("Run", true);
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity.x = -1;
            anim.SetBool("Run", false);
        }



        rb.linearVelocity = velocity;

        isGrounded = ExtendedRayCollisionCheck(0, 0);
        
         



    }
}