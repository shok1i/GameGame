using UnityEngine;

public class ShootGun : BaseWeaponsClass
{
    private bool isMobile = false;
    void Start()
    {
        joystick = GameObject.FindWithTag("FireStick").gameObject.GetComponent<Joystick>();
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            isMobile = true;
        }
    }
    protected override void Shoot()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0f;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        mouseWorldPos.z = firePoint.position.z;
        Vector2 baseDirection = (mouseWorldPos - firePoint.position).normalized;
        float baseAngle;
        if (!isMobile)
        {
            baseAngle = Mathf.Atan2(baseDirection.y, baseDirection.x) * Mathf.Rad2Deg;
        }
        else
        {
            Vector2 joystickDir = new Vector2(joystick.Horizontal, joystick.Vertical);
            joystickDir.Normalize();
            baseAngle = Mathf.Atan2(joystickDir.y, joystickDir.x) * Mathf.Rad2Deg;
        }
        float[] angleOffsets = { -15f, 0f, 15f };
        foreach (float offset in angleOffsets)
        {
            float angle = baseAngle + offset;
            float rad = angle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = direction * bulletSpeed;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
