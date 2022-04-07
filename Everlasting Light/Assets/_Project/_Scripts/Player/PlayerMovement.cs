using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    PlayerInputHandler inputHandler;

    //[SerializeField] PlayerDataSO playerData;
    [Header("Move Stat")]
    [SerializeField] float acceleration = 0.5f;
    [SerializeField] float maxHorizontalVelocity = 3f;
    [SerializeField] float hStopDamping = 0.5f;
    [SerializeField] float hTurnDamping = 0.5f;

    [Header("Jump Stat")]
    [SerializeField] float jumpSpeed = 10f;

    [Header("Ground Check")]
    [SerializeField] Vector2 boxCastSize = new Vector2(0.5f, 0.5f);
    [SerializeField] float groundCheckDistance = 0.5f;
    [SerializeField] LayerMask platformMask;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        inputHandler = GetComponent<PlayerInputHandler>();
    }

    public void Move()
    {
        Vector2 currentVelocity = myRigidbody.velocity;
        currentVelocity += new Vector2(inputHandler.MoveInput.x * acceleration, 0);

        currentVelocity.x = MovementDamping(currentVelocity, inputHandler.MoveInput.x, hStopDamping, hTurnDamping);

        currentVelocity.x = Mathf.Clamp(currentVelocity.x, -maxHorizontalVelocity, maxHorizontalVelocity);
        myRigidbody.velocity = currentVelocity;

        Flip();
    }

    public void Stop()
    {
        Vector2 currentVelocity = myRigidbody.velocity;
        currentVelocity.x = MovementDamping(currentVelocity, 0f, 1f, 1f);
        myRigidbody.velocity = currentVelocity;
    }

    private float MovementDamping(Vector2 currentVelocity, float input, float hStopDamping, float hTurnDamping)
    {
        if (Mathf.Abs(currentVelocity.x) < 0.01f)
        {
            return 0f;
        }
        else if (Mathf.Abs(input) < Mathf.Epsilon)
        {
            //Debug.Log("Stop");
            return currentVelocity.x *= 1f - hStopDamping;
        }
        else if (Mathf.Sign(input) != Mathf.Sign(currentVelocity.x))
        {
            //Debug.Log("Turn");
            return currentVelocity.x *= 1f - hTurnDamping;
        }
        else
        {
            return currentVelocity.x;
        }
    }

    public void Jump()
    {
        if (CheckIfGrounded() && inputHandler.JumpInput)
        {
            Vector2 newVerticalVelocity = new Vector2(myRigidbody.velocity.x, jumpSpeed);
            myRigidbody.velocity = newVerticalVelocity;
        }
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.BoxCast(transform.position, boxCastSize, 0f, -transform.up, groundCheckDistance, platformMask);
    }

    private void Flip()
    {
        if (Mathf.Abs(inputHandler.MoveInput.x) <= Mathf.Epsilon) { return; }
        transform.localScale = new Vector3(Mathf.Sign(myRigidbody.velocity.x), 1, 1);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position + (-transform.up * groundCheckDistance), boxCastSize);
    }
}
