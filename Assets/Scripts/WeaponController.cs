using System;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private Weapon _equippedWeapon;
    private Sprite _weaponSprite;

    [SerializeField] private Weapon startingWeapon;
    [SerializeField] private Transform weaponHold;
    [SerializeField] private Transform weaponRender;
    
    private void Start()
    {
        if (startingWeapon != null)
        {
            Equipped(startingWeapon);
            _weaponSprite = _equippedWeapon.GetComponent<SpriteRenderer>().sprite;
            weaponRender.GetComponent<SpriteRenderer>().sprite = _weaponSprite;
        }
    }

    private void Equipped(Weapon weaponToEquip)
    {
        if (_equippedWeapon != null)
        {
            Destroy(_equippedWeapon);
        }
        
        _equippedWeapon = Instantiate(weaponToEquip, weaponHold.position, weaponHold.rotation) as Weapon;
        _equippedWeapon.transform.parent = weaponHold;
    }

    public void Shoot()
    {
        if (_equippedWeapon != null)
        {
            _equippedWeapon.Shoot();
        }
    }
}
