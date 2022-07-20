using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Gun : MonoBehaviour, IPointerDownHandler
{
    public enum TypeOfGun {far, close};
    public TypeOfGun typeOfGun;

    public int curretAmmo, allAmmo;
    public float offset, startShotTime, timeReload;

    public GameObject bullet;
    public Transform shotPoint;

    int startAmmo, allClip, maxClip;
    float shotTime;
    Animator anim;
    Joystick joystick;

    [SerializeField]
    private Text ammoCount;

    [HideInInspector]
    public bool isReload;
    
    private void Start()
    {
        joystick = GameObject.Find("Canvas").transform.GetChild(4).GetChild(0).GetComponent<FixedJoystick>();
        ammoCount = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Text>();
        anim = GetComponent<Animator>();
        
        startAmmo = curretAmmo;
        curretAmmo = 0;     

        if (typeOfGun == TypeOfGun.far)
            maxClip = allAmmo / startAmmo;
        allClip = maxClip;
    }

    void Update()
    {
        if (typeOfGun == TypeOfGun.close)
        {
            ammoCount.text = null;
        }
        else if (typeOfGun == TypeOfGun.far)
        {
            ammoCount.text = allClip + "/" + maxClip;
        }
       

        if (shotTime <= 0 && ((curretAmmo >0 && isReload == false) || typeOfGun == TypeOfGun.close))
        {
                if (joystick.Horizontal != 0 || joystick.Vertical != 0)
                {
                    Shoot();
                }
                else
                {
                    anim.SetBool("IsShooting", false);
                }
        }
        else
        {
            shotTime -= Time.deltaTime;
        }

        if ((joystick.Horizontal != 0 || joystick.Vertical != 0) && curretAmmo == 0 && typeOfGun == TypeOfGun.far && isReload == false)
        {
            if (allAmmo > 0 && curretAmmo < startAmmo)
            {
                anim.SetTrigger("IsReloading");
                isReload = true;
                StartCoroutine(ReloadMoment());
            }
        }

        if(curretAmmo == 0 && typeOfGun == TypeOfGun.far)
        {
            anim.SetBool("IsShooting", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GunClip") && typeOfGun == TypeOfGun.far && allClip < maxClip)
        {
            allAmmo += startAmmo;
            allClip++;
            Destroy(collision.gameObject);
        }

    }

    public void Shoot()
    {
        Instantiate(bullet, shotPoint.position, transform.rotation);
        shotTime = startShotTime;
        anim.SetBool("IsShooting", true);
        if(typeOfGun == TypeOfGun.far)
            curretAmmo -= 1;

    }

    public void Reload()
    {
        allAmmo -= startAmmo;
        curretAmmo = startAmmo;
        allClip--;
    }

    IEnumerator ReloadMoment()
    {
        yield return new WaitForSeconds(timeReload);
        isReload = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (allAmmo > 0 && curretAmmo < startAmmo && isReload == false)
        {
            anim.SetTrigger("IsReloading");
            isReload = true;
            StartCoroutine(ReloadMoment());
        }
    }
}

