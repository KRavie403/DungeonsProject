using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : Singleton<IntroManager> //UI관련??메인타이틀??
{
    public GameObject StartPanel;
    public GameObject IntroPanel;
    public GameObject MapPanel;
    public GameObject CharacterPanel;
    public GameObject Settings;
    public GameObject CharacterSet;
    public GameObject darkImg;

    void Start()
    {
        StartCoroutine(DelayTime(4));
    }

    IEnumerator DelayTime(float time)
    {
        if (IntroPanel != null)
        {
            yield return new WaitForSeconds(time);
            IntroPanel.SetActive(false);
        }

        StartPanel.SetActive(true);
    }

    public void OpenSettingsMenu() //Settings
    {
        Settings.SetActive(true);
    }

    public void CloseSettingsMenu()
    {
        Settings.SetActive(false);
    }

    public void OpenStartMenu() //Start
    {
        darkImg.SetActive(true);
        MapPanel.SetActive(true);
        CharacterSet.SetActive(false);
        //SceneManager.LoadScene("MainGameScene");
    }

    public void QuitStartMenu()
    {
        MapPanel.SetActive(false);
        darkImg.SetActive(false);
    }
    public void CloseGame() //Quit
    {
        Application.Quit();
    }
}
