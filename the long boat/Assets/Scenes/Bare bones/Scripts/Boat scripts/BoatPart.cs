using UnityEngine;

public class BoatPart : MonoBehaviour
{
    public float health = 100f;
    private FixedJoint joint;

    void Start()
    {
        joint = GetComponent<FixedJoint>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            BreakOff();
        }
    }

    void BreakOff()
    {
        if (joint != null)
        {
            Destroy(joint); // Remove the physical connection
        }

        transform.parent = null; // Optional: detach from hierarchy
        // Rigidbody is already on the object
    }
}
