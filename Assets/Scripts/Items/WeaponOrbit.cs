using UnityEngine;

public class WeaponOrbit : MonoBehaviour
{
    public Transform target;
    public Joystick shootJoystick;
    public float orbitRadius = 1.5f;
    public float orbitSpeed = 180f;

    private float currentAngle = 0f;
    private bool isMobile;

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            isMobile = true;
        }
        else
        {
            isMobile = true;
        }
        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (target == null) return;
        if (target.transform.position.x < transform.position.x || shootJoystick.Horizontal >= 0f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipY = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipY = true;
        }
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        Vector3 direction;
        if (isMobile)
        {
            direction = new Vector3(shootJoystick.Horizontal, shootJoystick.Vertical, target.position.z);
        }
        else {
            direction = mouseWorldPos - target.position;
        }
        currentAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (currentAngle >= 360f) currentAngle -= 360f;
        float angleRad = currentAngle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0) * orbitRadius;
        transform.position = target.position + offset;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
