using UnityEngine;

public class FloatingTextSpawner : MonoBehaviour
{
    public static FloatingTextSpawner Instance;

    public FloatingText floatingTextPrefab;

    void Awake()
    {
        Instance = this;
    }

    public static void Show(string text, Vector3 position)
    {
        if (Instance == null) return;

        FloatingText ft = Instantiate(
            Instance.floatingTextPrefab,
            position,
            Quaternion.identity
        );

        // Default styling
        Color color = Color.white;

        if (text.Contains("TRAP"))
            color = Color.red;
        else if (text.Contains("MISS"))
            color = Color.gray;
        else
            color = Color.yellow;

        ft.SetText(text, color);
    }
}