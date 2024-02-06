using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float damage;
    [SerializeField] GameObject PersonalWeapon;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Detected Collision on " + gameObject.name + " Collision Object: " + other.gameObject.name);

        if (other.gameObject.tag == "Weapon")
        {
            if (other.gameObject == PersonalWeapon)
            {
                Debug.Log("Hit by own weapon on " + gameObject.name);
                return;
            }

            Debug.Log("Hit by other weapon on " + gameObject.name);
        }
    }

    void RegisterHit(float damage)
    {
        health -= damage;
    }
}
