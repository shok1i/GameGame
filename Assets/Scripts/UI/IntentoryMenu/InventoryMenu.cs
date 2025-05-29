using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{

    public GameObject player;
    public GameObject weaponContainer;
    public GameObject inventoryCanvas;
    public GameObject inGameUI;

    public AudioSource audioSource;

    public float distanceToPickup;

    private GameObject itemContainer;
    private GameObject spellContainer;
    private GameObject weaponOnFloor;
    private bool _isPaused;
    void Start()
    {
        weaponOnFloor = weaponContainer.transform.Find("WeaponsOnFloor").gameObject;
        init();
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

        if (weaponContainer.transform.childCount != 0)
        {
            foreach (Transform child in weaponOnFloor.transform)
            {
                if (Vector3.Distance(child.position, player.transform.position) < distanceToPickup)
                {
                    inventoryCanvas.transform.GetChild(2).gameObject.SetActive(true);
                    StartCoroutine(ShowTemporarily());
                    return;
                }
            }
        }
    }
    public void Resume()
    {
        inGameUI.SetActive(true);
        inventoryCanvas.transform.GetChild(0).gameObject.SetActive(false);
        inventoryCanvas.transform.GetChild(1).gameObject.SetActive(false);
        Time.timeScale = 1f;
        audioSource.UnPause();
        _isPaused = false;
    }
    public void Pause()
    {
        init();
        inGameUI.SetActive(false);
        inventoryCanvas.transform.GetChild(0).gameObject.SetActive(true);
        inventoryCanvas.transform.GetChild(1).gameObject.SetActive(true);
        inventoryCanvas.transform.GetChild(2).gameObject.SetActive(false);
        Time.timeScale = 0f;
        audioSource.Pause();
        _isPaused = true;
    }
    public void init()
    {
        itemContainer = inventoryCanvas.transform.Find("ImageContainer").transform.Find("ItemContainer").gameObject;
        spellContainer = inventoryCanvas.transform.Find("ImageContainer").transform.Find("SpellContainer").gameObject;
        Sprite weaponSprite = null;
        try
        {
            weaponSprite = weaponContainer.transform.Find("WeaponInUse").GetComponentInChildren<SpriteRenderer>().sprite;
        }
        catch
        {
            Color color = itemContainer.transform.Find("Weapon").gameObject.GetComponent<Image>().color;
            color.a = 0;
            itemContainer.transform.Find("Weapon").gameObject.GetComponent<Image>().color = color;
        }
        itemContainer.transform.Find("Weapon").gameObject.GetComponent<Image>().sprite = weaponSprite;
    }
    

    IEnumerator ShowTemporarily()
    {
        var obj = inventoryCanvas.transform.GetChild(2).gameObject;
        obj.SetActive(true);
        yield return new WaitForSeconds(3f);
        obj.SetActive(false);
    }
}
