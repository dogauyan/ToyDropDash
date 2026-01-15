using UnityEngine;

public class Toy : MonoBehaviour
{
    public int scoreValue = 1;

    [Header("Toy Behavior")]
    public bool isBonus = false;
    public bool causesMissOnCatch = false; // Trap = true
    public bool causesMissOnExit = true;   // Trap = false

    public void OnCaught(out byte toytype)
    {
        if (causesMissOnCatch)
        {
            // TRAP caught counts as miss, breaks combo, shakes camera
            ScoreManager.Instance.AddMiss();
            AudioManager.Instance.PlaySFX(AudioManager.Instance.catchTrap);
            CameraShake.Instance.Shake(0.2f, 0.3f);

            FloatingTextSpawner.Show("TRAP!", transform.position);
            toytype = 0;
        }
        else
        {
            // NORMAL / BONUS caught
            int awarded = ScoreManager.Instance.AddScore(scoreValue);

            if (isBonus)
                AudioManager.Instance.PlaySFX(AudioManager.Instance.catchBonus);
            else
                AudioManager.Instance.PlaySFX(AudioManager.Instance.catchNormal);

            FloatingTextSpawner.Show("+" + awarded, transform.position);
            CameraShake.Instance.Shake(0.1f, 0.15f);

            toytype = (byte)(isBonus ? 2 : 1);
        }

        Destroy(gameObject);
    }
}
