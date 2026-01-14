using UnityEngine;
using UnityEngine.InputSystem;

public class HoopMouseController : MonoBehaviour
{
    Camera cam;
    float hoopRadius;

    void Start()
    {
        cam = Camera.main;

        // Hide system cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;

        // Get hoop size from collider
        CircleCollider2D col = GetComponent<CircleCollider2D>();
        hoopRadius = col.bounds.extents.x;
    }

    void Update()
    {
        if (Mouse.current == null) return;

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        Vector3 mousePos = new Vector3(
            mouseScreenPos.x,
            mouseScreenPos.y,
            -cam.transform.position.z
        );

        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);

        // Camera bounds
        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        // Clamp inside screen (accounting for hoop size)
        worldPos.x = Mathf.Clamp(
            worldPos.x,
            -camWidth + hoopRadius,
            camWidth - hoopRadius
        );

        worldPos.y = Mathf.Clamp(
            worldPos.y,
            -camHeight + hoopRadius,
            camHeight - hoopRadius
        );

        transform.position = worldPos;
    }

    void OnDisable()
    {
        Cursor.visible = true;
    }
}
