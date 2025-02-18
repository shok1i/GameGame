using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Для передвижения
    [SerializeField] public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector3 _movement;

    // Для механик дэша
    [SerializeField] public float dashCalldawn = 0.001f;
    [SerializeField] public float dashDistance = 10f;
    private bool _dash = true;
    
    // Для подключения анимации
    [SerializeField] public Animator animator;
    
    void Start()
    { 
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        _movement.Normalize();
        
        rb.linearVelocity = _movement * moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift) && _dash && !rb.linearVelocity.Equals(Vector3.zero))
        {
            rb.linearVelocity = _movement * dashDistance;
            _dash = false;
            StartCoroutine(DashCallDawn());
        }
    }

    private IEnumerator DashCallDawn()
    {
        yield return new WaitForSeconds(dashCalldawn);
        _dash = true;
    }
}
