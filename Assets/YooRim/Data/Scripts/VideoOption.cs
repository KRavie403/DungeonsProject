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

    int optionNum = 0; //ó���� drop�� �� �ʱ�ȭ
    public int resolutionNum; //x��
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
        //resolutions.AddRange(Screen.resolutions); //�ػ� ���� list�� �־���
        resolutionDropdown.options.Clear();
        foreach(Resolution item in resolutions)
        {
            Debug.Log(item.width + "x" + item.height + " " + item.refreshRate);
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = item.width + "x" + item + " " +item.refreshRate + "hz"; //�ػ󵵰� �־���
            resolutionDropdown.options.Add(option);//option �߰�
            
            if(item.width == Screen.width && item.height == Screen.height) //���� �ػ��� �ʺ�� Screen.width ���̴� Screen.height�� ����ؼ� �� �� ����
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
