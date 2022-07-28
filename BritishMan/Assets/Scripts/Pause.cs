using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Pause : MonoBehaviour
{
    public bool gamePaused = false, isReload = false;

    public GameObject PauseUi, PauseButton, moreButtons, cellContainer;
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
}