using UnityEngine;
using Cinemachine;

public class CameraOrientationSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera portraitCamera;
    public CinemachineVirtualCamera landscapeCamera;

    private void Start()
    {
        UpdateCameraOrientation();
    }

    private void Update()
    {
        UpdateCameraOrientation();
    }

    private void UpdateCameraOrientation()
    {
        if (Screen.width < Screen.height)
        {
            // Ativa a c�mera para Portrait e desativa a de Landscape
            portraitCamera.Priority = 1;
            landscapeCamera.Priority = 0;
        }
        else
        {
            // Ativa a c�mera para Landscape e desativa a de Portrait
            portraitCamera.Priority = 0;
            landscapeCamera.Priority = 1;
        }
    }
}
