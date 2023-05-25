using System.Collections;
using System.Collections.Generic;
using System;
using System.Windows;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSet : Singleton<MapSet>
{
    public GameObject CharacterSet;
    public GameObject Caution;
    public CharacterDB currentCharacterDB;

    public int SceneIdx = 1;
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

    void CallCaution()
    {
        GameObject obj = Instantiate(Caution, this.transform);
        Destroy(obj, 2);
    }

    public void StartGame()
    {
        if(currentCharacterDB.characterList.Count < 4)
        {
            CallCaution();
        }
        else
        {
            SceneLoader.Inst.ChangeScene(SceneIdx);
            this.gameObject.SetActive(false); //스테이지 비활
        }
    }
}


