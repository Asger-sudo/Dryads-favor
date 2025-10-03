using UnityEngine;

public class playerTeleport : MonoBehaviour
{
    private GameObject currentTeleporter;
    private float lastTeleportTime = -10f; // Start så man kan teleportere med det samme
    public float teleportCooldown = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (Time.time - lastTeleportTime >= teleportCooldown)
            {
                currentTeleporter = collision.gameObject;
                Debug.Log("Teleporting!");
                transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
                lastTeleportTime = Time.time;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
            }
        }
    }
}