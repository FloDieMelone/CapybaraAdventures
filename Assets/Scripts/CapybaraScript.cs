using System;
using UnityEngine;

public class CapybaraScript : MonoBehaviour {

    public float moveSpeed = 8f;
    public float sprintMovementMult = 3f;
    public float jumpHeight = 8f;
    public float fallSpeed = 1.5f;
    public float lowJumpMultiplier = 2.0f;
    private bool lookRight = true;
    private bool sprintActivated = false;

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
        if (Input.GetKey(KeyCode.Space) && IsGrounded()) {
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }
        
        if (Input.GetKey(KeyCode.LeftShift)) {
            sprintActivated = true;
        }
        else {
            sprintActivated = false;
        }
    }

    private void FixedUpdate() {
        if (rb.linearVelocity.y < 0) {
            rb.linearVelocity += Vector2.up * (Physics2D.gravity.y * fallSpeed * Time.deltaTime);
        } else if (rb.linearVelocity.y > 0 && !Input.GetKeyDown(KeyCode.Space)) {
            rb.linearVelocity += Vector2.up * (Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime);
        }

        float xInput = Input.GetAxis("Horizontal");
        float xDist;
        if (sprintActivated) {
            xDist = xInput * moveSpeed * Time.deltaTime * sprintMovementMult;
            // Debug.Log("Sprint Boost ON!");
        } else {
            xDist = xInput * moveSpeed * Time.deltaTime;
            // Debug.Log("Sprint Boost OFF!");
        }

        if (Math.Abs(transform.position.x + xDist) > 9.95) {
            xDist = 0;
        }
        
        if (lookRight && xInput < 0 || !lookRight && xInput > 0) {
            lookRight = !lookRight;
            transform.Rotate(0, 180, 0);
        }

        transform.position += new Vector3(xDist, 0f, 0f);
    }

    private bool IsGrounded() {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.1f, groundLayer);
        return groundCheck is not null;
    }
    
}
