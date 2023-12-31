using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2 (10f,10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    Vector2 moveInput;
    Rigidbody2D rb;
    Animator myAnime;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float gravityScaleStart;

    bool isAlive = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnime = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleStart = rb.gravityScale;
    }

    
    void Update()
    {
      if(!isAlive) {return;}
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnFire(InputValue value)
    {
      if(!isAlive) {return;}
      Instantiate(bullet, gun.position, transform.rotation);
    }

    void OnMove(InputValue value)

    {
      if(!isAlive) {return;}
      moveInput = value.Get<Vector2>();
      Debug.Log(moveInput);
    }


    void OnJump (InputValue value)
    {
        if(!isAlive) {return;}
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) {return;}

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

    
    void ClimbLadder()
    {
      if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) 

      {
        rb.gravityScale = gravityScaleStart;
        myAnime.SetBool("isClimbing", false);
        return;
      }

       Vector2 climbVelocity = new Vector2 (rb.velocity.x, moveInput.y * climbSpeed);
      rb.velocity = climbVelocity;
      rb.gravityScale = 0f;
      
      bool playerHasVerticalSpeed = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
      myAnime.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    void Die()
    {
      if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
      {
        isAlive = false;
        myAnime.SetTrigger("Dying");
        rb.velocity = deathKick;
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
      }
    }
}

