using UnityEngine;

public class Ricochet : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Toy toy = collision.collider.GetComponent<Toy>();
        if (toy == null) return;

        if (toy.spawned)
        {
            toy.spawned = !toy.spawned;
            return;
        }

        if (Random.Range(0f, 100f) > 40)
        {
            toy.gameObject.layer = 7;
        }
        // if (Random.Range(0f, 100f) <= 40)
        // {
        //     toy.rig.linearVelocity = new Vector2(Mathf.Sign(toy.rig.linearVelocity.x) * toy.rig.linearVelocity.y, Mathf.Abs(toy.rig.linearVelocity.x) * 2);
        //     toy.rig.angularVelocity = -toy.rig.angularVelocity;
        // }
    }
}
