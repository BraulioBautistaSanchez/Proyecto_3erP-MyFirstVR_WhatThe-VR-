using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] Transform rigRoot;        // [BuildingBlock] Camera Rig
    [SerializeField] Transform head;           // CenterEyeAnchor
    [SerializeField] Transform spawnPoint;

    [Header("Configuración")]
    [SerializeField] float killHeight = -5f;   // altura mundial por debajo de la cual se hace respawn
    [SerializeField] float cooldown = 1f;

    float lastRespawn;

    void Start()
    {
        // Colocar al jugador en el punto de inicio al empezar
        Respawn();
    }

    void Update()
    {
        if (head == null || rigRoot == null || spawnPoint == null) return;

        if (head.position.y < killHeight && Time.time - lastRespawn > cooldown)
            Respawn();
    }

    public void Respawn()
    {
        lastRespawn = Time.time;

        // Si el rig tiene CharacterController, hay que apagarlo para poder moverlo
        var cc = rigRoot.GetComponent<CharacterController>();
        if (cc) cc.enabled = false;

        // Girar el rig para que la cabeza mire hacia donde apunta el SpawnPoint
        float yawDelta = spawnPoint.eulerAngles.y - head.eulerAngles.y;
        rigRoot.RotateAround(head.position, Vector3.up, yawDelta);

        // Mover el rig para que la cabeza quede sobre el SpawnPoint (en X y Z),
        // y los pies a la altura del punto
        Vector3 headOffset = head.position - rigRoot.position;
        Vector3 target = spawnPoint.position - new Vector3(headOffset.x, 0f, headOffset.z);
        target.y = spawnPoint.position.y;
        rigRoot.position = target;

        if (cc) cc.enabled = true;

        // Si hay rigidbody en el rig, quitar la velocidad acumulada
        var rb = rigRoot.GetComponent<Rigidbody>();
        if (rb) { rb.linearVelocity = Vector3.zero; rb.angularVelocity = Vector3.zero; }
    }
}