using UnityEngine;

public class playerTeleport : MonoBehaviour
{
    private GameObject currentTeleporter;
    private float lastTeleportTime = -10f;
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

                // Sæt nyt respawn-point
                var gc = GetComponent<GameController>();
                if (gc != null)
                {
                    gc.SetRespawnPoint(transform.position);
                }
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