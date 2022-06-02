using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    [SerializeField]
    private float Speed = 10f;

    [SerializeField]
    private float movementX = 1f;

    [SerializeField]
    private Rigidbody2D ball;

    private float randomDirection;
    
    private int score1;
    private int score2;

    [SerializeField]
    public Text score1Text;
    public Text score2Text;

  
    private string WALL_TAG =   "SideWall";
    private string LEFT_TAG = "LeftWall";
    private string RIGHT_TAG = "RightWall";

    // Start is called before the first frame update
    void Start()
    {
        score1 = 0;
        score2 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        BallStart();
    }
    void BallStart()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            randomDirection = Random.Range(-100, 100);
            ball.AddForce(new Vector2(Speed, randomDirection), ForceMode2D.Force);
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            restartGame();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {   
        if(collision.gameObject.CompareTag(RIGHT_TAG))
        {
            score1++;
            HandleScore();
            ball.transform.position = new Vector3(0f,0f,0f);
        }
        
        if(collision.gameObject.CompareTag(LEFT_TAG))
        {
            score2++;
            HandleScore();
            ball.transform.position = new Vector3(0f,0f,0f);
        }
    }
    private void HandleScore()
    {
        score1Text.text = score1.ToString();
        score2Text.text = score2.ToString();
    }

    private void onTriggerEnter2D(Collision collision)
    {
        if(collision.gameObject.CompareTag(WALL_TAG))
        {
            Destroy(gameObject);
        }
    }
    void restartGame()
    {
         SceneManager.LoadScene("GamePlay");
    }

}
