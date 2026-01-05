using UnityEngine;

public class Toy : MonoBehaviour
{
    public int scoreValue = 1;

    [Header("Toy Behavior")]
    public bool causesMissOnCatch = false; // Trap = true
    public bool causesMissOnExit = true;   // Trap = false

    public void OnCaught()
    {
        if (causesMissOnCatch)
        {
            // Trap caught → counts as a miss and breaks combo
            ScoreManager.Instance.AddMiss();
        }
        else
        {
            ScoreManager.Instance.AddScore(scoreValue);
        }

        Destroy(gameObject);
    }
}
