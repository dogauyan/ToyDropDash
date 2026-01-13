using UnityEngine;

public class HoopCatch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Toy toy = other.GetComponent<Toy>();
        if (toy == null) return;

        toy.OnCaught();
        GetComponent<HoopFeedback>()?.PlayCatchFeedback();
    }
}
