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

    public GameObject bullet, buttonReload, onFloor;
    public Transform shotPoint;
    public Joystick joystick;

    int startAmmo, allClip, maxClip;
    float shotTime;
    bool isReload;
    Animator anim;
    
    [SerializeField]
    private Text ammoCount;

    private void Start()
    {
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
            buttonReload.SetActive(false);
            ammoCount.gameObject.SetActive(false);
        }
        else if (typeOfGun == TypeOfGun.far)
        {
            buttonReload.SetActive(true);
            ammoCount.gameObject.SetActive(true);
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
            ReloadAnim();
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

    public void ReloadAnim()
    {
        if(allAmmo > 0 && curretAmmo < startAmmo)
        {
            anim.SetTrigger("IsReloading");
            isReload = true;
            buttonReload.GetComponent<Button>().enabled = false;
            StartCoroutine(ReloadMoment());
            

        }
    }

    IEnumerator ReloadMoment()
    {
        yield return new WaitForSeconds(timeReload);
        isReload = false;
        buttonReload.GetComponent<Button>().enabled = true;
    }

    //public void DropGun()
    //{
    //    Instantiate(onFloor, transform.position, Quaternion.identity);
    //    Destroy(gameObject);
    //}
}

