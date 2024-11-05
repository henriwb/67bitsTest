using UnityEngine;
using UnityEngine.EventSystems;

public class CustomJoystick3DController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public GameObject objectToMove;
    public RectTransform joystick;
    public float moveSpeed = 5f;

    private Vector2 joystickStartPos;
    private Vector3 moveDirection;

    void Start()
    {
        joystickStartPos = joystick.anchoredPosition;
    }

    void Update()
    {
        objectToMove.transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 dragPos = eventData.position;
        Vector2 direction = dragPos - joystickStartPos;
        direction = Vector2.ClampMagnitude(direction, 100f); // Limita o alcance do joystick

        joystick.anchoredPosition = joystickStartPos + direction;

        moveDirection = new Vector3(direction.x, 0, direction.y).normalized;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        joystick.anchoredPosition = joystickStartPos;
        moveDirection = Vector3.zero;
    }
}
