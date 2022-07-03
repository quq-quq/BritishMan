using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CurrentItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    public int index;
    GameObject inventoryObj;
    Inventory inventory;

    private void Start()
    {
        inventoryObj = GameObject.FindGameObjectWithTag("Player");
        inventory = inventoryObj.GetComponent<Inventory>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (inventory.item[index].id != 0)
        {
            GameObject dropedobj = Instantiate(Resources.Load<GameObject>(inventory.item[index].pathPrefab));
            dropedobj.transform.position = new Vector2(inventoryObj.transform.position.x + 4, inventoryObj.transform.position.y - 1);
            if (inventory.item[index].count > 1)
                inventory.item[index].count--;
            else
                inventory.item[index] = new Item();

            inventory.DisplayItems();
        }


    }
}
