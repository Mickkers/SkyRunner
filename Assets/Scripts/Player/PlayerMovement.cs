using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CapsuleCollider2D capsuleCollider;
    private Animator animator;
    private Rigidbody2D rbody;
    private AudioController audioController;

    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpHeight;


    [SerializeField] float wallDistance;
    [SerializeField] private float groundDistance;
    [SerializeField] private ContactFilter2D castFilter;

    RaycastHit2D[] wallHits = new RaycastHit2D[10];
    RaycastHit2D[] groundHits = new RaycastHit2D[10];
    private bool _isGrounded;
    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }

    private bool _isMoving = false;
    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            audioController.WalkSFX(_isMoving && IsGrounded);
            animator.SetBool(AnimationStrings.isMoving, _isMoving);
        }
    }

    private bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }

            _isFacingRight = value;
        }
    }
    private bool _isOnWall;
    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        audioController = FindObjectOfType(typeof(AudioController)) as AudioController;
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded = capsuleCollider.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = capsuleCollider.Cast(IsFacingRight ? Vector2.right : Vector2.left, castFilter, wallHits, wallDistance) > 0;

    }

    public void Movement(float value)
    {
        IsMoving = value != 0;
        if(IsOnWall && value != 0f || transform.position.x <= -8.55f && value < 0)
        {
            rbody.velocity = new Vector2(0f, rbody.velocity.y);
        }
        else
        {
            rbody.velocity = new Vector2(value * playerSpeed, rbody.velocity.y);
        }
        SetFacingDirection(value);
        animator.SetFloat(AnimationStrings.yVelocity, rbody.velocity.y);
    }
    private void SetFacingDirection(float value)
    {
        if (value > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (value < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void Jump()
    {
        if (!IsGrounded) return;
        audioController.JumpSFX();
        animator.SetTrigger(AnimationStrings.jump);
        rbody.velocity = new Vector2(rbody.velocity.x, jumpHeight);
    }
}
