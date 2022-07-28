using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Gun : MonoBehaviour, IPointerDownHandler
{
    public enum TypeOfGun {big, small, shotGun};
    public TypeOfGun typeOfGun;

    public int startAmmo;
    public float startShotTime, timeReload;
    public bool close;

    public GameObject bullet;
    public Transform shotPoint;

    float shotTime;
    Pause pause;
    Animator anim;
    Joystick joystick;
    Slider bulletsPoint;
    GameObject switcher;

    [HideInInspector]
    public bool isReload;

    public int curretAmmo;

    private void Start()
    {
        joystick = GameObject.Find("Canvas").transform.GetChild(5).GetChild(0).GetComponent<FixedJoystick>();
        bulletsPoint = GameObject.Find("Canvas").transform.GetChild(1).GetComponent<Slider>();
        pause = GameObject.Find("Canvas").GetComponent<Pause>();
        switcher = GameObject.Find("Switcher");
        anim = GetComponent<Animator>();

        curretAmmo = startAmmo;
        bulletsPoint.minValue = 0;
        if (close)
            curretAmmo = 50;
        if (!close)
            curretAmmo = 0;
    }

    void Update()
    {
        bulletsPoint.gameObject.SetActive(true);

        bulletsPoint.maxValue = startAmmo;
        bulletsPoint.value = curretAmmo;


        if (shotTime <= 0 && ((curretAmmo >0 && isReload == false) || close && curretAmmo > 0))
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

        if ((joystick.Horizontal != 0 || joystick.Vertical != 0) && curretAmmo == 0 && isReload == false)
        {
            if (((pause.bigInt > 0 && typeOfGun == TypeOfGun.big) || (pause.smallInt > 0 && typeOfGun == TypeOfGun.small) || (pause.shotInt > 0 && typeOfGun == TypeOfGun.shotGun)) && curretAmmo < startAmmo && !close)
            {
                anim.SetTrigger("IsReloading");
                isReload = true;
                pause.isReload = true;
                StartCoroutine(ReloadMoment());
            }
        }

        if(curretAmmo <= 0)
        {
            anim.SetBool("IsShooting", false);
        }
    }


    public void Shoot()
    {
        Instantiate(bullet, shotPoint.position, transform.rotation);
        shotTime = startShotTime;
        anim.SetBool("IsShooting", true);
        curretAmmo -= 1;

    }

    public void Reload()
    {
       int reason = startAmmo - curretAmmo;

        if(typeOfGun == TypeOfGun.big)
        {
            if(pause.bigInt >= reason)
            {
                pause.bigInt -= reason;
                curretAmmo = startAmmo;
            }
            else
            {
                curretAmmo += pause.bigInt;
                pause.bigInt = 0;
            }
        }

        if (typeOfGun == TypeOfGun.small)
        {
            if (pause.smallInt >= reason)
            {
                pause.smallInt -= reason;
                curretAmmo = startAmmo;
            }
            else
            {
                curretAmmo += pause.smallInt;
                pause.smallInt = 0;
            }
        }

        if (typeOfGun == TypeOfGun.shotGun)
        {
            if (pause.shotInt >= reason)
            {
                pause.shotInt -= reason;
                curretAmmo = startAmmo;
            }
            else
            {
                curretAmmo += pause.shotInt;
                pause.shotInt = 0;
            }
        }
    }

    IEnumerator ReloadMoment()
    {
        yield return new WaitForSeconds(timeReload);
        isReload = false;
        pause.isReload = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (((pause.bigInt > 0 && typeOfGun == TypeOfGun.big) || (pause.smallInt > 0 && typeOfGun == TypeOfGun.small) || (pause.shotInt > 0 && typeOfGun == TypeOfGun.shotGun)) && curretAmmo < startAmmo && !close && !isReload)
        {
            anim.SetTrigger("IsReloading");
            isReload = true;
            pause.isReload = true;
            StartCoroutine(ReloadMoment());
        }
    }
}

