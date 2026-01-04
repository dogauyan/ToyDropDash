using UnityEngine;

public class CollidableExit : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Toy")) return;

        // Check if toy exited at the bottom
        if (collision.transform.position.y < transform.position.y)
        {
            ScoreManager.Instance.AddMiss();
        }

        Destroy(collision.gameObject);
    }
}
