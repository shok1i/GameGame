using Unity.VisualScripting;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    [SerializeField] public GameObject target;

    void Update()
    {
        transform.position = (Vector2) target.transform.position;        
        Debug.Log($"Camera pos: {transform.position} | Target pos: {target.transform.position} | Distance: {Vector2.Distance(transform.position, target.transform.position)}");
    }
}
