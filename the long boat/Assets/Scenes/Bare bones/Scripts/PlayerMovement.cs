using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 6f;
    public float mouseSensitivity = 2f;
    public Transform cameraHolder;
    public LayerMask groundMask;

    private Rigidbody rb;
    private float verticalLookRotation = 0f;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        cameraHolder.localEulerAngles = new Vector3(verticalLookRotation, 0, 0);

        // Jump
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundMask);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Re-orient if tipped over
        if (Vector3.Dot(transform.up, Vector3.up) < 0.5f)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StandUp();
            }
        }
    }

    void FixedUpdate()
    {
        // Movement
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 move = transform.TransformDirection(input) * moveSpeed;
        Vector3 velocity = rb.linearVelocity;
        velocity.x = move.x;
        velocity.z = move.z;
        rb.linearVelocity = velocity;
    }

    void StandUp()
    {
        transform.up = Vector3.up;
        rb.angularVelocity = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
    }
}
