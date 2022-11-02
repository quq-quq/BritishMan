using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Pause : MonoBehaviour
{
    public bool gamePaused = false, isReload = false, isOptions = false;

    public GameObject PauseUi, PauseButton, moreButtons, cellContainer, PanelOptions;
    public Text smallBullet, bigBullet, shotGunBullet;

    public int smallInt, bigInt, shotInt;

    private void Start()
    {
        Resume();
        smallInt =64;
        bigInt = 32;
        shotInt = 16;        
    }

    private void Update()
    {
        smallBullet.text = smallInt.ToString();
        bigBullet.text = bigInt.ToString();
        shotGunBullet.text = shotInt.ToString();
    }

    public void Resume()
    {
        PauseUi.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
        PauseButton.SetActive(true);
        moreButtons.SetActive(true);
        for(int i =0 ; i < cellContainer.transform.childCount; i++)
        {
            cellContainer.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
            cellContainer.transform.GetChild(i).GetComponent<CurrentItem>().onoff = false;
        }
    }

    public void PauseMod()
    {
        if(isReload == false)
        {
            PauseUi.SetActive(true);
            Time.timeScale = 0f;
            gamePaused = true;
            PauseButton.SetActive(false);
            moreButtons.SetActive(false);
        }

    }

    public void OptionsOpen()
    {
        if (!isOptions)
        {
            PanelOptions.SetActive(true);
            isOptions = true;
        }
        else
        {
            PanelOptions.SetActive(false);
            isOptions = false;
        }
    }

    public void GoHome()
    {
        Time.timeScale = 1f;
        gameObject.GetComponent<Animator>().SetTrigger("Exit");
    }

    public void Loading()
    {
        SceneManager.LoadScene("Menu");
    }

}