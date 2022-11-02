using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public GameObject gun, spawner;
    
    GameObject player;     

    public enum TypeOfEnemy { common , dummy};
    public TypeOfEnemy typeOfEnemy;

    void Start()
    {
        player = GameObject.Find("Player");
        Destroy(gameObject, 60);
    }

    void Update()
    {
        if (health <= 0)
        {
            player.GetComponent<Player>().mind -= 5;
            Instantiate(spawner);
            Destroy(gameObject);
        }
        if(typeOfEnemy != TypeOfEnemy.dummy)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            Flip();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    void Flip()
    {
        if(transform.position.x < player.transform.position.x)
        {
            transform.eulerAngles = new Vector2(0, 0);
            gun.transform.localScale = new Vector2(1, 1);
        }
        else if (transform.position.x > player.transform.position.x)
        {
            transform.eulerAngles = new Vector2(0, 180);
            gun.transform.localScale = new Vector2(1, -1);
        }
    }
}
