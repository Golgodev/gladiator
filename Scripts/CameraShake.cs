using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private static CameraShake instance;

    public float shakeDuration = 0.1f;
    public float shakeMagnitude = 0.5f;

    private Vector3 originalPosition;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        originalPosition = transform.localPosition;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.R)) {
            Shake();
        }
    }

    public static void Shake()
    {
        if (instance != null)
            instance.StartCoroutine(instance.DoShake());
    }

    IEnumerator DoShake()
    {
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = originalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
