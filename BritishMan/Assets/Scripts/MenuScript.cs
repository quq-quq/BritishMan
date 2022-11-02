using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject andOptions;
    bool isOptions = false;

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
}
