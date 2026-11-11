using System.Collections.Generic;
using UnityEngine;

public class FloorRespawnZone : MonoBehaviour
{
    readonly Dictionary<Rigidbody, (Vector3 pos, Quaternion rot)> origins = new();

    void Start()
    {
        // Guardar la posición inicial de las pelotas que ya están en la escena
        foreach (var go in GameObject.FindGameObjectsWithTag("Ball"))
        {
            var rb = go.GetComponent<Rigidbody>();
            if (rb) origins[rb] = (rb.position, rb.rotation);
        }
    }

    // Funciona si el suelo es sólido
    void OnCollisionEnter(Collision c) { Handle(c.rigidbody); }

    // Funciona si el suelo es trigger
    void OnTriggerEnter(Collider other) { Handle(other.attachedRigidbody); }

    void Handle(Rigidbody rb)
    {
        if (rb == null || !rb.CompareTag("Ball")) return;

        if (origins.TryGetValue(rb, out var o))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.position = o.pos;
            rb.rotation = o.rot;
        }
        else
        {
            Destroy(rb.gameObject);   // pelota lanzada con la resortera
        }
    }
}
