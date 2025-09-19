using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour

{
    Vector2 startPos;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        
    }
    private void Start()
    {
        startPos = transform.position;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Torne"))
        {
            Dø();
        }
    }


    void Dø()
    {
        StartCoroutine(Respawn(0.5f));
    }

    IEnumerator Respawn(float duration)
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(duration);
        transform.position = startPos;
        spriteRenderer.enabled = true;
    }
   
}
