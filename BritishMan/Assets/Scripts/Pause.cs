using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pause : MonoBehaviour
{
    public static bool gamePaused = false;

    public GameObject PauseUi, PauseButton, moreButtons;
    private void Start()
    {
        Resume();
    }

    public void Resume()
    {
        PauseUi.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
        PauseButton.SetActive(true);
        moreButtons.SetActive(true);
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
