using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public InventoryMenu invManager;
    private GameObject weapon;
    private GameObject weaponInUse;
    private GameObject weaponsOnFloor;
    private GameObject player;
    void Start()
    {
        weaponInUse = invManager.weaponContainer.transform.Find("WeaponInUse").gameObject;
        weaponsOnFloor = invManager.weaponContainer.transform.Find("WeaponsOnFloor").gameObject;
        player = invManager.player;
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && weaponInUse.transform.childCount == 0)
        {
            GameObject weapon = getWeaponNearBy();
            if (weapon)
            {
                weapon.GetComponent<WeaponOrbit>().enabled = true;
                weapon.GetComponent<WeaponOrbit>().setTarget(player);
                weapon.GetComponent<BaseWeaponsClass>().enabled = true;
                weapon.GetComponent<WeaponOutline>().disableOutline();
                weapon.GetComponent<WeaponOutline>().enabled = false;
                weapon.transform.SetParent(weaponInUse.transform);
                invManager.init();
            }
        }
    }
    public void dropWeapon()
    {
        weapon = weaponInUse.transform.GetChild(0).gameObject;
        weapon.GetComponent<WeaponOrbit>().enabled = false;
        weapon.GetComponent<BaseWeaponsClass>().enabled = false;
        weapon.GetComponent<WeaponOutline>().enabled = true;
        weapon.GetComponent<WeaponOutline>().enableOutline();
        weapon.transform.position = player.transform.position + new Vector3(1f, 0f, 0f);
        weapon.transform.SetParent(weaponsOnFloor.transform);
        invManager.init();
    }
    public GameObject getWeaponNearBy()
    {
        foreach (Transform child in weaponsOnFloor.transform)
        {
            Debug.Log(child + " " + (Vector3.Distance(child.position, player.transform.position)));
            if (Vector3.Distance(child.position, player.transform.position) < invManager.distanceToPickup)
            {
                Debug.Log(child);
                return child.gameObject;
            }
        }
        return null;
    }
}
