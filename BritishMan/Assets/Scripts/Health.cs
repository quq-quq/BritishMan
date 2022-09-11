using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health = 2;

    public GameObject partOfBody;
    public Player player;
    public Text lastSecTimer;

    float lastSec = 10f;

    void Update()
    {
        if (health >= 2)
        {
            partOfBody.GetComponent<Image>().color = Color.white;
        }
        else if (health == 1)
        {
            partOfBody.GetComponent<Image>().color = Color.red;
        }
        else
        {
            partOfBody.GetComponent<Image>().color = Color.black;
            lastSec -= Time.deltaTime;
            lastSecTimer.text = lastSec.ToString("F1");
        }

        if (!player.isDying)
        {
            lastSecTimer.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        if (player.speed > 1 && health == 2)
            player.speed -= 1;
        health -= damage;
        if (health <= 0 && !player.isDying)
        {
            StartCoroutine(Dead());
            lastSecTimer.gameObject.SetActive(true);
            lastSecTimer.text = lastSec.ToString("F1");
            player.isDying = true;
        }
        if (health < 0)
            health = 0;
    }

    public void Regenerate()
    {
        if (health <= 0)
        {
            lastSec = 10f;
        }
        health += Random.Range(1, 3);
        if (health > 2)
            health = 2;
        if (player.speed < 4 && health == 2)
            player.speed += 1;
    }

    public IEnumerator Dead()
    {
        yield return new WaitForSeconds(10);
        if (lastSec <= 0)
        {
            if (player.isDying)
            {
                player.speed = 0f;
                GameObject.Find("Body").SetActive(false);
                player.anim.SetTrigger("IsDying");
                lastSecTimer.gameObject.SetActive(false);
                Destroy(player.pauseBut);
                Destroy(player.joystick.gameObject);
                Destroy(player.joystickShooting.gameObject);
            }
            else
            {
                lastSecTimer.gameObject.SetActive(false);
            }

        }
    }
}
