using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public int health = 100;
    [SerializeField] public float baseAttack = 5f;
    
    [SerializeField] public float moveSpeed = 5f;
    
    [SerializeField] public float dashCooldown = 1f;
    [SerializeField] public float dashSpeed = 95f;
    private bool _canDash = true;

    private Vector2 _mousePosition;
    
    private Rigidbody2D _rb;
    

    // TODO:
    //  - Базово: Ношение предмета \\ При лучшем исходе: поднятие предмета и кидание предмета

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MouseRotation();
        
        Vector3 velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        velocity.Normalize();
        
        _rb.linearVelocity = velocity * moveSpeed;
        
        if (_canDash && Input.GetKey(KeyCode.LeftShift) && !_rb.linearVelocity.Equals(Vector3.zero))
        {
            _rb.linearVelocity = velocity * (moveSpeed + dashSpeed);
            StartCoroutine(Dash());
        }
    }
    
    private void MouseRotation()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.localScale = _mousePosition.x < transform.position.x ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
    }

    private IEnumerator Dash()
    {
        _canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        _canDash = true;
    }

    private void ItemPickUp()
    {
        
    }

    private void ItemDrop()
    {
        
    }
}
