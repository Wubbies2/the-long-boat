using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatBuoyancy : MonoBehaviour
{
    public Transform[] floatPoints;           // Points on the boat where buoyancy is applied
    public float waterDrag = 0.3f;            // Linear drag in water
    public float waterAngularDrag = 0.2f;     // Rotational drag in water
    public float floatHeight = 1.0f;          // How far below water the float point can go
    public float buoyancyForce = 10f;         // Strength of the buoyancy
    public float velocityDamping = 0.3f;      // How much to damp motion based on speed

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void FixedUpdate()
    {
        if (floatPoints == null || floatPoints.Length == 0) return;

        foreach (Transform point in floatPoints)
        {
            Vector3 pointPos = point.position;
            float waterHeight = WaveManager.GetWaveHeightAtPosition(point.position);
            float difference = waterHeight - pointPos.y;

            // Spring-like buoyancy force, even if slightly above water
            float forceFactor = Mathf.Clamp(difference / floatHeight, -1f, 1f);
            Vector3 uplift = -Physics.gravity * forceFactor * buoyancyForce;

            // Optional damping (based on how fast the point is moving)
            Vector3 velocity = rb.GetPointVelocity(pointPos);
            uplift -= velocity * velocityDamping;

            rb.AddForceAtPosition(uplift, pointPos, ForceMode.Force);
        }

        rb.linearDamping = waterDrag;
        rb.angularDamping = waterAngularDrag;
    }

    void OnDrawGizmos()
    {
        // Show red spheres where buoyancy is applied
        if (floatPoints == null) return;

        Gizmos.color = Color.red;
        foreach (Transform point in floatPoints)
        {
            if (point != null)
                Gizmos.DrawSphere(point.position, 0.1f);
        }
    }
}
