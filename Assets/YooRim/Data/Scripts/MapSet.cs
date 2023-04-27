using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSet : MonoBehaviour
{
    public GameObject Map1;
    public GameObject Map2;
    public GameObject CharacterSet;
    public GameObject Map;
    public GameObject character1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CharacterSelection() //Settings
    {
        CharacterSet.SetActive(true);
    }

    public void CharacterSelect()
    {
        if(character1.activeSelf == true) character1.SetActive(false);
        character1.SetActive(true);
    }

    public void CloseMapSet()
    {
        //Map.SetActive(false);
        CharacterSet.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainGameScene");
        this.gameObject.SetActive(false); //스테이지 비활
    }

}


