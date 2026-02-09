
using System.Collections;

using UnityEngine;

public class Coin : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public AudioSource _audioSource;
    public AudioClip _audioClip;
    
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(DestroyCoin());
    }

    private IEnumerator DestroyCoin()
    {
        if (spriteRenderer != null)
        {
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < 3; i++)
            {
                spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.color = Color.white;
                yield return new WaitForSeconds(1f);
            }
        }

        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _audioSource.PlayOneShot(_audioClip);
            Destroy(gameObject);
        }
    }
}
