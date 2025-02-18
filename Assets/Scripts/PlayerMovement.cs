using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    // Для передвижения
    [SerializeField] public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 _movement;

    // Для механик дэша
    [SerializeField] public float dashCooldown = 1f;
    [SerializeField] public float dashSpeed = 100f;
    private bool _dash = true;
    
    // Для подключения анимации
    [SerializeField] public Animator animator;
    
    void Start()
    { 
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        _movement.Normalize();
        
        rb.linearVelocity = _movement * moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift) && _dash && !rb.linearVelocity.Equals(Vector3.zero))
        {
            rb.linearVelocity = _movement * (moveSpeed + dashSpeed);
            StartCoroutine(DashCooldown());
        }
    }

    private IEnumerator DashCooldown()
    {
        _dash = false;
        yield return new WaitForSeconds(dashCooldown);
        _dash = true;
    }
}
