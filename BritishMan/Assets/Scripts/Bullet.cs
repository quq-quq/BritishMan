using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    bool enemyBullet;

    public float speed, lifetime, distance;
    public int damage, rotateUp, rotateDown;

    public LayerMask isSolid;
    public GameObject effects, effectsBlood;

    public enum TypeOfBullet { far, close };
    public TypeOfBullet typeOfBullet;

    private void Start()
    {
        Invoke("DestroyBullet", lifetime);
        transform.Rotate(transform.rotation.x, transform.rotation.y, Random.Range(rotateDown, rotateUp));
    }

    void Update()
    {
        if (typeOfBullet == TypeOfBullet.far)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance, isSolid);
            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.CompareTag("Enemy"))
                {
                    Instantiate(effects, transform.position, Quaternion.identity);
                    hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
                }
                if (hitInfo.collider.CompareTag("Player") && enemyBullet)
                {
                    Instantiate(effects, transform.position, Quaternion.identity);
                    hitInfo.collider.GetComponent<Health>().TakeDamage(damage);
                }
                DestroyBullet();
            }
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public void DestroyBullet()
    {         
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) //
    {
        if (collision.CompareTag("Enemy") && typeOfBullet == TypeOfBullet.close)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            DestroyBullet();
        }

    }
}
