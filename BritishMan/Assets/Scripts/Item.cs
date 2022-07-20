using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string pathItem, pathPrefab, pathGun;
    public int id, count, gunCount;
    public bool IsStack, isGun;

    [Multiline((5))]
    public string description;
}
