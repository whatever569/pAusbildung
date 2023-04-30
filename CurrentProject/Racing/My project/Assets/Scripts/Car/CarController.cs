using UnityEngine;
using System.Collections;
public class CarController : MonoBehaviour
{
    //make z turning with horizontal input and when not grounded
    //Todo: Make xturining with Vertical input and when not grounded
    //similar to gta
    //Optimized version of the car controller script on my github www.github.com/whatever569

    #region Variables
    [SerializeField] private float acceleration = 3f, yTurnSpeed = 180f
            , defaultDrag = 3f, inAirDrag = 0.1f
                , rollSpeed = 2.5f, groundCheckDistance = 0.5f, jumpForce = 5f;
    [SerializeField] private Transform groundCheckOrigin;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private ForceMode rollForceMode = ForceMode.Impulse, jumpForceMode = ForceMode.Impulse;

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
    private void Start() 
    {
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

    /// <summary>Get current CarController settings</summary>
    public CarControllerSettings GetCarControllerSettings()
    {
        return new CarControllerSettings(acceleration, yTurnSpeed, defaultDrag, inAirDrag, rollSpeed, groundCheckDistance, jumpForce, groundCheckOrigin, groundLayer, rollForceMode, jumpForceMode);
    }
    
    /// <summary>
    /// Set the CarController settings to custom values, with a CarControllerSettings object
    /// </summary>
    ///Tip: You can use the GetCarControllerSettings() method to get the current settings, then edit them as needed and then use this method to set the settings
    public void SetCarControllerSettings(CarControllerSettings settings)
    {
        this.acceleration = settings.Acceleration;
        this.yTurnSpeed = settings.YTurnSpeed;
        this.defaultDrag = settings.DefaultDrag;
        this.inAirDrag = settings.InAirDrag;
        this.rollSpeed = settings.RollSpeed;
        this.groundCheckDistance = settings.GroundCheckDistance;
        this.jumpForce = settings.JumpForce;
        this.groundCheckOrigin = settings.GroundCheckOrigin;
        this.groundLayer = settings.GroundLayer;
        this.rollForceMode = settings.RollForceMode;
        this.jumpForceMode = settings.JumpForceMode;
    }
}
