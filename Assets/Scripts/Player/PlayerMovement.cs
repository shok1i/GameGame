using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    public float dashSpeed;
    public float dashCooldown;

    private Vector3 _movement;
    private Rigidbody2D _rigidbody2D;
    private bool _canMove = true;
    private bool dashState = true;


    // Call once before Start()
    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        DrawMoveDir();
    }

    void FixedUpdate()
    {
        if (_canMove)
        {
            _rigidbody2D.linearVelocity = _movement.normalized * playerSpeed;
            if (Input.GetKeyDown(KeyCode.LeftShift) && !_rigidbody2D.linearVelocity.Equals(Vector2.zero) && dashState)
            {
                _rigidbody2D.linearVelocity *= playerSpeed + dashSpeed;
                StartCoroutine(PlayerDashCooldown());
            }
        }
        else
            _rigidbody2D.linearVelocity= Vector2.zero;
    }
    
    private IEnumerator PlayerDashCooldown()
    {
        dashState = false;
        yield return new WaitForSeconds(dashCooldown);
        dashState = true;
    }

    void DrawMoveDir()
    {
        Color c = Color.red;
        Debug.DrawRay(transform.position, _rigidbody2D.linearVelocity, c);
    }
}