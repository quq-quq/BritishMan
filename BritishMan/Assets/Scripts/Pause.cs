using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Pause : MonoBehaviour
{
    [SerializeField]
    public bool gamePaused = false;

    public GameObject PauseUi, PauseButton, moreButtons, cellContainer;

    private void Start()
    {
        Resume();
    }

    private void Update()
    {

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
        PauseUi.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
        PauseButton.SetActive(false);
        moreButtons.SetActive(false);
    }
}