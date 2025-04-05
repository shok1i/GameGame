using UnityEngine;

[CreateAssetMenu(fileName = "newWeapon", menuName = "Items/Weapon")]
public class Weapon : ItemBase
{
    public float weaponDamage;
    public float weaponAttackCooldown;
    
    public override void Use()
    {
        Debug.Log("Using weapon " + itemName);
    }
}