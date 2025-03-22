using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    // // Using for get and set player`s fields
    [NonSerialized] public Inventory playerInventory;
    [NonSerialized] public PlayerHealth playerHealth;
    [NonSerialized] public PlayerAttack playerAttack;
    [NonSerialized] public PlayerMovement playerMovement;
    [NonSerialized] public PlayerAnimation playerAnimation;
    [NonSerialized] public PlayerClosestItem playerClosestItem;
    [NonSerialized] public PlayerMouseRotation playerMouseRotation;

    // Data fields
    public float maxHealth;
    
    public int inventorySize;
    public float pickableDistance;
    
    public float playerSpeed;
    public float dashSpeed;
    public float dashCooldown;


    // Call once before Start()
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        
        // Add Scripts to player
        playerHealth = gameObject.AddComponent<PlayerHealth>();
        playerAttack = gameObject.AddComponent<PlayerAttack>();
        playerInventory = gameObject.AddComponent<Inventory>();
        playerMovement = gameObject.AddComponent<PlayerMovement>();
        playerAnimation = gameObject.AddComponent<PlayerAnimation>();
        playerClosestItem = gameObject.AddComponent<PlayerClosestItem>();
        playerMouseRotation = gameObject.AddComponent<PlayerMouseRotation>();
        
        // Set Data to player scripts
        playerHealth.maxHealth = maxHealth;
        playerMovement.dashSpeed = dashSpeed;
        playerMovement.playerSpeed = playerSpeed;
        playerMovement.dashCooldown = dashCooldown;
        playerInventory.inventorySize = inventorySize;
        playerClosestItem.pickableDistance = pickableDistance;
    }
    // Для обращения других объектов к игроку исползовать
    // // PlayerManager.instance.(и дальше компонент который нам нужен)
}