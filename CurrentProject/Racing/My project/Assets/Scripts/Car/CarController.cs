using UnityEngine;
using System.Collections;
public class CarController : MonoBehaviour
{
    //make z turning with horizontal input and when not grounded
    //Make xturining with Vertical input and when not grounded\
    //similar to gta
    //Optimized version of the car controller script on my github
    #region Variables
    // Configuration fields for car behavior
    [SerializeField] private float acceleration = 3f, yTurnSpeed = 180f
            , defaultDrag = 3f, inAirDrag = 0.1f
                , rollSpeed = 2.5f, groundCheckDistance = 0.5f, turningDampening = 0.5f, minimumYTurnSpeed = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private ForceMode rollForceMode = ForceMode.Impulse, jumpForceMode = ForceMode.Impulse;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Transform groundCheckOrigin;

    // Internal fields for storing component references and input values
    private Transform _transform;
    private Rigidbody _rigidbody;
    private bool _isGrounded;
    private float _verticalInput, _horizontalInput;
    #endregion 

    // Initialize component references on Awake
    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Start() {
        StartCoroutine(GroundChecking());
    }

    // Get user input and check for grounding in Update
    private void Update()
    {
        GetUserInput();
    }

//coroutine for ground checking, saves performance
     IEnumerator GroundChecking()
    {
        while(true) {CheckGround(); yield return new WaitForSeconds(0.1f);}
    }

    // Perform physics-based movement and rotation in FixedUpdate
    private void FixedUpdate()
    {
        Rotate();
        Move();
        UpdateDrag();
        Jumping();
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

    // Rotate the car around Y axis based on horizontal inputprivate void RotateYAxis()
    private void RotateYAxis()
    {
    
        if (_isGrounded && _verticalInput != 0)
        {
            float rotationAngle = yTurnSpeed * _horizontalInput * Time.deltaTime;
            _transform.Rotate(Vector3.up, rotationAngle);
        }
    }

    private void RotateZAxis()
    {
        if (!_isGrounded)
        {
            Vector3 torque = _transform.forward * rollSpeed * _horizontalInput;
            _rigidbody.AddTorque(torque, rollForceMode);
        }
    }

    // Retrieve user input for movement and rotation
    private void GetUserInput()
    {
        _verticalInput = Input.GetAxis("Vertical");
        _horizontalInput = Input.GetAxis("Horizontal");
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

    private void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, jumpForceMode);
        }
    }
    #endregion
}
