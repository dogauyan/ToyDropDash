using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float lifetime = 1f;

    TextMeshPro text;
    Color startColor;
    float timer;

    void Awake()
    {
        text = GetComponent<TextMeshPro>();
        startColor = text.color;
    }

    void Update()
    {
        // Float upward
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * 5f);


        // Fade out
        timer += Time.deltaTime;
        float t = timer / lifetime;
        text.color = Color.Lerp(startColor, Color.clear, t);

        if (timer >= lifetime)
            Destroy(gameObject);
    }

    public void SetText(string value, Color color)
    {
        transform.localScale = Vector3.one * 1.2f;
        text.text = value;
        text.color = color;
        startColor = color;
    }
}
