using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementFirst : MonoBehaviour, IConveyBelt ,IFanpPlatform,IKnockback
{
    private Rigidbody rb;
    public GameObject rotateGfx;
    public Transform orientation;
   
    public Transform camTransform;
    [SerializeField] private LayerMask whatIsWall;
    //private LookAround lookAroundObject;//Wall run
    private CinemachinePOVExtension wallCinemachine;
    private IEnumerator bufferCoroutine;

    [Header("Player State")]
    private bool jumpPressed, isJumping;
    [SerializeField] private bool isOnGround;
    private bool isWallRun, canWallHop, isKnockBack = false;
    private bool isPlayerDeath=false;


    [Header("Input Setting")]
    private float horizontalInput, verticalInput;
    private bool sprint;

    [Header("Move Setting")]
    [SerializeField] private float defaultMaxSpeed;
    [SerializeField] private float moveSpeed, adjustedSpeed;
    public Vector3 inputVector;
    Vector3 targetVelocity;
    //private float turnSmoothTime = 0.1f , turnSmoothVelocity;
    //Vector3 newPosition;

    [Header("External Force Setting")]
    [SerializeField] private float sprintSpeed;
    private bool isSprinting;
    private float SprintingMaxSpeed=20f, currentMaxSpeed;
    Vector3 conveyorVector;

    [Header("Jumping Setting")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float rayCastDistance;
    //[SerializeField] private float fallMultiplier = 2.5f, lowJumpMultiplier = 2f;
    //private Vector3 jumpV;

    [Header("Gravity Setting")]
    [SerializeField] float gravityMultiplier = 12;
    [SerializeField] float airVerticalMultiplier;//z-axis
    [SerializeField] float airHorizontalMultiplier;//x-axis

    [Header("WallRun Setting")]
    [SerializeField] float wallRunStickForceDivider;
    [SerializeField] private float wallHopForce, extraPushSideForce = 10f;
    [SerializeField] private float maxWallSpeed, wallRunForce , minWallRunSpeed;
    [SerializeField] private float minWallAngle = 65, maxWallAngle = 115, signAngle;
    [SerializeField] private Vector3 normalVector;
    private bool isTouchingWall,isResetWallRun;


    [Header("External Force Setting")]
    Vector3 externalForce;
    [SerializeField] private float conveyMaxSpeed;

    

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        adjustedSpeed = moveSpeed;
        //lookAroundObject = FindObjectOfType<LookAround>().GetComponent<LookAround>();
        wallCinemachine = FindObjectOfType<CinemachinePOVExtension>().GetComponent<CinemachinePOVExtension>();
        camTransform = Camera.main.transform;
        currentMaxSpeed = defaultMaxSpeed;
    }

    void Update()
    {
        OrientationControl();
        GetPlayerInput();
        MovementCheck();
        SprintingCheck();

        if (isWallRun)
        {
            adjustedSpeed = adjustedSpeed/2;
        }
        else if (!isWallRun)
        {
            adjustedSpeed = moveSpeed;
        }

        if(isTouchingWall)
        {
            UIManager.instance.ShowWallRunTip();
        }
        else
        {
            UIManager.instance.ShowMovemntTip();
        }
       
       
    }

    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            isJumping = false;
        }
        JumpingCheck();
        ApplyForce();

    }

    void GetPlayerInput()
    {
        if (isPlayerDeath)
            return;
        if(!isKnockBack)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
        }
        sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        //jumpV = Vector3.zero;
        if (Input.GetButtonDown("Jump") && IsGrounded() && !isSprinting && !isWallRun)
        {
            jumpPressed = true;
        }
        else if (Input.GetButtonDown("Jump") && isTouchingWall && !IsGrounded())
        {
            canWallHop = true;
        }
        
    }

    void OrientationControl()
    {
        orientation.localEulerAngles = new Vector3(0, camTransform.localEulerAngles.y, 0);
    }

    

    Vector3 MovementCheck()
    {
        isOnGround = IsGrounded();
        
        inputVector = new Vector3(horizontalInput, 0, verticalInput);
        targetVelocity = orientation.transform.TransformDirection(inputVector * adjustedSpeed * Time.deltaTime);
        CheckSpeedLimit();


        //MovementClamping ();
        if (!isOnGround&&!isWallRun)
        {
            airVerticalMultiplier = 0.5f;//Deduce half force while not on ground
            airHorizontalMultiplier = 0.8f;

        }
        else
        {
            airVerticalMultiplier = 1;
            airHorizontalMultiplier = 1;
        }
        
        targetVelocity = new Vector3(targetVelocity.x * airHorizontalMultiplier, 0, targetVelocity.z * airVerticalMultiplier);
        return targetVelocity;
    }

    void SprintingCheck()
    {
        isSprinting = false;
        if (sprint && (Mathf.Abs(verticalInput) > 0|| Mathf.Abs(horizontalInput) > 0) && !jumpPressed)
        {
            isSprinting = true;
        }


        if (isSprinting)
        {
            adjustedSpeed = sprintSpeed;
            currentMaxSpeed = SprintingMaxSpeed;

        }
        else
        {
            adjustedSpeed = moveSpeed;
            currentMaxSpeed = defaultMaxSpeed;
        }
    }

    void CheckSpeedLimit()
    {
        //Clamp the amount of velocity after add force method
        //Debug.Log("Current velocity : "+rb.velocity.magnitude);
        //if (rb.velocity.magnitude > currentMaxSpeed)
        //{
        //    //Direct affecting velocity is a bad way

        //    targetVelocity = targetVelocity.normalized;
        //    targetVelocity *= currentMaxSpeed;
        //    Debug.Log("current magnitude is : " + rb.velocity.magnitude);
        //}
        

        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0f;
        targetVelocity = targetVelocity * (1 - (horizontalVelocity.magnitude / currentMaxSpeed));
    }

    IEnumerator RemoveControl()
    {
        isKnockBack = true;
        yield return new WaitForSeconds(2f);
        isKnockBack = false;
    }

    public void DoKnockback(Vector3 normalVector)
    {
        normalVector = new Vector3(Mathf.Abs(normalVector.x), normalVector.y, Mathf.Abs(normalVector.z));
        Vector3 knockbackForce = (normalVector * 10 + Vector3.up * 2);
        StartCoroutine(RemoveControl());
        rb.AddForce(knockbackForce, ForceMode.Impulse);
    }

    public void ApplyExternalForce(Vector3 eForce)
    {
        externalForce += eForce;
    }
    public void ApplyFanForce(Vector3 fanForce,float maxFanSpeed)
    {
        if(rb.velocity.magnitude < maxFanSpeed)
        {
            ApplyExternalForce(fanForce);
        }

    }

    public void ApplyConveyorForce(Vector3 direction)
    {
        conveyorVector += direction;
    }

    void ApplyForce()
    {
        Vector3 moveV = MovementCheck();
        Vector3 gravityV = ApplyGravity();
        Vector3 wallHopVector = WallHopCheck(normalVector);
        

        //Do Clapming
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * GravityClamping(), rb.velocity.z);
        //Do movePosition first 
        rb.MovePosition(transform.position + conveyorVector* Time.deltaTime);
        //Clamp here 
        rb.AddForce(moveV + gravityV + externalForce + wallHopVector, ForceMode.Force);
        //Clamp the amount of velocity after add force

        //Debug.Log("External Force Value : " + externalForce);

        //Reset force and state
        externalForce = Vector3.zero;
        conveyorVector = Vector3.zero;
    }


    private float GravityClamping()
    {
        float wallClamp;
        //Normal Gravity Clamp
        if (rb.velocity.y <-20f)
        {
            wallClamp = 0.95f;
        }

        //Clamp for wall Running
        else if (isWallRun && (rb.velocity.y < -8f || rb.velocity.y > 3f))
        {
            wallClamp = 0.8f;
        }
        else
        {
            wallClamp = 1f;
        }

        return wallClamp;
    }

    void JumpingCheck()
    {
        if (jumpPressed)
        {
            //One-Time Event , so this will not affect a lot
            Vector3 jumpVector = Vector3.up * jumpForce;
            //Debug.Log("Jump is apply Vector : " + jumpVector);
            rb.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
            isJumping = true;
            jumpPressed = false;
            
        }
    }

    Vector3 ApplyGravity()
    {
        //Debug.Log("Current Player Velocity Y : " + rb.velocity.y);
        //set Clamp 
        Vector3 gravityVector;
        if (rb.velocity.y > -18f)
        {
            gravityVector = Vector3.down * Time.deltaTime * gravityMultiplier;
        }
        else
        {
            gravityVector = Vector3.zero;
        }
        return gravityVector;

    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, rayCastDistance);
    }

    bool CheckWallRunRequirement()
    {
        bool canWallRun;
        //Check for player velocity , use int to reduce accuracy
        int intVelocityZ = Convert.ToInt32(rb.velocity.z);
        int intVelocityX = Convert.ToInt32(rb.velocity.x);
        //min wall Run Speed is 6
        if ((Math.Abs(intVelocityZ) > minWallRunSpeed || Math.Abs(intVelocityX) > minWallRunSpeed) && !isOnGround)
        {
            canWallRun = true;
        }
        else
        {
            canWallRun = false;
        }
        return canWallRun;
    }

    float orienSign;
    void DoWallRun(Vector3 norVector, IRunnableWall runWall, bool isReset)
    {
        
        Vector3 stickVector;
        if (CheckWallRunRequirement()&&!isReset)
        {
            if (rb.useGravity == true)
            {
                rb.useGravity = runWall.DisableGravity();
                isWallRun = true;
            }

            stickVector = -norVector * (wallRunForce / wallRunStickForceDivider) * Time.deltaTime;
            ApplyExternalForce(stickVector);
            //pnSign = signAngle / Mathf.Abs(signAngle);
            //Debug.Log("Stick Vector : " + stickVector);
            //lookAroundObject.wallRunCamera(povNegSign);
            wallCinemachine.wallRunCamera(CheckWallSide());

        }
        else if(isReset)
        {
            rb.useGravity = runWall.EnableGravity();
            //lookAroundObject.wallRunCameraReset(pnSign);
            wallCinemachine.wallRunCameraReset();
            isWallRun = false;
            isResetWallRun = false;
        }
        else//This is required
        {
            rb.useGravity = runWall.EnableGravity();
            wallCinemachine.wallRunCameraReset();
            isWallRun = false;
        }
    }

    //NormalV is collision.contact[].normal
    Vector3 WallHopCheck(Vector3 normalV)
    {
        Vector3 wallHopVector;
        if (canWallHop && isWallRun)
        {
            //Run Wall Hop 
            wallHopVector = ((Vector3.up * 7) + normalV * extraPushSideForce + (orientation.forward * 0.3f)) * wallHopForce * Time.deltaTime;
            canWallHop = false;
            
        }
        else if (canWallHop && !isWallRun)
        {
            wallHopVector = (Vector3.up * (10) + normalV * extraPushSideForce) * wallHopForce * Time.deltaTime;
            canWallHop = false;
        }
        else
        {
            canWallHop = false;
            wallHopVector = Vector3.zero;

        }

        return wallHopVector;
    }

    private void OnCollisionEnter(Collision collision)//One time event
    {
        
        IRunnableWall runnableWall = collision.gameObject.GetComponent<IRunnableWall>();
        if (runnableWall != null && CheckWallRunRequirement())
        {
            isWallRun = true;
        }
    }

    private void OnCollisionStay(Collision collision)//Dynamic event
    {
        IRunnableWall runnableWall = collision.gameObject.GetComponent<IRunnableWall>();
        //Debug.Log("Player Speed min is 7: " + rb.velocity.z);
        //Debug.Log("Can Wall run ?  : " + CheckWallRunRequirement(setBuffer));
        if (runnableWall != null)//First prior check it is runnable wall
        {
            signAngle = Vector3.SignedAngle(Vector3.up, collision.contacts[0].normal, collision.contacts[0].point);
            normalVector = collision.contacts[0].normal;
            //Debug.Log("Can Wall run : " + CheckWallRunRequirement());
            if (Math.Abs(signAngle) > minWallAngle && Math.Abs(signAngle) < maxWallAngle)//Second prior check the wall angle is qualifiied or not
            {
                //Do all wall run and wall jump
                isTouchingWall = true;
                //pnSign = signAngle / Mathf.Abs(signAngle);
                DoWallRun(normalVector, runnableWall,isResetWallRun);
            }
        }
        

    }

    private void OnCollisionExit(Collision collision)
    {
        
        IRunnableWall runnableWall = collision.gameObject.GetComponent<IRunnableWall>();
        if (runnableWall != null && isWallRun)
        {
            isResetWallRun = true;
            DoWallRun(normalVector, runnableWall,isResetWallRun);
            //rb.useGravity = runnableWall.EnableGravity();
            //lookAroundObject.wallRunCameraReset(pnSign);
            //isWallRun = false;
            isTouchingWall = false;
        }
        else if(runnableWall!=null)
        {
            isTouchingWall = false;
        }
    }

    float rayCastDistant = 1.5f;
    float CheckWallSide()
    {
        bool isWallRight, isWallLeft;
        float sign = 1;

        isWallRight = Physics.Raycast(transform.position, orientation.right, rayCastDistant, whatIsWall);
        isWallLeft = Physics.Raycast(transform.position, -orientation.right, rayCastDistant, whatIsWall);

        

        if (isWallRight)
        {
            //wallRunCameraTilt += Time.deltaTime * maxWallRunCameraTilt * 2;
            sign = 1;
            //Debug.Log("Sign is positive : " + sign);
        }
        else if (isWallLeft)
        {
            //wallRunCameraTilt -= Time.deltaTime * maxWallRunCameraTilt * 2;
            sign = -1;
            //Debug.Log("Sign is negative : " + sign);
        }
        
        return sign;
    }

    public void RemovePlayerControl()
    {
        isPlayerDeath = true;
    }

    public void ResetPlayerControl()
    {
        isPlayerDeath = false;
    }
}
