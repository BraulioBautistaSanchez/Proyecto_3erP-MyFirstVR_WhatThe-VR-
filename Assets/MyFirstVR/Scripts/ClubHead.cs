using UnityEngine;

public class ClubHead : MonoBehaviour
{
    [SerializeField] float hitMultiplier = 1.5f;
    Vector3 lastPos, velocity;

    void FixedUpdate()
    {
        velocity = (transform.position - lastPos) / Time.fixedDeltaTime;
        lastPos = transform.position;
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.rigidbody != null && c.gameObject.CompareTag("Ball"))
            c.rigidbody.AddForce(velocity * hitMultiplier, ForceMode.VelocityChange);
    }
}
