using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string pathItem, pathPrefab;
    public int id, count;
    public bool IsStack, isGun;

    [Multiline((6))]
    public string description;

    public GameObject gunActive;

}
