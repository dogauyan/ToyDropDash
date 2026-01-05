using UnityEngine;

public class CollidableExit : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collision)
    {
        Toy toy = collision.GetComponent<Toy>();
        if (toy == null) return;

        // Only count miss if this toy allows it
        if (toy.causesMissOnExit &&
            collision.transform.position.y < transform.position.y)
        {
            ScoreManager.Instance.AddMiss();
        }

        Destroy(collision.gameObject);
    }
}
