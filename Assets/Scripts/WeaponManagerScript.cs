using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject assaultRifle;

    private WeaponScript currentWeapon;

    void Start()
    {
        EquipWeapon(pistol); // Default to pistol
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(pistol);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(assaultRifle);
        }
    }

    void EquipWeapon(GameObject weapon)
    {
        // Disable all weapons
        pistol.SetActive(false);
        assaultRifle.SetActive(false);

        // Enable selected weapon
        weapon.SetActive(true);
        currentWeapon = weapon.GetComponent<WeaponScript>();
    }

    public void FireWeapon()
    {
        if (currentWeapon != null)
        {
            currentWeapon.Fire();
        }
    }

}
