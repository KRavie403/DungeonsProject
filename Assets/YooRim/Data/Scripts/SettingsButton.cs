using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public GameObject Settings;
    //public GameObject ResetButton;
    public void QuitSettingsMenu()
    {
        Settings.SetActive(false);
    }

    public void ResetSettingsMenu()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
