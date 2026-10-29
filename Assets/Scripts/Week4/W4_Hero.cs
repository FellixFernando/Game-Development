using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class W4_Hero : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpForce = 7f;

    private Vector2 moveInput;
    private Rigidbody rb;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void Update()
    {
        float h = moveInput.x;
        float v = moveInput.y;

        transform.Translate(
            new Vector3(h, 0f, v) * Time.deltaTime * moveSpeed
        );

        transform.Rotate(Vector3.up * h * 3f);

        if (isGrounded && Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                return;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
