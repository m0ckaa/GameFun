using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Defining variables for player movement
    private Rigidbody rb;
    private float horizontalInput, verticalInput;
    private Vector3 direction, velocity;
    [SerializeField]
    private float moveSpeed = 5f;


    // Start is called before the first frame update
    void Start()
    {
        //Retrieves rigidbody and character controller to help move player object
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Gets user input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        

        //Sets directions to vector3 from floats
        velocity = new Vector3(horizontalInput * moveSpeed, rb.velocity.y, verticalInput * moveSpeed);
        rb.velocity = velocity;
        

        //Sends player inputs to console
        //Debug.Log("Horizontal Movement: " + horizontalInput + ", Vertical Movement: " + verticalInput);
    }
}
