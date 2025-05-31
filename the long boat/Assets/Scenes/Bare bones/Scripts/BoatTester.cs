using UnityEngine;

public class BoatTester : MonoBehaviour
{
    public BoatPart partToDamage;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            partToDamage.TakeDamage(50f);
        }
    }
}
