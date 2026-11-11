using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] Transform rigRoot;   // [BuildingBlock] Camera Rig (el padre)
    [SerializeField] Transform head;      // CenterEyeAnchor
    [SerializeField] Transform spawnPoint;

    [Header("Configuración")]
    [SerializeField] float killHeight = -5f;
    [SerializeField] float cooldown = 3f;
    [SerializeField] float startDelay = 2f;

    float lastRespawn;
    bool ready;

    IEnumerator Start()
    {
        // Espera a que el visor empiece a rastrear antes de hacer nada
        yield return new WaitForSeconds(startDelay);
        ready = true;
    }

    void Update()
    {
        if (!ready || head == null || rigRoot == null || spawnPoint == null) return;

        if (head.position.y < killHeight && Time.time - lastRespawn > cooldown)
        {
            Debug.Log($"Respawn: la cabeza estaba en Y={head.position.y}, límite={killHeight}");
            Respawn();
        }
    }

    public void Respawn()
    {
        lastRespawn = Time.time;

        var cc = rigRoot.GetComponent<CharacterController>();
        if (cc) cc.enabled = false;

        float yawDelta = spawnPoint.eulerAngles.y - head.eulerAngles.y;
        rigRoot.RotateAround(head.position, Vector3.up, yawDelta);

        Vector3 headOffset = head.position - rigRoot.position;
        Vector3 target = spawnPoint.position - new Vector3(headOffset.x, 0f, headOffset.z);
        target.y = spawnPoint.position.y;
        rigRoot.position = target;

        if (cc) cc.enabled = true;
    }
}