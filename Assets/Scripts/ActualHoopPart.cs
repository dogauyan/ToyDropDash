using UnityEngine;

public class ActualHoopPart : MonoBehaviour
{
    [SerializeField] ActualHoop hoop;



    Toy _toy;
    void OnTriggerEnter2D(Collider2D collision)
    {
        _toy = collision.GetComponent<Toy>();
        if (_toy == null) return;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        _toy = collision.GetComponent<Toy>();
        if (_toy == null) return;
        
        if (_toy.atDown)
        {
            _toy.atHoop = true;
        }
    }
}
