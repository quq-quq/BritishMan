using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed, mind;
    public bool isDying;
    public Joystick joystick, joystickShooting;
    public GameObject sprite, guns, pauseBut;
    public Transform flipPoint, flipPointMain, playerPos;
    public Health legs, body, head;

    public Image face;
    public List<Sprite> varFace;

    float rotZ;  
    Rigidbody2D rb;
    Vector2 moveInput, pos, moveVelocity;
    Camera main;

    [HideInInspector]
    public Animator anim;

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

        if(head.health > 0 && body.health > 0 && legs.health > 0)
        {
            isDying = false;
        }

        if(mind >= 66)
        {
            face.sprite = varFace[0];
        }
        else if(mind < 66 && mind > 33)
        {
            face.sprite = varFace[1];
        }
        else if(mind <= 33 && mind > 0)
        {
            face.sprite = varFace[2];
        }
        else
        {
            transform.GetChild(0).GetComponent<Health>().TakeDamage(3);
        }
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Gas"))
            mind -= 5 * Time.deltaTime;
    }

    public void End()
    {
        GameObject.Find("SeeZone").GetComponent<Animator>().SetTrigger("END");
    }
}


