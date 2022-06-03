using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public Joystick joystick, joystickShooting;
    public GameObject sprite, guns;
    public Transform flipPoint, flipPointMain, playerPos;

    float rotZ;
    Rigidbody2D rb;
    Vector2 moveInput, pos, moveVelocity;
    Animator anim;
    Camera main;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        main = FindObjectOfType<Camera>();
    }

    public void Update()
    {
        if(Mathf.Abs(joystickShooting.Horizontal) > 0.3f || Mathf.Abs(joystickShooting.Vertical) > 0.3f)
        {
            rotZ = Mathf.Atan2(joystickShooting.Vertical, joystickShooting.Horizontal) * Mathf.Rad2Deg;
            flipPointMain.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }


        moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        moveVelocity = moveInput.normalized * speed;

        if (moveInput.x != 0 || moveInput.y != 0)
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }

        pos = main.WorldToScreenPoint(transform.position);
        Flip();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    void Flip()
    {
        if (joystickShooting.Horizontal != 0 || joystickShooting.Vertical != 0)
        {
            if (flipPoint.position.x >= playerPos.position.x)
            {
                sprite.transform.eulerAngles = new Vector2(0, 0);
                guns.transform.localScale = new Vector2(1, 1);
            }
            else if (flipPoint.position.x < playerPos.position.x)
            {
                sprite.transform.eulerAngles = new Vector2(0, 180);
                guns.transform.localScale = new Vector2(1, -1);
            }
        }
        else
        {
            if (joystick.Horizontal > 0)
            {
                sprite.transform.eulerAngles = new Vector2(0, 0);
                guns.transform.localScale = new Vector2(1, 1);
                guns.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (joystick.Horizontal < 0)
            {
                sprite.transform.eulerAngles = new Vector2(0, 180);
                guns.transform.localScale = new Vector2(-1, 1);
                guns.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }
}


