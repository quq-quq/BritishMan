using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentItem : MonoBehaviour
{
    public int index;

    public GameObject buttons, outOfAmmo, active;
 
    public bool onoff = false ;

    GameObject inventoryObj;
    Inventory inventory;
    Transform switcher;
    Pause pause;


    private void Start()
    {
        inventoryObj = GameObject.FindGameObjectWithTag("Player");
        inventory = inventoryObj.GetComponent<Inventory>();
        switcher =inventoryObj.GetComponent<Transform>().GetChild(2);
        pause = GameObject.Find("Canvas").GetComponent<Pause>();
        inventory.DisplayItems();
    }

    private void Update()
    {
        if (inventory.item[index].id == 0)
        {
            GetComponent<Button>().enabled = false;
        }
        else
            GetComponent<Button>().enabled = true;

        if (inventory.item[index].isGun && inventory.item[index].gunActive.GetComponent<Gun>().curretAmmo == 0 && ((inventory.item[index].gunActive.GetComponent<Gun>().typeOfGun == Gun.TypeOfGun.big && pause.bigInt == 0) || (inventory.item[index].gunActive.GetComponent<Gun>().typeOfGun == Gun.TypeOfGun.small && pause.smallInt == 0) || (inventory.item[index].gunActive.GetComponent<Gun>().typeOfGun == Gun.TypeOfGun.shotGun && pause.shotInt == 0)))
            outOfAmmo.SetActive(true);
        else
            outOfAmmo.SetActive(false);

        if (inventory.item[index].isGun == true)
        {
            if (inventory.item[index].gunActive.GetComponent<Gun>().curretAmmo <= 0 && inventory.item[index].gunActive.GetComponent<Gun>().close)
            {
                Destroy(inventory.item[index].gunActive);
                switcher.GetChild(0).gameObject.SetActive(true);
                inventory.item[index] = new Item();
                buttons.SetActive(false);
                onoff = false;
                inventory.description.text = null;
                inventory.DisplayItems();
            }
        }
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
                    Destroy(inventory.item[index].gunActive);
                    switcher.GetChild(0).gameObject.SetActive(true);
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
            for(int i = 0; i < switcher.childCount; i++)
            {
                switcher.GetChild(i).gameObject.SetActive(false);
            }
            for (int i = 0; i < inventory.cellContainer.transform.childCount; i++)
            {
                inventory.cellContainer.transform.GetChild(i).GetComponent<CurrentItem>().active.SetActive(false);
            }
            active.SetActive(true);
            inventory.item[index].gunActive.SetActive(true);

            GameObject.Find("Canvas").GetComponent<Pause>().Resume();
        }
        else
        {

            if (inventory.item[index].id == 3)
            {
                int head = inventoryObj.GetComponent<Transform>().GetChild(0).GetChild(0).GetComponent<Health>().health;
                int body = inventoryObj.GetComponent<Transform>().GetChild(0).GetComponent<Health>().health;
                int legs = inventoryObj.GetComponent<Transform>().GetChild(0).GetChild(1).GetComponent<Health>().health;

                if (head < 2 || body < 2 || legs < 2)
                {
                    if (head < body && head < legs)
                        inventoryObj.GetComponent<Transform>().GetChild(0).GetChild(0).GetComponent<Health>().Regenerate();
                    else if (body < legs)
                        inventoryObj.GetComponent<Transform>().GetChild(0).GetComponent<Health>().Regenerate();
                    else
                        inventoryObj.GetComponent<Transform>().GetChild(0).GetChild(1).GetComponent<Health>().Regenerate();
                    
                    if (inventory.item[index].count > 1)
                        inventory.item[index].count--;
                    else
                    {

                        inventory.item[index] = new Item();
                        buttons.SetActive(false);
                        onoff = false;
                        inventory.description.text = null;
                    }
                }

            }

            if (inventory.item[index].id == 4)
            {
                Pause pause = GameObject.Find("Canvas").GetComponent<Pause>();

                if (pause.bigInt < 32 || pause.smallInt < 64 ||  pause.shotInt < 16)
                {
                    pause.bigInt += 8;
                    pause.smallInt += 16;
                    pause.shotInt += 4;

                    if (pause.bigInt > 32)
                        pause.bigInt = 32;
                    if (pause.smallInt > 64)
                        pause.smallInt = 64;
                    if (pause.shotInt > 16)
                        pause.shotInt = 16;

                    if (inventory.item[index].count > 1)
                        inventory.item[index].count--;
                    else
                    {

                        inventory.item[index] = new Item();
                        buttons.SetActive(false);
                        onoff = false;
                        inventory.description.text = null;
                    }
                }
            }

            inventory.DisplayItems();
        }

    }
}
