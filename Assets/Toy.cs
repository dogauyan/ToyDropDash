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
            ScoreManager.Instance.AddMiss();
            AudioManager.Instance.PlaySFX(AudioManager.Instance.catchTrap);
        }
        else
        {
            ScoreManager.Instance.AddScore(scoreValue);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.catchNormal);
        }

        Destroy(gameObject);
    }
}
