using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject andOptions, menu, avtorsTitle;
    bool isOptions = false, isAvtors = false;

    public void Options()
    {


        if (!isOptions)
        {
            andOptions.SetActive(true);
            isOptions = true;
        }
        else
        {
            andOptions.SetActive(false);
            isOptions = false;
        }
        
    }

    public void ExitMenu()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Exit");
    }

    public void Lading()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Avtors()
    {
        if (!isAvtors)
        {
            avtorsTitle.SetActive(true);
            menu.SetActive(false);
            isAvtors = true;
        }

        else
        {
            avtorsTitle.SetActive(false);
            menu.SetActive(true);
            isAvtors = false;
        }
           
    }
}
