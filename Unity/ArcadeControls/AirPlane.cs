using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlane : MonoBehaviour
{
    //NOTE:: change tilt mechanism to applying force from bottom to raise aeroplane
    /*
    This script works by applying force to the airplane
    then slowly tilting it upwards to gain altitude, this 
    tilting stops when the airplane reaches a certian height called
    "tiltUpwardsEnd", which is a variable that controls the length of a 
    raycast that starts from "rayPos" and looks at the global down direction.
    */


    //Vars
    [SerializeField] Transform rayPos;
    [SerializeField] LayerMask floorLayer;
    [SerializeField] float speed = 10f, brakes = 1f, liftForce = 1f, zTurnSpeed = 50f, yTurnSpeed = 50f;
    Rigidbody rigidBody;
    Transform transform;
    int isGrounded = 0;
    float WInput, SInput, ADInput, ECInput;
    void Awake() 
    {
        transform = GetComponent<Transform>();
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
    }

    void FixedUpdate() 
    {
        Rotation();
        GroundDetection();
        Movement();
        Lift();

    }

    void Movement()
    {
        //Move forwards
        rigidBody.AddForce(transform.forward * speed * 100 * WInput);

        //Brake
        rigidBody.AddForce(-transform.forward * brakes * SInput);
    }

    void Rotation()
    {
        rigidBody.AddTorque(-transform.forward * ADInput * zTurnSpeed);
        rigidBody.AddTorque(-transform.right * ECInput * yTurnSpeed);
    }

    void Lift()
    {
        rigidBody.AddForce(Vector3.up * rigidBody.velocity.magnitude * liftForce);
    }

    void GroundDetection()
    {

    }

    void GetInput()
    {
        if (Input.GetKey(KeyCode.W))
            WInput = 1;
        else WInput = 0;

        if (Input.GetKey(KeyCode.S))
            SInput = 1;
        else SInput = 0;

        if (Input.GetKey(KeyCode.E))
            ECInput = 1;
        else if (Input.GetKey(KeyCode.C))
            ECInput = -1;
        else ECInput = 0;

        ADInput = Input.GetAxis("Horizontal");
    }
}
