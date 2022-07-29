using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActive : MonoBehaviour
{
    public List<GameObject> enemies;

    void Start()
    {
        float timer = GameObject.Find("Canvas").GetComponent<Generator>().timeStart;
        if (timer <= 60)
            Instantiate(enemies[Random.Range(0, 2)], transform.position, Quaternion.identity);
        else
        {

        }
    }
}
