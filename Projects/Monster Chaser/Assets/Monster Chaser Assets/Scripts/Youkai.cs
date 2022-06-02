using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Youkai : MonoBehaviour
{
    public float speed;

    [SerializeField]
    private Rigidbody2D mybody;


    // Start is called before the first frame update
    void Awake()
    {
        mybody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mybody.velocity = new Vector2(speed, mybody.velocity.y);
    }
}
