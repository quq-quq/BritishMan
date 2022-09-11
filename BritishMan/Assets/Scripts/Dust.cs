using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    float lifeTime = 1;


    void Start()
    {
        Invoke("DestroyAboba", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyAboba()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(2);
        }

        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(2);
        }
    }
}
