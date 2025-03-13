using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public int health = 100;
    [SerializeField] public float baseAttack = 5f;
    
    [SerializeField] public float moveSpeed = 5f;
    
    [SerializeField] public float dashCooldown = 1f;
    [SerializeField] public float dashSpeed = 95f;
    private bool _canDash = true;

    private float _pickableRange = 1.25f;
    
    private Vector2 _mousePosition;
    
    private Rigidbody2D _rb;
    
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

        HighlightClosestItem();
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



    private Outliner highlightedItem;
    private void HighlightClosestItem()
    {
        Collider2D[] itemsInRange = Physics2D.OverlapCircleAll(transform.position, _pickableRange);
        Outliner closestItem = null;
        float closestDistance = float.MaxValue;
        
        foreach (var collider in itemsInRange)
        {
            Outliner item = collider.TryGetComponent<Outliner>(out item) ? item : null;
            if (item)
            {
                float distance = Vector2.Distance(transform.position, item.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestItem = item;
                }
            }
        }

        if (highlightedItem != closestItem)
        {
            if (highlightedItem)
            {
                highlightedItem.ChangeHighlightStatus(false);
            }

            if (closestItem)
            {
                closestItem.ChangeHighlightStatus(true);
            }
            
            highlightedItem = closestItem;
        }
    }

    // For Debug radius of pickable range
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _pickableRange);
    }
}
