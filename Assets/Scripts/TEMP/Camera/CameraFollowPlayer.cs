using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] public Transform target;
    float _smoothSpeed = 0.125f;
    
    void FixedUpdate()
    {
        Vector3 offset = new Vector3(0f, 0f, -10f);
        if (target) transform.position = Vector3.Lerp(transform.position, target.position, _smoothSpeed) + offset;
    }
}
