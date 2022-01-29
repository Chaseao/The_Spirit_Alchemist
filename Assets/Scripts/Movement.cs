using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float movementMultiplier = 3f;
    [SerializeField] float jumpMultiplier = 3f;
    [SerializeField] float characterHeight = 3f;

    Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        ControllerInput.move += OnMove;
        ControllerInput.jump += OnJump;
    }

    private void OnMove(float direction)
    {
        float horizontalVelocity = direction * movementMultiplier;
        float verticalVelocity = rb.velocity.y;

        rb.velocity = new Vector2(horizontalVelocity, verticalVelocity);
    }

    private void OnJump()
    {
        if (TouchingGround())
        {
            Jump();
        }
    }

    private bool TouchingGround()
    {
        LayerMask platformLayerMask = LayerMask.GetMask("Platforms");

        RaycastHit2D hitInformation = Physics2D.Raycast(transform.position, Vector2.down, characterHeight, platformLayerMask);

        return hitInformation && hitInformation.collider;
    }

    private void Jump()
    {
        float horizontalVelocity = rb.velocity.x;
        float verticalVelocity = jumpMultiplier;

        rb.velocity = new Vector2(horizontalVelocity, verticalVelocity);
    }

    private void OnDisable()
    {
        ControllerInput.move -= OnMove;
        ControllerInput.jump -= OnJump;
    }
}
