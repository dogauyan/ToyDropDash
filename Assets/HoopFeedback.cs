using UnityEngine;

public class HoopFeedback : MonoBehaviour
{
    Vector3 originalScale;
    Coroutine punchRoutine;

    void Awake()
    {
        originalScale = transform.localScale;
    }

    public void PlayCatchFeedback()
    {
        if (punchRoutine != null)
            StopCoroutine(punchRoutine);

        punchRoutine = StartCoroutine(Punch());
    }

    System.Collections.IEnumerator Punch()
    {
        transform.localScale = originalScale * 1.15f;
        yield return new WaitForSeconds(0.08f);
        transform.localScale = originalScale;
    }
}
