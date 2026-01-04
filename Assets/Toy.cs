using UnityEngine;

public class Toy : MonoBehaviour
{
    public int scoreValue = 1;
    public bool causesMissOnCatch = false;
    public bool causesMissOnExit = true; 

    public void OnCaught()
    {
        if (causesMissOnCatch)
        {
            ScoreManager.Instance.AddMiss();
        }
        else
        {
            ScoreManager.Instance.AddScore(scoreValue);
        }

        Destroy(gameObject);
    }
}
