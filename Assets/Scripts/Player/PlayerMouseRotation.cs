using UnityEngine;

public class PlayerMouseRotation : MonoBehaviour
{
    private Vector2 _mousePosition;
    void Update()
    {
        MouseRotation();
    }
    private void MouseRotation()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.localScale = _mousePosition.x < transform.position.x ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
    }
}
