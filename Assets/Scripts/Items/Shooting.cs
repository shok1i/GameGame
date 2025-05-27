using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 3f;
    public GameObject hitEffectPrefab;
    public AudioClip hitSound;
    public float effectLifetime = 1f; 
    public bool enemyShooting = false;
    public float enemyDamage;
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
        if (other.CompareTag("Wall"))
        {
            if (hitEffectPrefab != null)
            {
                GameObject effect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
                Destroy(effect, effectLifetime);
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player") && enemyShooting)
        {
            if (hitEffectPrefab != null)
            {
                GameObject effect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
                Destroy(effect, effectLifetime);
            }
            if (hitSound != null)
            {
                audioSource.PlayOneShot(hitSound);
            }
            PlayerManager player = other.GetComponent<PlayerManager>();
            player.playerHealth.TakeDamage(enemyDamage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Enemy") && !enemyShooting)
        {
            if (hitEffectPrefab != null)
            {
                GameObject effect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
                Destroy(effect, effectLifetime);
            }
            if (hitSound != null)
            {
                audioSource.PlayOneShot(hitSound);
            }
            EnemyManager enemy = other.GetComponent<EnemyManager>();
            enemy.getDamage(20f);
            Destroy(gameObject);
        }
    }
}
