using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public RectTransform joystick;

    private Vector2 joystickStartPos;
    private Vector3 moveDirection;

    public static Action<bool> OnUsingJoystick;
    public static Action<Vector3> OnDirectionChanged;
    private bool joystickUsedFlag;

    public float deadZone = 0.2f; // Valor da dead zone (pode ser ajustado)

    void Start()
    {
        joystickStartPos = joystick.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 dragPos = eventData.position;
        Vector2 direction = dragPos - joystickStartPos;
        direction = Vector2.ClampMagnitude(direction, 100f);

        joystick.anchoredPosition = joystickStartPos + direction;

        // Verifica se a magnitude da dire��o est� acima da dead zone
        if (direction.magnitude > 100f * deadZone)
        {
            moveDirection = new Vector3(direction.x, 0, direction.y).normalized;
            OnDirectionChanged?.Invoke(moveDirection);

            if (!joystickUsedFlag)
            {
                OnUsingJoystick?.Invoke(true);
                joystickUsedFlag = true;
            }
        }
        else
        {
            moveDirection = Vector3.zero; // Zera a dire��o se estiver dentro da dead zone
            OnDirectionChanged?.Invoke(moveDirection);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        joystick.anchoredPosition = joystickStartPos; // Reseta a posi��o do joystick
        moveDirection = Vector3.zero; // Zera a dire��o

        OnDirectionChanged?.Invoke(moveDirection);

        if (joystickUsedFlag)
        {
            joystickUsedFlag = false;
            OnUsingJoystick?.Invoke(false);
        }

        // Reinicia a posi��o de in�cio do joystick para o pr�ximo drag
        joystickStartPos = joystick.anchoredPosition;
    }
}
