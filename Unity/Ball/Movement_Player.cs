using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Player : MonoBehaviour
{
    Rigidbody rb;
    Transform transform;
    [SerializeField] float jumpHeight = 5f, movementSpeed = 10f, fallSpeed = 1.5f;
    [SerializeField] KeyCode jumpKey = KeyCode.W, leftKey = KeyCode.A,
                             rightKey = KeyCode.D;
    [SerializeField] LayerMask floorLayer;
    [SerializeField] Transform rayPosition;
    int jumpButtonValue, horizontalAxisValue, isGrounded;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
    }

    private void Update() 
    {
        Grounded();
        GetControlValues();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        rb.AddForce(Vector3.up * jumpButtonValue * isGrounded * jumpHeight, ForceMode.Impulse);

        if(rb.velocity.y < 0)
        {
            rb.AddForce(Vector3.up * Physics.gravity.y * fallSpeed * Time.fixedDeltaTime);
        }
        rb.AddForce(Vector3.right * horizontalAxisValue * movementSpeed, ForceMode.Force);
    }

    void GetControlValues()
    {
        jumpButtonValue = Input.GetKey(jumpKey) ? 1 : 0;

        if (!Input.GetKey(leftKey) && !Input.GetKey(rightKey))
        {
            horizontalAxisValue = 0;
        }
        else
        {
            horizontalAxisValue = Input.GetKey(rightKey) ? 1 : -1;
        }
    }

    void Grounded()
    {
        isGrounded = Physics.Raycast(rayPosition.position, -transform.up, 0.5f, floorLayer) ? 1 : 0;
    }
}
