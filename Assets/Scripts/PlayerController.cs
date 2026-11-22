using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public Collider2D col;
    public Vector2 playerInput = new Vector2();
    public FacingDirection facing;
    public enum FacingDirection
    {
        left, right
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {

        if (Keyboard.current.aKey.isPressed)
        {
            playerInput.x = -1f;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            playerInput.x = 1f;
        } else
        {
            playerInput.x = 0f;
        }
        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.

        MovementUpdate(playerInput);
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        rb.linearVelocity = playerInput * speed;
        IsWalking();
        facing = GetFacingDirection();
    }

    public bool IsWalking()
    {
        if (playerInput.x != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IsGrounded()
    {
        return col.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    public FacingDirection GetFacingDirection()
    {
        if (playerInput.x > 0)
        {
            return FacingDirection.right;
        }
        else if (playerInput.x < 0)
        {
            return FacingDirection.left;
        } else
        {
            return facing;
        }
    }
}
