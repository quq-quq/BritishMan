using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health = 2;

    public GameObject partOfBody;

    void Update()
    {
        if (health == 2)
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
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
            health = 0;
    }

    public void Regenerate()
    {
        health += Random.Range(1, 3);
        if (health > 2)
            health = 2;
    }
}
