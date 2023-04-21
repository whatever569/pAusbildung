using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] float accelaration = 3f, yTurningConstant = 180f, defaultDrag = 3f, inAirDrag = 0.1f, zTurningConstant = 30f, rayLength = 0.5f;
    [SerializeField] LayerMask floorLayer;
    [SerializeField] ForceMode forceModeForZTurn = ForceMode.Force;
    [SerializeField] Transform rayPos;
    Transform transform;
    Rigidbody rigidbody;
    int isGrounded = 1;
    float verticalInput, horizontalInput, zInput;

    void Awake()
    {
        //gets transform and rigidbody components from the game object.
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Ground();
    }
    //Use when doing physics related stuff.
    void FixedUpdate()
    {
        Rotation();
        Movement();
        DragControl();
    }

    //the function responsible for forward and backward movment.
    void Movement()
    {
        // forward and backwards movment
        rigidbody.AddForce(transform.forward * verticalInput * accelaration * isGrounded * 100);
    }

    //the function that is responsible for rotation, changes transform;  y turning Won't work if the player hasn't engaged the "Vertical" controls.
    void Rotation()
    {
        //turn in y axis
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +
            Vector3.up * yTurningConstant * isGrounded * verticalInput * horizontalInput * Time.deltaTime);
        //turn car in z axis
        if (isGrounded == 0)
        {
                rigidbody.AddTorque(transform.forward * zTurningConstant * zInput, forceModeForZTurn);
        }
    }

    //the function that is responsible for getting user input.
    void GetInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        
        if (Input.GetKey(KeyCode.E)) zInput = 1;
        else if (Input.GetKey(KeyCode.C)) zInput = -1;
        else zInput = 0;
    }

    //prevents the vehicle from flying off by detecting if the gameobject is on the ground.
    void Ground()
    {
        //changing rayLength to a higher value will make the car stop accelarating much later when going of a cliff or some tall object... lowering it too much will make the car get stuck.
        isGrounded = Physics.Raycast(rayPos.position, -transform.up, rayLength, floorLayer) ? 1 : 0;
    }

    //controls air time, prevents gameobject from falling down suddenly when not grounded.
    void DragControl()
    {
        if (isGrounded == 0) rigidbody.drag = inAirDrag;
        else rigidbody.drag = defaultDrag;
    }

}