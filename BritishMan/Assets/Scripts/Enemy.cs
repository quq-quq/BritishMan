using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
      
    Animator anim;

    public enum TypeOfEnemy { knife , dummy };
    public TypeOfEnemy typeOfEnemy;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        if(typeOfEnemy != TypeOfEnemy.dummy)
        {
          ///  transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

}
