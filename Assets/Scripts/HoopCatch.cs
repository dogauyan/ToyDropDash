using UnityEngine;

public class HoopCatch : MonoBehaviour
{
    public GameObject CatchEffect_Good;
    public GameObject CatchEffect_Bad;
    public GameObject CatchEffect_Neutral;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Toy toy = other.GetComponent<Toy>();
        if (toy == null) return;

        toy.OnCaught(out byte toytype);
        GetComponent<HoopFeedback>()?.PlayCatchFeedback();

        switch (toytype)
        {
            case 0:
            Instantiate(CatchEffect_Bad, transform.position, Quaternion.identity);
            break;

            case 1:
            Instantiate(CatchEffect_Neutral, transform.position, Quaternion.identity);
            break;

            case 2:
            Instantiate(CatchEffect_Good, transform.position, Quaternion.identity);
            break;

            case 3:
                //Recovery fx
            break;

            case 4:
                //Blank fx
            break;
        }
    }
}
