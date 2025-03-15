using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] float playerDistance;
    
    private Vector3 _movement;

    bool _canMove = true;

    void Update()
    {
        _movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // WallChecker();
        DrawMoveDir();
    }

    void DrawMoveDir()
    {
        Color c = Color.red;
        Debug.DrawRay(transform.position,  _movement * playerDistance, c);
    }

    void WallChecker()
    {
        var hit = Physics2D.Raycast(transform.position, _movement, playerDistance);
        _canMove = !hit.collider;
    }

    void FixedUpdate()
    {
        // if (_canMove) 
            transform.position += _movement.normalized * playerSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, playerDistance);
    }
}