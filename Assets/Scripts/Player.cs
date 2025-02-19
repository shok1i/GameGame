using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public int health = 100;
    [SerializeField] public float moveSpeed = 5f;
    
    private Vector2 _mousePosition;

    // TODO:
    //  - Базово: Ношение предмета \\ При лучшем исходе: поднятие предмета
    //  - сделать дэш в сторону отсносительно мыши, а не в направление движения

    private void FixedUpdate()
    {
        MouseRotation();
    }
    
    private void MouseRotation()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GetComponent<SpriteRenderer>().flipX = _mousePosition.x < transform.position.x;
    }

    private void Dash()
    {
        
    }

    private void ItemPickUp()
    {
        
    }

    private void ItemDrop()
    {
        
    }
}
