using UnityEngine;

public class WeaponOrbit : MonoBehaviour
{
    public Transform target;      // Объект, вокруг которого вращаемся (например, игрок)
    public float orbitRadius = 1.5f;  // Радиус вращения оружия
    public float orbitSpeed = 180f;   // Скорость вращения в градусах в секунду

    private float currentAngle = 0f;

    void Update()
    {
        if (target == null) return;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        Vector3 direction = mouseWorldPos - target.position;

        // Увеличиваем угол вращения
        currentAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //currentAngle += orbitSpeed * Time.deltaTime;
        if (currentAngle >= 360f) currentAngle -= 360f;

        // Переводим угол в радианы
        float angleRad = currentAngle * Mathf.Deg2Rad;

        // Вычисляем позицию оружия вокруг цели
        Vector3 offset = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0) * orbitRadius;

        // Обновляем позицию оружия
        transform.position = target.position + offset;

        // Поворачиваем оружие в сторону курсора мыши (для 2D)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
