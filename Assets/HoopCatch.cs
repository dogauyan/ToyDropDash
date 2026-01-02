using UnityEngine;

public class HoopCatch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Toy")) return;

        // Optional: play sound / particles here

        Destroy(other.gameObject);
    }
}
