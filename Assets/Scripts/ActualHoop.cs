using UnityEngine;

public class ActualHoop : MonoBehaviour
{
    public GameObject CatchEffect_Good;
    public GameObject CatchEffect_Bad;
    public GameObject CatchEffect_Neutral;
    public GameObject CatchEffect_Recovery;
    public GameObject CatchEffect_Blank;




    Toy _toy;
    void OnTriggerEnter2D(Collider2D collision)
    {
        _toy = collision.GetComponent<Toy>();
        if (_toy == null) return;

        _toy.atDown = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        _toy = collision.GetComponent<Toy>();
        if (_toy == null) return;
        
        if (_toy.atHoop)
        {
            _toy.OnCaught(out byte toytype);
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
                    Instantiate(CatchEffect_Recovery, transform.position, Quaternion.identity);
                break;

                case 4:
                    //Blank fx
                    Instantiate(CatchEffect_Blank, transform.position, Quaternion.identity);
                break;
            }
        }
        else
        {
            _toy.atDown = false;
        }
    }
}
