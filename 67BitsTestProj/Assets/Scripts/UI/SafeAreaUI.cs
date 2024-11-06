using UnityEngine;

public class SafeAreaUI : MonoBehaviour
{
    public RectTransform uiRectTransform;

    void Start()
    {
        // Ajuste a safe area para o Canvas
        ApplySafeArea();
    }

    void ApplySafeArea()
    {
        // Obtém a Safe Area do dispositivo
        Rect safeArea = Screen.safeArea;

        // Converte para as coordenadas da UI
        Vector2 min = safeArea.position;
        Vector2 max = safeArea.position + safeArea.size;

        // Ajusta o Canvas para respeitar a Safe Area
        uiRectTransform.anchorMin = new Vector2(min.x / Screen.width, min.y / Screen.height);
        uiRectTransform.anchorMax = new Vector2(max.x / Screen.width, max.y / Screen.height);
        uiRectTransform.offsetMin = Vector2.zero;
        uiRectTransform.offsetMax = Vector2.zero;
    }
}
