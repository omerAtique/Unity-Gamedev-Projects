using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 11f;

    [SerializeField]
    private Rigidbody2D mybody;

    private SpriteRenderer sr;

    private string BARRIER_TAG = "youkai_barrier";

    // Start is called before the first frame update
    void Awake()
    {
        mybody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        youkaiMovement();
    }

    private void youkaiMovement()
    {
        if(sr.flipX == true)
            mybody.velocity = new Vector2(speed, mybody.velocity.y);
        else if(sr.flipX == false)
            mybody.velocity = new Vector2(-speed, mybody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(BARRIER_TAG))
        {
            if(sr.flipX == true)
            {
                sr.flipX = false;
            }
            else if(sr.flipX == false)
            {
                sr.flipX = true;
            }
        }
    }
}
