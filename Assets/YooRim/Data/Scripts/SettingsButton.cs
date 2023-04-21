using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public GameObject Settings;
    void Start()
    {

    }
    void Update()
    {

    }
    public void QuitSettingsMenu()
    {
        Settings.SetActive(false);
    }

    public void ResetSettingsMenu()
    {

    }


}
