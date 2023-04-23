using UnityEngine;

public class CarController : MonoBehaviour
{
    //Optimized version of the car controller script on my github
    #region Variables
    // Configuration fields for car behavior
    [SerializeField] private float acceleration = 3f, yTurnSpeed = 180f, defaultDrag = 3f, inAirDrag = 0.1f, zTurnSpeed = 2.5f, groundCheckDistance = 0.5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private ForceMode zTurnForceMode = ForceMode.Impulse;
    [SerializeField] private Transform groundCheckOrigin;

    // Internal fields for storing component references and input values
    private Transform _transform;
    private Rigidbody _rigidbody;
    private bool _isGrounded;
    private float _verticalInput, _horizontalInput, _zInput;
    #endregion 

    // Initialize component references on Awake
    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Get user input and check for grounding in Update
    private void Update()
    {
        GetUserInput();
        CheckGround();
    }

    // Perform physics-based movement and rotation in FixedUpdate
    private void FixedUpdate()
    {
        Rotate();
        Move();
        UpdateDrag();
    }
    #region Movement
    // Apply forward and backward movement based on user input
    private void Move()
    {
        if (!_isGrounded) return;
        Vector3 force = _transform.forward * _verticalInput * acceleration * 100;
        _rigidbody.AddForce(force);
    }

    // Perform rotation in Y and Z axes based on user input
    private void Rotate()
    {
        RotateYAxis();
        RotateZAxis();
    }

    // Rotate the car around Y axis based on horizontal input
    private void RotateYAxis()
    {
        if (_isGrounded && _verticalInput != 0)
        {
            float rotationAngle = yTurnSpeed * _horizontalInput * Time.deltaTime;
            _transform.Rotate(Vector3.up, rotationAngle);
        }
    }

    // Rotate the car around Z axis based on Z input (E and C keys)
    private void RotateZAxis()
    {
        if (!_isGrounded)
        {
            Vector3 torque = _transform.forward * zTurnSpeed * _zInput;
            _rigidbody.AddTorque(torque, zTurnForceMode);
        }
    }

    // Retrieve user input for movement and rotation
    private void GetUserInput()
    {
        _verticalInput = Input.GetAxis("Vertical");
        _horizontalInput = Input.GetAxis("Horizontal");
        _zInput = Input.GetKey(KeyCode.E) ? 1 : (Input.GetKey(KeyCode.C) ? -1 : 0);
    }

    // Check if the car is grounded using a raycast
    private void CheckGround()
    {
        _isGrounded = Physics.Raycast(groundCheckOrigin.position, -_transform.up, groundCheckDistance, groundLayer);
    }

    // Update the rigidbody's drag based on whether the car is grounded or in the air
    private void UpdateDrag()
    {
        _rigidbody.drag = _isGrounded ? defaultDrag : inAirDrag;
    }
    #endregion
}
