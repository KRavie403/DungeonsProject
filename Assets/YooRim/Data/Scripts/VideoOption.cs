using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoOption : MonoBehaviour
{
    FullScreenMode screenMode;
    public Dropdown resolutionDropdown;
    public Toggle fullscreenBtn;
    List<Resolution> resolutions = new List<Resolution>();

    int optionNum = 0; //처음에 drop된 값 초기화
    public int resolutionNum; //x값
    void Start()
    {
        InitUI();
    }

    void InitUI()
    {
        for(int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRate == 60)
                resolutions.Add(Screen.resolutions[i]);
        }
        //resolutions.AddRange(Screen.resolutions); //해상도 값을 list에 넣어줌
        resolutionDropdown.options.Clear();
        foreach(Resolution item in resolutions)
        {
            Debug.Log(item.width + "x" + item.height + " " + item.refreshRate);
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = item.width + "x" + item + " " +item.refreshRate + "hz"; //해상도값 넣어줌
            resolutionDropdown.options.Add(option);//option 추가
            
            if(item.width == Screen.width && item.height == Screen.height) //현재 해상도의 너비는 Screen.width 높이는 Screen.height를 사용해서 알 수 있음
            {
                resolutionDropdown.value = optionNum;
            }optionNum++;
        }
        resolutionDropdown.RefreshShownValue();

        fullscreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    public void DropboxOptionChange(int x)
    {
        resolutionNum = x;
    }

    public void FullScreenBtn(bool isFull)
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }
    public void OkBtnClick()
    {
        Screen.SetResolution(resolutions[resolutionNum].width,
            resolutions[resolutionNum].height,
            screenMode);
    }
}
