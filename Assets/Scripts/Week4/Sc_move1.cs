using UnityEngine;
using UnityEngine.InputSystem;

public class Sc_move1 : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotation = 5f;
    [SerializeField] private float jumpForce = 5f;

    private bool isGrounded;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(0f, 0f, v) * speed * Time.deltaTime);
        transform.Rotate(new Vector3(0f, h, 0f) * rotation);

        if (isGrounded && Keyboard.current.spaceKey.wasPressedThisFrame)
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
