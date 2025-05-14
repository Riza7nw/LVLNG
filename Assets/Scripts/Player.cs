using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;
    protected bool facingRight = true;
    protected bool isGrounded = true;

    public float speed = 5f;
    public float jumpForce = 5f;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D tidak ditemukan! Tambahkan Rigidbody2D ke GameObject ini.");
        }
        if (anim == null)
        {
            Debug.LogError("Animator tidak ditemukan! Tambahkan Animator ke GameObject ini.");
        }
    }

    protected virtual void Update()
    {
        // Gerakan horizontal
        float moveInput = GetHorizontalInput();
        Move(moveInput);

        // Input lompat
        if (IsJumpPressed() && isGrounded)
        {
            Jump();
        }

        // Update animasi
        UpdateAnimationState(moveInput);
    }

    protected virtual void Move(float moveInput)
    {
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        Flip(moveInput);
    }

    protected virtual void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
    }

    protected virtual void Flip(float moveInput)
    {
        if (moveInput > 0 && !facingRight)
        {
            facingRight = true;
            Vector3 localScale = transform.localScale;
            localScale.x = Mathf.Abs(localScale.x);
            transform.localScale = localScale;
        }
        else if (moveInput < 0 && facingRight)
        {
            facingRight = false;
            Vector3 localScale = transform.localScale;
            localScale.x = -Mathf.Abs(localScale.x);
            transform.localScale = localScale;
        }
    }

    protected virtual void UpdateAnimationState(float moveInput)
    {
        anim.SetBool("isWalking", Mathf.Abs(moveInput) > 0.1f && isGrounded);
    }

    protected abstract float GetHorizontalInput();
    protected abstract bool IsJumpPressed();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}

public class Player : BaseCharacter
{
    protected override float GetHorizontalInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    protected override bool IsJumpPressed()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    protected override void UpdateAnimationState(float moveInput)
    {
        base.UpdateAnimationState(moveInput);

        // Tambahkan logika untuk animasi lari
        anim.SetBool("isRunning", Mathf.Abs(moveInput) > 0.1f && speed == 5f && isGrounded);
    }

    protected override void Update()
    {
        // Tentukan kecepatan berdasarkan input
        speed = Input.GetKey(KeyCode.LeftShift) ? 5f : 2f;

        // Logging input dan update
        Debug.Log("Update jalan");

        float moveInput = Input.GetAxisRaw("Horizontal");
        Debug.Log("Input horizontal: " + moveInput);

        // Panggil logika dari kelas dasar
        base.Update();
    }
}
