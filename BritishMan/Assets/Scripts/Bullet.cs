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
    public GameObject effectsBlood, dust;

    public enum TypeOfBullet { far, close };
    public TypeOfBullet typeOfBullet;
    

    private void Start()
    {
        Invoke(nameof(EffectPlay), lifetime);
        Destroy(gameObject, lifetime);
        transform.Rotate(transform.rotation.x, transform.rotation.y, Random.Range(rotateDown, rotateUp));
    }

    void Update()
    {
        if (typeOfBullet == TypeOfBullet.far)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance, isSolid);
            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.gameObject.isStatic)
                {
                    EffectPlay();
                }
                else if (hitInfo.collider.CompareTag("Enemy"))
                {
                    hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
                    Instantiate(effectsBlood, transform.position, Quaternion.identity);
                }
 
                if (hitInfo.collider.CompareTag("Player") && enemyBullet)
                {
                    hitInfo.collider.GetComponent<Health>().TakeDamage(damage);
                    Instantiate(effectsBlood, transform.position, Quaternion.identity);
                }

                Destroy(gameObject);
            }
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);

    }

    public void EffectPlay()
    {
        Instantiate(dust, transform.position, transform.rotation);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemyBullet)
        {
            if (collision.CompareTag("Player") && typeOfBullet == TypeOfBullet.close)
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
                Instantiate(effectsBlood, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        else
        {
            if (collision.CompareTag("Enemy") && typeOfBullet == TypeOfBullet.close)
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                Instantiate(effectsBlood, transform.position, Quaternion.identity);
            }
        }
    }
}
