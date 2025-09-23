using UnityEngine;

public class GranatKaster : MonoBehaviour
{
    [Header("Prefabs & points")]
    public GameObject granatPrefab;    
    public Transform throwPoint;       

    [Header("Throw settings")]
    public float throwForce = 10f;     
    public float upwardBias = 0.2f;    
    public float spinTorque = 5f;      

    [Header("Cooldown settings")]
    public float cooldownTime = 6f;    // hvor mange sekunder cooldown
    private float cooldownTimer = 0f;  // tæller ned

    void Update()
    {
        // opdater cooldown-timer
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // klik + ingen cooldown
        if (Input.GetMouseButtonDown(0) && cooldownTimer <= 0f)
        {
            ThrowAtMouse();
            cooldownTimer = cooldownTime; // reset cooldown
        }
    }

    void ThrowAtMouse()
    {
        if (granatPrefab == null || throwPoint == null || Camera.main == null)
        {
            Debug.LogWarning("[GranatKaster] Mangler reference! Tjek inspector og kamera tag.");
            return;
        }

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        Vector2 dir = (mouseWorld - throwPoint.position);
        dir = new Vector2(dir.x, dir.y + upwardBias).normalized;

        GameObject g = Instantiate(granatPrefab, throwPoint.position, Quaternion.identity);
        Rigidbody2D rb = g.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(dir * throwForce, ForceMode2D.Impulse);
            rb.AddTorque(spinTorque, ForceMode2D.Impulse);
        }
    }
}
