using UnityEngine;

public class HoopCatch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Toy")) return;

        ScoreManager.Instance.AddScore(1);
        Destroy(other.gameObject);
    }
}
