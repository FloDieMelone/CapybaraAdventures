using System;
using UnityEngine;

public class CapybaraScript : MonoBehaviour {

    public float moveSpeed = 8f;
    public float jumpHeight = 8f;
    public float fallSpeed = 1.5f;
    public float lowJumpMultiplier = 2.0f;
    private bool lookRight = true;

    public Transform feet;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded()) {
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate() {
        if (rb.linearVelocity.y < 0) {
            rb.linearVelocity += Vector2.up * (Physics2D.gravity.y * fallSpeed * Time.deltaTime);
        } else if (rb.linearVelocity.y > 0 && !Input.GetKeyDown(KeyCode.Space)) {
            rb.linearVelocity += Vector2.up * (Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime);
        }

        float xInput = Input.GetAxis("Horizontal");
        float xDist = xInput * moveSpeed * Time.deltaTime;

        if (Math.Abs(transform.position.x + xDist) > 9.95) {
            xDist = 0;
        }
        
        if (lookRight && xInput < 0 || !lookRight && xInput > 0) {
            lookRight = !lookRight;
            transform.Rotate(0, 180, 0);
        }

        transform.position += new Vector3(xDist, 0f, 0f);
    }

    private bool isGrounded() {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.1f, groundLayer);
        return groundCheck is not null;
    }
    
}
