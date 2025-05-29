using UnityEngine;

public class Cocodemon : EnemyManager
{
    public int bulletCount;
    
    protected override void _distanceAttack()
    {
        float angleStep = 360f / bulletCount;
        float angle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            float bulletDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float bulletDirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 direction = new Vector2(bulletDirX, bulletDirY).normalized;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = direction * bulletSpeed;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
            angle += angleStep;
        }
    }
}
