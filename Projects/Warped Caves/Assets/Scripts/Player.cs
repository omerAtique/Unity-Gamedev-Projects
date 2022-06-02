using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 11f;

    [SerializeField]
    private float jumpForce = 5f;
    private float movementX = 0.7f;

    private SpriteRenderer sr;
    private Animator anim;
    
    [SerializeField]
    private Rigidbody2D mybody;

    private bool isGrounded = true;
    [SerializeField]
    private bool facingRight;

    private string GROUND_TAG = "Ground";
    private string WALK_ANIMATION = "Walk";

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        mybody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement();
        playerJump();
        AnimatePlayer();
    }

    void FixedUpdate()
    {
        playerJump();
        cheakDirection();
    }

    private void playerMovement()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;
    }

    private void playerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            mybody.velocity  = new Vector2(mybody.velocity.x, jumpForce);
        }
    }

    private void flipPlayer()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void cheakDirection()
    {
        if(facingRight == true && movementX < 0)
        {
            flipPlayer();
        }
        else if(facingRight == false && movementX >0)
        {
            flipPlayer();
        }
    }
    void AnimatePlayer()
    {
        if(movementX > 0 || movementX < 0)
        {
            anim.SetBool(WALK_ANIMATION, true);
        }
        else
        {
            anim.SetBool(WALK_ANIMATION, false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = true;
        }
    }
}
