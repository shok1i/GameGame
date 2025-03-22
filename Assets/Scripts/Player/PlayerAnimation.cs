using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private  Animator _animator;
    
    // Cal once before Start()
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        
    }
}
