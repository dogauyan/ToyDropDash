using UnityEngine;

public class FloatingTextSpawner : MonoBehaviour
{
    public static FloatingTextSpawner Instance;

    public FloatingText floatingTextPrefab;
    public Transform floatingTextParent;

    void Awake()
    {
        Instance = this;
    }

    public static void Show(string text, Vector3 position, Color? _color = null)
    {
        if (Instance == null) return;

        FloatingText ft = Instantiate(
            Instance.floatingTextPrefab,
            position,
            Quaternion.identity
        );

        Color color = Color.white;

        if (_color != null)
        {
            color = _color.Value;
        }
        else
        {
            if (text.Contains("TRAP"))
                color = Color.red;
            else if (text.Contains("MISS"))
                color = Color.gray;
            else
                color = Color.yellow;
        }

        ft.SetText(text, color);
    }
}