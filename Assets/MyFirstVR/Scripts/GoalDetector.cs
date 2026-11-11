using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalDetector : MonoBehaviour
{
    [Header("Score Settings")]
    public int currentScore = 0;
    [Tooltip("Puntos que se suman al marcador general por cada hoyo.")]
    public int pointsPerGoal = 5;

    [Header("UI References")]
    [Tooltip("The main TextMeshPro display above the goal.")]
    public TextMeshPro mainScoreText;

    [Tooltip("The floating text prefab that is spawned when a goal is scored.")]
    public GameObject floatingFeedbackPrefab;

    // Evita contar dos veces la misma pelota
    readonly HashSet<Collider> countedBalls = new HashSet<Collider>();

    void Start()
    {
        Collider col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;

        UpdateScoreDisplay();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball")) return;
        if (!countedBalls.Add(other)) return;   // ya contó

        RegisterGoal(other.transform.position);
    }

    // Si la pelota sale del hoyo, puede volver a contar en otro intento
    void OnTriggerExit(Collider other)
    {
        countedBalls.Remove(other);
    }

    void RegisterGoal(Vector3 ballPosition)
    {
        currentScore++;
        UpdateScoreDisplay();

        // Suma al marcador general (el del HUD)
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.Add(pointsPerGoal);

        // Texto flotante, con el valor ganado
        if (floatingFeedbackPrefab != null)
        {
            GameObject fx = Instantiate(floatingFeedbackPrefab, ballPosition, Quaternion.identity);
            TMP_Text label = fx.GetComponentInChildren<TMP_Text>();
            if (label != null) label.text = $"+{pointsPerGoal}";
        }
    }

    void UpdateScoreDisplay()
    {
        if (mainScoreText != null)
            mainScoreText.text = currentScore.ToString();
    }
}