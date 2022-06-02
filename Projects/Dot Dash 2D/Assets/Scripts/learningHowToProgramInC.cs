using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class learningHowToProgramInC : MonoBehaviour
{
    private Rigidbody2D myBody;
    private BoxCollider2D myCollider;
    private AudioSource audioSource;
    private Animator anim;
    private Transform myTransform;
    

    private void Start()
    {
        myBody = GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>();

        myTransform = transform;

        myTransform.position = new Vector3(13,3, 3);
    }
    
    
}

