using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public enum TypeOfGun {far, close};
    public TypeOfGun typeOfGun;

    public int curretAmmo, allAmmo;
    public float offset, startShotTime, timeReload;

    public GameObject bullet, buttonReload;
    public Transform shotPoint;
    public Joystick joystick;

    int startAmmo;
    float shotTime;
    bool isReload;
    Animator anim;
    
    [SerializeField]
    private Text ammoCount;

    private void Start()
    {
        anim = GetComponent<Animator>();
        startAmmo = curretAmmo;
    }

    void Update()
    {

        if(typeOfGun == TypeOfGun.close)
        {
            buttonReload.SetActive(false);
            ammoCount.gameObject.SetActive(false);
        }
        else if (typeOfGun == TypeOfGun.far)
        {
            buttonReload.SetActive(true);
            ammoCount.gameObject.SetActive(true);
            ammoCount.text = "Bullets: " + curretAmmo + "/" + allAmmo;
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

        if ((joystick.Horizontal != 0 || joystick.Vertical != 0) && curretAmmo == 0 && typeOfGun == TypeOfGun.far)
        {
            ReloadAnim();
        }

        if(curretAmmo == 0 && typeOfGun == TypeOfGun.far)
        {
            anim.SetBool("IsShooting", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GunClip") && typeOfGun == TypeOfGun.far)
        {
            allAmmo += startAmmo;
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
        int reason = startAmmo - curretAmmo;
        if(allAmmo >= reason)
        {
            allAmmo -= reason;
            curretAmmo += reason;
        }
        else
        {
            curretAmmo += allAmmo;
            allAmmo = 0;
        }
    }

    public void ReloadAnim()
    {
        if(allAmmo > 0 && curretAmmo < startAmmo)
        {
            anim.SetTrigger("IsReloading");
            isReload = true;
            StartCoroutine(ReloadMoment());
        }
    }

    IEnumerator ReloadMoment()
    {
        yield return new WaitForSeconds(timeReload);
        isReload = false;
    }
}

