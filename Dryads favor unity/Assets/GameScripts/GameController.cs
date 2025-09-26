using System.Collections;
using UnityEngine;
using UnityEngine.UI; 

public class GameController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Vector2 startPos;

    public GameObject gameOverUI; 

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        startPos = transform.position;
        if (gameOverUI != null)
            gameOverUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Torne"))
        {
            Die();
        }
    }

    void Die()
    {
        StartCoroutine(GameOverAndRespawn());
    }

    IEnumerator GameOverAndRespawn()
    {
        spriteRenderer.enabled = false;
        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        
        float timer = 0f;
        while (timer < 1.5f && !Input.anyKeyDown)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        if (gameOverUI != null)
            gameOverUI.SetActive(false);

        transform.position = startPos;
        spriteRenderer.enabled = true;
    }
}