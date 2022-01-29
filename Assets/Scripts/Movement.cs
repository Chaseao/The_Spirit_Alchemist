using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Movement : MonoBehaviour
{
    [SerializeField] float movementMultiplier = 3f;
    [SerializeField] float jumpMultiplier = 3f;
    [SerializeField] float characterHeight = 3f;
    [SerializeField] float climbSpeed = 3f;
    [SerializeField] float climbStartOffset = 0.2f;
    [SerializeField, ReadOnly] bool currentlyClimbing;

    Rigidbody2D rb;
    float originalGravityScale;

    Collider2D playerCollider;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
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
    }

    private void OnMove(float direction)
    {
        if (!currentlyClimbing)
        {
            SetHorizontalVelocity(direction * movementMultiplier);
        }
    }

    private void OnJump()
    {
        if (TouchingGround() && !currentlyClimbing)
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
        if (TouchingVines() && !currentlyClimbing && climbDirection != 0)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + climbStartOffset);
            StartClimbing();
            SetVerticalVelocity(climbSpeed * climbDirection);
        }

        else if (TouchingVines())
        {
            if (climbDirection == 0)
            {
                SetVerticalVelocity(0);
            }
            else
            {
                SetVerticalVelocity(climbSpeed * climbDirection);
            }
        }
    }

    private void StartClimbing()
    {
        currentlyClimbing = true;
        rb.gravityScale = 0;
        playerCollider.enabled = false;
        SetHorizontalVelocity(0);
    }

    private void StopClimbing()
    {
        currentlyClimbing = false;
        rb.gravityScale = originalGravityScale;
        playerCollider.enabled = true;
    }

    private bool TouchingGround()
    {
        LayerMask platformLayerMask = LayerMask.GetMask(LayerMask.LayerToName(6));

        RaycastHit2D hitInformation = Physics2D.Raycast(transform.position, Vector2.down, characterHeight, platformLayerMask);

        return hitInformation && hitInformation.collider;
    }
    private bool InsideGround()
    {
        LayerMask platformLayerMask = LayerMask.GetMask(LayerMask.LayerToName(6));

        RaycastHit2D hitInformation = Physics2D.Raycast(transform.position, Vector2.down, characterHeight - 0.1f, platformLayerMask);

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

        RaycastHit2D hitInformation = Physics2D.Raycast(transform.position, Vector2.down, characterHeight + climbingOffset, vinesLayerMask);

        return hitInformation && hitInformation.collider;
    }

    private void OnDisable()
    {
        ControllerInput.move -= OnMove;
        ControllerInput.jump -= OnJump;
        ControllerInput.climb -= OnClimb;
    }
}
