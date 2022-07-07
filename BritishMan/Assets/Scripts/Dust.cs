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
}
