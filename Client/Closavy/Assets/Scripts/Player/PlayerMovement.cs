using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody rb;
    private bool jumpRequested;
    private bool isGrounded;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
            input.y += 1;

        if (Keyboard.current.sKey.isPressed)
            input.y -= 1;

        if (Keyboard.current.dKey.isPressed)
            input.x += 1;

        if (Keyboard.current.aKey.isPressed)
            input.x -= 1;

        Vector3 movement = new(input.x, 0f, input.y);

        if (jumpRequested)
        {
            Jump();
            jumpRequested = false;
        }

        rb.linearVelocity = new Vector3(
            movement.x * moveSpeed,
            rb.linearVelocity.y,
            movement.z * moveSpeed);
    }

    private void Jump()
    {
        Debug.Log("JUMP!");

        rb.linearVelocity = new Vector3(
            rb.linearVelocity.x,
            5f,
            rb.linearVelocity.z);
    }
}
