using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 3f;
    public GameObject hitEffectPrefab;
    public AudioClip hitSound;
    public float effectLifetime = 1f; 
    private AudioSource audioSource;

    void Start()
    {
        Destroy(gameObject, lifetime);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Enemy"))
        {
            if (other.CompareTag("Enemy"))
            {
                EnemyManager enemy = other.GetComponent<EnemyManager>();
                enemy.getDamage(20f); // обновить для Weapon
            }
            if (hitEffectPrefab != null)
            {
                GameObject effect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
                Destroy(effect, effectLifetime);
            }

            if (hitSound != null)
            {
                audioSource.PlayOneShot(hitSound);
            }

            Destroy(gameObject);
        }
    }
}
