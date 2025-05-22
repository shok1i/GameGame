using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    
    public GameObject player;
    public GameObject weaponContainer;
    public GameObject inventoryCanvas;
    public GameObject inGameUI;

    public AudioSource audioSource;

    private GameObject itemContainer;
    private GameObject spellContainer;
    private bool _isPaused;
    void Start()
    {
        itemContainer = inventoryCanvas.transform.Find("ImageContainer").transform.Find("ItemContainer").gameObject;
        spellContainer = inventoryCanvas.transform.Find("ImageContainer").transform.Find("SpellContainer").gameObject;
        var weaponSprite = weaponContainer.GetComponentInChildren<SpriteRenderer>().sprite;
        Debug.Log(weaponSprite);
        Debug.Log(itemContainer);
        itemContainer.transform.Find("Weapon").gameObject.GetComponent<Image>().sprite = weaponSprite;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (_isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        inGameUI.SetActive(true);
        inventoryCanvas.SetActive(false);
        Time.timeScale = 1f;
        audioSource.UnPause();
        _isPaused = false;
    }
    public void Pause()
    {
        inGameUI.SetActive(false);
        inventoryCanvas.SetActive(true);
        Time.timeScale = 0f;
        audioSource.Pause();
        _isPaused = true;
    }

}
