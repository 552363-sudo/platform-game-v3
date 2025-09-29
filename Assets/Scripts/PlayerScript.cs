using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float jumpForce = 1f;
    public Animator anim;
    Vector3 velocity;
    Rigidbody2D rb;
    private bool isGrounded;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
        anim = GetComponent<Animator>(); 
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("Jump", false); // Reset jump animation when grounded
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            anim.SetBool("Jump", true);
        }
    }


    // Update is called once per frame
    void Update()

    {
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
            anim.SetBool("Run", true);
        }
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            anim.SetBool("Jump", true);
        }



        rb.linearVelocity = velocity;
    }



}
