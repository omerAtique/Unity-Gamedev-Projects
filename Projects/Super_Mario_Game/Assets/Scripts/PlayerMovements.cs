using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 11f;

    private int coins;

    [SerializeField]
    private float jumpForce = 11f;
    private float movementX = 0.7f;

    [SerializeField]
    private float Yscale;

    [SerializeField]
    private Rigidbody2D mybody;
  
    private SpriteRenderer sr;
    
    private Animator anim;
    private string WALK_ANIMATION = "walk";

    private int score;

    [SerializeField]
    public Text scoreText;

    private bool isGrounded = true;
    private bool isObstacles = true;
    private bool isYoukai = true;
    private string GROUND_TAG = "Ground";
    private string OBSTACLES_TAG = "Obstacles";
    private string ENEMY_TAG = "youkai";
    private string TOP_TAG = "top";
    private string COIN_TAG = "coin";
    private string PRIZE_TAG = "PrizeBlock";
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        mybody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {

        PlayerMoveKeyboard();
        AnimatePlayer();
        PlayerCrouch();
        PlayerJump();
       
    }

    void FixedUpdate()
    {
        PlayerJump();
    }

    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;
    }
    void AnimatePlayer()
    {
        if(movementX > 0){
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = false;
        }
        else if(movementX < 0){
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = true;
        }
        else{
            anim.SetBool(WALK_ANIMATION, false);
        }

    }
    void PlayerCrouch()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            gameObject.transform.localScale += new Vector3(0f, -0.3f, 0f);
        }

    }
    void PlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            mybody.velocity = new Vector2(mybody.velocity.x, jumpForce);
        }
        else if(Input.GetKeyDown(KeyCode.Space) && isObstacles)
        {
            isObstacles = false;
            mybody.velocity = new Vector2(mybody.velocity.x, jumpForce);  
        }
        else if(Input.GetKeyDown(KeyCode.Space) && isYoukai)
        {
            isYoukai = false;
            mybody.velocity = new Vector2(mybody.velocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = true;
        }
        else if(collision.gameObject.CompareTag(OBSTACLES_TAG))
        {
            isObstacles = true;
        }
        else if(collision.gameObject.CompareTag(ENEMY_TAG))
        {
            Destroy(gameObject);
            SceneManager.LoadScene("GamePlay");    
        }
        else if(collision.gameObject.CompareTag(TOP_TAG))
        {
            Destroy(collision.transform.parent.gameObject);
        }
        else if(collision.gameObject.CompareTag(PRIZE_TAG))
        {
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag(COIN_TAG))
        {
            score++;
            HandleScore();
            Destroy(collision.gameObject);
        }
    } 
    private void HandleScore()
    {
        scoreText.text = score.ToString();
    } 
}
