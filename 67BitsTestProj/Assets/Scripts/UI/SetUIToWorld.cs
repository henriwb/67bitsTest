using UnityEngine;
using Cinemachine;

public class UIFollow3DObject : MonoBehaviour
{
    [SerializeField] private Transform target3D;
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool followContinuously;
    [SerializeField] private RectTransform rectTransform;
    public Camera uiCamera; // Camera ativa do Cinemachine

    void Start()
    {
        
    }

    void OnEnable()
    {
        UpdatePosition();
    }

    void Update()
    {
        if (followContinuously)
        {
            UpdatePosition();
        }
    }

    void UpdatePosition()
    {
        if (target3D == null || uiCamera == null) return;

        // Converte a posição do objeto 3D para coordenadas de tela
        Vector3 screenPosition = uiCamera.WorldToScreenPoint(target3D.position + offset);

        // Verifica se o objeto está na frente da câmera
        if (screenPosition.z > 0)
        {
            rectTransform.position = screenPosition;
        }
        else
        {
            // Opcional: caso o objeto fique atrás da câmera, desativa o UI ou redefine sua posição
            rectTransform.position = new Vector3(-1000, -1000, 0); // Move para fora da tela
        }
    }
}
