using UnityEngine;

public class ChoppableTree : MonoBehaviour
{
    public int health = 3;
    private Rigidbody rb;
    private bool isFallen = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    public void Chop()
    {
        if (isFallen) return;

        health--;
        Debug.Log("Tree hit! Remaining health: " + health);

        if (health <= 0)
        {
            Fall();
        }
    }

    void Fall()
    {
        isFallen = true;
        rb.isKinematic = false; // Enable physics
    }
}
