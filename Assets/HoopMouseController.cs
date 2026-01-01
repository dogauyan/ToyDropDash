using UnityEngine;
using UnityEngine.InputSystem;

public class HoopMouseController : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        Vector3 mousePos = new Vector3(
            mouseScreenPos.x,
            mouseScreenPos.y,
            -cam.transform.position.z
        );

        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
        transform.position = worldPos;
    }
}
