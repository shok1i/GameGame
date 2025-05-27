using UnityEngine;

public class WeaponOrbit : MonoBehaviour
{
    public Transform target;      // Объект, вокруг которого вращаемся (например, игрок)
    public float orbitRadius = 1.5f;  // Радиус вращения оружия
    public float orbitSpeed = 180f;   // Скорость вращения в градусах в секунду

    private float currentAngle = 0f;

    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (target == null) return;
        if (target.transform.position.x < transform.position.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipY = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipY = true;
        }
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        Vector3 direction = mouseWorldPos - target.position;
        currentAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (currentAngle >= 360f) currentAngle -= 360f;
        float angleRad = currentAngle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0) * orbitRadius;
        transform.position = target.position + offset;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
