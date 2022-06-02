using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFondler : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 11f;

    [SerializeField]
    private float jumpForce = 11f;
    private float movementX = 0.7f;

    [SerializeField]
    private Rigidbody2D mybody;

    private SpriteRenderer sr;
    private Animator anim;

    private string WALK_ANIMATION = "Walk";

    private bool isGrounded = true;

    public Transform firePoint;
    public GameObject bulletPrefab;

    public bool facingRight;

    private string GROUND_TAG = "Ground";
    // Start is called before the first frame update
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        mybody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
        PlayerJump();
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        
    }
    void FixedUpdate()
    {
        PlayerJump();
        cheakDirection();
    }

    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        
        transform.position += new Vector3(movementX, 0f, 0f)* Time.deltaTime * moveForce;

    }

    private void cheakDirection()
    {
        if(facingRight == true && movementX < 0)
        {
            Flip();
        }
        else if(facingRight == false && movementX > 0)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    void PlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            mybody.velocity = new Vector2(mybody.velocity.x, jumpForce);
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
    private void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
