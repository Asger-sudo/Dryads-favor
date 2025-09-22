using UnityEngine;

public class Granat : MonoBehaviour
{
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private float radius;
    [SerializeField] private int explosionForce;
    [SerializeField] private float explosionTime;
    private float explosionTimer;
    void Start()
    {

    }

    void FixedUpdate()
    {
        explosionTimer += Time.fixedDeltaTime;

        if (explosionTimer >= explosionTime)
        {
            Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, radius, interactLayer);

            foreach (Collider2D coll in collisions)
            {
                if (coll.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
                {
                    Vector3 dir = rb.transform.position - transform.position;
                    rb.AddForce(dir.normalized * explosionForce);
                }
            }

            Destroy(gameObject);





        }

    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
