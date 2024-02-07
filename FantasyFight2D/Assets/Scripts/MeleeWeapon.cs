using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] private GameObject Weapon;
    [SerializeField] private Transform pivotTransform;
    [SerializeField] private Animator animationController;

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
            PlaySwing(0.5f);
            Invoke("StopAttack", 0.5f);
            StartCooldown(2.0f);
        }
    }

    void PlaySwing(float length = 1.0f)
    {
        animationController.speed = 1.0f/length;
        animationController.SetTrigger("Swing");
    }

    /// <summary>
    /// Start a cooldown for basic attack and adds a stack, if there are no stacks of cooldown basic attack is reenabled.
    /// </summary>
    /// <param name="fCooldownTime"> how long the cooldown is in seconds</param>
    public void StartCooldown(float fCooldownTime)
    {
        nCooldownStacks++;
        canAttack = false;
        Invoke("EndCooldown", fCooldownTime);
    }

    /// <summary>
    /// Adds a cooldown stack without starting a cooldown, used for infinitely long cooldowns while a certain action takes place.
    /// Use in conjuction with StartCooldownWithoutStack
    /// </summary>
    public void AddCooldownStack()
    {
        nCooldownStacks++;
        canAttack = false;
    }

    /// <summary>
    /// Start a cooldown for basic attack without adding a cooldown stack.
    /// Use in conjunction with AddCooldownStack
    /// </summary>
    /// <param name="fCooldownTime"> how long the cooldown is in seconds</param>
    public void StartCooldownWithoutStack(float fCooldownTime)
    {
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

    void StopAttack() 
    { 
        Weapon.SetActive(false);
        animationController.ResetTrigger("Swing");
    }
}
