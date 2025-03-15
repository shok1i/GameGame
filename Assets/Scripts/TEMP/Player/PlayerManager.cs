using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public PlayerMovement playerMovement;
    public PlayerAttack playerAttack;
    public PlayerHealth playerHealth;
    public Inventory playerInventory;
    public PlayerClosestItem playerClosestItem;

    // Call once before Start()
    public void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        playerHealth = GetComponent<PlayerHealth>();
        playerAttack = GetComponent<PlayerAttack>();
        playerInventory = GetComponent<Inventory>();
        playerMovement = GetComponent<PlayerMovement>();
        playerClosestItem = GetComponent<PlayerClosestItem>();
    }

    // Для обращения других объектов к игроку исползовать
    // // PlayerManager.instance.(и дальше компонент который нам нужен)

    // Debug 
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            PlayerManager.instance.playerHealth.TakeDamage(10f);

        if (Input.GetKeyDown(KeyCode.G))
            PlayerManager.instance.playerInventory.AddItem(new Weapon());
    }
}