using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    int health = 3;

    public GameObject partOfBody;

    void Start()
    {
    }

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
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
