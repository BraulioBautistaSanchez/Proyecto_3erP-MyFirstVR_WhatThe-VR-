using TMPro;
using UnityEngine;

public class PinScore : MonoBehaviour
{
    [SerializeField] GameObject floatingTextPrefab;
    [SerializeField] int points = 1;
    [SerializeField] float tiltThreshold = 45f;
    [SerializeField] float settleTime = 1f;

    float spawnTime;
    float startY;
    bool counted;

    void Start() { spawnTime = Time.time; startY = transform.position.y; }

    void Update()
    {
        if (counted || Time.time - spawnTime < settleTime) return;

        bool tilted = Vector3.Angle(transform.up, Vector3.up) > tiltThreshold;
        bool fellOff = transform.position.y < startY - 0.5f;
        if (!tilted && !fellOff) return;

        counted = true;
        if (ScoreManager.Instance != null) ScoreManager.Instance.Add(points);

        if (floatingTextPrefab != null)
        {
            GameObject fx = Instantiate(floatingTextPrefab, transform.position + Vector3.up * 0.3f, Quaternion.identity);
            TMP_Text label = fx.GetComponentInChildren<TMP_Text>();
            if (label != null) label.text = $"+{points}";
        }
    }
}