using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PaddleMovement : MonoBehaviour
{

    [SerializeField]
    private float moveForce = 10f;

    [SerializeField]
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PaddleMoveKeyboard();
    }

    void PaddleMoveKeyboard()
    {
        
        movementY = Input.GetAxisRaw("Vertical");
        transform.position += new Vector3(0f, movementY, 0f) * Time.deltaTime * moveForce;
    }
}
