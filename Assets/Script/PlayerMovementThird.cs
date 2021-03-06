using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementThird : MonoBehaviour
{
    private Rigidbody rb;
    private bool jumpPressed;
    public GameObject rotateGfx;
    public Transform orientation;
    public Transform camTransform;

    [Header("Input Setting")]
    private float horizontalInput, verticalInput;
    private bool sprintInput;

    [Header("Move Setting")]
    [SerializeField]
    private float moveSpeed = 5, adjustedSpeed;
    public Vector3 inputVector;
    private float turnSmoothTime = 0.1f, turnSmoothVelocity;
    float currentMaxSpeed;
    Vector3 newPosition;
    Vector3 targetVelocity;

    [Header("Sprint Setting")]
    [SerializeField] private float sprintSpeed = 650f;
    private bool isSprinting;

    [Header("Jumping Setting")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float rayCastDistance;
    [SerializeField] private float fallMultiplier = 1.5f, lowJumpMultiplier = 1.2f;


    void Awake()
    {
        
        rb = GetComponent<Rigidbody>();
        currentMaxSpeed = moveSpeed;
        adjustedSpeed = moveSpeed;
    }

    void Update()
    {
        getPlayerInput();
        Jumping();
        Sprinting();
        //rotatePlayer();
        //checkSpeedLimit();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void getPlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        sprintInput = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        inputVector = Vector3.zero;
        inputVector = new Vector3(horizontalInput, rb.velocity.y, verticalInput);

        if (inputVector.magnitude > 1)//This might affect jumping
        {
            inputVector.Normalize();
        }


        if (Input.GetButtonDown("Jump") && IsGrounded()&&!isSprinting)
        {
            jumpPressed = true;
        }
    }


    void Movement()
    {
        //inputVector.Normalize();
        //targetVelocity = transform.TransformDirection(inputVector * adjustedSpeed * Time.deltaTime);
        //targetVelocity.y = rb.velocity.y;
        targetVelocity = RotateWithView() * adjustedSpeed * Time.deltaTime;
        targetVelocity.y = rb.velocity.y;
        rb.velocity = targetVelocity;
    }

    Vector3 RotateWithView()
    {
        if(camTransform!=null)
        {
            Vector3 dir = orientation.transform.TransformDirection(inputVector);//
            //dir = dir.normalized * inputVector.magnitude;
            dir.Set(dir.x, 0, dir.z);
            return dir;


        }
        else
        {
            Debug.Log("Camera transform is not assigned , now fetching the transformation of camera");
            camTransform = Camera.main.transform;
            Debug.Log("Fetching Complete");
            return inputVector;
        }
    }

    void ChangeOrientation()
    {
        
    }

    void Sprinting()
    {
        isSprinting = false;
        if (sprintInput && verticalInput > 0 && !jumpPressed)
        {
            isSprinting = true;
        }


        if (isSprinting)
        {
            adjustedSpeed = sprintSpeed;

        }
        else
        {
            adjustedSpeed = moveSpeed;
        }
    }

    void Jumping()
    {
        if (jumpPressed)
        {
            //isOnGround = false;
            rb.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
            jumpPressed = false;
            
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }

    }

    private bool IsGrounded()
    {
        Debug.DrawRay(transform.position, Vector3.down * rayCastDistance, Color.red);
        return Physics.Raycast(transform.position, Vector3.down, rayCastDistance);
    }

    //This function affect the input vector
    void rotatePlayer()
    {
        Vector3 direction = inputVector.normalized;

        if (direction.magnitude >= 0.1f)
        {
            // rotate the player direction
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camTransform.eulerAngles.y;

            // function of smoothing 
            //rotateGfx.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.localRotation = Quaternion.Euler(0f, angle, 0f);
            orientation.transform.localRotation = Quaternion.Euler(0f, angle, 0f);


            //Quaternion rotation = Quaternion.LookRotation(cam.transform.eulerAngles);
            //Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
    }

    
}
