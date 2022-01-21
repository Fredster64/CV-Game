using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public Animator animator;

    private Vector2 movementInputVector;
    private Vector2 latestNetMovementVector;
    private bool facingRight;
    private bool playerCanMove = true;
    private float slipperiness = 0;

    private const float movementSpeed = 3f;
    private const float slipperinessOnIce = 0.975f;
    
    void Update()
    {
        movementInputVector.x = playerCanMove ? Input.GetAxisRaw("Horizontal") : 0;
        movementInputVector.y = playerCanMove ? Input.GetAxisRaw("Vertical") : 0;

        facingRight = movementInputVector.x > 0 || facingRight && movementInputVector.x == 0;
        animator.SetBool("FacingRight", facingRight);
    }

    void FixedUpdate()
    {
        if (playerCanMove)
        {
            var slipperinessFactor = slipperiness * latestNetMovementVector;
            var movementInputFactor = (1 - slipperiness) * movementInputVector;
            latestNetMovementVector = slipperinessFactor + movementInputFactor;

            var positionChange = latestNetMovementVector * movementSpeed * Time.fixedDeltaTime;
            rigidBody.MovePosition(rigidBody.position + positionChange);
        }
    }

    void LateUpdate()
    {
        animator.SetFloat("Speed", latestNetMovementVector.magnitude * movementSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6) // Ice layer
        {
            slipperiness = slipperinessOnIce;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 6) // Ice layer
        {
            slipperiness = 0f;
        }
    }

    public Vector3 GetPlayerPosition()
    {
        return gameObject.transform.position;
    }

    public void TogglePlayerMovement(bool active)
    {
        playerCanMove = active;
    }
}
