using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 50f;

    private bool isDriving = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!isDriving) return;

        float forward = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        rb.AddForce(transform.forward * forward * moveSpeed, ForceMode.Force);
        rb.AddTorque(transform.up * turn * turnSpeed, ForceMode.Force);
    }

    public void SetDriving(bool driving)
    {
        isDriving = driving;
    }
}
