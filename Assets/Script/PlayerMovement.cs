using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator myAnime;
    CapsuleCollider2D myCapCollider;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnime = GetComponent<Animator>();
        myCapCollider = GetComponent<CapsuleCollider2D>();
    }

    
    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value)

    {
      moveInput = value.Get<Vector2>();
      Debug.Log(moveInput);
    }

    void OnJump (InputValue value)
    {
        if(!myCapCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {return;}

        if(value.isPressed)
        {
          rb.velocity += new Vector2 (0f, jumpSpeed);
        }
    }
    
    void Run()
    {
      Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, rb.velocity.y);
      rb.velocity = playerVelocity;

      bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
      myAnime.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
      bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
      
      if(playerHasHorizontalSpeed)
      {
        transform.localScale = new Vector2 (Mathf.Sign(rb.velocity.x), 1f);
      }

    }
}
