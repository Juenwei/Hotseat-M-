using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    Animator animator;
    private bool isFrezeeAnimation=false;
    int runFwdHash;
    int runBackHash;
    int jumpHash;

    void Start()
    {
        animator = GetComponent<Animator>();
        runFwdHash = Animator.StringToHash("RunFwd");
        runBackHash = Animator.StringToHash("RunBack");
        jumpHash = Animator.StringToHash("Jump");
    }

    void Update()
    {
        if (!isFrezeeAnimation)
            PlayerInput();
    }

    void PlayerInput()
    {
        bool forwardPressed = Input.GetKey("w");
        bool backwardPressed = Input.GetKey("s");
        bool jumpPressed = Input.GetKey("space");
        bool isRunningFWD = animator.GetBool(runFwdHash);
        bool isRunningBack = animator.GetBool(runBackHash);
        bool isJumping = animator.GetBool(jumpHash);

        if (forwardPressed && !isRunningFWD)
        {
            animator.SetBool(runFwdHash, true);
        }
        if (!forwardPressed && isRunningFWD)
        {
            animator.SetBool(runFwdHash, false);
        }

        if (backwardPressed && !isRunningBack)
        {
            animator.SetBool(runBackHash, true);
        }

        if (!backwardPressed && isRunningBack)
        {
            animator.SetBool(runBackHash, false);
        }

        if (jumpPressed && !isJumping)
        {
            animator.SetBool(jumpHash, true);
        }
        if (!jumpPressed && isJumping)
        {
            animator.SetBool(jumpHash, false);
        }


    }

    public void FreezeAnimation()
    {
        isFrezeeAnimation = true;
    }

    public void InvokeAnimation()
    {
        isFrezeeAnimation = false;
    }
}
