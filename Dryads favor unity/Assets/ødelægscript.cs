using UnityEngine;

public class Destructible : MonoBehaviour
{
    private Vector3 startPosition;
    public GameObject destructiblePrefab;
    public float respawnDelay = 8f;

    void Start()
    {
        startPosition = transform.position;
    }

    public void DestroyBox()
    {
        Debug.Log("[Destructible] " + gameObject.name + " bliver ødelagt!");
        StartCoroutine(RespawnAfterDelay());
        // Fjern gameObject.SetActive(false);
    }

    private System.Collections.IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        Debug.Log("Respawning box!");
        Instantiate(destructiblePrefab, startPosition, Quaternion.identity);
        Destroy(gameObject);
    }
}
