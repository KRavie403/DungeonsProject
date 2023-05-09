using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MapSet : MonoBehaviour
{
    public GameObject CharacterSet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void backCharacterSelection() //Settings
    {
        CharacterSet.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainGameScene");
        this.gameObject.SetActive(false); //스테이지 비활
    }

}


