using UnityEngine;

public class Toy : MonoBehaviour
{
    public int scoreValue = 1;

    [Header("Toy Behavior")]
    public bool causesMissOnCatch = false; // Trap = true
    public bool causesMissOnExit = true;   // Trap = false

    public void OnCaught(out byte toytype)
    {
        if (causesMissOnCatch)
        {
            // Trap caught → counts as miss, breaks combo
            ScoreManager.Instance.AddMiss();
            AudioManager.Instance.PlaySFX(AudioManager.Instance.catchTrap);
            CameraShake.Instance.Shake(0.2f, 0.3f); // trap = stronger

            // Floating feedback
            FloatingTextSpawner.Show("TRAP!", transform.position);
            toytype = 0;
        }
        else
        {
            // Normal / bonus toy
            int awarded = ScoreManager.Instance.AddScore(scoreValue);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.catchNormal);

            // Floating feedback (FINAL score, combo included)
            FloatingTextSpawner.Show("+" + awarded, transform.position);
            CameraShake.Instance.Shake(0.1f, 0.15f); // normal
            toytype = (byte)(scoreValue == 1 ? 1 : 2);
        }

        Destroy(gameObject);
    }
}