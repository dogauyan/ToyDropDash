using UnityEngine;

public class CollidableExit : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Toy"))    Destroy(collision.gameObject);
    }
}
