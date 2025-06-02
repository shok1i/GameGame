using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMouseRotation : MonoBehaviour
{
    private Vector2 _mousePosition;
    public Joystick _moveJoystick;
    public Joystick _shootJoystick;
    void Update()
    {
        MouseRotation();
    }
    private void MouseRotation()
    {
        if (Math.Abs(_moveJoystick.Horizontal) != 0 || Math.Abs(_shootJoystick.Horizontal) != 0)
        {
            if (Math.Abs(_moveJoystick.Horizontal) != 0)
            {
                transform.localScale = _moveJoystick.Horizontal <= 0 ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = _shootJoystick.Horizontal <= 0 ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
            }
        }
        else
        {
            _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.localScale = _mousePosition.x < transform.position.x ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
        }
    }
}
