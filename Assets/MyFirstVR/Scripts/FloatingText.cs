using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] float riseSpeed = 0.4f;
    [SerializeField] float lifetime = 1.2f;

    TMP_Text label;
    float timer;

    public static void Spawn(FloatingText prefab, Vector3 position, string message)
    {
        var obj = Instantiate(prefab, position, Quaternion.identity);
        obj.GetComponent<TMP_Text>().text = message;
    }

    void Awake() { label = GetComponent<TMP_Text>(); }

    void Update()
    {
        timer += Time.deltaTime;
        float k = timer / lifetime;

        transform.position += Vector3.up * riseSpeed * Time.deltaTime;

        // Siempre mirando al jugador
        var cam = Camera.main;
        if (cam) transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);

        // Entra con "pop", luego se desvanece
        float pop = Mathf.Clamp01(k * 6f);
        transform.localScale = Vector3.one * Mathf.Lerp(0.2f, 1f, pop);
        label.alpha = 1f - Mathf.Clamp01((k - 0.5f) * 2f);

        if (timer >= lifetime) Destroy(gameObject);
    }
}