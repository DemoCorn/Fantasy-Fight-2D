using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] private GameObject Weapon;
    [SerializeField] private Transform pivotTransform;

    private int nCooldownStacks;
    private bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        Weapon.SetActive(false);
    }

    
    // Update is called once per frame
    void Update()
    {
        UpdateRotation();
        UpdateAttack();
    }

    void UpdateRotation()
    {
        if (Weapon.activeSelf == true)
        {
            return;
        }
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        pivotTransform.eulerAngles = new Vector3(0, 0, angle);
    }
    
    void UpdateAttack()
    {
        if (Input.GetButtonDown("Swipe") && canAttack)
        {
            Weapon.SetActive(true);
            Invoke("StopAttack", 0.5f);
            
            canAttack = false;
            StartCooldown(2.0f);
        }
    }

    public void StartCooldown(float fCooldownTime)
    {
        nCooldownStacks++;
        Invoke("EndCooldown", fCooldownTime);
    }

    private void EndCooldown()
    {
        nCooldownStacks--;
        
        if (nCooldownStacks == 0)
        {
            canAttack = true;
        }
    }

    void StopAttack() { Weapon.SetActive(false); }
}
