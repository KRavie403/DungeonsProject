using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject HomeButton;
    public GameObject NextButton;
    public GameObject MapPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Next() //Settings
    {
        NextButton.SetActive(true);
    }

    public void CloseStart()
    {
        MapPanel.SetActive(false);
    }
}
