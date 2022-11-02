using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public float startShotTime;
    public bool isShotGun;
    public int howMuch;
    public GameObject bullet;
    public Transform shotPoint;

    float shotTime;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 diff =  player.transform.position - transform.position;
        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        if (shotTime <= 0 && player.transform.position.x < 20)
        {
            Shoot();
        }
        else
        {
            shotTime -= Time.deltaTime;
        }
    }

    public void Shoot()
    {
        if (isShotGun)
        {
            for (int i = 0; i < howMuch; i++)
            {
                Instantiate(bullet, shotPoint.position, shotPoint.rotation);
            }
        }
        else
            Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        shotTime = startShotTime;
    }
}
