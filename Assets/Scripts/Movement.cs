using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using PixelCrushers.DialogueSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float movementMultiplier = 3f;
    [SerializeField] float jumpMultiplier = 3f;
    [SerializeField] float characterHeight = 3f;
    [SerializeField] float climbSpeed = 3f;
    [SerializeField] float climbStartOffset = 0.2f;
    [SerializeField] float circleRadius = 0.2f;
    [SerializeField, ReadOnly] bool currentlyClimbing;
    [SerializeField, ReadOnly] bool currentlyInteracting;

    Rigidbody2D rb;
    float originalGravityScale;
    float currentMoveVelocity = 0;

    Collider2D playerCollider;

    Animator animator;

    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        currentlyClimbing = false;
        originalGravityScale = rb.gravityScale;
    }

    private void OnEnable()
    {
        ControllerInput.move += OnMove;
        ControllerInput.jump += OnJump;
        ControllerInput.climb += OnClimb;
    }

    private void Update()
    {
        if(TouchingVines() && TouchingGround() && !InsideGround())
        {
            StopClimbing();
        }
        else if (!TouchingVines())
        {
            StopClimbing();
        }

        SetHorizontalVelocity(currentMoveVelocity);
    }

    private void OnMove(float direction)
    {
        if (!currentlyClimbing && !currentlyInteracting)
        {
            currentMoveVelocity = direction * movementMultiplier;
            animator.SetBool("Moving", direction != 0);
            if (direction != 0) 
            {
                Transform image = transform.GetChild(0);
                image.localScale = new Vector2(Mathf.Abs(image.localScale.x) * direction, image.localScale.y);
            }
        }
    }

    private void OnJump()
    {
        if (TouchingGround() && !currentlyClimbing && !currentlyInteracting)
        {
            Jump();
        }
    }

    private void Jump()
    {
        SetVerticalVelocity(jumpMultiplier);
    }

    private void OnClimb(float climbDirection)
    {
        if (TouchingVines() && !currentlyInteracting)
        {
            if (!currentlyClimbing && climbDirection == 1)
            {
                StartClimbing();
            }

            if (currentlyClimbing)
            {
                SetVerticalVelocity(climbSpeed * climbDirection);
            }
        }
    }

    private void StartClimbing()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + climbStartOffset);
        currentlyClimbing = true;
        rb.gravityScale = 0;
        playerCollider.enabled = false;
        currentMoveVelocity = 0;
        animator.SetBool("Moving", false);
        animator.SetBool("Climbing", true);
    }

    private void StopClimbing()
    {
        currentlyClimbing = false;
        rb.gravityScale = originalGravityScale;
        playerCollider.enabled = true;
        animator.SetBool("Climbing", false);
    }

    public void OnInteractionBegin()
    {
        currentlyInteracting = true;
        currentMoveVelocity = 0;
        animator.SetBool("Moving", false);
    }

    public void OnInteractionEnd()
    {
        currentlyInteracting = false;
    }

    private bool TouchingGround()
    {
        LayerMask platformLayerMask = LayerMask.GetMask(LayerMask.LayerToName(6));

        RaycastHit2D hitInformation = Physics2D.CircleCast(transform.position + Vector3.down * characterHeight, circleRadius, Vector2.down, 0.2f, platformLayerMask);

        return hitInformation && hitInformation.collider;
    }
    private bool InsideGround()
    {
        LayerMask platformLayerMask = LayerMask.GetMask(LayerMask.LayerToName(6));

        RaycastHit2D hitInformation = Physics2D.CircleCast(transform.position, circleRadius, Vector2.down, characterHeight - 0.1f, platformLayerMask);

        return hitInformation && hitInformation.collider;
    }

    private void SetVerticalVelocity(float verticalVelocity)
    {
        rb.velocity = new Vector2(rb.velocity.x, verticalVelocity);
    }

    private void SetHorizontalVelocity(float horizontalVelocity)
    {
        rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);
    }

    private bool TouchingVines()
    {
        LayerMask vinesLayerMask = LayerMask.GetMask(LayerMask.LayerToName(7));

        float climbingOffset = currentlyClimbing ? 0.1f : -0.1f;

        RaycastHit2D hitInformation = Physics2D.CircleCast(transform.position, circleRadius, Vector2.down, characterHeight + climbingOffset, vinesLayerMask);

        return hitInformation && hitInformation.collider;
    }

    private void OnDisable()
    {
        ControllerInput.move -= OnMove;
        ControllerInput.jump -= OnJump;
        ControllerInput.climb -= OnClimb;
    }
}
