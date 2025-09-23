using UnityEngine;

public class Granat : MonoBehaviour
{
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private float radius = 3f;
    [SerializeField] private float explosionForce = 10f;
    [SerializeField] private float explosionTime = 2f;
    private float explosionTimer;

    void Start()
    {
        explosionTimer = 0f;
    }

    void FixedUpdate()
    {
        explosionTimer += Time.fixedDeltaTime;

        if (explosionTimer >= explosionTime)
        {
            Explode();
        }
    }

    private void Explode()
    {
        Debug.Log("[Granat] BOOM!");

        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, radius, interactLayer);

        foreach (Collider2D coll in collisions)
        {
            GameObject go = coll.gameObject;
            Debug.Log("[Granat] Ramte: " + go.name);

            // 👉 Hvis objektet har tag "Box", destroy det
            if (go.CompareTag("Box"))
            {
                Debug.Log("[Granat] Ødelægger: " + go.name);
                Destroy(go);
                continue; // stop her så vi ikke også giver knockback
            }

            // ellers: giv knockback hvis Rigidbody2D
            if (coll.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                Vector2 dir = (rb.position - (Vector2)transform.position);
                rb.AddForce(dir.normalized * explosionForce, ForceMode2D.Impulse);
            }
        }

        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
