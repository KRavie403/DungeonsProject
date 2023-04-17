using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour //UI관련??메인타이틀??
{
    public GameObject StartPanel;
    public GameObject IntroPanel;
    public GameObject InputPanel;
    public GameObject SoundMenu;
    public string channelName;
    void Start()
    {
        StartCoroutine(DelayTime(4));
    }

    IEnumerator DelayTime(float time)
    {
        yield return new WaitForSeconds(time);
        IntroPanel.SetActive(false);
        StartPanel.SetActive(true);
    }

    public void OpenSoundMenu()
    {
        SoundMenu.SetActive(true);
    }

    public void CloseSoundMenu()
    {
        SoundMenu.SetActive(false);
    }

    public void CloseInputMenu()
    {
        InputPanel.SetActive(false);
    }

    public void MainGameScene()
    {
        SceneManager.LoadScene("MainGameScene");
    }
    public void CloseGame()
    {
        Application.Quit();
    }
   
}
