using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [HideInInspector]
    public List<Item> item;

    public GameObject cellContainer;

    // Start is called before the first frame update
    void Start()
    {
        item = new List<Item>();

        for(int i = 0; i<cellContainer.transform.childCount; i++)
        {
            cellContainer.transform.GetChild(i).GetComponent<CurrentItem>().index =i;
        }

        for (int i= 0; i < cellContainer.transform.childCount; i++)
        {
            item.Add(new Item());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Item>())
        {
            if (collision.GetComponent<Item>().IsStack == true)
            {
                for (int i = 0; i < cellContainer.transform.childCount; i++)
                {
                    if (item[i].id == collision.GetComponent<Item>().id)
                    {
                        item[i].count++;
                        DisplayItems();
                        Destroy(collision.GetComponent<Item>().gameObject);
                        break;
                    }
                    else if(item[i].id == 0)
                    {
                        item[i] = collision.GetComponent<Item>();
                        DisplayItems();
                        Destroy(collision.GetComponent<Item>().gameObject);
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < cellContainer.transform.childCount; i++)
                {
                    if (item[i].id == 0)
                    {
                        item[i] = collision.GetComponent<Item>();
                        DisplayItems();
                        Destroy(collision.GetComponent<Item>().gameObject);
                        break;
                        
                    }
                }
            }

        }
    }

    public void DisplayItems()
    {
        for (int i = 0; i < item.Count; i++)
        {
            Transform cell = cellContainer.transform.GetChild(i);
            Transform icon = cell.GetChild(0);
            Transform count = icon.GetChild(0);

            Text txt = count.GetComponent<Text>();
            Image img = icon.GetComponent<Image>();

            if (item[i].id != 0)
            {

                img.enabled = true;
                img.sprite = Resources.Load<Sprite>(item[i].pathItem);
                if (item[i].IsStack && item[i].count >= 1)
                {
                    txt.text = item[i].count.ToString();
                }
                else
                {
                    txt.text = null;
                }
  
 
            }
            else
            {
                img.enabled = false;
                img.sprite = null;
                txt.text = null;
            }
        }
    }
}
