using UnityEngine;

public class Toy : MonoBehaviour
{
    public int scoreValue = 1;
    public SpriteRenderer blankBubble;
    public Rigidbody2D rig;
    public bool spawned = true;

    [Header("Toy Behavior")]
    public bool isBonus = false;
    public bool causesMissOnCatch = false; // Trap = true
    public bool causesMissOnExit = true;   // Trap = false
    public bool isRecovery = false;        // removes 1 miss



    void Start()
    {
        Invoke(nameof(Touchable), 1);
    }
    private void Touchable()
    {
        gameObject.layer = 8;
    }
    public void OnCaught(out byte toytype)
    {
        if (causesMissOnCatch)
        {
            ScoreManager.Instance.AddMiss();
            AudioManager.Instance.PlaySFX(AudioManager.Instance.catchTrap);
            CameraShake.Instance.Shake(0.2f, 0.3f);

            FloatingTextSpawner.Show("TRAP!", transform.position);
            toytype = 0;
        }
        // RECOVERY toy 
        else if (isRecovery)
        {
            ScoreManager.Instance.RemoveMiss(1);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.catchBonus);

            FloatingTextSpawner.Show("MISS -1", transform.position, Color.green);
            CameraShake.Instance.Shake(0.05f, 0.1f);

            toytype = 3; // recovery type
        }
        //  NORMAL / BONUS toy
        else
        {
            int awarded = ScoreManager.Instance.AddScore(scoreValue);

            if (isBonus)
                AudioManager.Instance.PlaySFX(AudioManager.Instance.catchBonus);
            else
                AudioManager.Instance.PlaySFX(AudioManager.Instance.catchNormal);

            FloatingTextSpawner.Show("+" + awarded, transform.position);
            CameraShake.Instance.Shake(0.1f, 0.15f);

            if (scoreValue == 1)
            {
                toytype = 4;
            }
            else toytype = (byte)(isBonus ? 2 : 1);
        }

        Destroy(gameObject);
    }
}
