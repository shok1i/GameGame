using UnityEngine;

public class BaseWeaponsClass : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public bool isShooting;
    public float bulletSpeed = 10f;
    public float fireRate = 0.2f;

    private float fireCooldown = 0f;


    void Update()
    {
        fireCooldown -= Time.deltaTime;
        if (Input.GetButton("Fire1") && fireCooldown <= 0f)
        {
            if (isShooting)
            {
                Shoot();
            }
            else
            {
                Attack();
            }
            fireCooldown = fireRate;
        }
    }

    protected virtual void Shoot()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0f;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        mouseWorldPos.z = firePoint.position.z;
        Vector2 direction = (mouseWorldPos - firePoint.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity); // new Vector3(0.5f,0.5f,0f)
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * bulletSpeed;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Attack()
    {
        
    }
}
