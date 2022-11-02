using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public List<GameObject> items;

    void Start()
    {
        int chanse = Random.Range(1, 100);
        if (chanse > 90)
        {
            Instantiate(items[Random.Range(0, items.Count)]);
        }
        Destroy(gameObject);
    }
}
