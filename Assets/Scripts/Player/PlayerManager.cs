using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    // // Using for get and set player`s fields
    [NonSerialized] public InventoryScript playerInventory;
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

    public Joystick MoveJoystick;
    public Joystick ShootJoystick;

    // Call once before Start()
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
        {
            MoveJoystick.gameObject.SetActive(false);
            ShootJoystick.gameObject.SetActive(false);
        }
        // Add Scripts to player
        playerHealth = gameObject.AddComponent<PlayerHealth>();
        playerAttack = gameObject.AddComponent<PlayerAttack>();
        playerInventory = gameObject.AddComponent<InventoryScript>();
        playerMovement = gameObject.AddComponent<PlayerMovement>();
        playerAnimation = gameObject.AddComponent<PlayerAnimation>();
        playerClosestItem = gameObject.AddComponent<PlayerClosestItem>();
        playerMouseRotation = gameObject.AddComponent<PlayerMouseRotation>();

        // Set Data to player scripts
        playerHealth.maxHealth = maxHealth;
        playerMovement.dashSpeed = dashSpeed;
        playerMovement.playerSpeed = playerSpeed;
        playerMovement.dashCooldown = dashCooldown;
        playerClosestItem.pickableDistance = pickableDistance;
        playerMovement.joystick = MoveJoystick;
        playerMouseRotation._shootJoystick = ShootJoystick;
        playerMouseRotation._moveJoystick = MoveJoystick;
    }
    // Для обращения других объектов к игроку исползовать
    // // PlayerManager.instance.(и дальше компонент который нам нужен)
}