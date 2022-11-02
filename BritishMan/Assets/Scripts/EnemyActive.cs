using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActive : MonoBehaviour
{
    public List<GameObject> enemies;

    Player player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        float timer = GameObject.Find("Canvas").GetComponent<Generator>().timeStart;

        if(player.mind >= 50)
        {
            if (timer <= 60)
                Instantiate(enemies[Random.Range(0, enemies.Count)], transform.position, Quaternion.identity);
            else
            {

            }
        }
        else
        {

        }


    }
}
