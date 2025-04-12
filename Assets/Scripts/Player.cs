using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    bool facingRight = true;
    float speed = 5f;

    public float jumpForce = 5f; // Kekuatan lompatan
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Debugging untuk memastikan Rigidbody2D dan Animator ditemukan
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D tidak ditemukan! Tambahkan Rigidbody2D ke GameObject ini.");
        }
        if (anim == null)
        {
            Debug.LogError("Animator tidak ditemukan! Tambahkan Animator ke GameObject ini.");
        }
    }

    void Update()
    {
        // Input speed (Shift untuk lari)
        if (Input.GetKey(KeyCode.LeftShift))
            speed = 5f;
        else
            speed = 2f;

        // Input lompat
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            rb.AddForce(new Vector2(horizontalInput * jumpForce, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        // Gerakan horizontal
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Flip karakter berdasarkan input
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }

        // Update animasi
        AnimationState();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void AnimationState()
    {
        // Reset semua animasi
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", false);

        // Jika karakter bergerak di tanah
        if (Mathf.Abs(rb.velocity.x) > 0.1f && Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            if (speed == 5f) // Jika sedang lari
                anim.SetBool("isRunning", true);
            else // Jika sedang jalan
                anim.SetBool("isWalking", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Deteksi apakah player menyentuh tanah
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else
        {
            Debug.LogWarning($"Player menyentuh objek tanpa tag 'Ground': {collision.gameObject.name}");
        }
    }
}
