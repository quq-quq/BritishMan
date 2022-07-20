using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentItem : MonoBehaviour
{
    [SerializeField]
    public int index;

    public GameObject buttons;
 
    public bool onoff = false ;

    GameObject inventoryObj;
    Inventory inventory;
    Transform switcher;

    private void Start()
    {
        inventoryObj = GameObject.FindGameObjectWithTag("Player");
        inventory = inventoryObj.GetComponent<Inventory>();
        switcher =inventoryObj.GetComponent<Transform>().GetChild(4);
        //int head = inventoryObj.GetComponent<Transform>().GetChild(0).GetComponent<Health>().health;
        //int body = inventoryObj.GetComponent<Transform>().GetChild(1).GetComponent<Health>().health;
        //int legs = inventoryObj.GetComponent<Transform>().GetChild(2).GetComponent<Health>().health;

    }

    private void Update()
    {


        if (inventory.item[index].id == 0)
        {
            GetComponent<Button>().enabled = false;
        }
        else
            GetComponent<Button>().enabled = true;
    }

    public void OnOrOff()
    {
        if (inventory.item[index].id != 0)
        {
            if (!onoff)
            {
                for(int i = 0; i < inventory.cellContainer.transform.childCount; i++)
                {
                    if (inventory.cellContainer.transform.GetChild(i).GetComponent<CurrentItem>().onoff == true)
                    {
                        inventory.cellContainer.transform.GetChild(i).GetComponent<CurrentItem>().buttons.SetActive(false);
                        inventory.cellContainer.transform.GetChild(i).GetComponent<CurrentItem>().onoff = false;
                    }
                }
                onoff = true;
                buttons.SetActive(true);
                inventory.description.text = inventory.item[index].description;
            }
            else
            {
                onoff = false;
                buttons.SetActive(false);
                inventory.description.text = null;
            }

        }
    }

    public void Drop()
    {
        if (inventory.item[index].id != 0)
        {
            GameObject dropedobj = Instantiate(Resources.Load<GameObject>(inventory.item[index].pathPrefab));
            dropedobj.transform.position = new Vector2(inventoryObj.transform.position.x + Random.Range(4, 5), inventoryObj.transform.position.y + Random.Range(-3, 3));

            if (inventory.item[index].count > 1)
                inventory.item[index].count--;
            else
            {
                if (inventory.item[index].isGun == true)
                {
                    switcher.GetComponent<SwitchWeapon>().weaponSwitch = 0;
                    switcher.GetComponent<SwitchWeapon>().SelectWeapon();
                    Destroy(switcher.GetChild(inventory.item[index].gunCount - 1).gameObject);
                }
                inventory.item[index] = new Item();
                buttons.SetActive(false);
                onoff = false;
                inventory.description.text = null;
            }

            inventory.DisplayItems();
        }
    }

    public void Activate()
    {

        if (inventory.item[index].isGun == true)
        {
            switcher.GetComponent<SwitchWeapon>().weaponSwitch = inventory.item[index].gunCount - 1;
            switcher.GetComponent<SwitchWeapon>().SelectWeapon();
        }

    }
}
