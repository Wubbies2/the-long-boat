using UnityEngine;

public class BoatSeat : MonoBehaviour
{
    public BoatController boat;
    public Transform cameraFocusPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBoatInteraction player = other.GetComponent<PlayerBoatInteraction>();
            if (player != null)
                player.ShowDrivePrompt(true, this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBoatInteraction player = other.GetComponent<PlayerBoatInteraction>();
            if (player != null)
                player.ShowDrivePrompt(false, null);
        }
    }
}
