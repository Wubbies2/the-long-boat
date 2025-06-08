using UnityEngine;

public class PlayerBoatInteraction : MonoBehaviour
{
    public GameObject drivingUI;
    private BoatSeat currentSeat;
    private bool isDriving = false;

    void Update()
    {
        if (!isDriving && currentSeat != null && Input.GetKeyDown(KeyCode.E))
        {
            StartDriving(currentSeat);
        }
        else if (isDriving && Input.GetKeyDown(KeyCode.F))
        {
            StopDriving();
        }
    }

    public void ShowDrivePrompt(bool show, BoatSeat seat)
    {
        drivingUI.SetActive(show);
        currentSeat = show ? seat : null;
    }

    void StartDriving(BoatSeat seat)
    {
        isDriving = true;
        drivingUI.SetActive(false);
        GetComponent<PlayerController>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false; // or disable model

        Camera.main.GetComponent<CameraFollow>().target = seat.cameraFocusPoint;

        seat.boat.SetDriving(true);
    }

    void StopDriving()
{
    isDriving = false;
    GetComponent<PlayerController>().enabled = true;
    GetComponent<MeshRenderer>().enabled = true;

    Camera.main.GetComponent<CameraFollow>().target = transform;

    if (currentSeat != null && currentSeat.boat != null)
    {
        currentSeat.boat.SetDriving(false);
    }

    currentSeat = null; // Clear currentSeat to avoid stale references
}

}
