using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    private float fHorizontal;
    [SerializeField] private float fSpeed = 8.0f;
    [SerializeField] private float fJumpPower= 16.0f;

    [SerializeField] private float RunSpeedMultiplier;
    [SerializeField] private float RunAttackCooldown;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private MeleeWeapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fHorizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, fJumpPower);
        }

        if (Input.GetButtonUp("Jump") && rigidBody.velocity.y > 0.0f)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            weapon.AddCooldownStack();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            weapon.StartCooldownWithoutStack(RunAttackCooldown);
        }
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(
            fHorizontal * fSpeed * (Input.GetKey(KeyCode.LeftShift) ? RunSpeedMultiplier : 1), 
            rigidBody.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
