using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] TMP_Text scoreText;
    int score;

    void Awake()
    {
        Instance = this;
        Refresh();
    }

    public void Add(int points)
    {
        score += points;
        Refresh();
        StopAllCoroutines();
        StartCoroutine(PopAnimation());
    }

    void Refresh() { if (scoreText) scoreText.text = $"Puntos: {score}"; }

    // El texto "salta" cada vez que sumas
    IEnumerator PopAnimation()
    {
        float t = 0f;
        while (t < 0.3f)
        {
            t += Time.deltaTime;
            float s = 1f + Mathf.Sin(t / 0.3f * Mathf.PI) * 0.4f;
            scoreText.transform.localScale = Vector3.one * s;
            yield return null;
        }
        scoreText.transform.localScale = Vector3.one;
    }
}