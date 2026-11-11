using System.Collections.Generic;
using UnityEngine;

public class BallRespawnZone : MonoBehaviour
{
    readonly Dictionary<Rigidbody, (Vector3 pos, Quaternion rot)> origins = new();

    void OnTriggerEnter(Collider other)
    {
        var rb = other.attachedRigidbody;
        if (rb == null || !rb.CompareTag("Ball")) return;

        if (!origins.ContainsKey(rb)) return;   // sin posición guardada, no hace nada
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.position = origins[rb].pos;
        rb.rotation = origins[rb].rot;
    }

    void Start()
    {
        // Guardar la posición inicial de cada pelota de la escena
        foreach (var go in GameObject.FindGameObjectsWithTag("Ball"))
        {
            var rb = go.GetComponent<Rigidbody>();
            if (rb) origins[rb] = (rb.position, rb.rotation);
        }
    }
}